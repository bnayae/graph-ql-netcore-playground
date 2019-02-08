using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.Definitions
{
    public class UserType: ObjectGraphType<User>
    {
        private readonly IRepository _repository;
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;

        public UserType(IDataLoaderContextAccessor dataLoaderContextAccessor, IRepository repository)
        {
            _repository = repository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Field(x => x.Id).Description("The Id");
            Field(x => x.Name, nullable: false).Description("The Name");
            Field(x => x.Email, nullable: false).Description("The Email");

            Field<ListGraphType<QuestionType>>("questions",
                      resolve: ResolveQuestionsAsync,
                      description: "The related questions");
        }

        private Task<Question[]> ResolveQuestionsAsync(ResolveFieldContext<User> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with specific single request (context) level caching key
            // The loader will fetch data from the repository
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, Question[]>("GetQuestionsByUserIds", _repository.GetQuestionsByCreatorsIdsAsync);

            // build the batch data
            return loader.LoadAsync(context.Source.Id);
        }
    }
}

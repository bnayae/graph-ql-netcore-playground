using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Repositories;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.Definitions
{
    public class ReviewType: ObjectGraphType<Review>
    {
        private readonly IRepository _repository;
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;

        public ReviewType(IDataLoaderContextAccessor dataLoaderContextAccessor, IRepository repository)
        {
            _repository = repository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Field(x => x.Id).Description("The question's Id.");
            Field(x => x.Title, nullable: false).Description("The title");
            Field(x => x.Body).Description("The body");
            Field(x => x.UserId, nullable: false).Description("The id the related user");
            Field(x => x.QuestionId, nullable: false).Description("The id the related question");

            Field<UserType>(nameof(User),
                            resolve: ResolveUsersAsync,
                            description: "The related user");

            Field<QuestionType>(nameof(Question),
                            resolve: ResolveQuestionsAsync,
                            description: "The related question");
        }

        private Task<User> ResolveUsersAsync(ResolveFieldContext<Review> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with specific single request (context) level caching key
            // The loader will fetch data from the repository
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, User>(
                                            "GetQuestionsByIds", _repository.GetUsersByIdsAsync);

            // build the batch data
            return loader.LoadAsync(context.Source.UserId);
        }

        private Task<Question> ResolveQuestionsAsync(ResolveFieldContext<Review> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with specific single request (context) level caching key
            // The loader will fetch data from the repository
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, Question>(
                                            "GetQuestionsByIds", _repository.GetQuestionsByIdsAsync);

            // build the batch data
            return loader.LoadAsync(context.Source.QuestionId);
        }
    }
}

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
    public class QuestionType : ObjectGraphType<Question>
    {
        private readonly IRepository _repository;
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;

        public QuestionType(IDataLoaderContextAccessor dataLoaderContextAccessor, IRepository repository)
        {
            _repository = repository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Field(x => x.Id).Description("The question's Id.");
            Field(x => x.Title, nullable: false).Description("The title");
            Field(x => x.Body).Description("The body");
            Field(x => x.CreatorId, nullable: false).Description("The creator id");

            Field<UserType>(nameof(User),
                            resolve: ResolveUserAsync,
                            description: "The related user");

            Field<ListGraphType<ReviewType>>(nameof(Review),
                            resolve: ResolveReviewsAsync,
                            description: "The related questions");
        }

        private Task<User> ResolveUserAsync(ResolveFieldContext<Question> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with specific single request (context) level caching key
            // The loader will fetch data from the repository
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, User>("GetUsersByIds", _repository.GetUsersByIdsAsync);

            // build the batch data
            return loader.LoadAsync(context.Source.CreatorId);
        }

        private Task<Review[]> ResolveReviewsAsync(ResolveFieldContext<Question> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with specific single request (context) level caching key
            // The loader will fetch data from the repository
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, Review[]>("GetReviewsByQuestionIds", _repository.GetReviewsByQuestionIdsAsync);

            // build the batch data
            return loader.LoadAsync(context.Source.Id);
        }
    }
}

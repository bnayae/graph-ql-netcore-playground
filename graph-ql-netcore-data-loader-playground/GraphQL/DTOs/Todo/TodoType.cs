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

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class TodoType : ObjectGraphType<Todo>
    {
        private readonly IRepository _repository;
        private readonly IDataLoaderContextAccessor _dataLoaderContextAccessor;

        public TodoType(IDataLoaderContextAccessor dataLoaderContextAccessor, IRepository repository)
        {
            _repository = repository;
            _dataLoaderContextAccessor = dataLoaderContextAccessor;

            Field(x => x.Id).Description("The Id of the TODO.");
            Field(x => x.Title, nullable: false).Description("The TODO title");
            Field(x => x.Completed).Description("TODO's state");
            Field(x => x.UserId, nullable: false).Description("The id of the TODO's user");

            // - This setup is not efficient and subject to 1 + N problem
            //   use data-loader to overcome the 1 + N problem
            // - resolved types don't have to be part of the entity (JSON representation only)
            Field<UserType>(nameof(User),
                            resolve: ResolveUserAsync,
                            description: "The ref of the TODO's user");
        }

        private Task<User> ResolveUserAsync(ResolveFieldContext<Todo> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with the key "GetUsersById"
            // The loader will call GetUsersByIdAsync for each batch of keys
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, User>("GetUsersByIds", _repository.GetUserByIdsAsync);

            // Add this UserId to the pending keys to fetch
            // The task will complete once the GetUsersByIdAsync() returns with the batched results
            return loader.LoadAsync(context.Source.UserId);
        }
    }
}

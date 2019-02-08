using Bnaya.Samples.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.DTOs
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
            Field(x => x.UserName, nullable: false).Description("The UserName");
            Field(x => x.Email, nullable: false).Description("The Email");
            Field(x => x.Phone, nullable: false).Description("The Phone");
            Field(x => x.Website, nullable: false).Description("The Website");
            Field(x => x.Address, nullable: true, type: typeof(AddressType)).Description("The Address");
            //Field(x => x.Company, nullable: true, type: typeof(CompanyType)).Description("The Company");

            Field(x => x.Company, nullable: true, type: typeof(CompanyType))
                .Description("The Company");

            Field<ListGraphType<TodoType>>("todos",
                      resolve: ResolveTodosAsync,
                      description: "The ref of the TODO's of this user");
        }

        private Task<Todo[]> ResolveTodosAsync(ResolveFieldContext<User> context)
        {
            // Get the context of the query
            var queryContext = _dataLoaderContextAccessor.Context;
            // Get or add a batch loader with specific single request (context) level caching key
            // The loader will fetch data from the repository
            // The underline framework will ignore the per item duplication
            var loader = queryContext.GetOrAddBatchLoader<int, Todo[]>("GetTodos", async ids =>
            {
                var todos = await _repository.GetAllTodosAsync().ConfigureAwait(false); // query once
                // in-memory mapping (can optimized if the data-source / service support batch operations)
                var split = from id in ids
                            let items = todos.Where(m => m.UserId == id)
                            select new { Id = id, Items = items.ToArray() };
                return split.ToDictionary(m => m.Id, m=> m.Items);
            });

            // build the batch data
            return loader.LoadAsync(context.Source.Id);
        }
    }
}

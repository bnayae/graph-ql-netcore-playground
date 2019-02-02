using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Services;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs
{
    public class Queries: ObjectGraphType
    {
        private readonly IRepository _repository;

        public Queries(IRepository repository)
        {
            Field<ListGraphType<TodoType>>("todos", resolve: GetAllTodosAsync);
            Field<ListGraphType<UserType>>("users", resolve: GetAllUsersAsync);
            Field<UserType>("user",
                                arguments: new QueryArguments(
                                    new QueryArgument<IntGraphType> { Name = "id" }),
                                resolve: GetUserByIdAsync);
            _repository = repository;
        }

        private Task<Todo[]> GetAllTodosAsync(ResolveFieldContext<object> context) => _repository.GetAllTodosAsync();

        private Task<User[]> GetAllUsersAsync(ResolveFieldContext<object> context) => _repository.GetAllUsersAsync();


        private Task<User> GetUserByIdAsync(ResolveFieldContext<object> context)
        {
            int id = context.GetArgument<int>("id");
            return _repository.GetUserByIdAsync(id);
        }
   }
}

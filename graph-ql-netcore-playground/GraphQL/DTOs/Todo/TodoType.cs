using Bnaya.Samples.Services;
using GraphQL.Client;
using GraphQL.Common.Request;
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


        public TodoType(IRepository repository)
        {
            _repository = repository;

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
            int usertId = context.Source.UserId;
            return _repository.GetUserByIdAsync(usertId);
        }
    }
}

using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQL.DTOs
{
    public class TodoType : ObjectGraphType<Todo>
    {
        private readonly HttpClient _httpClient = new HttpClient();


        public TodoType()
        {
            Field(x => x.Id).Description("The Id of the TODO.");
            Field(x => x.Title, nullable: false).Description("The TODO title");
            Field(x => x.Completed).Description("TODO's state");

            Field(x => x.UserId, nullable: false).Description("The id of the TODO's user");
            //Field(x => x.User, nullable: false, type: typeof(UserType))
            //    .ResolveAsync() // consider IoC + repository injection
            //    .Description("The ref of the TODO's user");
        }

        //private async Task<User> ResolveUserAsync(ResolveFieldContext<Todo> context)
        //{
        //    context.Source.UserId
        //}
    }
}

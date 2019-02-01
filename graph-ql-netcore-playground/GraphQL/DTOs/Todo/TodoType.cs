using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQL.DTOs
{
    public class TodoType: ObjectGraphType<Todo>
    {
        public TodoType()
        {
            Field(x => x.Id).Description("The Id of the TODO.");
            Field(x => x.Title, nullable: false).Description("The TODO title");
            Field(x => x.UserId, nullable: false).Description("The id of the TODO's user");
            Field(x => x.Completed).Description("TODO's state");

        }
    }
}

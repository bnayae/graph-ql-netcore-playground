using Bnaya.Samples.Services;
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

        public UserType(IRepository repository)
        {
            _repository = repository;

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
                      resolve: ResolveUserAsync,
                      description: "The ref of the TODO's of this user");
        }

        private Task<Todo[]> ResolveUserAsync(ResolveFieldContext<User> context)
        {
            int usertId = context.Source.Id;
            return _repository.GetTodosByUserIdAsync(usertId);
        }
    }
}

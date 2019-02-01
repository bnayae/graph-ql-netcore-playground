using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQL.DTOs
{
    public class UserType: ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id).Description("The Id");
            Field(x => x.Name, nullable: false).Description("The Name");
            Field(x => x.UserName, nullable: false).Description("The UserName");
            Field(x => x.Email, nullable: false).Description("The Email");
            Field(x => x.Phone, nullable: false).Description("The Phone");
            Field(x => x.Website, nullable: false).Description("The Website");
            Field(x => x.Address, nullable: true, type: typeof(AddressType)).Description("The Address");
            //Field(x => x.Company, nullable: true, type: typeof(CompanyType)).Description("The Company");

            Field(x => x.Company, nullable: true, type: typeof(CompanyType))
                //.Resolve(c => c.Source.Company)
            //Field(x => x.Company, nullable: true, type:typeof(ComplexGraphType<CompanyType>))
                //.ResolveAsync(async context =>
                //{
                //    await Task.Delay(200).ConfigureAwait(false);
                //    return null;
                //})
                .Description("The Company");
        }
    }
}

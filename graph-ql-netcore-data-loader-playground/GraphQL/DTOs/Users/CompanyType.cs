using GraphQL.Types;
using System;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class CompanyType : ObjectGraphType<Company>
    {
        public CompanyType()
        {
            Field(x => x.Name, nullable: false).Description("The Name");
            Field(x => x.CatchPhrase, nullable: true).Description("The CatchPhrase");
            Field(x => x.BS, nullable: true).Description("The BS");
        }
    }
}
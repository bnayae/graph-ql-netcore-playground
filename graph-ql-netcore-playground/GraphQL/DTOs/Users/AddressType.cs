using GraphQL.Types;

namespace Bnaya.Samples.GraphQL.DTOs
{
    public class AddressType: ObjectGraphType<Address>
    {
        public AddressType()
        {
            Field(x => x.Street, nullable: false).Description("The Street");
            Field(x => x.Suite, nullable: true).Description("The Suite");
            Field(x => x.City, nullable: false).Description("The City");
            Field(x => x.Zipcode, nullable: true).Description("The Zip-Code");
            Field(x => x.Geo, nullable: true, type: typeof(GeoType)).Description("The Geo coordinations");

        }
    }
}
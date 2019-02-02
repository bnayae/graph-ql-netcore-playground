using GraphQL.Types;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class GeoType: ObjectGraphType<Geo>
    {
        public GeoType()
        {
            Field(x => x.Lat, nullable: false).Description("The Latitude");
            Field(x => x.Lng, nullable: false).Description("The Longitude");
        }
    }
}
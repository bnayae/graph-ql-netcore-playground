using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Repositories;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs
{
    public class MainSchema: Schema
    {
        public MainSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<Queries>(); 
            // Mutation = resolver.Resolve<NHLStatsMutation>();
        }

        
    }
}

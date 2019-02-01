using Bnaya.Samples.GraphQL.DTOs;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQL
{
    public class MainSchema: Schema
    {
        public MainSchema() 
        {
            Query = new Queries();
            // Mutation = resolver.Resolve<NHLStatsMutation>();
        }

        
    }
}

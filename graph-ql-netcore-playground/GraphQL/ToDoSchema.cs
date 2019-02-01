using Bnaya.Samples.GraphQL.DTOs;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQL
{
    public class ToDoSchema: Schema
    {
        public ToDoSchema() 
        {
            Query = new ToDoQuery();
            // Mutation = resolver.Resolve<NHLStatsMutation>();
        }

        
    }
}

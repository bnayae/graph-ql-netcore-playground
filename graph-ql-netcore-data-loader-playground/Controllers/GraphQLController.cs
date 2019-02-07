using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL.Types;
using GraphQL;
using Bnaya.Samples.GraphQLs;
using GraphQL.DataLoader;
using GraphQL.Execution;

namespace Bnaya.Samples.Controllers
{
    [Route(Startup.GraphQlPath)]
    public class GraphQlController : Controller
    {
        private readonly IDocumentExecutionListener _dataLoaderListener;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema; 
        
        public GraphQlController(
            ISchema schema, 
            IDocumentExecuter documentExecuter,
            IDocumentExecutionListener documentExecutionListener)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _dataLoaderListener = documentExecutionListener;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQueryParameters query)
        {
            var result = await _documentExecuter.ExecuteAsync(opts =>
            {
                opts.Listeners.Add(_dataLoaderListener); // data loader listener
                opts.Schema = _schema;
                opts.Query = query.Query;
                opts.Inputs = query.Variables;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL.Types;
using GraphQL;
using Bnaya.Samples.GraphQLs;

namespace Bnaya.Samples.Controllers
{
    [Route(Startup.GraphQlPath)]
    public class GraphQlController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema; 
        
        public GraphQlController(ISchema schema, IDocumentExecuter documentExecuter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQueryParameters query)
        {
            var result = await _documentExecuter.ExecuteAsync(x =>
            {
                x.Schema = _schema;
                x.Query = query.Query;
                x.Inputs = query.Variables;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}

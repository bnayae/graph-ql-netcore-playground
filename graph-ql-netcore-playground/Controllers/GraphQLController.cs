using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL.Types;
using GraphQL;
using Bnaya.Samples.GraphQL;

namespace Bnaya.Samples.Controllers
{
    [Route(Startup.GraphQlPath)]
    public class GraphQlController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter = new DocumentExecuter();
        private readonly ISchema _schema = new ToDoSchema();

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
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

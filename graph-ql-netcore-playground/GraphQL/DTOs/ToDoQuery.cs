using Bnaya.Samples.GraphQL.DTOs;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQL
{
    public class ToDoQuery: ObjectGraphType
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public ToDoQuery()
        {
            Field<ListGraphType<TodoType>>("todos",  resolve: GetAllTodosAsync);

        }

        private async Task<Todo[]> GetAllTodosAsync(ResolveFieldContext<object> context)
        {
            var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<Todo[]>();
            return data;
        }
    }
}

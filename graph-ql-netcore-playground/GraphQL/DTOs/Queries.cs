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
    public class Queries: ObjectGraphType
    {
        const string BASE_URL = "https://jsonplaceholder.typicode.com/";

        private readonly HttpClient _httpClient = new HttpClient();

        public Queries()
        {
            Field<ListGraphType<TodoType>>("todos", resolve: GetAllTodosAsync);
            Field<ListGraphType<UserType>>("users", resolve: GetAllUsersAsync);
        }

        private async Task<Todo[]> GetAllTodosAsync(ResolveFieldContext<object> context)
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}todos").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<Todo[]>();
            return data;
        }

        private async Task<User[]> GetAllUsersAsync(ResolveFieldContext<object> context)
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}users").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<User[]>();
            return data;
        }
    }
}

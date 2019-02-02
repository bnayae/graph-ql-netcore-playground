using Bnaya.Samples.GraphQLs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.Services
{
    public class Repository : IRepository
    {
        const string BASE_URL = "https://jsonplaceholder.typicode.com/";

        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<Todo[]> GetAllTodosAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}todos").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<Todo[]>();
            return data;
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}users").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<User[]>();
            return data;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}users/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<User>();
            return data;
        }

        public async Task<Todo[]> GetTodosByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}todos") // ?userid={?} is not supported
                                            .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<Todo[]>();
            return data.Where(m => m.UserId == userId).ToArray();
        }
    }
}

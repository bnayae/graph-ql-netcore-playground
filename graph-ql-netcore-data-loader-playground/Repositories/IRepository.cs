using System.Collections.Generic;
using System.Threading.Tasks;
using Bnaya.Samples.GraphQLs.DTOs;

namespace Bnaya.Samples.Repositories
{
    public interface IRepository
    {
        Task<Todo[]> GetAllTodosAsync();
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IDictionary<int, User>> GetUserByIdsAsync(IEnumerable<int> ids); // perfect for data loader
        Task<Todo[]> GetTodosByUserIdAsync(int userId);
    }
}
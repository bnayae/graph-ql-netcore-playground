using System.Threading.Tasks;
using Bnaya.Samples.GraphQLs.DTOs;

namespace Bnaya.Samples.Services
{
    public interface IRepository
    {
        Task<Todo[]> GetAllTodosAsync();
        Task<User[]> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<Todo[]> GetTodosByUserIdAsync(int userId);
    }
}
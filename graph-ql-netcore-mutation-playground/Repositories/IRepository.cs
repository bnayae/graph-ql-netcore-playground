using System.Collections.Generic;
using System.Threading.Tasks;
using Bnaya.Samples.GraphQLs.DTOs;

namespace Bnaya.Samples.Repositories
{
    public interface IRepository
    {
        Task<Question> AddQuestionAsync(Question question);
        Task<Question> UpdateQuestionAsync(int id, QuestionUpdater question);

        Task<Question[]> GetAllQuestionAsync();
        Task<IDictionary<int, Question>> GetQuestionsByIdsAsync(IEnumerable<int> ids);
        Task<IDictionary<int, Question>> GetQuestionsByIdsAsync(params int[] ids);
        Task<IDictionary<int, Question[]>> GetQuestionsByCreatorsIdsAsync(IEnumerable<int> creatorIds);
        Task<IDictionary<int, Question[]>> GetQuestionsByCreatorsIdsAsync(params int[] creatorIds);

        Task<Review[]> GetAllReviewsAsync(int id);
        Task<IDictionary<int, Review[]>> GetReviewsByUserIdsAsync(IEnumerable<int> userIds);
        Task<IDictionary<int, Review[]>> GetReviewsByUserIdsAsync(params int[] userIds);
        Task<IDictionary<int, Review[]>> GetReviewsByQuestionIdsAsync(IEnumerable<int> questionIds);
        Task<IDictionary<int, Review[]>> GetReviewsByQuestionIdsAsync(params int[] questionIds);

        Task<User[]> GetAllUsersAsync();
        Task<IDictionary<int, User>> GetUsersByIdsAsync(IEnumerable<int> ids);
        Task<IDictionary<int, User>> GetUsersByIdsAsync(params int[] ids);
    }
}

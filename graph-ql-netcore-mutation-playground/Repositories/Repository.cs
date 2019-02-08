using Bnaya.Samples.GraphQLs.DTOs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.Repositories
{
    public class Repository : IRepository
    {
        private readonly ConcurrentDictionary<int, Question> _questions = new ConcurrentDictionary<int, Question>()
        {
            [1] = new Question(1, "seed 1", "bla bla bla 1", 10),
            [2] = new Question(2, "seed 2", "bla bla bla 2", 10),
            [3] = new Question(3, "seed 3", "bla bla bla 3", 100),
        };
        private readonly ConcurrentDictionary<int, User> _users = new ConcurrentDictionary<int, User>()
        {
            [1] = new User(1, "user 1", "user1@gmail.com"),
            [10] = new User(10, "user 10", "user10@gmail.com"),
            [100] = new User(100, "user 100", "user100@gmail.com"),
            [101] = new User(101, "user 101", "user101@gmail.com"),
            [102] = new User(102, "user 102", "user102@gmail.com"),
        };
        private readonly ConcurrentDictionary<int, Review> _reviews = new ConcurrentDictionary<int, Review>()
        {
            [1] = new Review(1, "like seed 1", "I'd liked seed 1", 1, 1),
            [2] = new Review(2, "dislike seed 1", "I'd liked seed 1", 10, 1),
            [3] = new Review(3, "like seed 2", "I'd liked seed 1", 100, 2),
            [4] = new Review(4, "the answer for seed 2", "i's simple, do it like that", 101, 2),
            [5] = new Review(5, "like", "keep writing", 102, 2),
        };


        public Task<Question[]> GetAllQuestionAsync() => Task.FromResult(_questions.Values.ToArray());

        public Task<IDictionary<int, Question[]>> GetQuestionsByCreatorsIdsAsync(IEnumerable<int> creatorIds) => GetQuestionsByCreatorsIdsAsync(creatorIds.ToArray());
        public Task<IDictionary<int, Question[]>> GetQuestionsByCreatorsIdsAsync(params int[] creatorIds)
        {
            var index = creatorIds.ToDictionary(m => m);
            IDictionary<int, Question[]> resuts =
                                _questions.GroupBy(m => m.Value.CreatorId, m => m.Value)
                                      .Where(g => index.ContainsKey(g.Key))
                                      .ToDictionary(g => g.Key, g => g.ToArray());
            return Task.FromResult(resuts);

        }

        public Task<IDictionary<int, Question>> GetQuestionsByIdsAsync(IEnumerable<int> ids) => GetQuestionsByIdsAsync(ids.ToArray());
        public Task<IDictionary<int, Question>> GetQuestionsByIdsAsync(params int[] ids)
        {
            var users = from id in ids
                        where _questions.ContainsKey(id)
                        select _questions[id];
            IDictionary<int, Question> resuts = users.ToDictionary(m => m.Id);
            return Task.FromResult(resuts);

        }

        public Task<Review[]> GetAllReviewsAsync(int id) => Task.FromResult(_reviews.Values.ToArray());

        public Task<IDictionary<int, Review[]>> GetReviewsByUserIdsAsync(IEnumerable<int> userIds) => GetReviewsByUserIdsAsync(userIds.ToArray());
        public Task<IDictionary<int, Review[]>> GetReviewsByUserIdsAsync(params int[] userIds)
        {
            var index = userIds.ToDictionary(m => m);
            IDictionary<int, Review[]> resuts =
                                _reviews.GroupBy(m => m.Value.UserId, m => m.Value)
                                      .Where(g => index.ContainsKey(g.Key))
                                      .ToDictionary(g => g.Key, g => g.ToArray());
            return Task.FromResult(resuts);

        }
        public Task<IDictionary<int, Review[]>> GetReviewsByQuestionIdsAsync(IEnumerable<int> questionIds) => GetReviewsByQuestionIdsAsync(questionIds.ToArray());
        public Task<IDictionary<int, Review[]>> GetReviewsByQuestionIdsAsync(params int[] questionIds)
        {
            var index = questionIds.ToDictionary(m => m);
            IDictionary<int, Review[]> resuts =
                                _reviews.GroupBy(m => m.Value.QuestionId, m => m.Value)
                                      .Where(g => index.ContainsKey(g.Key))
                                      .ToDictionary(g => g.Key, g => g.ToArray());
            return Task.FromResult(resuts);

        }

        public Task<User[]> GetAllUsersAsync() => Task.FromResult(_users.Values.ToArray());

        public Task<IDictionary<int, User>> GetUsersByIdsAsync(IEnumerable<int> ids) => GetUsersByIdsAsync(ids.ToArray());
        public Task<IDictionary<int, User>> GetUsersByIdsAsync(params int[] ids)
        {
            var users = from id in ids
                        where _users.ContainsKey(id)
                        select _users[id];
            IDictionary<int, User> resuts = users.ToDictionary(m => m.Id);
            return Task.FromResult(resuts);
        }
    }
}

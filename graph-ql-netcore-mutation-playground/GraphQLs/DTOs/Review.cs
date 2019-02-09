using System;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class Review
    {
        [Obsolete("For serialization only", true)]
        public Review() { }

        public Review(int id, string title, string body, int userId, int questionId)
        {
            Id = id;
            Title = title;
            Body = body;
            UserId = userId;
            QuestionId = questionId;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public int UserId { get; private set; }
        public int QuestionId { get; private set; }
    }
}
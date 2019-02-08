namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class Review
    {
        public Review(int id, string title, string body, int userId, int questionId)
        {
            Id = id;
            Title = title;
            Body = body;
            UserId = userId;
            QuestionId = questionId;
        }

        public readonly int Id;
        public readonly string Title;
        public readonly string Body;
        public readonly int UserId;
        public readonly int QuestionId;
    }
}
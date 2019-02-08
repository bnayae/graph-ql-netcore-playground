using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class Question
    {
        public Question(
            int id,
            string title,
            string body,
            int creatorId)
        {
            this.Id = id;
            Title = title;
            Body = body;
            CreatorId = creatorId;
        }

        public readonly int Id;
        public readonly string Title;
        public readonly string Body;
        public readonly int CreatorId;
    }
}

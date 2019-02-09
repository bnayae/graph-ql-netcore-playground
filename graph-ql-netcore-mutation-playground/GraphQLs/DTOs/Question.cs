using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class Question
    {
        private static int _runningId = 0;

        [Obsolete("For serialization only", true)]
        public Question() { }

        public Question(
            string title,
            string body,
            int creatorId)
        {
            Id = ++_runningId;
            Title = title;
            Body = body;
            CreatorId = creatorId;
        }

        public int Id { get; private set; }
        public string Title { get; internal set; }
        public string Body { get; internal set; }
        public int CreatorId { get; internal set; }

        internal void Increment() => Id = ++_runningId;
    }
}

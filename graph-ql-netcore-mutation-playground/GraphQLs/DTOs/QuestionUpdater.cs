using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.DTOs
{
    public class QuestionUpdater
    {
        private static int _runningId = 0;

        [Obsolete("For serialization only", true)]
        public QuestionUpdater() { }

        public string Title { get; private set; }
        public string Body { get; private set; }
        public int? CreatorId { get; private set; }
       
    }
}

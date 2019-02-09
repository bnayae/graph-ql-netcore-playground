using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Repositories;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs.Definitions
{
    public class UpdateQuestionInputType : InputObjectGraphType<Question>
    {
        private readonly IRepository _repository;

        public UpdateQuestionInputType(IRepository repository)
        {
            //Name
            _repository = repository;

            Field<StringGraphType>(nameof(Question.Title));
            Field<StringGraphType>(nameof(Question.Body));
            Field<IntGraphType>(nameof(Question.CreatorId));
        }
    }
}

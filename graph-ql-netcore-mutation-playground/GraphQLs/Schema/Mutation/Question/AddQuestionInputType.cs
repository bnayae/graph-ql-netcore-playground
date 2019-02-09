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
    public class AddQuestionInputType : InputObjectGraphType<Question>
    {
        private readonly IRepository _repository;

        public AddQuestionInputType(IRepository repository)
        {
            //Name
            _repository = repository;

            Field<IntGraphType>(nameof(Question.Id));
            Field<NonNullGraphType<StringGraphType>>(nameof(Question.Title));
            Field<NonNullGraphType<StringGraphType>>(nameof(Question.Body));
            Field<NonNullGraphType<IntGraphType>>(nameof(Question.CreatorId));
        }
    }
}

using Bnaya.Samples.GraphQLs.Definitions;
using Bnaya.Samples.GraphQLs.DTOs;
using Bnaya.Samples.Repositories;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bnaya.Samples.GraphQLs
{
    /// <summary>
    /// Top level mutations
    /// </summary>
    public class Mutations : ObjectGraphType
    {
        private readonly IRepository _repository;

        public Mutations(IRepository repository)
        {
            _repository = repository;

            Field<QuestionType>(
              "addQuestion",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<AddQuestionInputType>> { Name = "question" }
              ),
              resolve: context =>
              {
                  var question = context.GetArgument<Question>("question");
                  Question result = _repository.AddQuestionAsync(question).Result; // TODO: contribution of async flow
                  return result;
              });

            Field<QuestionType>(
              "upadteQuestion",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                new QueryArgument<NonNullGraphType<UpdateQuestionInputType>> { Name = "question" }
              ),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  var question = context.GetArgument<QuestionUpdater>("question");
                  Question result = _repository.UpdateQuestionAsync(id, question).Result; // TODO: contribution of async flow
                  return result;
              });
        }
    }
}

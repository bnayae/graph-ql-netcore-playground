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
    /// Top level queries
    /// </summary>
    public class Queries: ObjectGraphType
    {
        private readonly IRepository _repository;

        public Queries(IRepository repository)
        {
            Field<ListGraphType<QuestionType>>("questions", resolve: GetAllQuestionsAsync);
            Field<ListGraphType<UserType>>("users", resolve: GetAllUsersAsync);
            Field<QuestionType>("question",
                                arguments: new QueryArguments(
                                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                                resolve: GetQuestionByIdAsync);
            Field<UserType>("user",
                                arguments: new QueryArguments(
                                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                                resolve: GetUserByIdAsync);
            _repository = repository;
        }

        private Task<Question[]> GetAllQuestionsAsync(ResolveFieldContext<object> context) => _repository.GetAllQuestionAsync();

        private async Task<Question> GetQuestionByIdAsync(ResolveFieldContext<object> context)
        {
            int id = context.GetArgument<int>("id");
            var map = await _repository.GetQuestionsByIdsAsync(id);
            return map[id];
        }

        private Task<User[]> GetAllUsersAsync(ResolveFieldContext<object> context) => _repository.GetAllUsersAsync();

        private async Task<User> GetUserByIdAsync(ResolveFieldContext<object> context)
        {
            int id = context.GetArgument<int>("id");
            var map = await _repository.GetUsersByIdsAsync(id);
            return map[id];
        }
   }
}

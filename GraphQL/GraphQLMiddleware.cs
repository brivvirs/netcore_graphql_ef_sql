using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace graphqlpractise.GraphQL
{

    public class GraphQLRequest
    {
        private JObject _variables;
        public string operationName { get; set; }
        public string query { get; set; }
        public JObject variables
        {
            get => _variables == null ? new JObject() : _variables;
            set => _variables = value;
        }
    }

    public class GraphQLMiddleware<T> where T : ISchema
    {
        private readonly RequestDelegate _next;
        private T Schema { get; }

        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        public GraphQLMiddleware(
            RequestDelegate next,
            T schema,
            IDocumentExecuter executer,
            IDocumentWriter writer
        )
        {
            _next = next;
            Schema = schema;
            _executer = executer;
            _writer = writer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var notGraphQLRequest = context.Request.Method.ToLower() != "post" &&
                  context.Request.Path != "/graphql";
            if (notGraphQLRequest)
            {
                await _next(context);
                return;
            }
            else
            {
                var obj = (new StreamReader(context.Request.Body)).ReadToEnd();
                var request = JsonConvert.DeserializeObject<GraphQLRequest>(obj);

                var result = await _executer.ExecuteAsync(new ExecutionOptions
                {
                    Schema = Schema,
                    Query = request.query,
                    OperationName = request.operationName,
                    Inputs = request.variables.ToInputs()
                });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =
                    (int)(result.Errors != null && result.Errors.Any()
                    ? HttpStatusCode.BadRequest
                    : HttpStatusCode.OK);
                var res = await _writer.WriteToStringAsync(result);
                await context.Response.WriteAsync(res);
            }

        }
    }

    public static class GraphQLMiddlewareExtension
    {
        public static void UseGraphQLMiddleware<T>(this IApplicationBuilder appBuilder)
            where T : ISchema
        {
            appBuilder.UseMiddleware<GraphQLMiddleware<T>>();
        }
    }
}

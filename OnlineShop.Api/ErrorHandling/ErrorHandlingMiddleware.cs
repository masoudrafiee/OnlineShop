using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = Serilog.Log.ForContext<ErrorHandlingMiddleware>(); ;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;
                var problemDetails = ex.Map(traceId);

                if (problemDetails.Status >= 500)
                    _logger.Error(ex, "An unhandled exception occured. {@Error}", problemDetails);

                var serializedResult = Serialize(problemDetails);

                context.Response.ContentType = "application/problem+json; charset=utf-8";
                context.Response.StatusCode = problemDetails.Status.Value;
                await context.Response.WriteAsync(serializedResult);
            }
        }

        private string Serialize(ProblemDetails problemDetails)
        {
            return JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                MaxDepth = 10
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineShop.Api.ErrorHandling
{
    public static class ExceptionToCustomProblemDetailsMapper
    {
        public static CustomProblemDetails Map(this Exception ex, string traceId)
        {
            if (ex is ApplicationException applicationException)
                return new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Detail = applicationException.Message,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "appException",
                    TraceId = traceId
                };

            if (ex is NotFoundException notFoundException)
                return new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "notFound",
                    TraceId = traceId
                };

            if (ex is ArgumentException argumentException)
                return new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "validationFailed",
                    InvalidParams = new InvalidParam[] { new InvalidParam(argumentException.ParamName, argumentException.Message) },
                    TraceId = traceId,
                };

            if (ex is DbUpdateException dbUpdateException && dbUpdateException.InnerException.Message.Contains("duplicate key"))
                return new CustomProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "validationFailed",
                    InvalidParams = new InvalidParam[] { new InvalidParam("database", "entityWithTheSameKeyAlreadyExists") },
                    TraceId = traceId,
                };

            int status;
            if (ex is NotImplementedException)
                status = (int)HttpStatusCode.NotImplemented;
            else
                status = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new CustomProblemDetails
            {
                Status = status,
                Detail = ex.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6",
                Title = "serverErrorOccurred",
                TraceId = traceId,
            };

            return problemDetails;
        }
    }
}

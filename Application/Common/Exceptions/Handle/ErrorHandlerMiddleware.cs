using Application.Helper;
using Application.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions.Handle
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var h = error.GetType();
                var result = string.Empty;
                var response = context.Response;
                response.ContentType = "application/json";
                ErrorResultViewModel ListErrors = new ErrorResultViewModel() { NotFoundException = new List<string>() };
                switch (error)
                {
                    case AggregateException e:
                        // custom application error

                        var j = error.InnerException;
                        var z = j.GetType();

                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        // custom application error
                        result = JsonConvert.SerializeObject(new { error = error.Message });
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        var y = error.InnerException;
                        if (y != null)
                        {
                            var x = y.InnerException?.GetType();
                            if (x == typeof(NotFoundException))
                            {
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                ListErrors.NotFoundException.Add(y.InnerException?.Message);
                            }
                            else if (x == typeof(UnAuthorizeException))
                            {
                                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                ListErrors.NotFoundException.Add(y.InnerException?.Message);
                            }
                            else
                            {
                                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                ListErrors.NotFoundException.Add(y.InnerException?.Message);
                            }

                        }
                        result = JsonConvert.SerializeObject(new { errors = ListErrors });
                        break;

                }

                await response.WriteAsync(result);
            }
        }
    }
}

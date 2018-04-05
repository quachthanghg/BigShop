using BigShop.Model.Models;
using BigShop.Service.Services;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigShop.Web.Infrastructure.Core
{
    public class BaseApiController : ApiController
    {
        private IErrorService _errorService;

        public BaseApiController(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Add(error);
                _errorService.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                responseMessage = func.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return responseMessage;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationSvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EsearchSvc.Services.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionRequiredAttribute : Attribute, IAsyncActionFilter
    {
        private readonly DbContext _platfromdbContext;
        public TransactionRequiredAttribute(DbContext platfromdbContext)
        {
            _platfromdbContext = platfromdbContext;
        }

        public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null || next is null)
                throw new ArgumentNullException();
          IExecutionStrategy executionStrategy=  _platfromdbContext.Database.CreateExecutionStrategy();
          await  executionStrategy.ExecuteAsync(async () =>
           {
               var _tran= await _platfromdbContext.Database.BeginTransactionAsync();
               var resut = await next();
               if (resut.Exception is null)
                   await _tran.CommitAsync();
               else
                   await _tran.RollbackAsync();
           });
        }


    }

    public class FirstResponseActionFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //context.Result.ExecuteResultAsync()
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
          //  context.Cancel = true;
        }
    }

    public class FirstExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var c = context;
        }
    }

    public class FirstAutorisedFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticate = context.HttpContext.User.Identity.IsAuthenticated;
        }
    }

    public class ExampleFilterWithDI : IActionFilter
    {
        private ILogger _logger;
        public ExampleFilterWithDI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExampleFilterWithDI>();
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //To do : before the action executes  
            _logger.LogInformation("OnActionExecuting");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //To do : after the action executes  
            _logger.LogInformation("OnActionExecuted");
        }

       
    }
}

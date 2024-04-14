using AuthenticationSvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ReferigenatorSvc.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionRequiredAttribute : Attribute, IAsyncActionFilter
    {
        private readonly UserPlatfromdbContext _dbContext;
        public TransactionRequiredAttribute(UserPlatfromdbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null || next is null)
                throw new ArgumentNullException();
            IExecutionStrategy executionStrategy = _dbContext.Database.CreateExecutionStrategy();
            await executionStrategy.ExecuteAsync(async () =>
            {
                var _tran = await _dbContext.Database.BeginTransactionAsync();
                var resut = await next();
                if (resut.Exception is null)
                    await _tran.CommitAsync();
                else
                    await _tran.RollbackAsync();
            });
        }
       
    }
}

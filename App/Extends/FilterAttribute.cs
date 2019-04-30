using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace App
{
    public class DataFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //var f1 = context.HttpContext.Request.Form[""];
            //context.HttpContext.Request.
            var f2 = new StreamReader(context.HttpContext.Request.Body).ReadToEnd();
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}

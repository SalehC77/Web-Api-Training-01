using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplication2.Filters
{
    public class LogSensitiveActionAttribute:ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // context.HttpContext. you can get info from http
            Debug.WriteLine("sensitive action executed !!!!!!!");
        }


    }
}

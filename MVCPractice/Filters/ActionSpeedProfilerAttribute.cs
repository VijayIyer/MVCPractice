using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
namespace MVCPractice.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionSpeedProfilerAttribute : FilterAttribute ,IActionFilter
    {
        private Stopwatch Timer = new Stopwatch();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Timer.Stop();
            if(filterContext.Exception == null)
            {
                filterContext.Controller.ViewBag.TimeElapsed = Timer.ElapsedMilliseconds.ToString();
            }
          
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Timer.Start();
           
        }
    }
}
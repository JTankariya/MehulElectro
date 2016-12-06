using BAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace MehulIndustries.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /BaseController/
        protected Employee currUser
        {
            get
            {
                if (Session["User"] != null)
                {
                    return (Employee)Session["User"];
                }
                else
                {
                    return null;
                }
            }
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //Log error
            ExceptionParameters param = new ExceptionParameters(filterContext.Exception);
            ExceptionLogic.AddException(param);
            //If the request is AJAX return JSON else redirect user to Error view.
            //if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            //{
            //    //Return JSON
            //    filterContext.Result = new JsonResult
            //    {
            //        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //        Data = new { error = true, message = "Sorry, an error occurred while processing your request." }
            //    };
            //}
            //else
            //{
            //    //Redirect user to error page
            //    filterContext.ExceptionHandled = true;
            //    filterContext.Result = this.RedirectToAction("Index", "Error");
            //}
            base.OnException(filterContext);
        }

    }
}

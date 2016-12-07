using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MehulIndustries.Models
{
    public class AuthorizeWebFormAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loginUrl = "~/Account/Login";
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var sessions = filterContext.HttpContext.Session;
                if (sessions["User"] != null)
                {
                    return;
                }
                else
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            status = "401"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                    //xhr status code 401 to redirect
                    filterContext.HttpContext.Response.StatusCode = 401;

                    return;
                }
            }
            var session = filterContext.HttpContext.Session;
            if (session["User"] != null)
                return;
            else
                filterContext.Result = new RedirectResult(loginUrl);
        }
    }
}
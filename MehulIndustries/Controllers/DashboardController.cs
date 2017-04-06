using BAL;
using MehulIndustries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MehulIndustries.Controllers
{
    [AuthorizeWebForm]
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            ViewBag.BatchStatus = BatchLogic.GetBatchStatus();
            ViewBag.ReorderStatus = ReorderLogic.GetReorderStatus();
            return View();
        }

    }
}

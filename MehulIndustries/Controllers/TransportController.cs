using BAL;
using MehulIndustries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace MehulIndustries.Controllers
{
    [AuthorizeWebForm]
    public class TransportController : BaseController
    {
        //
        // GET: /Transport/

        public ActionResult Add(string ID)
        {
            if (Convert.ToInt32(ID) > 0)
            {
                var transport = TransportLogic.GetTransportByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(transport);
            }
            else
            {
                return View(new Transport());
            }
        }

        [HttpPost]
        public ActionResult Add(Transport transport)
        {
            ResponseMsg response = new ResponseMsg();
            TransportLogic.AddTransport(transport);
            response.IsSuccess = true;
            return Json(response);
        }

        public ActionResult GetAll()
        {
            return PartialView("GetAll", TransportLogic.GetTransportByID(0));
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                TransportLogic.DeleteTransportByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

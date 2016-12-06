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
    public class PackingController : BaseController
    {
        //
        // GET: /Packing/

        public ActionResult Add(string ID)
        {
            if (Convert.ToInt32(ID) > 0)
            {
                var packing = PackingLogic.GetPackingByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(packing);
            }
            else
            {
                return View(new Packing());
            }
        }

        [HttpPost]
        public ActionResult Add(Packing packing)
        {
            ResponseMsg response = new ResponseMsg();
            PackingLogic.AddPacking(packing);
            response.IsSuccess = true;
            return Json(response);
        }

        public JsonResult GetByProductID(int ProductID)
        {
            ResponseMsg response = new ResponseMsg();
            response.ResponseValue = PackingLogic.GetPackingByProductID(ProductID);
            response.IsSuccess = true;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public string CheckDuplicateName(string Name, string ID)
        {
            var packings = PackingLogic.GetPackingByID(0);
            if (packings != null && packings.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    packings = packings.Where(x => x.Name == Name && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    packings = packings.Where(x => x.Name == Name);
                }
                if (packings.Count() > 0)
                {
                    return "false";
                }
                else
                {
                    return "true";
                }
            }
            else
            {
                return "true";
            }
        }

        public ActionResult GetAll()
        {
            return PartialView("GetAll", PackingLogic.GetPackingByID(0));
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                PackingLogic.DeletePackingByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

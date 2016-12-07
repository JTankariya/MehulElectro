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
    public class ShadeController : BaseController
    {
        //
        // GET: /Shade/

        public ActionResult Add(string ID)
        {
            if (Convert.ToInt32(ID) > 0)
            {
                var shade = ShadeLogic.GetShadeByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(shade);
            }
            else
            {
                return View(new Shade());
            }
        }

        [HttpPost]
        public ActionResult Add(Shade shade)
        {
            ResponseMsg response = new ResponseMsg();
            ShadeLogic.AddShade(shade);
            response.IsSuccess = true;
            return Json(response);
        }

        public JsonResult GetByProductID(int ProductID)
        {
            ResponseMsg response = new ResponseMsg();
            response.ResponseValue = ShadeLogic.GetShadeByProductID(ProductID);
            response.IsSuccess = true;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public string CheckDuplicateName(string Name, string ID)
        {
            var shades = ShadeLogic.GetShadeByID(0);
            if (shades != null && shades.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    shades = shades.Where(x => x.Name == Name && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    shades = shades.Where(x => x.Name == Name);
                }
                if (shades.Count() > 0)
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
            return PartialView("GetAll", ShadeLogic.GetShadeByID(0));
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                response.IsSuccess = ShadeLogic.DeleteShadeByID(ID);
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}

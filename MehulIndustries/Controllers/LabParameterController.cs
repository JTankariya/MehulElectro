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
    public class LabParameterController : BaseController
    {
        //
        // GET: /LabParameter/
        public ActionResult Add(string ID)
        {
            ViewBag.LabParameterTypes = LabParameterValueTypeLogic.GetLabParameterTypes();
            if (Convert.ToInt32(ID) > 0)
            {
                var labparameter = LabParameterLogic.GetLabParameterByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(labparameter);
            }
            else
            {
                return View(new LabParameter());
            }
        }

        [HttpPost]
        public ActionResult Add(LabParameter labparameter)
        {
            ResponseMsg response = new ResponseMsg();
            LabParameterLogic.AddLabParameter(labparameter);
            response.IsSuccess = true;
            return Json(response);
        }

        public string CheckDuplicateName(string Name, string ID)
        {
            var labparameters = LabParameterLogic.GetLabParameterByID(0);
            if (labparameters != null && labparameters.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    labparameters = labparameters.Where(x => x.Name == Name && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    labparameters = labparameters.Where(x => x.Name == Name);
                }
                if (labparameters.Count() > 0)
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
            return PartialView("GetAll", LabParameterLogic.GetLabParameterByID(0));
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                LabParameterLogic.DeleteLabParameterByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

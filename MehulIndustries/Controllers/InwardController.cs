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
    public class InwardController : BaseController
    {
        //
        // GET: /Inward/

        public ActionResult Add(string ID)
        {
            ViewBag.Parties = PartyLogic.GetPartyByID(0).Where(x => x.PartyGroupID == (PartyGroupLogic.GetAllPartyGroup().FirstOrDefault(y => y.GroupCode == "100004").ID)).OrderBy(x=>x.Name);
            ViewBag.Products = ProductLogic.GetRawMaterialProducts();
            if (Convert.ToInt32(ID) > 0)
            {
                var Inward = InwardLogic.GetInwardByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(Inward);
            }
            else
            {
                return View(new Inward());
            }
        }
        
        [HttpPost]
        public ActionResult Add(Inward inward)
        {
            ResponseMsg response = new ResponseMsg();
            inward.CreatedBy = inward.UpdatedBy = currUser.ID;
            if (InwardLogic.SaveInward(inward))
            {
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
            return Json(response);
        }

        public ActionResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                InwardLogic.DeleteInwardByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetAll()
        {
            return PartialView("GetAll", InwardLogic.GetInwardByID(0));
        }
        
        public string CheckDuplicateInwardNo(string InwardNo, string ID)
        {
            var Inwards = InwardLogic.GetInwardByID(0);
            if (Inwards != null && Inwards.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    Inwards = Inwards.Where(x => x.InwardNo == InwardNo && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    Inwards = Inwards.Where(x => x.InwardNo == InwardNo);
                }
                if (Inwards.Count() > 0)
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
        
    }
}

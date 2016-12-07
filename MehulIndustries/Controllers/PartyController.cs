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
    public class PartyController : BaseController
    {
        //
        // GET: /Party/

        public ActionResult Add(string ID)
        {
            ViewBag.TransportList = TransportLogic.GetTransportByID(0);
            ViewBag.PartyGroups = PartyGroupLogic.GetAllPartyGroup();
            if (Convert.ToInt32(ID) > 0)
            {
                var party = PartyLogic.GetPartyByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(party);
            }
            else
            {
                return View(new Party());
            }

        }

        public JsonResult GetOrders(int ID)
        {
            ResponseMsg response = new ResponseMsg();
            response.IsSuccess = true;
            response.ResponseValue = PartyLogic.GetOrders(ID).Where(x => !x.IsClosed);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(Party partyModel)
        {
            ResponseMsg response = new ResponseMsg();
            partyModel.CreatedBy = partyModel.UpdatedBy = currUser.ID;
            if (PartyLogic.SaveParty(partyModel))
            {
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
            return Json(response);
        }

        public ActionResult GetAll()
        {
            return View(PartyLogic.GetPartyByID(0));
        }

        [HttpPost]
        public string CheckDuplicateEmailId(string EmailId, string ID)
        {
            var parties = PartyLogic.GetPartyByID(0);
            if (parties != null && parties.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    parties = parties.Where(x => x.EmailId == EmailId && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    parties = parties.Where(x => x.EmailId == EmailId);
                }
                if (parties.Count() > 0)
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

        [HttpPost]
        public string CheckDuplicateName(string Name, string ID)
        {
            var parties = PartyLogic.GetPartyByID(0);
            if (parties != null && parties.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    parties = parties.Where(x => x.Name == Name && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    parties = parties.Where(x => x.Name == Name);
                }
                if (parties.Count() > 0)
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

        public ActionResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                PartyLogic.DeletePartyByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPartyAddresses(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(ID) && Convert.ToInt32(ID) > 0)
            {
                response.IsSuccess = true;
                response.ResponseValue = PartyAddressLogic.GetPartyAddress(Convert.ToInt32(ID));
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

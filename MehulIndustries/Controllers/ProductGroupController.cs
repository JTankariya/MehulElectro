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
    public class ProductGroupController : BaseController
    {
        //
        // GET: /ProductGroup/


        public ActionResult Add(string ID)
        {
            ViewBag.ProductGroupTypes = ProductGroupTypeLogic.GetAllProductGroupTypes();
            if (Convert.ToInt32(ID) > 0)
            {
                var packing = ProductGroupLogic.GetProductGroupByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(packing);
            }
            else
            {
                return View(new ProductGroup());
            }
        }

        [HttpPost]
        public ActionResult Add(ProductGroup productGroup)
        {
            ResponseMsg response = new ResponseMsg();
            ProductGroupLogic.AddProductGroup(productGroup);
            response.IsSuccess = true;
            return Json(response);
        }

        public string CheckDuplicateName(string Name, string ID)
        {
            var groups = ProductGroupLogic.GetProductGroupByID(0);
            if (groups != null && groups.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    groups = groups.Where(x => x.Name == Name && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    groups = groups.Where(x => x.Name == Name);
                }
                if (groups.Count() > 0)
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
            return PartialView("GetAll", ProductGroupLogic.GetProductGroupByID(0));
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                ProductGroupLogic.DeleteProductGroupByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

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
    public class ProductController : BaseController
    {
        //
        // GET: /Product/

        public ActionResult Add(string ID)
        {
            ViewBag.Shades = ShadeLogic.GetShadeByID(0);
            ViewBag.Packings = PackingLogic.GetPackingByID(0);
            ViewBag.ProductGroups = ProductGroupLogic.GetProductGroupByID(0);
            ViewBag.ProductUnits = ProductUnitLogic.GetProductUnitByID(0);
            ViewBag.Parties = PartyLogic.GetPartyByID(0).Select(x => new { x.ID, x.Name });
            ViewBag.RawMaterialTypes = RawMaterialTypeLogic.RawMaterialTypeByID(0);
            if (Convert.ToInt32(ID) > 0)
            {
                var product = ProductLogic.GetProductByID(Convert.ToInt32(ID)).FirstOrDefault();
                return View(product);
            }
            else
            {
                return View(new Product());
            }
        }

        public JsonResult GetGroupType(int GroupID)
        {
            ResponseMsg response = new ResponseMsg();
            if (GroupID > 0)
            {
                var group = ProductGroupLogic.GetProductGroupByID(GroupID);
                if (group != null)
                {
                    response.IsSuccess = true;
                    response.ResponseValue = group;
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CheckDuplicateName(string Name, string ID)
        {
            var products = ProductLogic.CheckDuplicateProduct(Convert.ToInt32(ID),Name);
            if (products != null && products.Count() > 0)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        [HttpPost]
        public ActionResult Add(Product productModel)
        {
            ResponseMsg response = new ResponseMsg();
            productModel.CreatedBy = productModel.UpdatedBy = currUser.ID;
            if (ProductLogic.SaveProduct(productModel))
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
            return View(ProductLogic.GetProductsForView());
        }

        public JsonResult GetAllParties()
        {
            return Json(PartyLogic.GetPartyByID(0), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool CheckPackingInUse(int ProductID, int PackingID)
        {
            if (PackingLogic.CheckReference(ProductID, PackingID))
            {
                return false;
            }
            else
            {
                if (PackingID > 0 && ProductID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public ActionResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                if (!ProductLogic.DeleteProductByID(ID))
                {
                    response.IsSuccess = true;
                    response.ResponseValue = "";
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductShadesAndPackings(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(ID) && Convert.ToInt32(ID) > 0)
            {
                response.IsSuccess = true;
                response.ResponseValue = new
                {
                    Shades = ShadeLogic.GetShadeByProductID(Convert.ToInt32(ID)),
                    Packings = PackingLogic.GetPackingByProductID(Convert.ToInt32(ID))
                };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

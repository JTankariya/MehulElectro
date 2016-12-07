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
    public class BillOfMaterialController : BaseController
    {
        //
        // GET: /BillOfMaterial/

        public ActionResult Add(string ID)
        {
            ViewBag.FinishedProductList = ProductLogic.GetFinishedProducts();
            ViewBag.RawMaterialProductList = ProductLogic.GetRawMaterialProducts();
            ViewBag.BOMProcessList = BOMProcessLogic.BOMProcessByID(0);
            ViewBag.UnitList = ProductUnitLogic.GetProductUnitByID(0);
            ViewBag.LabParameters = LabParameterLogic.GetLabParameterByID(0);
            if (Convert.ToInt32(ID) > 0)
            {
                var bom = BillOfMaterialLogic.GetBillOfMaterialByID(Convert.ToInt32(ID)).FirstOrDefault();
                ViewBag.ShadeList = ShadeLogic.GetShadeByProductID(Convert.ToInt32(bom.ProductID)).Select(x => new { x.ShadeID, x.ShadeName }).Distinct();
                //var productShades = ShadeLogic.GetShadeByProductID(bom.ProductID);
                //if (productShades != null && productShades.Count() > 0)
                //{
                //    foreach (var productShade in productShades.Select(x => x.ShadeID).Distinct())
                //    {
                //        ViewBag.ShadeList = ShadeLogic.GetShadeByID(Convert.ToInt32(productShade));
                //    }
                //}
                //else
                //{
                //    ViewBag.ShadeList = null;
                //}
                return View(bom);
            }
            else
            {
                ViewBag.ShadeList = null;
                return View(new BillOfMaterial());
            }
        }
        [HttpPost]
        public ActionResult Add(BillOfMaterial billOfMaterial)
        {
            ResponseMsg response = new ResponseMsg();
            billOfMaterial.CreatedBy = billOfMaterial.UpdatedBy = currUser.ID;
            BillOfMaterialLogic.SaveBillOfMaterial(billOfMaterial);
            response.IsSuccess = true;
            return Json(response);
        }

        public ActionResult GetRawMaterialDetails(int ProductID, int ShadeID, string RevisionNo)
        {
            ViewBag.RawMaterialProductList = ProductLogic.GetRawMaterialProducts();
            ViewBag.BOMProcessList = BOMProcessLogic.BOMProcessByID(0);
            var lastReivision = BillOfMaterialDetailLogic.GetRawMaterialDetails(ProductID, ShadeID, RevisionNo);
            if (lastReivision != null)
            {
                return PartialView("_BOMRawMaterials", lastReivision);
            }
            else
            {
                return null;
            }
        }

        public ActionResult GetAll()
        {
            var BOMs = BillOfMaterialLogic.GetBillOfMaterialByID(0);
            return PartialView("GetAll", BOMs);
        }

        public ActionResult GetLastRevision(int ProductID, int ShadeID)
        {
            ViewBag.RawMaterialProductList = ProductLogic.GetRawMaterialProducts();
            ViewBag.BOMProcessList = BOMProcessLogic.BOMProcessByID(0);
            var lastReivision = BillOfMaterialLogic.GetLastRevision(ProductID, ShadeID);
            if (lastReivision != null)
            {
                return PartialView("_BOMRawMaterials", lastReivision);
            }
            else
            {
                return null;
            }
        }

        public JsonResult GetProductShadesAndBatchSize(int ProductID)
        {
            ResponseMsg response = new ResponseMsg();
            var product = ProductLogic.GetProductByID(ProductID).FirstOrDefault();
            List<Shade> shades = new List<Shade>();
            if (product.Shades != null && product.Shades.Count() > 0)
            {
                foreach (var productShade in product.Shades.Select(x => x.ShadeID).Distinct())
                {
                    shades.Add(ShadeLogic.GetShadeByID(productShade).FirstOrDefault());
                }
            }

            var batchsize = new { Min = product.BatchMin, Max = product.BatchMax, Ideal = product.BatchIdeal };
            response.IsSuccess = true;
            response.ResponseValue = new { Shades = shades, BatchSize = batchsize, ProductUnit = product.BatchUnitID };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductSalesRateAndGravity(int ProductID)
        {
            ResponseMsg response = new ResponseMsg();
            var product = ProductLogic.GetProductByID(ProductID).FirstOrDefault();
            if (product != null)
            {
                response.IsSuccess = true;
                response.ResponseValue = new
                {
                    Gravity = product.Gravity,
                    SPercentage = product.SolidPercentage,
                    SG = product.SolidGravity,
                    PR = product.PurchaseRate,
                    RawMaterialType = product.RawMaterialTypeID
                };
            }
            else
            {
                response.IsSuccess = false;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRevisionNo(int ProductID, int ShadeID)
        {
            ResponseMsg response = new ResponseMsg();
            var revisionNo = BillOfMaterialLogic.GetRevisionNo(ProductID, ShadeID);
            if (revisionNo > 0)
            {
                response.IsSuccess = true;
                response.ResponseValue = revisionNo;
            }
            else
            {
                response.IsSuccess = false;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                BillOfMaterialLogic.DeleteBillOfMaterialByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

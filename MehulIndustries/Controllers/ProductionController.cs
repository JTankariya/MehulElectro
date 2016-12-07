using BAL;
using MehulIndustries.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace MehulIndustries.Controllers
{
    [AuthorizeWebForm]
    public class ProductionController : BaseController
    {
        //
        // GET: /Production/

        public ActionResult BatchAllocation()
        {
            return View(BatchLogic.GetBatchAllocationData());
        }

        [HttpPost]
        public ActionResult AllocateBatch(List<Batch> batches)
        {
            ResponseMsg response = new ResponseMsg();
            if (batches != null)
            {
                foreach (var batch in batches)
                {
                    if (BatchLogic.CheckBatchNo(batch.BatchNo))
                    {
                        response.IsSuccess = false;
                        response.ResponseValue = "Batch No " + batch.BatchNo + " is already exist, please enter another batch number.";
                        return Json(response);
                    }
                }
                foreach (var b in batches)
                {
                    b.CreatedBy = ((Employee)Session["User"]).ID;
                    BatchLogic.Save(b);
                }
                response.IsSuccess = true;
            }
            return Json(response);
        }

        public ActionResult Grinding()
        {
            ViewBag.Employees = EmployeeLogic.GetEmployeeByID(0).Where(x => x.ID != currUser.ID);
            return View(GrindingLogic.GetGrindingBatches(0));
        }

        public JsonResult StartGrinding(int ID, int ProductID, int ShadeID,int EmployeeID)
        {
            ResponseMsg response = new ResponseMsg();
            var bom = BillOfMaterialLogic.GetLastRevision(ProductID, ShadeID);
            if (bom == null)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Bill of material for selected product and shade is not found, Please enter Bill of material detail for selected Product and Batch";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            var materials = GrindingLogic.Start(ID,EmployeeID);
            if (materials != null && materials.Count() > 0)
            {
                var fileName = GrindingLogic.PrintRawMaterial(materials, Server.MapPath(ConfigurationManager.AppSettings["ReportFolderPath"]), ID, currUser.Name,EmployeeID);
                if (!string.IsNullOrEmpty(fileName))
                {
                    response.IsSuccess = true;
                    response.ResponseValue = ConfigurationManager.AppSettings["ReportFolderPath"].Replace("~", "").Trim() + fileName;
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetGrindingStock(int BatchID)
        {
            var grindingStock = StockLogic.GetGrindingStock(BatchID);
            if (grindingStock != null)
            {
                foreach (var item in grindingStock)
                {
                    var productStock = StockLogic.GetStockReport(null, null, Convert.ToString(item.ProductID), null, null);
                    if (productStock != null && productStock.Count() > 0)
                    {
                        item.StockQty = Convert.ToString(productStock.FirstOrDefault().ClosingQty);
                    }
                    else
                    {
                        item.StockQty = "0";
                    }
                }
            }
            return PartialView("_GrindingStock", grindingStock);
        }

        [HttpPost]
        public ActionResult GrindingDone(Batch batch)
        {
            ResponseMsg response = new ResponseMsg();
            var bom = BillOfMaterialLogic.GetLastRevision(batch.ProductID, batch.ShadeID);
            if (bom == null)
            {
                response.IsSuccess = false;
                response.ResponseValue = "Bill of material for selected product and shade is not found, Please enter Bill of material detail for selected Product and Batch";
                return Json(response);
            }
            if (GrindingLogic.Done(batch))
            {
                response.IsSuccess = true;
            }
            return Json(response);
        }
        public ActionResult Shading()
        {
            return View(ShadingLogic.GetShadingBatches());
        }
        public JsonResult StartShading(int ID)
        {
            return Json(new ResponseMsg() { IsSuccess = ShadingLogic.Start(ID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ShadingDone(Batch batch)
        {
            ResponseMsg response = new ResponseMsg();
            if (ShadingLogic.Done(batch))
            {
                response.IsSuccess = true;
            }
            return Json(response);
        }

        public ActionResult QC()
        {
            return View(QCLogic.GetQCBatches());
        }

        public JsonResult StartQC(int ID)
        {
            return Json(new ResponseMsg() { IsSuccess = QCLogic.Start(ID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult QCDone(string ID, List<BillOfMaterialLabParameters> paramValue)
        {
            ResponseMsg response = new ResponseMsg();
            if (QCLogic.Done(ID, paramValue))
            {
                var fileName = QCLogic.PrintQCSlip(paramValue, Server.MapPath(ConfigurationManager.AppSettings["ReportFolderPath"]), Convert.ToInt32(ID), currUser.Name);
                if (!string.IsNullOrEmpty(fileName))
                {
                    response.IsSuccess = true;
                    response.ResponseValue = ConfigurationManager.AppSettings["ReportFolderPath"].Replace("~", "").Trim() + fileName;
                }
            }
            return Json(response);
        }
        public ActionResult Packing()
        {
            var batches = PackingProcessLogic.GetPackingBatches();
            if (batches != null)
            {
                ViewBag.PackingBatches = batches.Select(x => new
                {
                    ID = x.ID,
                    BatchNo = x.BatchNo + " ( " + x.ProductName + " , " + x.ShadeName + " )"
                });
            }
            
            return View();
        }
        public JsonResult StartPacking(int ID)
        {
            return Json(new ResponseMsg() { IsSuccess = PackingLogic.Start(ID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetBatchPackings(string ID)
        {
            ViewBag.PackingProductList = ProductLogic.GetDrums();
            var batchData = PackingProcessLogic.GetBatchProductShadePacking(ID).ToList();
            if (batchData != null)
            {
                foreach (var item in batchData)
                {
                    var bPacking = BatchLogic.GetBatchDetails(Convert.ToInt32(ID));
                    if (bPacking != null)
                        item.BatchPackings = bPacking.ToList();
                }
            }
            return PartialView("ProductPackingInfo", batchData);
        }

        [HttpPost]
        public ActionResult PackingDone(string ID, List<Batch> packingDetails)
        {
            ResponseMsg response = new ResponseMsg();
            if (PackingProcessLogic.Done(ID, packingDetails))
            {
                response.IsSuccess = true;
            }
            return Json(response);
        }
    }
}

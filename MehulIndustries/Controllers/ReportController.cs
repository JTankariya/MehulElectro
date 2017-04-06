using BAL;
using MehulIndustries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MehulIndustries.Controllers
{
    [AuthorizeWebForm]
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Stock()
        {
            ViewBag.Products = ProductLogic.GetFinishedProducts();
            return View(StockLogic.GetStockReport(null, null, null, null, null));
        }

        [HttpPost]
        public PartialViewResult Stock(DateTime? FromDate, DateTime? ToDate, string ProductID, string ShadeID, string PackingID)
        {
            return PartialView("_StockDetail", StockLogic.GetStockReport(FromDate, ToDate, ProductID, ShadeID, PackingID));
        }

        public JsonResult GetStock(string ProductID, string ShadeID, string PackingID)
        {
            var responseValue = new
            {
                StockData = StockLogic.GetStockReport(null, null, ProductID, ShadeID, PackingID),
                Factor = PackingLogic.GetPackingByProductID(Convert.ToInt32(ProductID)).FirstOrDefault(x => x.PackingID == Convert.ToInt32(PackingID))
            };
            return Json(new ResponseMsg { IsSuccess = true, ResponseValue = responseValue }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BatchStatus()
        {
            var batches = BatchLogic.GetBatchStatus();
            return View(batches);
        }

        public ActionResult BatchCompletionTime()
        {
            var batches = BatchLogic.GetBatchStatus();
            return View(batches);
        }

        public ActionResult OrderRegister()
        {
            ViewBag.Parties = PartyLogic.GetPartyByID(0);
            ViewBag.Products = ProductLogic.GetFinishedProducts();
            var orders = OrderLogic.GetOrderByID(0);
            if (orders != null)
                orders = orders.OrderBy(x => x.OrderDate);
            return View(orders);
        }

        [HttpPost]
        public ActionResult OrderRegister(DateTime? FromDate, DateTime? ToDate, string PartyID, string ProductID, string ShadeID, string PackingID)
        {
            var orders = OrderLogic.GetOrderByID(0);
            if (FromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= FromDate.Value);
            }
            if (ToDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= ToDate.Value);
            }
            if (!string.IsNullOrEmpty(PartyID))
            {
                orders = orders.Where(x => x.PartyID == Convert.ToInt32(PartyID));
            }
            if (!string.IsNullOrEmpty(ProductID))
            {
                orders = orders.Where(x => x.orderDetail.Where(y => y.ProductID == Convert.ToInt32(ProductID)).Count() > 0);
            }
            if (!string.IsNullOrEmpty(ShadeID))
            {
                orders = orders.Where(x => x.orderDetail.Where(y => y.ShadeID == Convert.ToInt32(ShadeID)).Count() > 0);
            }
            if (!string.IsNullOrEmpty(PackingID))
            {
                orders = orders.Where(x => x.orderDetail.Where(y => y.PackingID == Convert.ToInt32(PackingID)).Count() > 0);
            }
            return PartialView("_OrderRegisterDetail", orders.OrderBy(x => x.OrderDate));
        }
        public ActionResult DispatchRegister()
        {
            ViewBag.Parties = PartyLogic.GetPartyByID(0);
            ViewBag.Products = ProductLogic.GetFinishedProducts();
            var dispatches = DispatchLogic.GetDispatchByID(0);
            if (dispatches != null)
                dispatches = dispatches.OrderBy(x => x.DODate);
            return View(dispatches);
        }

        [HttpPost]
        public ActionResult DispatchRegister(DateTime? FromDate, DateTime? ToDate, string PartyID, string ProductID, string ShadeID, string PackingID)
        {
            var dispatches = DispatchLogic.GetDispatchByID(0);
            if (FromDate.HasValue)
            {
                dispatches = dispatches.Where(x => x.DODate >= FromDate.Value);
            }
            if (ToDate.HasValue)
            {
                dispatches = dispatches.Where(x => x.DODate <= ToDate.Value);
            }
            if (!string.IsNullOrEmpty(PartyID))
            {
                dispatches = dispatches.Where(x => x.PartyID == Convert.ToInt32(PartyID));
            }
            if (!string.IsNullOrEmpty(ProductID))
            {
                dispatches = dispatches.Where(x => x.details.Where(y => y.ProductID == Convert.ToInt32(ProductID)).Count() > 0);
            }
            if (!string.IsNullOrEmpty(ShadeID))
            {
                dispatches = dispatches.Where(x => x.details.Where(y => y.ShadeID == Convert.ToInt32(ShadeID)).Count() > 0);
            }
            if (!string.IsNullOrEmpty(PackingID))
            {
                dispatches = dispatches.Where(x => x.details.Where(y => y.PackingID == Convert.ToInt32(PackingID)).Count() > 0);
            }
            return PartialView("_DispatchRegisterDetail", dispatches.OrderBy(x => x.DODate));
        }
    }
}

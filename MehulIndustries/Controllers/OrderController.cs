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
    public class OrderController : BaseController
    {
        //
        // GET: /Order/

        public ActionResult Add(string ID)
        {
            ViewBag.Dispatch = false;
            ViewBag.Parties = PartyLogic.GetPartyByID(0).Where(x => x.PartyGroupID == (PartyGroupLogic.GetAllPartyGroup().FirstOrDefault(y => y.GroupCode == "100003").ID)).OrderBy(x => x.Name);
            ViewBag.Transports = TransportLogic.GetTransportByID(0);
            ViewBag.Products = ProductLogic.GetFinishedProducts();
            if (Convert.ToInt32(ID) > 0)
            {

                var order = OrderLogic.GetOrderByID(Convert.ToInt32(ID)).FirstOrDefault();
                ViewBag.Addresses = PartyAddressLogic.GetPartyAddress(order.PartyID);
                return View(order);
            }
            else
            {
                var order = new Order();
                order.OrderNo = OrderLogic.GetMaxOrderNo();
                return View(order);
            }
        }

        public JsonResult MoveToDispatch(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(ID))
            {
                OrderLogic.MoveToDispatch(ID);
                response.IsSuccess = true;
            }
            return Json(response);
        }

        public JsonResult GetClosingQty(int ProductID, int ShadeID, string PackingIDs)
        {
            ResponseMsg response = new ResponseMsg();
            var packings = StockLogic.GetClosingQty(ProductID, ShadeID, PackingIDs);
            if (packings != null && packings.Count() > 0)
            {
                response.IsSuccess = true;
                response.ResponseValue = packings;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(Order order)
        {
            ResponseMsg response = new ResponseMsg();
            order.CreatedBy = order.UpdatedBy = currUser.ID;
            if (OrderLogic.SaveOrder(order))
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
                OrderLogic.DeleteOrderByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reopen(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                OrderLogic.ReopenOrder(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAll()
        {
            var orders = OrderLogic.GetOrderByID(0);
            return PartialView("GetAll", (orders != null ? orders.OrderByDescending(x => x.OrderDate) : orders));
        }

        public ActionResult ClosedOrders()
        {
            return View(OrderLogic.GetClosedOrders());
        }

        public string CheckDuplicateOrderNo(string OrderNo, int ID)
        {
            var order = OrderLogic.CheckDuplicateOrderNo(ID, OrderNo);
            if (order != null && order.Count() > 0)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        public ActionResult ReadyAvailable()
        {
            var readyOrders = ReadyAvailableLogic.GetAllOrderTransactionStatus();
            return View((readyOrders != null ? readyOrders.OrderByDescending(x => x.OrderID) : readyOrders));
        }

        public ActionResult MoveToProduction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MoveToProduction(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(ID))
            {
                OrderLogic.MoveToProduction(ID);
                response.IsSuccess = true;
            }
            return Json(response);
        }

        [HttpPost]
        public ActionResult Close(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (!string.IsNullOrEmpty(ID))
            {
                OrderLogic.CloseOrder(ID);
                response.IsSuccess = true;
            }
            return Json(response);
        }
        public PartialViewResult GetDetailForOrderRegister(string ID)
        {
            var order = OrderLogic.GetOrderByID(Convert.ToInt32(ID)).FirstOrDefault();
            return PartialView("_OrderDetailForRegister", order);
        }
    }
}

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
    public class EmployeeController : BaseController
    {
        //
        // GET: /Employee/

        public ActionResult Add(string ID)
        {
            ViewBag.EmployeeTypes = EmployeeTypeLogic.GetAllEmployeeTypes();
            if (Convert.ToInt32(ID) > 0)
            {
                var employee = EmployeeLogic.GetEmployeeByID(Convert.ToInt32(ID)).FirstOrDefault();
                employee.Password = StringCipher.Decrypt(employee.Password);
                return View(employee);
            }
            else
            {
                return View(new Employee());
            }
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            ResponseMsg response = new ResponseMsg();
            employee.CreatedBy = employee.UpdatedBy = currUser.ID;
            EmployeeLogic.AddEmployee(employee);
            response.IsSuccess = true;
            return Json(response);
        }

        [HttpPost]
        public string CheckOldPassword(string Password)
        {
            if (StringCipher.Decrypt(((Employee)Session["User"]).Password) != Password)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        [HttpPost]
        public string CheckDuplicateUserName(string UserName, string ID)
        {
            var employees = EmployeeLogic.GetEmployeeByID(0);
            if (employees != null && employees.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    employees = employees.Where(x => x.UserName == UserName && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    employees = employees.Where(x => x.UserName == UserName);
                }
                if (employees.Count() > 0)
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
        public string CheckDuplicateEmailId(string EmailId, string ID)
        {
            var employees = EmployeeLogic.GetEmployeeByID(0);
            if (employees != null && employees.Count() > 0)
            {
                if (Convert.ToInt32(ID) > 0)
                {
                    employees = employees.Where(x => x.EmailId == EmailId && x.ID != Convert.ToInt32(ID));
                }
                else
                {
                    employees = employees.Where(x => x.EmailId == EmailId);
                }
                if (employees.Count() > 0)
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
            var employees = EmployeeLogic.GetEmployeeByID(0);
            return PartialView("GetAll", employees.Where(x => x.ID != ((Employee)Session["User"]).ID));
        }

        public JsonResult Delete(string ID)
        {
            ResponseMsg response = new ResponseMsg();
            if (Convert.ToInt32(ID) > 0)
            {
                EmployeeLogic.DeleteEmployeeByID(ID);
                response.IsSuccess = true;
                response.ResponseValue = "";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}

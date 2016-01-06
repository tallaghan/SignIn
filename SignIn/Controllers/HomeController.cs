using SignIn.Services;
using SignIn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SignIn.Controllers
{
    public class HomeController : Controller
    {
        private SigningsService _signingsService;
        private EmployeeService _employeeService;
        
        public HomeController(SigningsService signingsService, EmployeeService employeeService)
        {
            _signingsService = signingsService;
            _employeeService = employeeService;
        }

        public ActionResult WhoIsIn()
        {
            return View();
        }

        public ActionResult Employees()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult List(String id)
        {
            int employeeID = 0;

            try
            {
                // if it's numeric we assume it's employeeID, if not numeric it's employee username
                employeeID = Convert.ToInt32(id ?? "0");
            }
            catch (Exception)
            {
                // exception thrown so must be employee username
                EmployeeViewModel evm = _employeeService.GetByName(id);

                if (evm==null) {
                    employeeID = -1;
                } else {
                    employeeID = evm.EmployeeID;
                }
            }

            // store so that if client was displaying all employees then we can reload page with all employees
            // or if client displaying single employee then we can reload page with single employee
            TempData["EmployeeID"] = employeeID;

            return View(employeeID);
        }

        public ActionResult SaveEmployee(int employeeID,
                                    string username,
                                    string fullName,
                                    bool isActive)
        {

            EmployeeViewModel employee = _employeeService.SaveEmployee(new EmployeeViewModel()
            {
                EmployeeID = employeeID,
                Username = username,
                FullName = fullName,
                IsActive = isActive
            });


            return Json(employee, JsonRequestBehavior.AllowGet);  
        }

        public JsonResult DisableEmployee(int employeeId)
        {
            try
            {
                EmployeeViewModel emp = _employeeService.DisableEmployee(employeeId);

                return Json(new { Employee = emp, Success = true });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    ErrorMessage = string.Format("Unexpected error disabling employee id '{0}'", ex.Message)
                });
            }
        }

        public JsonResult GetActiveEmployees(int id)
        {
            IList<EmployeeViewModel> employees = _employeeService.GetAll().Where(x => (id == 0 || x.EmployeeID == id) && x.IsActive == true).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEmployees()
        {
            IList<EmployeeViewModel> employees = _employeeService.GetAll();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeesSignedInToday()
        {
            IList<EmployeeViewModel> employees = _employeeService.GetAll().Where(x => x.IsActive == true && x.LastSigningWasIn == true && x.LastSigningWasToday == true).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateSigning(int id)
        {         
            _signingsService.CreateSigning(id);

            int employeeID = Convert.ToInt32(TempData["EmployeeID"] ?? 0);

            EmployeeViewModel employee = _employeeService.GetById(id);

            return Json(employee, JsonRequestBehavior.AllowGet);      
        }

    }
}
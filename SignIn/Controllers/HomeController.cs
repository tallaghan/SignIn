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
    /// <summary>
    /// primary controller in app
    /// </summary>
    public class HomeController : Controller
    {
        private SigningsService _signingsService;
        private EmployeeService _employeeService;
        
        /// <summary>
        /// class ctor
        /// </summary>
        /// <param name="signingsService">signings service class</param>
        /// <param name="employeeService">employee service class</param>
        public HomeController(SigningsService signingsService, EmployeeService employeeService)
        {
            _signingsService = signingsService;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Display view showing who is currently signed in
        /// </summary>
        /// <returns></returns>
        public ActionResult WhoIsIn()
        {
            return View();
        }

        /// <summary>
        /// Display view showing who all employees
        /// </summary>
        /// <returns></returns>
        public ActionResult Employees()
        {
            return View();
        }


        /// <summary>
        /// view can be shown for all employees or just one employee
        /// </summary>
        /// <param name="id">employee name or id</param>
        /// <returns>display view showing 1 or all employees</returns>
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


        /// <summary>
        /// save employee and return saved employee object
        /// </summary>
        /// <param name="employeeID">id</param>
        /// <param name="username">username</param>
        /// <param name="fullName">full name</param>
        /// <param name="isActive">is user active</param>
        /// <returns>json representation of employee</returns>
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


        /// <summary>
        /// disable employee
        /// </summary>
        /// <param name="employeeId">employee id</param>
        /// <returns>json representation of employee and whether operation was successful</returns>
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

        /// <summary>
        /// get active employees
        /// </summary>
        /// <param name="id">employee id or 0 if all active employees are requested</param>
        /// <returns>json representation of list of active employees</returns>
        public JsonResult GetActiveEmployees(int id)
        {
            IList<EmployeeViewModel> employees = _employeeService.GetAll().Where(x => (id == 0 || x.EmployeeID == id) && x.IsActive == true).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get all employees
        /// </summary>
        /// <returns>json representation of list of all employees</returns>
        public JsonResult GetAllEmployees()
        {
            IList<EmployeeViewModel> employees = _employeeService.GetAll();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get all currently signed in employees
        /// </summary>
        /// <returns>json representation of list of all employees signed in today</returns>
        public JsonResult GetEmployeesSignedInToday()
        {
            IList<EmployeeViewModel> employees = _employeeService.GetAll().Where(x => x.IsActive == true && x.LastSigningWasIn == true && x.LastSigningWasToday == true).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sign a user in or out
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>jsdon representation of employee signed in/out</returns>
        public ActionResult CreateSigning(int id)
        {         
            _signingsService.CreateSigning(id);

            int employeeID = Convert.ToInt32(TempData["EmployeeID"] ?? 0);

            EmployeeViewModel employee = _employeeService.GetById(id);

            return Json(employee, JsonRequestBehavior.AllowGet);      
        }

    }
}
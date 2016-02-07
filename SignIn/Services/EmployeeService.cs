using SignIn.Repositories;
using SignIn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Services
{
    /// <summary>
    /// service the app
    /// </summary>
    public class EmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        /// <summary>
        /// class ctor
        /// </summary>
        /// <param name="employeeRepository">employee repository populated via unity</param>
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// get employee by name
        /// </summary>
        /// <param name="employeeName">employee name</param>
        /// <returns>employee viewmodel</returns>
        public EmployeeViewModel GetByName(string employeeName)
        {
            return _employeeRepository.GetByName(employeeName);
        }


        /// <summary>
        /// save employee
        /// </summary>
        /// <param name="employee">employee viewmodel</param>
        /// <returns>employee saved</returns>
        public EmployeeViewModel SaveEmployee(EmployeeViewModel employee)
        {
            return _employeeRepository.SaveEmployee(employee);
        }


        /// <summary>
        /// disable employee
        /// </summary>
        /// <param name="employeeId">employee id</param>
        /// <returns>employee disabled</returns>
        public EmployeeViewModel DisableEmployee(int employeeId)
        {
            return _employeeRepository.DisableEmployee(employeeId);
        }

        /// <summary>
        /// get employee by id
        /// </summary>
        /// <param name="employeeID">employee id</param>
        /// <returns>employee retrieved</returns>
        public EmployeeViewModel GetById(int employeeID)
        {
            return _employeeRepository.GetById(employeeID);
        }

        /// <summary>
        /// list of all active employees
        /// </summary>
        /// <returns>ilist of employee viewmodels</returns>
        public IList<EmployeeViewModel> GetAllActive()
        {
            return _employeeRepository.GetAll().Where(x => x.IsActive == true).ToList();
        }

        /// <summary>
        /// list of all employees
        /// </summary>
        /// <returns>ilist of employee viewmodels</returns>
        public IList<EmployeeViewModel> GetAll()
        {
            return _employeeRepository.GetAll().ToList();
        }

    }
}
using SignIn.Repositories;
using SignIn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Services
{
    public class EmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeViewModel GetByName(string employeeName)
        {
            return _employeeRepository.GetByName(employeeName);
        }

        public EmployeeViewModel SaveEmployee(EmployeeViewModel employee)
        {
            return _employeeRepository.SaveEmployee(employee);
        }

        public EmployeeViewModel DisableEmployee(int employeeId)
        {
            return _employeeRepository.DisableEmployee(employeeId);
        }

        public EmployeeViewModel GetById(int employeeID)
        {
            return _employeeRepository.GetById(employeeID);
        }

        public IList<EmployeeViewModel> GetAllActive()
        {
            return _employeeRepository.GetAll().Where(x => x.IsActive == true).ToList();
        }

        public IList<EmployeeViewModel> GetAll()
        {
            return _employeeRepository.GetAll().ToList();
        }

    }
}
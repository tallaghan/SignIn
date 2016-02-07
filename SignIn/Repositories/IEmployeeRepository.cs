using SignIn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignIn.Repositories
{
    /// <summary>
    /// Interface of employee repository
    /// </summary>
    public interface IEmployeeRepository
    {
        IList<EmployeeViewModel> GetAll();
        EmployeeViewModel GetById(int employeeID);
        EmployeeViewModel GetByName(string employeeName);
        EmployeeViewModel SaveEmployee(EmployeeViewModel emp);
        EmployeeViewModel DisableEmployee(int employeeId);
    }
}

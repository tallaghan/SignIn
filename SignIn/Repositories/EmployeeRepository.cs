using SignIn.Models;
using SignIn.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SignInEntities db = new SignInEntities();

        public EmployeeViewModel GetById(int employeeID)
        {
            Employee emp =  db.Employees.Where(x => x.ID == employeeID).FirstOrDefault();
            
            return Map(emp);             
        }

        public EmployeeViewModel GetByName(string employeeName)
        {
            int employeeID = db.Employees.Where(x => x.Username.ToLower() == employeeName.ToLower()).FirstOrDefault().ID;

            return GetById(employeeID);
        }

        private bool IsSigningToday(Signing thisSigning)
        {
            string dateFormat = "yyyy-MM-dd";

            return thisSigning.SignedOn.ToString(dateFormat).Equals(DateTime.Now.ToString(dateFormat));

        }

        private EmployeeViewModel Map(Employee emp)
        {
            Signing lastSign = emp.Signings.OrderByDescending(x => x.SignedOn).FirstOrDefault();

            DateTime? lastSigningDate = null;
            bool? lastSigningWasIn = false;
            bool? lastSigningWasToday = false;

            if (lastSign != null)
            {
                lastSigningDate = lastSign.SignedOn;
                lastSigningWasIn = lastSign.SignIn;
                lastSigningWasToday = IsSigningToday(lastSign);
            };

            EmployeeViewModel retValue;

            if (emp == null)
            {
                retValue = null;
            }
            else
            {
                retValue = new EmployeeViewModel()
                {
                    EmployeeID = emp.ID,
                    IsActive = emp.IsActive,
                    Username = emp.Username,
                    FullName = emp.FullName,
                    LastSigning = lastSigningDate,
                    LastSigningWasIn = lastSigningWasIn,
                    LastSigningWasToday = lastSigningWasToday
                };
            };

            return retValue; 
        }

        public IList<EmployeeViewModel> GetAll()
        {
            IList<EmployeeViewModel> retValue = new List<EmployeeViewModel>();

            foreach (Employee emp in db.Employees)
            {
                retValue.Add(Map(emp));
            }
            return retValue;

        }
        public EmployeeViewModel DisableEmployee(int employeeId)
        {
            Employee emp = db.Employees.Where(x => x.ID == employeeId).FirstOrDefault();

            emp.IsActive = false;

            db.SaveChanges();

            return Map(emp);
        }
        public EmployeeViewModel SaveEmployee(EmployeeViewModel employee)
        {
            Employee emp;

            if (employee.EmployeeID == 0)
            {
                 emp = new Employee() { Username = employee.Username,
                                        FullName = employee.FullName,
                                        IsActive = employee.IsActive};

                db.Employees.Add(emp);

            }
            else
            {
                
                emp = db.Employees.Where(x => x.ID == employee.EmployeeID).FirstOrDefault();

                emp.Username = employee.Username;
                emp.FullName = employee.FullName;
                emp.IsActive = employee.IsActive;

            }

            db.SaveChanges();

            return Map(emp);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignIn.ViewModels;

namespace SignIn.Hubs
{
    public class RealtimeHub : Hub
    {
        public void SigningCreated(EmployeeViewModel emp)
        {
            Clients.Others.signingCreated(emp);
        }

        public void EmployeeAdded(EmployeeViewModel emp)
        {
            Clients.Others.employeeAdded(emp);
        }

        public void EmployeeUpdated(EmployeeViewModel emp)
        {
            Clients.Others.employeeUpdated(emp);
        }
    }
}
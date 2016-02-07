using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignIn.ViewModels;

namespace SignIn.Hubs
{
    /// <summary>
    /// signalR hub
    /// </summary>
    public class RealtimeHub : Hub
    {
        /// <summary>
        /// Publish to all other clients that a signing has occurred
        /// </summary>
        /// <param name="emp">employee that has signed in</param>
        public void SigningCreated(EmployeeViewModel emp)
        {
            Clients.Others.signingCreated(emp);
        }

        /// <summary>
        /// Publish to all other clients that an employee has been added
        /// </summary>
        /// <param name="emp">employee that has been added</param>
        public void EmployeeAdded(EmployeeViewModel emp)
        {
            Clients.Others.employeeAdded(emp);
        }

        /// <summary>
        /// Publish to all other clients that an employee has been updated
        /// </summary>
        /// <param name="emp">employee that has been updated</param>
        public void EmployeeUpdated(EmployeeViewModel emp)
        {
            Clients.Others.employeeUpdated(emp);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime? LastSigning {get; set; }
        public bool? LastSigningWasIn { get; set; }
        public bool? LastSigningWasToday { get; set; }
    }
}
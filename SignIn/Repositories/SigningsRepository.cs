using SignIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Repositories
{
    public class SigningsRepository : ISigningsRepository
    {
        private SignInEntities db = new SignInEntities();

        public void SignInOrOut(int employeeID, bool signIn)
        {
            db.Signings.Add(new Signing { EmployeeID = employeeID, SignIn = signIn, SignedOn = DateTime.Now });
            db.SaveChanges();
        }
        
        public IQueryable<Signing> GetAll()
        {
            return db.Signings.AsQueryable();
        }
    }
}
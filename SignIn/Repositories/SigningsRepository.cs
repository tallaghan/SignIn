using SignIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Repositories
{
    /// <summary>
    /// Signings Repository
    /// </summary>
    public class SigningsRepository : ISigningsRepository
    {
        private SignInEntities db = new SignInEntities();

        /// <summary>
        /// Sign in or out
        /// </summary>
        /// <param name="employeeID">id of employee to sign in or out</param>
        /// <param name="signIn">true => sign in, false => sign out</param>
        public void SignInOrOut(int employeeID, bool signIn)
        {
            db.Signings.Add(new Signing { EmployeeID = employeeID, SignIn = signIn, SignedOn = DateTime.Now });
            db.SaveChanges();
        }
        
        /// <summary>
        /// get all signings
        /// </summary>
        /// <returns>iqueryable of signing objects</returns>
        public IQueryable<Signing> GetAll()
        {
            return db.Signings.AsQueryable();
        }
    }
}
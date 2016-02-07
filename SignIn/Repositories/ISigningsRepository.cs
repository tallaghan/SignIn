using SignIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignIn.Repositories
{
    /// <summary>
    /// interface of signings repository
    /// </summary>
    public interface ISigningsRepository
    {
        IQueryable<Signing> GetAll();
        void SignInOrOut(int employeeID, bool signIn);
    }
}

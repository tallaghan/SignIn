using SignIn.Models;
using SignIn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Services
{
    /// <summary>
    /// service the app for signing tasks
    /// </summary>
    public class SigningsService
    {
        private ISigningsRepository _signingsRepository;

        /// <summary>
        /// class ctor
        /// </summary>
        /// <param name="signingsRepository">signing repository to use, populated from unity IOC</param>
        public SigningsService(ISigningsRepository signingsRepository)
        {
            _signingsRepository = signingsRepository;
        }

        /// <summary>
        /// is signing pertaining to today
        /// </summary>
        /// <param name="thisSigning"></param>
        /// <returns></returns>
        public bool IsSigningToday(Signing thisSigning)
        {
            string dateFormat = "yyyy-MM-dd";

            return thisSigning.SignedOn.ToString(dateFormat).Equals(DateTime.Now.ToString(dateFormat));

        }

        /// <summary>
        /// create a new signing
        /// </summary>
        /// <param name="employeeID">id of employee to create signing for</param>
        public void CreateSigning(int employeeID)
        {
            bool signedIn = IsSignedIn(employeeID);

            _signingsRepository.SignInOrOut(employeeID, !signedIn);
        }

        /// <summary>
        /// is employee signed in
        /// </summary>
        /// <param name="employeeID">employee id to check</param>
        /// <returns>true/false depending on whether employee signed in or not</returns>
        public bool IsSignedIn(int employeeID)
        {
            Signing lastSigning = _signingsRepository.GetAll().Where(x => x.EmployeeID == employeeID).OrderByDescending(y => y.ID).FirstOrDefault();

            // if the last signing was IN but was on a previous day then the user is taken as being signed out
            if (lastSigning == null){
                return false;
            }else {
                if (lastSigning.SignIn == true)
                {
                    bool previousDay = !IsSigningToday(lastSigning);

                    if (previousDay == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
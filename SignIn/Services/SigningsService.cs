using SignIn.Models;
using SignIn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignIn.Services
{
    public class SigningsService
    {
        private ISigningsRepository _signingsRepository;

        public SigningsService(ISigningsRepository signingsRepository)
        {
            _signingsRepository = signingsRepository;
        }

        public bool IsSigningToday(Signing thisSigning)
        {
            string dateFormat = "yyyy-MM-dd";

            return thisSigning.SignedOn.ToString(dateFormat).Equals(DateTime.Now.ToString(dateFormat));

        }
        public void CreateSigning(int employeeID)
        {
            bool signedIn = IsSignedIn(employeeID);

            _signingsRepository.SignInOrOut(employeeID, !signedIn);
        }

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
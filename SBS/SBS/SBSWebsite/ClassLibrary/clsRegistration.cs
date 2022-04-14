using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class clsRegistration
    {
        DataAccessService DAS = new DataAccessService();

        public DataTable MemberRegistration(string FirstName, string LastName, string Mobile, string Email, string Gender, string City, string Country, string ReferralCode)
        {
            DataTable DT = DAS.NewGetDataTable("EXEC sp_MemberRegistration @FirstName='" + FirstName + "',@LastName ='" + LastName + "',@Mobile ='" + Mobile + "',@Email ='" + Email + "',@Gender =" + Gender + ",@City ='" + City + "',@Country ='" + Country + "',@MemberReferralCode='" + ReferralCode + "'");
            return DT;
        }

        public DataTable MemberLogin(string UserName, string Password)
        {
            DataTable DT = DAS.NewGetDataTable("EXEC sp_MemberLogin @UserName='" + UserName + "',@Password ='" + Password + "'");
            return DT;
        }
    }
}

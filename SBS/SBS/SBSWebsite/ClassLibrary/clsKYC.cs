using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class clsKYC
    {
        DataAccessService DAS = new DataAccessService();

        public DataTable GetMemberKYCDetail(string Criteria)
        {
            DataTable DT = DAS.SelectDataTable("EXEC SBS_GetMemberKYCDetail @Criteria='" + Criteria + "'");
            return DT;
        }

        public DataTable saveMemberKYCDetails(string MemberID, string AadharCard, string PanCard, string PassportPhoto, string AadharCardBack, string PanCardBack)
        {
            DataTable DT = DAS.NewGetDataTable("EXEC SBS_saveMemberKYCDetails @MemberID='" + MemberID + "',@AadharCard='" + AadharCard + "',@PanCard='" + PanCard + "',@PassportPhoto='" + PassportPhoto + "',@AadharCardBack='" + AadharCardBack + "',@PanCardBack='" + PanCardBack + "'");
            return DT;
        }
    }
}

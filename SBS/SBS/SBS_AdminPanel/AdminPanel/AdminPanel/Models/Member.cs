using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.Models
{
    public class Member
    {
        public Int64 sNo { get; set; }
        public Int64 ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string SBSMemberID { get; set; }
        public string SBSPassword { get; set; }
        public string IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiyDate { get; set; }
        public string referralCode { get; set; }
        public int referralCount { get; set; }
        public string IsKYCFilled { get; set; }

        public Int64 KYC_ID { get; set; }
        public string KYC_MemberID { get; set; }
        public string KYC_AadharCard { get; set; }
        public string KYC_AadharCard_back { get; set; }
        public string KYC_PanCard { get; set; }
        public string KYC_PanCard_back { get; set; }
        public string PassportPhoto { get; set; }
        public bool Verified { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime KYC_CreationDate { get; set; }
        public DateTime KYC_ModifiyDate { get; set; }
    }


}
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminPanel.DataModel
{
    using System;
    
    public partial class SBS_Verifiy_KYC_Documents_Result
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string SBSMemberID { get; set; }
        public string SBSPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModifiyDate { get; set; }
        public string referralCode { get; set; }
        public Nullable<int> referralCount { get; set; }
        public Nullable<long> referralBY { get; set; }
    }
}

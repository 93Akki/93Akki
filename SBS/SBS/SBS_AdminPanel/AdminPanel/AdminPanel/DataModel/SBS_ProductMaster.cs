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
    using System.Collections.Generic;
    
    public partial class SBS_ProductMaster
    {
        public long PID { get; set; }
        public string PName { get; set; }
        public Nullable<decimal> marketRate { get; set; }
        public Nullable<decimal> sbsRate { get; set; }
        public string imgPath { get; set; }
        public Nullable<long> catID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsAddToCart { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModifiyDate { get; set; }
        public string ProductDescription { get; set; }
        public string DocPath { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public string QtyType { get; set; }
    }
}

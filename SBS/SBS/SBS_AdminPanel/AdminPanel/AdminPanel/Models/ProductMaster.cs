using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.Models
{
    public class ProductMaster
    {
        public Int64 sNo { get; set; }
        public Int64 catID { get; set; }
        public string catName { get; set; }
        public Int64 PID { get; set; }
        public string PName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAddToCart { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiyDate { get; set; }
    }

    public class AddProduct
    {
        public Int64 catID { get; set; }
        public string PName { get; set; }
        public decimal marketRate { get; set; }
        public decimal sbsRate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAddToCart { get; set; }
        public string ProductDescription { get; set; }
        public Decimal Qty { get; set; }
        public string QtyType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiyDate { get; set; }
    }
}
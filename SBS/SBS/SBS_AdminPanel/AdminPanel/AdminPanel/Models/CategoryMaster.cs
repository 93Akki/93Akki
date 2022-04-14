using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.Models
{
    public class CategoryMaster
    {
        public Int64 catID { get; set; }
        public string CatName { get; set; }
        public string IsActive { get; set; }
    }
}
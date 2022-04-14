using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel.Models
{
    public class EmployeeSave
    {
        public Int64 EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string DOB { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiyDate { get; set; }
        public string Department { get; set; }
    }
}
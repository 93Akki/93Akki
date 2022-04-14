using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Models
{
    public class Employee
    {
        public Int64 EmployeeID { get; set; }
        public Int64 Sno { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Department { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiyDate { get; set; }
    }
}
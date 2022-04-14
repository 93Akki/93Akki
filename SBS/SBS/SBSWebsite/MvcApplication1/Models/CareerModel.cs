using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CareerModel
    {
        #region[declaration]
        private string firstName;
        private string lastName;
        private string mobile;
        private string email;
        #endregion



        #region[properties]
        public string _FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string _LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string _Email
        {
            get { return email; }
            set { email = value; }
        }
        public string _Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        #endregion

    }
}
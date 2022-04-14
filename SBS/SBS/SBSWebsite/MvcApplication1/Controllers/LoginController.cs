using ClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult MemberLogin(string UserName, string Password)
        {
            string res = "";
            try
            {
                DataTable DT = new DataTable();
                clsRegistration objRegistration = new clsRegistration();
                DT = objRegistration.MemberLogin(UserName, Password);
                if (DT.Rows.Count > 0)
                {
                    Session["UserName"] = DT.Rows[0]["FirstName"].ToString() + " " + DT.Rows[0]["LastName"].ToString();
                    res = "S|" + JsonConvert.SerializeObject(DT);
                }
                else
                    res = "E|Invalid User Name Or Password.";
            }
            catch (Exception ex)
            {
                res = "E|" + ex.Message.ToString();
            }

            return Json(new { success = true, data = res }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SingOut()
        {
            string res = "";
            try
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                res = "S|Logout";
            }
            catch (Exception ex)
            {
                res = "E|" + ex.Message.ToString();
            }

            return Json(new { success = true, data = res }, JsonRequestBehavior.AllowGet);
        }

    }
}

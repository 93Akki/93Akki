using AdminPanel.DataModel;
using AdminPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
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
        [ValidateAntiForgeryToken]
        public ActionResult SBSLogin(Login objUser)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var obj = SBSEntitie.SBS_Login.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        //Update last login detail
                        var query = from data in SBSEntitie.SBS_Login orderby data.UserId select data;
                        foreach (SBS_Login details in query)
                        {
                            if (details.UserId == obj.UserId)
                            {
                                //Assign the new values to name whose id is 1  
                                details.LastLogin = DateTime.Now;
                            }
                        }
                        //Save the changes back to the database.  
                        SBSEntitie.SaveChanges();

                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid email/mobile number or password.";
                        return RedirectToAction("Index", "Login");
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}

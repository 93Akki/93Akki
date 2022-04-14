using AdminPanel.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            try
            {
                var Parameter = new SqlParameter("@Criteria", "");
                var result = SBSEntitie.Database.SqlQuery<Dashboard>("SBS_GetDashboardData @Criteria", Parameter).ToList();
                return View("Dashboard", result);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult signOut()
        {
            return RedirectToAction("Index", "Login");
        }

    }
}

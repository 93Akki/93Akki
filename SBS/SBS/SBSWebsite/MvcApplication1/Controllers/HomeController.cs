using ClassLibrary;
using System;
using System.Data;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            try
            {
                clsHome ObjHome = new clsHome();
                DataSet DT1 = ObjHome.GetProducts("", "Product");
                ViewData["DTProducts"] = DT1.Tables[0];
            }
            catch (Exception ex)
            {

            }
            return View("Home");
        }


    }
}

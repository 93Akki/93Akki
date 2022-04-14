using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ProductDetailController : BaseController
    {
        //
        // GET: /ProductDetail/

        public ActionResult Index(Int32 id)
        {
            try
            {
                clsHome ObjHome = new clsHome();
                DataSet DS = ObjHome.GetProducts(" AND PID =" + id + "", "ProductDetail");
                ViewData["ProductDetail"] = DS;
            }
            catch (Exception)
            {

                throw;
            }
            return View("ProductDetail");
        }


        [HttpPost]
        public ActionResult AddToCartItems(string PID, string quantity)
        {
            string message = "";
            try
            {
                clsHome ObjHome = new clsHome();
                DataTable DT = ObjHome.SaveAddToCartDetails(PID, quantity);
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

    }
}

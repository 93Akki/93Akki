using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ProductsController : BaseController
    {
        //
        // GET: /Products/

        public ActionResult Index(Int32 id)
        {
            try
            {
                // var justValue = this.ControllerContext.RouteData.Values["value"]; Get Paramter value from Route

                clsHome ObjHome = new clsHome();
                DataSet DT = ObjHome.GetProducts(" AND SBS_ProductMaster.catID =" + id + "", "Product");
                ViewData["DTProducts"] = DT.Tables[0];
                ViewData["DTCategoryNew"] = ViewData["DTCategory"];
            }
            catch (Exception)
            {
                throw;
            }
            return View("Products");
        }

    }
}

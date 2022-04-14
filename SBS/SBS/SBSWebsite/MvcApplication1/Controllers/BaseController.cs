using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            clsHome ObjHome = new clsHome();
            DataTable DT = ObjHome.GetProductCategory("");
            ViewData["DTCategory"] = DT;
        }

    }
}

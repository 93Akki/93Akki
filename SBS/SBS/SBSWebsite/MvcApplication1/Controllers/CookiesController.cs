using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CookiesController : BaseController
    {
        //
        // GET: /Cookies/

        public ActionResult Index()
        {
            return View("Cookies");
        }

    }
}

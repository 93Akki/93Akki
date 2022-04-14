using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AboutController : BaseController
    {
        //
        // GET: /About/

        public ActionResult Index()
        {
            return View("About");
        }

    }
}

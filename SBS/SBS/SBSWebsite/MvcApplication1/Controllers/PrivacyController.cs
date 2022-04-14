using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class PrivacyController : BaseController
    {
        //
        // GET: /Privacy/

        public ActionResult Index()
        {
            return View("Privacy");
        }

    }
}

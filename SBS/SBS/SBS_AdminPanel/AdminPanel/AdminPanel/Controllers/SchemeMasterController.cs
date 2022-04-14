using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class SchemeMasterController : Controller
    {
        //
        // GET: /SchemeMaster/

        public ActionResult Index()
        {
            return View("SchemeMaster");
        }

    }
}

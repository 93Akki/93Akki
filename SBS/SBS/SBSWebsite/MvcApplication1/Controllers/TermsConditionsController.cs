using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class TermsConditionsController : BaseController
    {
        //
        // GET: /TermsConditions/

        public ActionResult Index()
        {
            return View("TermsConditions");
        }

    }
}

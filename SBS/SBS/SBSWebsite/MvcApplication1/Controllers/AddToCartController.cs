using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AddToCartController : BaseController
    {
        //
        // GET: /AddToCart/

        public ActionResult Index()
        {
            return View("AddToCart");
        }

    }
}

using AdminPanel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminPanel.Controllers
{
    public class BaseController : Controller
    {
        public sbsjodhpurEntities SBSEntitie = new sbsjodhpurEntities();     

    }
}

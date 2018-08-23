using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;

namespace CPQaunt2.APP.Controllers
{
    public class TestController : TopControllerBase
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}
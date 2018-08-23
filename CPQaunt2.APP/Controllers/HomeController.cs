using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC;
using Winner.Framework.MVC.Controllers;
using Winner.Platform.MVC;

namespace CPQaunt2.APP.Controllers
{
    public class HomeController : TopControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [AuthRight(true)]
        public ActionResult Right()
        {
            return View();
        }

        /// <summary>
        /// 刷新权限
        /// </summary>
        /// <returns></returns>
        [AuthRight(true)]
        public JsonResult RefreshRight()
        {
            Session.Remove(RightConfig.SystemRightsSessionKey);
            return SuccessResult();
        }

        [AuthRight(true)]
        public ActionResult Logout()
        {
            Session.Clear();
            base.Logout();
            return View();
        }
    }
}
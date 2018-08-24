using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;
using Winner.Platform.MVC;

namespace CPQaunt2.APP.Controllers
{
    public class HomeController : TopControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        
         public ActionResult ApiDoc()
        {
            return View();
        }
        #region 注释

        /*
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
        */
        #endregion

        [HttpPost]
        public ActionResult Select()
        {
            CPQaunt.DataAccess.Tcp_ClscriptCollection clscriptCollection = new CPQaunt.DataAccess.Tcp_ClscriptCollection();
            if (clscriptCollection.ListAll())
            {
                var list = MapProvider.Map<DataScriptModel>(clscriptCollection.DataTable);
                return SuccessResultList(list, clscriptCollection.ChangePage);

            }
            return FailResult("查询失败！");
        }

        [HttpPost]
        public ActionResult Delete()
        {
            int lid = int.Parse(Request.Form["lid"]);
            CPQaunt.DataAccess.Tcp_Clscript tcp = new CPQaunt.DataAccess.Tcp_Clscript();

            if (tcp.Delete(lid))
            {
                return SuccessResult();
            }

            return FailResult();
        }

    }
}
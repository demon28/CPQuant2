using CPQaunt.DataAccess;
using CPQaunt.Entitest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Winner.Framework.MVC.Controllers;

namespace CPQaunt2.APP.Controllers
{
    public class AddController : TopControllerBase
    {
        // GET: Add
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Insert()
        {
            Tcp_Clscript tcp = new Tcp_Clscript();
            tcp.Name = Request.Form["name"];
            tcp.Content = Request.Form["content"];
            tcp.Remark = Request.Form["remark"];

            if (tcp.Insert())
            {
                return SuccessResult();
            }
            return FailResult();
        }

        [HttpPost]
        public ActionResult Build()
        {
            string scriptcode = Request.Form["content"];

            string expect = "20180824042";
            if (!string.IsNullOrEmpty(Request.Form["expect"]))
            {
                 expect = Request.Form["expect"];
            }
       

            CPQaunt.Facade.BuilderScriptFacade builder = new CPQaunt.Facade.BuilderScriptFacade();

            MessageScriptModel message = builder.Builder(expect, scriptcode);

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;

            string json = jsSerializer.Serialize(message);

            if (message.status)
            {
                return   SuccessResult(json);
            }
            

            return FailResult(json);
        }

    }
}
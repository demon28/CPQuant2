using CPQaunt.DataAccess;
using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;

namespace CPQaunt2.APP.Controllers
{
    public class CreateController : TopControllerBase
    {
        // GET: Create
        public ActionResult Index()
        {
            return View();
        }


        

        [HttpPost]
        public ActionResult GetQiHao()
        {
            Tcp_Hiscode tcp = new Tcp_Hiscode();

            if (!tcp.GetLastSingle())
            {
                return FailResult("获取失败");
            }
            

            return SuccessResult(JsonProvider.ToJson(tcp.DataRow));
        }




        [HttpPost]
        public ActionResult Create()
        {
            int cid = int.Parse(Request.Form["cid"]);
            int lid = int.Parse(Request.Form["lid"]);

            Tcp_Clscript tcp = new Tcp_Clscript();

            if (!tcp.SelectByPK(lid))
            {
                return FailResult("获取策略脚本失败！");
            }
            CPQaunt.Facade.BuilderScriptFacade builder = new CPQaunt.Facade.BuilderScriptFacade();

            MessageScriptModel message = builder.Builder(cid, tcp.Content);  //得到下注数字集合

            if (message.type == MessageType.Log)
            {
                return FailResult(message.message);
            }

            StringBuilder stringBuilder = new StringBuilder();


            foreach (var item in message.numbers)
            {
                string str = item.N1.ToString() + item.N2.ToString() + item.N3.ToString() + item.N4.ToString() + item.N5.ToString() + ",";
                stringBuilder.AppendLine(str);
            }


            return SuccessResult(stringBuilder.ToString());

        }
    }
}
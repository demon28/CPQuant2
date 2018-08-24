using CPQaunt.DataAccess;
using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Winner.Framework.MVC.Controllers;
using Winner.Framework.Utils;

namespace CPQaunt2.APP.Controllers
{
    public class TestController : TopControllerBase
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Load()
        {
            int lid = int.Parse(Request.Form["lid"]);

            Tcp_Clscript tcp = new Tcp_Clscript();

            if (tcp.SelectByPK(lid))
            {
                var model = MapProvider.Map<DataScriptModel>(tcp.DataRow);
                return JsonResult(model);
            }

            return FailResult("查询失败！");

        }

        [HttpPost]
        public ActionResult GetHis()
        {
            DateTime stardate = DateTime.Parse(Request.Form["stardate"]);
            DateTime enddate = DateTime.Parse(Request.Form["enddate"]).AddDays(1);

            Tcp_HiscodeCollection tcp = new Tcp_HiscodeCollection();

            if (tcp.ListByDate(stardate, enddate))
            {
                if (tcp.DataTable.Rows.Count==0)
                {
                    return FailResult("没有回测数据！");
                }

                var list = MapProvider.Map<DataHisCodeModel>(tcp.DataTable);
                return SuccessResultList(list, tcp.ChangePage);
            }
            return FailResult("获取历史数据失败");

        }



        [HttpPost]
        public ActionResult HuiCe()
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

            if (message.type==MessageType.Log)
            {
                return FailResult(message.message);
            }

            Tcp_Hiscode tcphis = new Tcp_Hiscode();

            if (tcphis.SelectByPK(cid))
            {
                string[] sl = tcphis.Opencode.Split(',');
                NumberModel number = new NumberModel();
                number.N1 = int.Parse(sl[0]);
                number.N2 = int.Parse(sl[1]);
                number.N3 = int.Parse(sl[2]);
                number.N4 = int.Parse(sl[3]);
                number.N5 = int.Parse(sl[4]);

                foreach (var item in message.numbers)
                {
                    if (item.N1 == number.N1 && item.N2 == number.N2 && item.N3 == number.N3 && item.N4 == number.N4 && item.N5 == number.N5)
                    {
                        return SuccessResult(message.numbers.Count);
                    }
                }
                return SuccessResult("0");
            }
            return FailResult("回测失败!");



        }

    }
}
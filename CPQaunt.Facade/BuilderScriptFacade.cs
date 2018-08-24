using CPQaunt.Entitest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace CPQaunt.Facade
{
    public class BuilderScriptFacade
    {

        /// <summary>
        /// 编译传递的JavaScript 代码
        /// </summary>
        /// <param name="cid">默认需求本期开奖id，回测系统使用</param>
        /// <param name="code">编译代码</param>
        /// <returns></returns>
        public MessageScriptModel Builder(string expect, string code) {

            StringBuilder stringBuilder = new StringBuilder();

            string system = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Js/CPQaunt.js"));
            string jsonparese = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Js/json_parse.js"));
            string json2 = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Js/json2.js"));

            stringBuilder.AppendLine(jsonparese);
            stringBuilder.AppendLine(json2);

            stringBuilder.AppendLine(system);
            stringBuilder.AppendLine(code);

            try
            {

                using (Microsoft.ClearScript.ScriptEngine engine = new Microsoft.ClearScript.V8.V8ScriptEngine())
                {
                    engine.AddHostObject("LotteryFacade", new CPQaunt.Facade.LotteryFacade());
                    engine.Execute(stringBuilder.ToString());

                    var s = engine.Script.main(expect);

                    MessageScriptModel message = new MessageScriptModel();
                    message.type = MessageType.List;
                    message.status = true;

                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    jsSerializer.MaxJsonLength = Int32.MaxValue;

                    message.numbers = jsSerializer.Deserialize<List<NumberModel>>(s);

                    return message;
                }

            }
            catch (Exception e)
            {

                MessageScriptModel message = new MessageScriptModel();
                message.type = MessageType.Log;
                message.status = false;
                message.message = e.Message;
                return message;
            }




        }



    }
}

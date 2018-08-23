using CPQaunt.DataAccess;
using CPQaunt.Entitest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPQaunt.Facade
{
    /// <summary>
    /// 向前端公开给JavaScript直接调用的方法
    /// </summary>
    public class LotteryFacade
    {

        /// <summary>
        /// 公开JavaScript使用
        /// </summary>
        /// <returns></returns>
        public string GetAllForUser() {


            MessageScriptModel message = new MessageScriptModel();
            message.type = MessageType.List;
            message.numbers = GetALL();
            message.status = true;
            return JsonConvert.SerializeObject(message);

        }

        /// <summary>
        /// 获得上期数据 JavaScript 使用
        /// </summary>
        /// <returns></returns>
        public string GetAllForUser(int cid, int count)
        {
            MessageScriptModel message = new MessageScriptModel();
            message.type = MessageType.List;
            message.numbers = GetLast(cid, count);
            message.status = true;

            return JsonConvert.SerializeObject(message);
          
        }

        /// <summary>
        /// 获得上期数据 底层使用
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<NumberModel> GetLast(int cid,int count)
        {
            List<NumberModel> numbers = new List<NumberModel>();

            Tcp_HiscodeCollection hiscodeCollection = new Tcp_HiscodeCollection();
            if (!hiscodeCollection.ListByLast(cid,count))
            {
                return numbers;
            }

            for(int i=0;i< hiscodeCollection.DataTable.Rows.Count; i++)
            {
                NumberModel number = new NumberModel();
                string[] sl =  hiscodeCollection.DataTable.Rows[i]["opencode"].ToString().Split(',');
                number.N1 = int.Parse(sl[0]);
                number.N2 = int.Parse(sl[1]);
                number.N3 = int.Parse(sl[2]);
                number.N4 = int.Parse(sl[3]);
                number.N5 = int.Parse(sl[4]);
                numbers.Add(number);

            }
            return numbers;

        }




        /// <summary>
        /// 系统底层使用
        /// </summary>
        /// <returns></returns>
        public List<NumberModel> GetALL()
        {
            List<NumberModel> numbers = new List<NumberModel>();

            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    for (int k = 0; k < 10; k++)
                    {

                        for (int l = 0; l < 10; l++)
                        {

                            for (int z = 0; z < 10; z++)
                            {
                                NumberModel number = new NumberModel();
                                number.N1 = i;
                                number.N2 = j;
                                number.N3 = k;
                                number.N4 = l;
                                number.N5 = z;

                                numbers.Add(number);
                            }
                        }
                    }
                }
            }
            return numbers;
        }




    }
}

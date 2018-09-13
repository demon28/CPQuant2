using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpQuant.CkEditor
{
    public class KillTwo
    {

        static int count = 0;
        static int winnercount = 0;
        static int betscount = 0;
        static int failcount = 0;
        static decimal xiazhuamount = 0;
        static decimal yingliamount = 0;
        static int listcount = 0;
        static string iswinner = "没中";

        public static void Kill()
        {
            DateTime stardate = DateTime.Today.AddDays(-10);
            CPQaunt.DataAccess.Tcp_HiscodeCollection collection = new CPQaunt.DataAccess.Tcp_HiscodeCollection();

            collection.ListByDate(stardate, DateTime.Today.AddDays(1));

            for (int i = 0; i < collection.DataTable.Rows.Count; i++)
            {

                int ik = i;
                if (ik-1>0)
                {
                    ik = ik - 1;
                }

                NumberModel number0 = new NumberModel();
                string[] sl0 = collection.DataTable.Rows[ik]["opencode"].ToString().Split(',');
                number0.N1 = int.Parse(sl0[0]);
                number0.N2 = int.Parse(sl0[1]);
                number0.N3 = int.Parse(sl0[2]);
                number0.N4 = int.Parse(sl0[3]);
                number0.N5 = int.Parse(sl0[4]);


                NumberModel number = new NumberModel();
                string[] sl = collection.DataTable.Rows[i]["opencode"].ToString().Split(',');
                number.N1 = int.Parse(sl[0]);
                number.N2 = int.Parse(sl[1]);
                number.N3 = int.Parse(sl[2]);
                number.N4 = int.Parse(sl[3]);
                number.N5 = int.Parse(sl[4]);

                string qihao = collection.DataTable.Rows[i]["expect"].ToString();


                int kill1 = number0.N5;

                string str = (number0.N1 + number0.N2 + number0.N3 + number0.N4 + number0.N5).ToString();

                 int kill2 = int.Parse(str.Substring(str.Length - 1, 1));

            
                 iswinner = "没中奖";


                if (kill1 == kill2)
                {
                    kill2 = int.Parse(qihao.Substring(qihao.Length - 1, 1)) + 1;
                }



                if (number.N1 != kill1 && number.N2 != kill1 && number.N3 != kill1 && number.N4 != kill1 && number.N5 != kill1)
                {
                    if (number.N1 != kill2 && number.N2 != kill2 && number.N3 != kill2 && number.N4 != kill2 && number.N5 != kill2)
                    {
                        winnercount = winnercount + 1;

                        yingliamount = yingliamount + 950;
                        iswinner = "中奖了";
                    }
                    else {
                        failcount = failcount + 1;
                    }
                }
                else {
                    failcount = failcount + 1;
                }


              

              
                xiazhuamount = xiazhuamount + 328;
              


                Console.WriteLine(i+":结果："+iswinner+"----总收益：" + yingliamount + " -----总支出" + xiazhuamount+ "----开奖号码：" + number.GetString() + "----杀号" + kill1 + "," + kill2);


            }


            Console.WriteLine("总下注：" + collection.DataTable.Rows.Count + "-----没中：" + failcount + "---中了" + winnercount + "--中奖率："+((winnercount * 100 / collection.DataTable.Rows.Count ))+"%"+"--盈亏："+(yingliamount- xiazhuamount), ConsoleColor.Green);

        }


    }
}

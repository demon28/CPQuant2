using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpQuant.CkEditor
{
    class Programun
    {

        static int count = 0;
        static int winnercount = 0;
        static int betscount = 0;
        static int failcount = 0;
        static decimal xiazhuamount = 0;
        static decimal yingliamount = 0;
        static int listcount = 0;
      

        public static void Huice()
        {

            DateTime stardate = DateTime.Today.AddDays(-3);

            CPQaunt.DataAccess.Tcp_HiscodeCollection collection = new CPQaunt.DataAccess.Tcp_HiscodeCollection();
            collection.ListByDate(stardate, DateTime.Today.AddDays(1));


            for (int i = 0; i < collection.DataTable.Rows.Count; i++)
            {

                NumberModel number = new NumberModel();
                string[] sl = collection.DataTable.Rows[i]["opencode"].ToString().Split(',');
                number.N1 = int.Parse(sl[0]);
                number.N2 = int.Parse(sl[1]);
                number.N3 = int.Parse(sl[2]);
                number.N4 = int.Parse(sl[3]);
                number.N5 = int.Parse(sl[4]);

                if (number.N1!=3 && number.N2 != 3 && number.N3 != 3 && number.N4 != 3 && number.N5 != 3 )
                {
                    if (number.N1 != 6 && number.N2 != 6 && number.N3 != 6 && number.N4 != 6 && number.N5 != 6)
                    {
                        if (number.N1 != 9 && number.N2 != 9 && number.N3 != 9 && number.N4 != 9 && number.N5 != 9)
                        {
                            winnercount = winnercount + 1;
                            yingliamount = yingliamount + 950;
                        }
                    }
                }

                failcount = failcount + 1;
                xiazhuamount = xiazhuamount + 168;
               Console.WriteLine("总下注：" + collection.DataTable.Rows.Count + "-----没中：" + failcount + "---中了" + winnercount + "-----总收益：" + yingliamount + " -----总下注" + xiazhuamount);
            }




        }
    }
}
using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpQuant.CkEditor
{
    class Program
    {

        static void Main(string[] args)
        {
            int cid = 13089;

            CPQaunt.Facade.LotteryFacade lottery = new CPQaunt.Facade.LotteryFacade();


            List<NumberModel> baselist = lottery.GetALL();

            //获取当期之前的10期数据
            List<NumberModel> lastlist = lottery.GetLast(cid, 10);

            List<NumberModel> mylist = new List<NumberModel>(); ;

            foreach (var item in baselist)
            {

                //去掉5个一模一样的号 约：10个
                if (item.N1 == item.N2 && item.N1 == item.N3 && item.N1 == item.N4 && item.N1 == item.N5)
                {
                    continue;
                }
                //去掉前四个一样的号 约：10个
                if (item.N1 == item.N2 && item.N1 == item.N3 && item.N1 == item.N4)
                {
                    continue;
                }


                NumberModel number = new NumberModel();
                number.N1 = item.N1;
                number.N2 = item.N2;
                number.N3 = item.N3;
                number.N4 = item.N4;
                number.N5 = item.N5;

                mylist.Add(number);

            }

            Console.WriteLine(mylist.Count);
            Console.ReadLine();

        }
    }
}

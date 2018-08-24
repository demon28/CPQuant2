﻿using CPQaunt.Entitest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpQuant.CkEditor
{
    class Program
    {
     
        static int count = 0;
        static int winnercount = 0;
        static int betscount = 0;
        static void Main(string[] args)
        {
           
     



            //回测


            DateTime stardate = DateTime.Today.AddDays(0);


            CPQaunt.DataAccess.Tcp_HiscodeCollection collection = new CPQaunt.DataAccess.Tcp_HiscodeCollection();
            collection.ListByDate(stardate, DateTime.Today.AddDays(1));


            for (int i = 0; i < collection.DataTable.Rows.Count; i++)
            {
                betscount = betscount + 1;

                if (collection.DataTable.Rows[i]["opencode"].ToString()=="2,4,4,9,1")
                {

                }


                List<NumberModel>  mylist = Enter( collection.DataTable.Rows[i]["expect"].ToString());

                NumberModel number = new NumberModel();
                string[] sl= collection.DataTable.Rows[i]["opencode"].ToString().Split(',');
                number.N1 = int.Parse(sl[0]);
                number.N2 = int.Parse(sl[1]);
                number.N3 = int.Parse(sl[2]);
                number.N4 = int.Parse(sl[3]);
                number.N5 = int.Parse(sl[4]);

                string res= collection.DataTable.Rows[i]["expect"] +"-----"+ collection.DataTable.Rows[i]["opencode"] + "---没中啊？";
                foreach (var item in mylist)
                {

                    if (item.N1 == number.N1 && item.N2 == number.N2 && item.N3 == number.N3 && item.N4 == number.N4 && item.N5 == number.N5)
                    {
                        winnercount = winnercount + 1;
                        res=collection.DataTable.Rows[i]["expect"] + "-----" + collection.DataTable.Rows[i]["opencode"] + "---中奖啦！";
                    }
                   
                }
                Console.WriteLine(res);


            }


            //count = 100000 - mylist.Count;
            //Console.WriteLine(count);
          
            Console.ReadLine();

        }



        public static List<NumberModel> Enter(string expect) {
            CPQaunt.Facade.LotteryFacade lottery = new CPQaunt.Facade.LotteryFacade();

            List<NumberModel> baselist = lottery.GetALL();

            //获取当期之前的10期数据
            List<NumberModel> lastlist = lottery.GetLast(expect, 10);


            List<NumberModel> mylist = new List<NumberModel>(); ;

            foreach (var item in baselist)
            {
                if (item.N1==2 && item.N2 == 4 && item.N3==4 && item.N4 == 9 && item.N4 == 1)
                {
                   
                }



                #region 硬性去除
                //去掉5个一模一样的号 00000 约：10个
                if (item.N1 == item.N2 && item.N1 == item.N3 && item.N1 == item.N4 && item.N1 == item.N5)
                {
                    continue;
                }

                //去掉前四个一样的号 00001 约：90个
                if (item.N1 == item.N2 && item.N1 == item.N3 && item.N1 == item.N4)
                {
                    continue;
                }

                //去掉后四个一样的号 10000 约：90个
                if (item.N2 == item.N3 && item.N2 == item.N4 && item.N2 == item.N5)
                {
                    continue;
                }

                //去掉后第二位四个一样的号01000 约：90个
                if (item.N1 == item.N3 && item.N1 == item.N4 && item.N1 == item.N5)
                {
                    continue;
                }

                //去掉后第三位四个一样的号00100 约：90个
                if (item.N1 == item.N2 && item.N1 == item.N4 && item.N1 == item.N5)
                {
                    continue;
                }

                //去掉后第四位四个一样的号00100 约：90个
                if (item.N1 == item.N2 && item.N1 == item.N3 && item.N1 == item.N5)
                {
                    continue;
                }


                //去掉过往10期一样的号 lastnumber 
                if (CheckHisTop(lastlist, item))
                {
                    continue;
                }



                #endregion



                #region 根据过往数据分析

                //如果上一局开的是偶数02468，这一局去掉所有偶数
                if (CheckOdd(lastlist[0]))
                {
                    if (CheckOdd(item))
                    {
                        continue;
                    }
                }
                //如果上一局开的是奇数13579，这一局去掉所有奇数
                if (ChecNotkOdd(lastlist[0]))
                {
                    if (ChecNotkOdd(item))
                    {
                        continue;
                    }
                }


                //如果上一局开的是大数56789，这一局去掉所有大数
                if (CheckBig(lastlist[0]))
                {
                    if (CheckBig(item))
                    {
                        continue;
                    }
                }

                //如果上一局开的是小数数12345，这一局去掉所有小数
                if (CheckSmall(lastlist[0]))
                {
                    if (CheckSmall(item))
                    {
                        continue;
                    }
                }
                #endregion



                mylist.Add(item);

            }
            //Console.WriteLine(mylist.Count);

            return mylist;

        }
        


        /// <summary>
        /// 判断是否与往期数据相同
        /// </summary>
        /// <param name="lastnumbers"></param>
        /// <param name="nownumber"></param>
        /// <returns></returns>
        public static bool CheckHisTop(List<NumberModel> lastnumbers,NumberModel nownumber) {

            foreach (var item in lastnumbers)
            {
                // 与过往10期 一模一样的
                if (item.N1==nownumber.N1 && item.N2 == nownumber.N2 && item.N3 == nownumber.N3 && item.N4 == nownumber.N4 && item.N5 == nownumber.N5)
                {
                    return true;
                }

                //过往10期其余四个一模一样的

                if (item.N2 == nownumber.N2 && item.N3 == nownumber.N3 && item.N4 == nownumber.N4 && item.N5 == nownumber.N5)
                {
                    return true;
                }
                if (item.N1 == nownumber.N1 && item.N3 == nownumber.N3 && item.N4 == nownumber.N4 && item.N5 == nownumber.N5)
                {
                    return true;
                }
                if (item.N1 == nownumber.N1 && item.N2 == nownumber.N2 && item.N4 == nownumber.N4 && item.N5 == nownumber.N5)
                {
                    return true;
                }
                if (item.N1 == nownumber.N1 && item.N2 == nownumber.N2 && item.N3 == nownumber.N3 && item.N4 == nownumber.N4)
                {
                    return true;
                }

                //过往10期前三，中三，后三 一模一样的  （顺序）
                ///1=1,2=2,3=3
                if (item.N1 == nownumber.N1 && item.N2== nownumber.N2 && item.N3 == nownumber.N3 )
                {
                    return true;
                }
                ///2=2,3=3,4=4
                if (item.N2 == nownumber.N2 && item.N3 == nownumber.N3 && item.N4 == nownumber.N4)
                {
                    return true;
                }
                ///3=3,4=4,5=5
                if (item.N3 == nownumber.N3 && item.N4 == nownumber.N4 && item.N5 == nownumber.N5 )
                {
                    return true;
                }


                //号尾 咬 号头  三个一模一样 （顺序）
                ///1=1,4=4,5=5
                if (item.N4 == nownumber.N4 && item.N5 == nownumber.N5 && item.N1 == nownumber.N1)
                {
                    return true;
                }
                ///1=1,2=2,5=5
                if (item.N5 == nownumber.N5 && item.N1 == nownumber.N1 && item.N2 == nownumber.N2)
                {
                    return true;
                }


                //跳位 三个一模一样的  （非顺序）
                ///1=1,2=2,4=4
                if (item.N1 == nownumber.N1 && item.N2 == nownumber.N2 && item.N4 == nownumber.N4)
                {
                    return true;
                }

                ///1=1,3=3,4=4
                if (item.N1 == nownumber.N1 && item.N3 == nownumber.N3 && item.N4 == nownumber.N4)
                {
                    return true;
                }

                ///1=1,3=3,5=5
                if (item.N1 == nownumber.N1 && item.N3 == nownumber.N3 && item.N5 == nownumber.N5)
                {
                    return true;
                }
                ///2=2,3=3,5=5
                if (item.N2 == nownumber.N2 && item.N3 == nownumber.N3 && item.N5 == nownumber.N5)
                {
                    return true;
                }


            }
            return false;
        }

        /// <summary>
        /// 奇偶判断
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsOdd(int n)
        {
            return Convert.ToBoolean(n % 2);
        }

        /// <summary>
        /// 是否全是偶数
        /// </summary>
        /// <param name="lastnumber"></param>
        /// <returns></returns>
        public static bool CheckOdd(NumberModel lastnumber) {

            if (IsOdd(lastnumber.N1) && IsOdd(lastnumber.N2)&& IsOdd(lastnumber.N3) && IsOdd(lastnumber.N4) && IsOdd(lastnumber.N5))
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 是否全是奇数
        /// </summary>
        /// <param name="lastnumber"></param>
        /// <returns></returns>
        public static bool ChecNotkOdd(NumberModel lastnumber)
        {

            if (!IsOdd(lastnumber.N1) && !IsOdd(lastnumber.N2) && !IsOdd(lastnumber.N3) && !IsOdd(lastnumber.N4) && !IsOdd(lastnumber.N5))
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 是否全是小数
        /// </summary>
        /// <param name="lastnumber"></param>
        /// <returns></returns>
        public static bool CheckBig(NumberModel lastnumber) {
            if (lastnumber.N1<5 && lastnumber.N2 < 5 && lastnumber.N3 < 5 && lastnumber.N4 < 5 && lastnumber.N5 < 5 )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否全是大数
        /// </summary>
        /// <param name="lastnumber"></param>
        /// <returns></returns>
        public static bool CheckSmall(NumberModel lastnumber)
        {
            if (lastnumber.N1 >=5 && lastnumber.N2 >= 5 && lastnumber.N3 >= 5 && lastnumber.N4 >= 5 && lastnumber.N5 >= 5)
            {
                return true;
            }
            return false;
        }

    }
}

 /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tcp_Hiscode.generate.cs 
 * CreateTime : 2018-08-23 20:45:45 
 * Version : V 1.1.0
 * E_Mail : 6e9e@163.com
 * Blog : http://www.cnblogs.com/fineblog/
 * Copyright (C) Winner(深圳-乾海盛世)
 * 
 ***************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Winner.Framework.Core;
using Winner.Framework.Core.DataAccess;
using Winner.Framework.Core.DataAccess.Oracle;
using Winner.Framework.Utils;

namespace CPQaunt.DataAccess
{
    /// <summary>
    /// Data Access Layer Object Of Tcp_Hiscode
    /// </summary>
    public partial class Tcp_Hiscode : DataAccessBase
    {
        public bool GetLastSingle() {

            string sql= @"select * from (
            select t.*,rownum from tcp_hiscode t  order by t.datetime desc) s where rownum = 1";

           return SelectBySql(sql);
        }


        public bool SelectByExpect(string expect)
        {

            string sql = "select t.* from tcp_hiscode t where t.expect='" + expect + "'";
            return SelectBySql(sql);

        
        }


        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tcp_Hiscode
    /// </summary>
    public partial class Tcp_HiscodeCollection : DataAccessCollectionBase
    {
        public bool ListByLast(string expect, int count)
        {
            string sql = " SELECT s.* FROM (SELECT t.*,rownum FROM  tcp_hiscode t WHERE t.expect <'"+ expect + "' ORDER BY t.expect desc) s WHERE rownum <="+count+" order by s.datetime desc";

            return ListBySql(sql);


        }

        public bool ListByDate(DateTime star, DateTime end)
        {
            string sql = "SELECT t.* FROM tcp_hiscode t WHERE t.datetime < "+SQLHelper.ToSQLDateTime(end)+"  AND t.datetime >="+ SQLHelper.ToSQLDateTime(star)+ " order by t.datetime desc";

            return ListBySql(sql);


        }



        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
} 
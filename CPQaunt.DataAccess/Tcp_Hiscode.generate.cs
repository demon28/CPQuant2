﻿ /***************************************************
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
       
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tcp_Hiscode
    /// </summary>
    public partial class Tcp_HiscodeCollection : DataAccessCollectionBase
    {
        public bool ListByLast(int cid, int count)
        {
            string sql = "SELECT s.* FROM (SELECT t.*,rownum FROM  tcp_hiscode t WHERE t.cid >" + cid + " ORDER BY t.cid ASC) s WHERE rownum < " + count + " order by s.datetime desc";

            return ListBySql(sql);


        }

        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
} 
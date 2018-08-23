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
        #region 默认构造

        public Tcp_Hiscode() : base() { }

        public Tcp_Hiscode(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _CID="CID";
		public const string _EXPECT="EXPECT";
		public const string _OPENCODE="OPENCODE";
		public const string _DATETIME="DATETIME";
		public const string _REMARK="REMARK";

    
        public const string _TABLENAME="Tcp_Hiscode";
        #endregion 对应表结构的常量属性

        #region 公开属性
        
		/// <summary>
		/// [default:0]
		/// </summary>
		public int Cid
		{
			get { return Convert.ToInt32(DataRow[_CID]); }
			set { DataRow[_CID] = value; }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Expect
		{
			get { return DataRow[_EXPECT].ToString(); }
			set { DataRow[_EXPECT] = value; }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Opencode
		{
			get { return DataRow[_OPENCODE].ToString(); }
			set { DataRow[_OPENCODE] = value; }
		}
		/// <summary>
		/// [default:DBNull.Value]
		/// </summary>
		public DateTime? Datetime
		{
			get { return Helper.ToDateTime(DataRow[_DATETIME]); }
			set { DataRow[_DATETIME] = Helper.FromDateTime(value); }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Remark
		{
			get { return DataRow[_REMARK].ToString(); }
			set { DataRow[_REMARK] = value; }
		}

        #endregion 公开属性
        
        #region 私有成员
        
        protected override string TableName
        {
            get { return _TABLENAME; }
        }

        protected override DataRow BuildRow()
        {
            DataTable dt = new DataTable(_TABLENAME);
			dt.Columns.Add(_CID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_EXPECT, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_OPENCODE, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_DATETIME, typeof(DateTime)).DefaultValue = DBNull.Value;
			dt.Columns.Add(_REMARK, typeof(string)).DefaultValue = string.Empty;

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TCP_HISCODE WHERE " + condition;
            return base.DeleteBySql(sql);
        }
		
        public bool Delete(int cid)
        {
            string condition = "CID=:CID";
            AddParameter(_CID, cid);
            return DeleteByCondition(condition);
        }
		
        public bool Delete()
        {
            string condition = "CID=:CID";
            AddParameter(_CID, Cid);
            return DeleteByCondition(condition);
        }

        public bool Insert()
        {
			Cid=base.GetSequence("SELECT SEQ_TCP_HISCODE.NEXTVAL FROM DUAL");
string sql=@"INSERT INTO
TCP_HISCODE(
  CID,
  EXPECT,
  OPENCODE,
  DATETIME,
  REMARK)
VALUES(
  :CID,
  :EXPECT,
  :OPENCODE,
  :DATETIME,
  :REMARK)";
			AddParameter(_CID,DataRow[_CID]);
			AddParameter(_EXPECT,DataRow[_EXPECT]);
			AddParameter(_OPENCODE,DataRow[_OPENCODE]);
			AddParameter(_DATETIME,DataRow[_DATETIME]);
			AddParameter(_REMARK,DataRow[_REMARK]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            string sql = @"UPDATE Tcp_Hiscode SET
 EXPECT=:EXPECT,
 OPENCODE=:OPENCODE,
 DATETIME=:DATETIME,
 REMARK=:REMARK
WHERE CID=:CID ";
			AddParameter(_CID,DataRow[_CID]);
			AddParameter(_EXPECT,DataRow[_EXPECT]);
			AddParameter(_OPENCODE,DataRow[_OPENCODE]);
			AddParameter(_DATETIME,DataRow[_DATETIME]);
			AddParameter(_REMARK,DataRow[_REMARK]);

            if (!string.IsNullOrEmpty(condition))
                sql += " AND " + condition;
            return base.UpdateBySql(sql);
        }

        protected bool SelectByCondition(string condition)
        {
            string sql = @"
SELECT
  CID,
  EXPECT,
  OPENCODE,
  DATETIME,
  REMARK
FROM TCP_HISCODE
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int cid)
        {
            string condition = "CID=:CID";
            AddParameter(_CID, cid);
            return SelectByCondition(condition);
        }



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tcp_Hiscode
    /// </summary>
    public partial class Tcp_HiscodeCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Tcp_HiscodeCollection() { }

        public Tcp_HiscodeCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tcp_Hiscode(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Tcp_Hiscode().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Tcp_Hiscode._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  CID,
  EXPECT,
  OPENCODE,
  DATETIME,
  REMARK
FROM TCP_HISCODE
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListByCid(int cid)
        {
            string condition = "CID=:CID";
            AddParameter(Tcp_Hiscode._CID, cid);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TCP_HISCODE WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Tcp_Hiscode this[int index]
        {
            get
            {
                return new Tcp_Hiscode(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Tcp_Hiscode Find(Predicate<Tcp_Hiscode> match)
        {
            foreach (Tcp_Hiscode item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tcp_HiscodeCollection FindAll(Predicate<Tcp_Hiscode> match)
        {
            Tcp_HiscodeCollection list = new Tcp_HiscodeCollection();
            foreach (Tcp_Hiscode item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tcp_Hiscode> match)
        {
            foreach (Tcp_Hiscode item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tcp_Hiscode> match)
        {
            BeginTransaction();
            foreach (Tcp_Hiscode item in this)
            {
                item.ReferenceTransactionFrom(Transaction);
                if (!match(item))
                    continue;
                if (!item.Delete())
                {
                    Rollback();
                    return false;
                }
            }
            Commit();
            return true;
        }
        #endregion Linq
        #endregion
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
} 
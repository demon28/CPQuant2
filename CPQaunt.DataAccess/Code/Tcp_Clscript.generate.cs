 /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tcp_Clscript.generate.cs 
 * CreateTime : 2018-08-23 20:45:35 
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
    /// Data Access Layer Object Of Tcp_Clscript
    /// </summary>
    public partial class Tcp_Clscript : DataAccessBase
    {
        #region 默认构造

        public Tcp_Clscript() : base() { }

        public Tcp_Clscript(DataRow dataRow)
            : base(dataRow) { }

        #endregion 默认构造

        #region 对应表结构的常量属性
        
		public const string _LID="LID";
		public const string _NAME="NAME";
		public const string _CONTENT="CONTENT";
		public const string _REMARK="REMARK";

    
        public const string _TABLENAME="Tcp_Clscript";
        #endregion 对应表结构的常量属性

        #region 公开属性
        
		/// <summary>
		/// [default:0]
		/// </summary>
		public int Lid
		{
			get { return Convert.ToInt32(DataRow[_LID]); }
			set { DataRow[_LID] = value; }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Name
		{
			get { return DataRow[_NAME].ToString(); }
			set { DataRow[_NAME] = value; }
		}
		/// <summary>
		/// [default:string.Empty]
		/// </summary>
		public string Content
		{
			get { return DataRow[_CONTENT].ToString(); }
			set { DataRow[_CONTENT] = value; }
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
			dt.Columns.Add(_LID, typeof(int)).DefaultValue = 0;
			dt.Columns.Add(_NAME, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_CONTENT, typeof(string)).DefaultValue = string.Empty;
			dt.Columns.Add(_REMARK, typeof(string)).DefaultValue = string.Empty;

            return dt.NewRow();
        }
        
        #endregion 私有成员
        
        #region 常用方法
        
		protected bool DeleteByCondition(string condition)
        {
            string sql = @"DELETE FROM TCP_CLSCRIPT WHERE " + condition;
            return base.DeleteBySql(sql);
        }
		
        public bool Delete(int lid)
        {
            string condition = "LID=:LID";
            AddParameter(_LID, lid);
            return DeleteByCondition(condition);
        }
		
        public bool Delete()
        {
            string condition = "LID=:LID";
            AddParameter(_LID, Lid);
            return DeleteByCondition(condition);
        }

        public bool Insert()
        {
			Lid=base.GetSequence("SELECT SEQ_TCP_CLSCRIPT.NEXTVAL FROM DUAL");
string sql=@"INSERT INTO
TCP_CLSCRIPT(
  LID,
  NAME,
  CONTENT,
  REMARK)
VALUES(
  :LID,
  :NAME,
  :CONTENT,
  :REMARK)";
			AddParameter(_LID,DataRow[_LID]);
			AddParameter(_NAME,DataRow[_NAME]);
			AddParameter(_CONTENT,DataRow[_CONTENT]);
			AddParameter(_REMARK,DataRow[_REMARK]);
            return base.InsertBySql(sql);
        }
		
		public bool Update()
        {
			return UpdateByCondition(string.Empty);
        }
		
        protected bool UpdateByCondition(string condition)
        {
            string sql = @"UPDATE Tcp_Clscript SET
 NAME=:NAME,
 CONTENT=:CONTENT,
 REMARK=:REMARK
WHERE LID=:LID ";
			AddParameter(_LID,DataRow[_LID]);
			AddParameter(_NAME,DataRow[_NAME]);
			AddParameter(_CONTENT,DataRow[_CONTENT]);
			AddParameter(_REMARK,DataRow[_REMARK]);

            if (!string.IsNullOrEmpty(condition))
                sql += " AND " + condition;
            return base.UpdateBySql(sql);
        }

        protected bool SelectByCondition(string condition)
        {
            string sql = @"
SELECT
  LID,
  NAME,
  CONTENT,
  REMARK
FROM TCP_CLSCRIPT
WHERE " + condition;
            return base.SelectBySql(sql);
        }

        public bool SelectByPK(int lid)
        {
            string condition = "LID=:LID";
            AddParameter(_LID, lid);
            return SelectByCondition(condition);
        }



        #endregion 常用方法
        
        //提示：此类由代码生成器生成，如无特殊情况请不要更改。如要扩展请在外部同名类中扩展
    }
    
    /// <summary>
    /// Data Access Layer Object Collection Of Tcp_Clscript
    /// </summary>
    public partial class Tcp_ClscriptCollection : DataAccessCollectionBase
    {
        #region 默认构造
 
        public Tcp_ClscriptCollection() { }

        public Tcp_ClscriptCollection(DataTable table)
            : base(table) { }
            
        #endregion 默认构造
        
        #region 私有成员
        protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tcp_Clscript(DataTable.Rows[index]);
        }
        
        protected override DataTable BuildTable()
        {
            return new  Tcp_Clscript().CloneSchemaOfTable();
        }
        
        protected override string TableName
        {
            get { return Tcp_Clscript._TABLENAME; }
        }
        
        protected bool ListByCondition(string condition)
        {
            string sql = @"
SELECT
  LID,
  NAME,
  CONTENT,
  REMARK
FROM TCP_CLSCRIPT
WHERE " + condition;
            return base.ListBySql(sql);
        }

        public bool ListByLid(int lid)
        {
            string condition = "LID=:LID";
            AddParameter(Tcp_Clscript._LID, lid);
            return ListByCondition(condition);
        }

        public bool ListAll()
        {
            string condition = "1=1";
            return ListByCondition(condition);
        }
        
        public bool DeleteByCondition(string condition)
        {
            string sql = "DELETE FROM TCP_CLSCRIPT WHERE " + condition;
            return DeleteBySql(sql);
        }
        #endregion
        
        #region 公开成员
        public Tcp_Clscript this[int index]
        {
            get
            {
                return new Tcp_Clscript(DataTable.Rows[index]);
            }
        }

        public bool DeleteAll()
        {
            return this.DeleteByCondition(string.Empty);
        }
        
        #region Linq
        
        public Tcp_Clscript Find(Predicate<Tcp_Clscript> match)
        {
            foreach (Tcp_Clscript item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tcp_ClscriptCollection FindAll(Predicate<Tcp_Clscript> match)
        {
            Tcp_ClscriptCollection list = new Tcp_ClscriptCollection();
            foreach (Tcp_Clscript item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tcp_Clscript> match)
        {
            foreach (Tcp_Clscript item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }

        public bool DeleteAt(Predicate<Tcp_Clscript> match)
        {
            BeginTransaction();
            foreach (Tcp_Clscript item in this)
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
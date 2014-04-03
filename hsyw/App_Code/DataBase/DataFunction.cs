using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using System.Collections;

	/// <summary>
	/// DataFunction 的摘要说明。
	/// </summary>
	public class DataFunction
	{

		private static readonly string DATABASEINSTANCE = "Database Instance";
		public DataFunction()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}	
	    /// <summary>
	    /// 执行update、insert、delete等无返回值的sql语句。
	    /// </summary>
	    /// <param name="Type"></param>
	    /// <param name="strSql"></param>
	    /// <returns></returns>
	    /// 
		public static int ExecuteNonQuery(string strSql)
		{
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);				
			return database.ExecuteNonQuery(CommandType.Text,strSql);			
		}

		public static void ExecuteNonQuery(System.Data.IDbCommand command)
		{
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
			System.Data.IDbConnection connect=database.GetConnection();
			if(connect.State.Equals(System.Data.ConnectionState.Closed))
			{
				connect.Open();
			}
			command.Connection=connect;
			try
			{
				command.ExecuteNonQuery();
			}
			catch(Exception ee)
			{
			   string temp=ee.ToString();
			}
			finally
			{
				if(connect.State.Equals(System.Data.ConnectionState.Open))
				{
					connect.Close();
				}
			}

		}

		

		public static int ExecuteNonQuery(CommandType Type, string strSql)
		{
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
			return database.ExecuteNonQuery(Type, strSql);
		}

        public static ArrayList SetParameter(string pName, System.Data.DbType pType, string pValue)
        {
            ArrayList parame = new ArrayList();
            parame.Add(pName);
            parame.Add(pType);
            parame.Add(pValue);
            return parame;
        }

        public static int ExecuteNonQuery(string sql, ArrayList parameters)
        {
            Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);         
            Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper command = database.GetSqlStringCommandWrapper(sql);
            foreach (ArrayList p in parameters)
            {
                command.AddInParameter((string)p[0],( DbType )p[1], p[2]);  
            }
            try
            {
                database.ExecuteNonQuery(command);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public static int InsertData(ArrayList parameters,string tableName)
        {
            string StrColum = "";
            string StrValue = "";
            foreach (ArrayList p in parameters)
            {
                if (StrColum != "")
                {
                    StrColum += ",";
                    StrValue += ",";
                }
                StrColum += p[0].ToString().Replace(":","");
                StrValue +=(string)p[0];
            }
            string sql = "insert into " + tableName + " (" + StrColum + ") values(" + StrValue + ")";
          return  ExecuteNonQuery(sql, parameters);
        }

        public static int UpdateData(ArrayList parameters, string tableName,string sqlWhere)
        {
            string StrColum = "";
            foreach (ArrayList p in parameters)
            {
                if (StrColum != "")
                {
                    StrColum += ",";
                }
                StrColum += p[0].ToString().Replace(":", "") + "=" + p[0].ToString();
            }
            string sql = "update " + tableName + " set " + StrColum + " where " + sqlWhere;
           return ExecuteNonQuery(sql, parameters);
        }

		/// <summary>
		/// 填充数据到dataset中
		/// </summary>
		/// <param name="strSql"></param>
		/// <returns></returns>
		public static DataSet FillDataSet(string strSql)
		{
			try
			{
				Database database = DatabaseFactory.CreateDatabase();
				if (database != null)
				{
					DBCommandWrapper cmdWrapper = database.GetSqlStringCommandWrapper(strSql);	
					DataSet ds = database.ExecuteDataSet(cmdWrapper);					
					return ds;
				}
				else
					return null;
			}
			catch (Exception ex)
			{
				//return null;
				throw new Exception(ex.Message, ex);				
			}
		}
		public static void FillDataSet(string strSql,DataSet ds,string[] tableName)
		{
			Database database = DatabaseFactory.CreateDatabase();			
			database.LoadDataSet(CommandType.Text,strSql,ds,tableName);			
		}
		public static DataRow GetSingleRow(string sql)
		{
			DataRow row = null;
			try
			{
				
				DataSet ds = DataFunction.FillDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        row = ds.Tables[0].Rows[0];
                    }
                    else
                    {
                        row = ds.Tables[0].NewRow();
                    }
                }
               
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
			return row;
		}
		
		public static int UpdateDataSet(DataSet dataSet, string tableName)
		{
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
			return database.UpdateDataSet(dataSet, tableName, null, null, null, UpdateBehavior.Transactional);
		}

		public static int UpdateDataSet(DataSet dataSet, string tableName, string selectSql, UpdateBehavior updateBehavior)
		{
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
		    return database.UpdateDataSet(dataSet, selectSql, updateBehavior);
		}
		//执行一个事务
		public static bool ExecuteTransaction(string [] strSqlGather)
		{
			IDbTransaction transaction = null;
			try
			{
				Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
				if (database != null)
				{
					IDbConnection conn = database.GetConnection();
					if(conn!=null&&conn.State == ConnectionState.Closed)
						conn.Open();
					transaction = conn.BeginTransaction();
					if (transaction != null)
					{
						foreach (string strSql in strSqlGather)
						{
							ExecuteNonQuery(CommandType.Text, strSql);
						}
						transaction.Commit();
					}
					else
						return false;
				}
				else
					return false;
			}
			catch
			{
				if(transaction!=null)
                    transaction.Rollback();
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// 保存数据
		/// </summary>
		/// <param name="dataSet">保存的数据集</param>
		/// <param name="tName">表名</param>
		/// <returns></returns>
		public static  bool SaveData(DataSet dataSet, string tableName)
		{
			string sql = "select * from " + tableName;
			dataSet.Tables[0].TableName = tableName;
			return SaveData( dataSet, tableName, sql);
		}

	
		public static string GetStringResult(string GetDataSQL)
		{
			string result="";
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
			IDataReader iDataReader=database.ExecuteReader(CommandType.Text,GetDataSQL);
			if (iDataReader.Read())	
			{
				result=Convert.ToString(iDataReader[0]);
			}
			iDataReader.Close();
			return result;
		}

		public static int GetIntResult(string GetDataSQL)
		{
			int result=0;
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);
			IDataReader iDataReader=database.ExecuteReader(CommandType.Text,GetDataSQL);
			if (iDataReader.Read())	
			{
				if(iDataReader[0].ToString() != String.Empty)
				{
					result=Convert.ToInt32(iDataReader[0]);
				}
			}
			iDataReader.Close();
			return result;
		}
	
		/// <summary>
		/// 保存数据
		/// </summary>
		/// <param name="dataSet">保存的数据集</param>
		/// <param name="tName">表名</param>
		/// <param name="sql">取数据的SQL语句</param>
		/// <returns></returns>
		public static  bool SaveData(DataSet dataSet,string tableName,string selectSql)
		{
			if(!dataSet.HasChanges())
				return true;
			DataSet changesDataSet = dataSet.GetChanges();			
			UpdateDataSet(changesDataSet, tableName, selectSql, UpdateBehavior.Transactional);			
			dataSet.AcceptChanges();
			return true;
		}

		
	
		/// <summary>
		/// 判断是否有记录
		/// </summary>
		/// <param name="tName"></param>
		/// <param name="sql"></param>
		/// <returns></returns>
		/// 
		public static  bool HasRecord(string sql)
		{
			Database database = DatabaseFactory.CreateDatabase(DATABASEINSTANCE);				
			IDataReader iDataReader=database.ExecuteReader(CommandType.Text,sql);
			if (iDataReader.Read())	
			{
				iDataReader.Close();
				return true;
			}
			else
			{
				iDataReader.Close();
				return false;
			}
        }

        /// <summary>
        /// 填充数据到dataset中
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet FillDataSet(string strSql,string strDataBase)
        {
            try
            {
                Database database = DatabaseFactory.CreateDatabase(strDataBase);
                if (database != null)
                {
                    DBCommandWrapper cmdWrapper = database.GetSqlStringCommandWrapper(strSql);
                    DataSet ds = database.ExecuteDataSet(cmdWrapper);
                    return ds;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                //return null;
                throw new Exception(ex.Message, ex);
            }
        }
	}


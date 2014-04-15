using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace DBSync
{
    class Program
    {
        

        /*origin server info:=>
       address,user,pass,dbname
       *target server info:=>
       address,user,pass,dbname
        */

       

        protected static string origin_conn_string;
        protected static string target_conn_string;


        public static void getParams(ref List<string> param_list)
        {
            int len = param_list.Count();
            string str = "";
            switch (len)
            { 
                case 0:
                    Console.WriteLine("请输入原始数据库地址：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 1:
                    Console.WriteLine("请输入原始数据库名称：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 2:
                    Console.WriteLine("请输入原始数据库登录用户名：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 3:
                    Console.WriteLine("请输入原始数据库登录密码：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 4:
                    Console.WriteLine("请输入目标数据库地址：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 5:
                    Console.WriteLine("请输入目标数据库名称：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 6:
                    Console.WriteLine("请输入目标数据库登录用户名：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 7:
                    Console.WriteLine("请输入目标数据库登录密码：");
                    str = Console.ReadLine();
                    param_list.Add(str);
                    getParams(ref param_list);
                    break;
                case 8:
                    origin_conn_string = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};",param_list[0],param_list[1],param_list[2],param_list[3]);
                    target_conn_string = String.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", param_list[4], param_list[5], param_list[6], param_list[7]);
                    break;
            }
            
        }
       
        static void Main(string[] args)
        {
            
            List<string> param_list = new List<string>();
            foreach (string param in args)
            {
                param_list.Add(param);
            }

            getParams(ref param_list);


            int args_len = args.Length;
            if (args_len < 6)
            { 
            
            }

            string connectString = origin_conn_string;

            string connectString_new = target_conn_string;
            
            List<string> table_list = getTableList(connectString);

            DateTime now = DateTime.Now;

            string app_path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            doLocalBackup("RMSS", app_path, connectString);
            Console.WriteLine("Backup database ok");

            foreach (string table_name in table_list)
            {
                string pk_column_name = GetprimaryKey(table_name, connectString);

                if (pk_column_name.Length > 0)
                {
                    Console.WriteLine(String.Format("Get table {0},  PK name is {1}", table_name, pk_column_name));
                    
                    List<string> fields = getFields(table_name, connectString);
                    List<string> pk_list = selectTableAllPrimaryKey(pk_column_name, table_name, connectString);
                    List<string> pk_list_new = selectTableAllPrimaryKey(pk_column_name, table_name, connectString_new);

                    var differenceQuery = pk_list.Except(pk_list_new).ToList();

                    foreach (string pk in differenceQuery)
                    {
                        string data = selectTableByPrimaryKey(pk_column_name, pk, table_name, connectString);
                        insertDataToTable(fields, data, table_name, connectString_new);
                    }

                    TimeSpan usedTime = DateTime.Now - now;
                    int usedTimeMin = usedTime.Minutes;
                    Console.WriteLine(String.Format("used {0} minutes", usedTimeMin));
                    break;

                }
                else
                {
                    Console.WriteLine(String.Format("Table {0} not set primary key,Skip it", table_name));
                }

            }

            TimeSpan span = DateTime.Now - now;
            int used = span.Minutes;
            Console.WriteLine("Fin,Use {0} minutes.Thanks.", used);
            Console.Read();
            

        }

        public static void doLocalBackup(string dbName, string backupPath, string connString)
        {
            string file_name = String.Format(@"{0}\BACKUP_RMSS.bak", backupPath);
            Console.WriteLine(String.Format(@"DB backup path:{0}",file_name));
            if (!File.Exists(file_name))
            {
                string sql = String.Format(@"backup database {0} to disk = N'{1}'", dbName, file_name);
                SqlConnection mSqlConnection = new SqlConnection(connString);
                mSqlConnection.Open();
                SqlCommand mSqlCommand = new SqlCommand(sql, mSqlConnection);
                mSqlCommand.ExecuteNonQuery();
                mSqlConnection.Close();
            }
            
        }

        public static string listToString(List<string> list)
        {
            return string.Join(",", list.ToArray());
        }

        public static void insertDataToTable(List<string> fields, string data, string table_name, string cnn_string)
        {
            string sql = "";
            
            string strFields = listToString(fields);

            sql = String.Format(@"set identity_insert {0} on;
insert into {0} ({1}) values ({2});
set identity_insert {0} off", 
                                table_name, strFields, data);
            
            Console.WriteLine(sql);
            Console.WriteLine("============================================================");
            
            SqlConnection mSqlConnection = new SqlConnection(cnn_string);
            mSqlConnection.Open();
            SqlCommand mSqlCommand = new SqlCommand(sql, mSqlConnection);
            mSqlCommand.ExecuteNonQuery();
            mSqlConnection.Close();
        }

        public static string listValueToString(List<string> list)
        {
            string str = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i != list.Count - 1)
                {
                    str += String.Format(@"'{0}',", list[i]); 
                }
                else {
                    str += String.Format(@"'{0}'", list[i]);
                }
            }
            return str;
        }

        public static string selectTableByPrimaryKey(string primary_key, string pk_value, string table_name, string cnn_string)
        {
            string data="";
            string sql = String.Format(@"select * from {0} where {1} = '{2}'",table_name,primary_key,pk_value);
            SqlConnection mSqlConnection = new SqlConnection(cnn_string);
            mSqlConnection.Open();
            SqlCommand mSqlCommand = new SqlCommand(sql, mSqlConnection);
            SqlDataReader reader = mSqlCommand.ExecuteReader();

            while (reader.Read())
            {
                IDataRecord record = (IDataRecord)reader;

                List<string> values = new List<string>();
                for (int i = 0; i < record.FieldCount; i++)
                {
                    values.Add(record[i].ToString());
                }
                data = listValueToString(values);

            }
            mSqlConnection.Close();
            return data;
        }

        public static List<string> selectTableAllPrimaryKey(string primary_key, string table_name, string cnn_string)
        {
            List<string> pk_list = new List<string>();
            string sql = String.Format(@"select {0} from {1}", primary_key, table_name);
            SqlConnection mSqlConnection = new SqlConnection(cnn_string);
            mSqlConnection.Open();
            SqlCommand mSqlCommand = new SqlCommand(sql, mSqlConnection);
            SqlDataReader reader = mSqlCommand.ExecuteReader();
            while (reader.Read())
            {
                pk_list.Add(reader[0].ToString());
            }
            mSqlConnection.Close();
            return pk_list;

        }

        public static List<string> getFields(string table_name, string cnn_string)
        {
            List<string> fields = new List<string>();
            string sql = String.Format(@"select column_name from information_schema.columns where table_name = '{0}' ORDER BY ordinal_position", table_name);
            SqlConnection mSqlConnection = new SqlConnection(cnn_string);
            mSqlConnection.Open();
            SqlCommand mSqlCommand = new SqlCommand(sql, mSqlConnection);
            SqlDataReader reader = mSqlCommand.ExecuteReader();
            while (reader.Read())
            {
                fields.Add(reader[0].ToString());
            }
            mSqlConnection.Close();
            return fields;
        }

        public static List<string> getTableList(string cnn_string)
        {
            List<string> table_list = new List<string>();
            string sql = String.Format("SELECT name FROM sysobjects WHERE xtype='U' ORDER BY name");
            SqlConnection sqlConnection = new SqlConnection();
            SqlCommand sqlCommand = new SqlCommand();

            sqlConnection = new SqlConnection(cnn_string);
            sqlConnection.Open();
            sqlCommand = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                table_list.Add(reader[0].ToString());
            }
            sqlConnection.Close();
            return table_list;

        }

        public static string GetprimaryKey(string tableName ,string cnnString)
        {
            //string names,
            string ID = "";
            SqlDataReader mReader;
            SqlConnection mSqlConnection = new SqlConnection();
            SqlCommand mSqlCommand = new SqlCommand();
            string cnString= cnnString;
            mSqlConnection = new SqlConnection(cnString);
            mSqlConnection.Open();
            // sp_pkeys is SQL Server default stored procedure
            // you pass it only table Name, it will return
            // primary key column
            mSqlCommand = new SqlCommand("sp_pkeys",mSqlConnection);
            mSqlCommand.CommandType = CommandType.StoredProcedure;mSqlCommand.Parameters.Add
			            ("@table_name", SqlDbType.NVarChar).Value= tableName;
            mReader = mSqlCommand.ExecuteReader();
                while (mReader.Read())
                {
                //the primary key column resides at index 4 
                ID = mReader[3].ToString();
                }
                
            mSqlConnection.Close();
            return ID;
        }
        
    }
}
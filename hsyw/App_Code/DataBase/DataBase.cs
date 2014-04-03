using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
////using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
////using System.Xml.Linq;
//51-As-p-x

using System.Collections;

/// <summary>
/// Summary description for DataBase
/// </summary>
public class DataBase
{
    private static Object _ClassLock = typeof(DataBase);



    private string ConnStr = null;

    public string connStr()
    {
        return ConfigurationManager.ConnectionStrings["DataBaseConnString"].ConnectionString;
    }
    public DataBase()
    {
        //ConnStr = ConfigurationSettings.AppSettings["ConnStr"];
        ConnStr = ConfigurationManager.ConnectionStrings["DataBaseConnString"].ConnectionString;
        
    }
    public DataBase(string Str)
    {
        try
        {
            this.ConnStr = Str;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 返回connection对象srxljl 
    /// </summary>
    /// <returns></returns>
    public OracleConnection ReturnConn()
    {

        OracleConnection Conn = new OracleConnection(ConnStr);
        Conn.Open();
        return Conn;
    }
    public void Dispose(OracleConnection Conn)
    {
        if (Conn != null)
        {
            Conn.Close();
            Conn.Dispose();
        }
        GC.Collect();
    }
    /// <summary>
    /// 运行SQL语句
    /// </summary>
    /// <param name="SQL"></param>
    public void RunProc(string SQL)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleCommand Cmd;
        Cmd = CreateCmd(SQL, Conn);
        try
        {
            Cmd.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception(SQL);
        }
        Dispose(Conn);
        return;
    }

    /// <summary>
    /// 运行SQL语句返回DataReadersrxljl 
    /// </summary>
    /// <param name="SQL"></param>
    /// <returns>OracleDataReader对象.</returns>
    public OracleDataReader RunProcGetReader(string SQL)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleCommand Cmd;
        Cmd = CreateCmd(SQL, Conn);
        
        OracleDataReader Dr;
        try
        {
            Dr = Cmd.ExecuteReader(CommandBehavior.Default);
        }
        catch
        {
            throw new Exception(SQL);
        }
        //Dispose(Conn);
        return Dr;
    }

    /// <summary>
    /// 生成Command对象srxljl 
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="Conn"></param>
    /// <returns></returns>
    public OracleCommand CreateCmd(string SQL, OracleConnection Conn)
    {
        OracleCommand Cmd;
        Cmd = new OracleCommand(SQL, Conn);
        return Cmd;
    }

    /// <summary>
    /// 生成Command对象
    /// </summary>
    /// <param name="SQL"></param>
    /// <returns></returns>
    public OracleCommand CreateCmd(string SQL)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleCommand Cmd;
        Cmd = new OracleCommand(SQL, Conn);
        return Cmd;
    }

    /// <summary>
    /// 返回adapter对象srxljl 
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="Conn"></param>
    /// <returns></returns>
    public OracleDataAdapter CreateDa(string SQL)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        
        OracleDataAdapter Da;
        Da = new OracleDataAdapter(SQL, Conn);
        return Da;
    }

    /// <summary>
    /// 运行SQL语句,返回DataSet对象srxljl 
    /// </summary>
    /// <param name="procName">SQL语句</param>
    /// <param name="prams">DataSet对象</param>
    public DataSet RunProc(string SQL, DataSet Ds)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleDataAdapter Da;
        //Da = CreateDa(SQL, Conn);
        Da = new OracleDataAdapter(SQL, Conn);
        try
        {
            Da.Fill(Ds);
        }
        catch (Exception Err)
        {
            throw Err;
        }
        Dispose(Conn);
        return Ds;
    }

    /// <summary>
    /// 运行SQL语句,返回DataSet对象srxljl 
    /// </summary>
    /// <param name="procName">SQL语句</param>
    /// <param name="prams">DataSet对象</param>
    /// <param name="dataReader">表名</param>
    public DataSet RunProc(string SQL, DataSet Ds, string tablename)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleDataAdapter Da;
        Da = CreateDa(SQL);
        try
        {
            Da.Fill(Ds, tablename);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        Dispose(Conn);
        return Ds;
    }

    /// <summary>
    /// 运行SQL语句,返回DataSet对象srxljl 
    /// </summary>
    /// <param name="procName">SQL语句</param>
    /// <param name="prams">DataSet对象</param>
    /// <param name="dataReader">表名</param>
    public DataSet RunProc(string SQL, DataSet Ds, int StartIndex, int PageSize, string tablename)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleDataAdapter Da;
        Da = CreateDa(SQL);
        try
        {
            Da.Fill(Ds, StartIndex, PageSize, tablename);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        Dispose(Conn);
        return Ds;
    }

    /// <summary>
    /// 检验是否存在数据srxljl 
    /// </summary>
    /// <returns></returns>
    public bool ExistDate(string SQL)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleDataReader Dr;
        Dr = CreateCmd(SQL, Conn).ExecuteReader();
        if (Dr.Read())
        {
            Dispose(Conn);
            return true;
        }
        else
        {
            Dispose(Conn);
            return false;
        }
    }

    /// <summary>
    /// 返回SQL语句执行结果的第一行第一列srxljl 
    /// </summary>
    /// <returns>字符串</returns>
    public string ReturnValue(string SQL)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        string result;
        OracleDataReader Dr;
        try
        {
            Dr = CreateCmd(SQL, Conn).ExecuteReader();
            if (Dr.Read())
            {
                result = Dr[0].ToString();
                Dr.Close();
            }
            else
            {
                result = "";
                Dr.Close();
            }
        }
        catch
        {
            throw new Exception(SQL);
        }
        Dispose(Conn);
        return result;
    }

    /// <summary>
    /// 返回SQL语句第一列,第ColumnI列,srxljl 
    /// </summary>
    /// <returns>字符串</returns>
    public string ReturnValue(string SQL, int ColumnI)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        string result;
        OracleDataReader Dr;
        try
        {
            Dr = CreateCmd(SQL, Conn).ExecuteReader();
        }
        catch
        {
            throw new Exception(SQL);
        }
        if (Dr.Read())
        {
            result = Dr[ColumnI].ToString();
        }
        else
        {
            result = "";
        }
        Dr.Close();
        Dispose(Conn);
        return result;
    }

    /// <summary>
    /// 生成一个存储过程使用的OracleCommand.
    /// </summary>
    /// <param name="procName">存储过程名.</param>
    /// <param name="prams">存储过程入参数组.</param>
    /// <returns>OracleCommand对象.</returns>
    public OracleCommand CreateCmd(string procName, OracleParameter[] prams)
    {
        
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleCommand Cmd = new OracleCommand(procName, Conn);
        Cmd.CommandType = CommandType.StoredProcedure;
        if (prams != null)
        {
            foreach (OracleParameter parameter in prams)
            {
                if (parameter != null)
                {
                    Cmd.Parameters.Add(parameter);
                }
            }
        }
        return Cmd;
    }

    /// <summary>
    /// 为存储过程生成一个OracleCommand对象srxljl 
    /// </summary>
    /// <param name="procName">存储过程名</param>
    /// <param name="prams">存储过程参数</param>
    /// <returns>OracleCommand对象</returns>
    private OracleCommand CreateCmd(string procName, OracleParameter[] prams, OracleDataReader Dr)
    {
        OracleConnection Conn;
        Conn = new OracleConnection(ConnStr);
        Conn.Open();
        OracleCommand Cmd = new OracleCommand(procName, Conn);
        Cmd.CommandType = CommandType.StoredProcedure;
        if (prams != null)
        {
            foreach (OracleParameter parameter in prams)
                Cmd.Parameters.Add(parameter);
        }
        Cmd.Parameters.Add(
         new OracleParameter("ReturnValue", OracleType.Int32, 4,
         ParameterDirection.ReturnValue, false, 0, 0,
         string.Empty, DataRowVersion.Default, null));

        return Cmd;
    }

    /// <summary>
    /// 运行存储过程,返回.srxljl 
    /// </summary>
    /// <param name="procName">存储过程名</param>
    /// <param name="prams">存储过程参数</param>
    /// <param name="dataReader">OracleDataReader对象</param>
    public void RunProc(string procName, OracleParameter[] prams, OracleDataReader Dr)
    {

        OracleCommand Cmd = CreateCmd(procName, prams, Dr);
        Dr = Cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        return;
    }

    /// <summary>
    /// 运行存储过程,返回.srxljl 
    /// </summary>
    /// <param name="procName">存储过程名</param>
    /// <param name="prams">存储过程参数</param>
    public string RunProc(string procName, OracleParameter[] prams)
    {
        OracleDataReader Dr;
        OracleCommand Cmd = CreateCmd(procName, prams);
        Dr = Cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        if (Dr.Read())
        {
            return Dr.GetValue(0).ToString();
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 运行存储过程,返回dataset.srxljl 
    /// </summary>
    /// <param name="procName">存储过程名.</param>
    /// <param name="prams">存储过程入参数组.</param>
    /// <returns>dataset对象.</returns>
    public DataSet RunProc(string procName, OracleParameter[] prams, DataSet Ds)
    {
        OracleCommand Cmd = CreateCmd(procName, prams);
        OracleDataAdapter Da = new OracleDataAdapter(Cmd);
        try
        {
            Da.Fill(Ds);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
        return Ds;
    }



}




using System;
using System.Data;
using System.Configuration;
////using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
////using System.Xml.Linq;

using System.Data.Common;

public class classLog
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT l.* FROM t_sys_Log l ";
        StrSql += " WHERE 1 = 1 " + StrTemp;
        return StrSql;
    }

    //删除记录，返回记录的条数===================================================================================
    public int Delete(string ID, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            string str_sql = "";
            str_sql = " delete from t_sys_Log ";
            str_sql += " WHERE ID = '" + ID + "' ";

            nResult = DataFunction.ExecuteNonQuery(str_sql);
            if (nResult == 0)
            {
                strMsg = "操作失败！";
            }
            else
            {
                strMsg = "操作成功，" + nResult.ToString().Trim() + "条记录变更！";

            }
        }
        catch (Exception e)
        {
            //异常代码
            strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
        }
        finally
        {
            //无论异常发生与否，都会执行的代码
        }

        return nResult;
    }

    //插入记录，返回记录的条数===================================================================================
    public int Insert(string UserName, string Title, string Memo, out string strMsg)   //写日志文件
    {
        int nResult = 0;

        try
        {
            //正常代码

            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            string Ip = publ.GetClientIP();

            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, Guid.NewGuid().ToString()));
            parameters.Add(DataFunction.SetParameter(":Ip", DbType.String, Ip));
            parameters.Add(DataFunction.SetParameter(":UserName", DbType.String, UserName));
            parameters.Add(DataFunction.SetParameter(":Title", DbType.String, Title));
            parameters.Add(DataFunction.SetParameter(":Memo", DbType.String, Memo));
            parameters.Add(DataFunction.SetParameter(":USERDATETIME", DbType.DateTime, DateTime.Now.ToString()));
            nResult=DataFunction.InsertData(parameters, "T_SYS_LOG");
            if (nResult == 0)
            {
                strMsg = "操作失败！";
            }
            else
            {
                strMsg = "操作成功，" + nResult.ToString().Trim() + "条记录变更！";

            }
        }
        catch (Exception e)
        {
            //异常代码
            strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
        }
        finally
        {
            //无论异常发生与否，都会执行的代码
        }

        return nResult;
    }
}

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
////using System.Xml.Linq;
using System.Collections;
using System.Data.OracleClient;
using System.Data.Common;
/// <summary>
///classUserOnline 的摘要说明
/// </summary>
public class classUserOnline
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT  ";
        StrSql += " uo.*  ";

        StrSql += " FROM t_sys_UserOnline uo ";

        StrSql += " WHERE 1 = 1 " + StrTemp;

        return StrSql;
    }

    //删除记录，返回记录的条数===================================================================================
    public int Delete(string strWhere, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            string str_sql = "";
            str_sql = " delete from t_sys_UserOnline ";
            str_sql += " WHERE 1 = 1 " + strWhere;

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
    public int Insert(string UserName, string PageUrl, string PageTitle, out string strMsg)
    {
        int nResult = 0;

        try
        {
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            //正常代码
            string UserIP = publ.GetClientIP();
            string OnlineDateTime = DateTime.Now.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserIP", UserIP);
            //dbSys.AddParameter("@OnlineDateTime", OnlineDateTime);
            //dbSys.AddParameter("@PageUrl", PageUrl);
            //dbSys.AddParameter("@PageTitle", PageTitle);
          
            //nResult = dbSys.ExecuteNonQuery("p_UserOnline_insert");

            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID",DbType.String,ID));
            parameters.Add(DataFunction.SetParameter(":UserName",DbType.String,UserName));
            parameters.Add(DataFunction.SetParameter(":UserIP", DbType.String, UserIP));
            parameters.Add(DataFunction.SetParameter(":OnlineDateTime", DbType.String, OnlineDateTime));
            parameters.Add(DataFunction.SetParameter(":PageUrl", DbType.String, PageUrl));
            parameters.Add(DataFunction.SetParameter(":PageTitle", DbType.String, PageTitle));
            nResult = DataFunction.InsertData(parameters,"T_SYS_USERONLINE");
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


    //编辑记录，返回记录的条数===================================================================================
    public int Update(string UserName, string PageUrl, string PageTitle, out string strMsg)
    {
        int nResult = 0;

        try
        {
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            //正常代码
            string UserIP = publ.GetClientIP();
            string OnlineDateTime = DateTime.Now.ToString().Trim();

            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@PageUrl", PageUrl);
            //dbSys.AddParameter("@PageTitle", PageTitle);
            //dbSys.AddParameter("@UserIP", UserIP);
            //dbSys.AddParameter("@OnlineDateTime", OnlineDateTime);

            //nResult = dbSys.ExecuteNonQuery("p_UserOnline_update");
            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, ID));
            parameters.Add(DataFunction.SetParameter(":UserName", DbType.String, UserName));
            parameters.Add(DataFunction.SetParameter(":PageUrl", DbType.String, ID));
            parameters.Add(DataFunction.SetParameter(":PageTitle", DbType.String, PageTitle));
            parameters.Add(DataFunction.SetParameter(":UserIP", DbType.String, UserIP));
            parameters.Add(DataFunction.SetParameter(":OnlineDateTime", DbType.String, OnlineDateTime));

            nResult = DataFunction.UpdateData(parameters, "T_SYS_USERONLINE", "ID='" + ID + "'");
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

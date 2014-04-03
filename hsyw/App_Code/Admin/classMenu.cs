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

using System.Data.Common;
using System.Collections;

public class classMenu
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT m.* FROM t_sys_Menu m ";
        StrSql += " WHERE 1 = 1 " + StrTemp;
        return StrSql;
    }

    //删除记录，返回记录的条数===================================================================================
    public int Delete(string MenuCode, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            string sql = "delete from T_SYS_MENU where MenuCode='" + MenuCode + "'";
            nResult = DataFunction.ExecuteNonQuery(sql);
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
    public void Delete(string strmenu, string strprivate)
    { 
        string[] sql = new string[2];
        if (!strmenu.Equals(""))
        {
            sql[0] = string.Format("delete from T_SYS_MENU where MENUCODE in ({0})", strmenu);
            if (!strprivate.Equals(""))
            {
                sql[1] = string.Format("delete from T_SYS_PRIVATE where PCODE in ({0})", strprivate);
            }
        }
        else
        {
            if (!strprivate.Equals(""))
            {
                sql[0] = string.Format("delete from T_SYS_PRIVATE where PCODE in ({0})", strprivate);
            }
        }
        DataFunction.ExecuteTransaction(sql);
    }
    //插入记录，返回记录的条数===================================================================================
    public int Insert(string MenuCode, string MenuName, string PMenuCode, string FileName, string Ico, string DisplayOrder, string IsExpand, string IsUse, string IsVisible, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, Guid.NewGuid().ToString()));
            parameters.Add(DataFunction.SetParameter(":MenuCode", DbType.String, MenuCode));
            parameters.Add(DataFunction.SetParameter(":MenuName", DbType.String, MenuName));
            parameters.Add(DataFunction.SetParameter(":PMenuCode", DbType.String, PMenuCode));
            parameters.Add(DataFunction.SetParameter(":FileName", DbType.String, FileName));
            parameters.Add(DataFunction.SetParameter(":Ico", DbType.String, Ico));
            parameters.Add(DataFunction.SetParameter(":DisplayOrder", DbType.String, DisplayOrder));
            parameters.Add(DataFunction.SetParameter(":IsExpand", DbType.String, IsExpand));
            parameters.Add(DataFunction.SetParameter(":IsUse", DbType.String, IsUse));
            parameters.Add(DataFunction.SetParameter(":IsVisible", DbType.String, IsVisible));
            nResult = DataFunction.InsertData(parameters, "T_SYS_MENU");
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
    public int Update(string MenuCode, string MenuName, string PMenuCode, string FileName, string Ico, string DisplayOrder, string IsExpand, string IsUse, string IsVisible, out string strMsg)
    {
        int nResult = 0;

        try
        {

            ArrayList parameters = new ArrayList();         
            parameters.Add(DataFunction.SetParameter(":MenuCode", DbType.String, MenuCode));
            parameters.Add(DataFunction.SetParameter(":MenuName", DbType.String, MenuName));
            parameters.Add(DataFunction.SetParameter(":PMenuCode", DbType.String, PMenuCode));
            parameters.Add(DataFunction.SetParameter(":FileName", DbType.String, FileName));
            parameters.Add(DataFunction.SetParameter(":Ico", DbType.String, Ico));
            parameters.Add(DataFunction.SetParameter(":DisplayOrder", DbType.String, DisplayOrder));
            parameters.Add(DataFunction.SetParameter(":IsExpand", DbType.String, IsExpand));
            parameters.Add(DataFunction.SetParameter(":IsUse", DbType.String, IsUse));
            parameters.Add(DataFunction.SetParameter(":IsVisible", DbType.String, IsVisible));
            nResult =DataFunction.UpdateData(parameters, "T_SYS_MENU", "MenuCode='" + MenuCode+"'");
            
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
    public string GetMenuCode(string PMenuCode)
    {
        string sql = string.Format("select MenuCode from T_SYS_MENU where PMenuCode = '{0}' order by MenuCode desc", PMenuCode);
        string mc = DataFunction.GetStringResult(sql);
        int len = mc.Length;
        if (len == 0)
        {
            mc = PMenuCode + "01";
        }
        else
        {
            string qz = mc.Substring(0, len - 2);
            mc = mc.Substring(len - 2, 2);
            mc = (Convert.ToInt32(mc) + 1).ToString();
            mc = mc.Length == 2 ? mc : ("0" + mc);
            mc = qz + mc;
        }
        return mc;
    }
    public DataSet GetMenuPrivte(string menucode)
    {
        string sql = string.Format("select * from T_SYS_PRIVATE where MENUCODE = '{0}' order by XH",menucode);
        DataSet ds = DataFunction.FillDataSet(sql);
        return ds;
    }
}

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

public class classGroup
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT g.* FROM t_sys_Group g ";
        StrSql += " WHERE 1 = 1 " + StrTemp;
        return StrSql;
    }

    //返回菜单代码===================================================================================
    public string GetMenuCode(string StrTemp, out string strMsg)
    {
        string strReturn = "''";

        try
        {
            string strSql = " ";
            strSql += " SELECT rg.* FROM t_sys_r_GroupMenu rg ";
            strSql += " WHERE rg.GroupCode = '" + StrTemp + "' ";
            
            DataSet ds = DataFunction.FillDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strReturn += ", '" + dr["MenuCode"].ToString().Trim() + "' ";
            }

            strMsg = "";

        }
        catch (Exception e)
        {
            //异常代码
            strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
        }
        return strReturn;
    }

    //从角色代码得到角色名称===================================================================================
    public string GetGroupName(string StrTemp, out string strMsg)
    {
        string strReturn = "";

        try
        {
            string strSql = " ";
            strSql += " SELECT g.* FROM t_sys_Group g ";
            strSql += " WHERE g.GroupCode = '" + StrTemp + "' ";

            DataRow dr = DataFunction.GetSingleRow(strSql);

            if (dr != null)
            {
                strReturn = dr["GroupName"].ToString().Trim();
            }

            strMsg = "";

        }
        catch (Exception e)
        {
            //异常代码
            strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
        }

        return strReturn;
    }


    //删除关系表中记录，返回记录的条数===================================================================================
    public int DelGroupMenu(string StrTemp, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            string str_sql = "";
            str_sql = " delete from t_sys_r_GroupMenu ";
            str_sql += " WHERE GroupCode = '" + StrTemp + "' ";

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
    public int DelGroupPrivate(string groupcode)
    {
        return DataFunction.ExecuteNonQuery(string.Format("delete from T_SYS_R_GROUPPRIVATE where GROUPCODE = '{0}'",groupcode));
    }
    //删除记录，返回记录的条数===================================================================================
    public int Delete(string GroupCode)
    {
        int nResult = 0;
        if (!DataFunction.HasRecord(string.Format("select * from t_sys_r_usergroup where GROUPCODE = '{0}'",GroupCode)))
        {
            nResult = DataFunction.ExecuteNonQuery(string.Format("delete from T_SYS_GROUP where GROUPCODE = '{0}'",GroupCode));
        }
        return nResult;
    }

    //插入记录，返回记录的条数===================================================================================
    public int InsGroupMenu(string GroupCode, string MenuCode, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@GroupCode", GroupCode);
            //dbSys.AddParameter("@MenuCode", MenuCode);

            //nResult = dbSys.ExecuteNonQuery("p_GroupMenu_insert");
            nResult = DataFunction.ExecuteNonQuery(string.Format("insert into T_SYS_R_GROUPMENU values ('{0}','{1}','{2}')", ID, GroupCode, MenuCode));
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
    public int Insert(string GroupCode, string GroupName, string PGroupCode, string DisplayOrder, string IsUse, string IsVisible,string groupms, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //System.Guid guid = System.Guid.NewGuid();
            //string ID = guid.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@GroupCode", GroupCode);
            //dbSys.AddParameter("@GroupName", GroupName);
            //dbSys.AddParameter("@PGroupCode", PGroupCode);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Group_insert");
            nResult = DataFunction.ExecuteNonQuery(string.Format("insert into T_SYS_GROUP values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", Guid.NewGuid().ToString(), GroupCode, GroupName, PGroupCode, DisplayOrder, IsUse, IsVisible,groupms));
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
    public int Update(string GroupCode, string GroupName, string PGroupCode, string DisplayOrder, string IsUse, string IsVisible,string groupms, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@GroupCode", GroupCode);
            //dbSys.AddParameter("@GroupName", GroupName);
            //dbSys.AddParameter("@PGroupCode", PGroupCode);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            nResult = DataFunction.ExecuteNonQuery(string.Format("update T_SYS_GROUP set GroupName = '{0}',PGroupCode='{1}',DisplayOrder='{2}',IsUse='{3}',IsVisible='{4}',GROUPMS='{6}' where GroupCode='{5}'", GroupName, PGroupCode, DisplayOrder, IsUse, IsVisible, GroupCode,groupms));
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





    //返回菜单代码===================================================================================
    public string GetGroupMenu(string GroupCode, out string strMsg)
    {
        string strReturn = "''";
        try
        {
            string strSql = " ";
            strSql += " SELECT rg.* FROM t_sys_r_GroupMenu rg ";
            strSql += " WHERE rg.GroupCode in (" + GroupCode + ") ";

            DataSet ds = DataFunction.FillDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strReturn += ", '" + dr["MenuCode"].ToString().Trim() + "' ";
            }

            strMsg = "";

        }
        catch (Exception e)
        {
            //异常代码
            strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
        }

        return strReturn;
    }
    public string GetGroupCode(string PGroupCode)
    {
        string sql = string.Format("select GroupCode from T_SYS_GROUP where PGroupCode = '{0}' order by GroupCode desc", PGroupCode);
        string gc = DataFunction.GetStringResult(sql);
        int len = gc.Length;
        if (len == 0)
        {
            gc = PGroupCode + "01";
        }
        else
        {
            string qz = gc.Substring(0, len - 2);
            gc = gc.Substring(len - 2, 2);
            gc = (Convert.ToInt32(gc) + 1).ToString();
            gc = gc.Length == 2 ? gc : ("0" + gc);
            gc = qz + gc;
        }
        return gc;
    }
    public bool IsHaveMenu(string groupcode, string menucode)
    {
        return DataFunction.HasRecord(string.Format("select * from T_SYS_R_GROUPMENU where GROUPCODE = '{0}' and MENUCODE = '{1}'",groupcode,menucode));
    }
    public bool IsHavePrivate(string groupcode, string pcode)
    {
        return DataFunction.HasRecord(string.Format("select * from T_SYS_R_GROUPPRIVATE where GROUPCODE = '{0}' and PCODE = '{1}'", groupcode, pcode));
    }
}

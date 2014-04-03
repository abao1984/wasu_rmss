using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Common;
using yueue.ADOKeycap;
using System.Collections;

public class classAdmin
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT  ";
        StrSql += " b.BranchName as BranchName, ";
        StrSql += " a.*  ";

        StrSql += " FROM t_sys_Admin a ";
        StrSql += " LEFT JOIN t_sys_Branch b ON b.BranchCode = a.BranchCode  ";

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
            str_sql = " delete from t_sys_Admin ";
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
    public int Insert(string UserName, string UserRealName, string AdminPass, string UserGroup, string BranchCode, string DisplayOrder, string IsUse, string IsVisible, out string strMsg)
    {
        int nResult = 0;

        try
        {
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();
            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, Guid.NewGuid().ToString()));
            parameters.Add(DataFunction.SetParameter(":UserName", DbType.String, UserName));
            parameters.Add(DataFunction.SetParameter(":UserRealName", DbType.String, UserRealName));
            parameters.Add(DataFunction.SetParameter(":AdminPass", DbType.String, AdminPass));
            parameters.Add(DataFunction.SetParameter(":UserGroup", DbType.String, UserGroup));
            parameters.Add(DataFunction.SetParameter(":BranchCode", DbType.String, BranchCode));
            parameters.Add(DataFunction.SetParameter(":DisplayOrder", DbType.String, DisplayOrder));
            parameters.Add(DataFunction.SetParameter(":IsUse", DbType.String, IsUse));
            parameters.Add(DataFunction.SetParameter(":IsVisible", DbType.String, IsVisible));
             nResult =DataFunction.InsertData(parameters, "t_sys_admin");
           
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserRealName", UserRealName);
            //dbSys.AddParameter("@AdminPass", AdminPass);
            //dbSys.AddParameter("@UserGroup", UserGroup);
            //dbSys.AddParameter("@BranchCode", BranchCode);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Admin_insert");
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
    public int Update(string UserName, string UserRealName, string UserGroup, string BranchCode, string DisplayOrder, string IsUse, string IsVisible, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserRealName", UserRealName);
            //dbSys.AddParameter("@UserGroup", UserGroup);
            //dbSys.AddParameter("@BranchCode", BranchCode);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Admin_update");
            nResult = DataFunction.ExecuteNonQuery(string.Format(@"update t_sys_admin set UserRealName = '{0}',UserGroup='{1}',BranchCode='{2}',DisplayOrder='{3}',IsUse='{4}',IsVisible='{5}' where UserName = '{6}'", UserRealName, UserGroup, BranchCode, DisplayOrder, IsUse, IsVisible, UserName));
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



    //更改密码，返回记录的条数===================================================================================
    public int UpdatePass(string UserName, string AdminPass, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@AdminPass", AdminPass);

            //nResult = dbSys.ExecuteNonQuery("p_Admin_pass_update");
            nResult = DataFunction.ExecuteNonQuery(string.Format("update T_SYS_ADMIN set AdminPass = '{0}' where UserName = '{1}'", AdminPass, UserName));
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

    //更改密码，返回记录的条数===================================================================================
    public int UpdatePassSelf(string UserName, string AdminPass, string AdminPassOld, out string strMsg)
    {
        int nResult = 0;

        bool bl = false;
        try
        {
            string strSql = " select * from t_sys_Admin where UserName = '" + UserName + "' and AdminPass = '" + AdminPassOld + "' ";
            bl = DataFunction.HasRecord(strSql);

            if (bl)
            {
                nResult = DataFunction.ExecuteNonQuery(string.Format("update T_SYS_ADMIN set AdminPass = '{0}' where UserName = '{1}'", AdminPass, UserName));
                if (nResult == 0)
                {
                    strMsg = "操作失败！";
                }
                else
                {
                    strMsg = "操作成功，" + nResult.ToString().Trim() + "条记录变更！";

                }
            }
            else
            {
                strMsg = "旧密码输入错误，操作失败！";
            }

        }
        catch (Exception e)
        {
            //异常代码
            strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
        }
        return nResult;
    }


}

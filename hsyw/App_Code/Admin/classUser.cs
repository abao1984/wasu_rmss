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

using System.Data.OracleClient;
using System.Data.Common;
using System.Collections;

public class classUser
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT  ";
        StrSql += " b.BranchName,b.PATH, ";
        StrSql += " u.*  ";

        StrSql += " FROM t_sys_User u ";
        StrSql += " LEFT JOIN t_sys_Branch b ON b.BranchCode = u.BranchCode  ";

        StrSql += " WHERE 1 = 1 " + StrTemp;

        return StrSql;
    }

    //删除记录，返回记录的条数===================================================================================
    public int Delete(string UserName, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@UserName", UserName);

            //nResult = dbSys.ExecuteNonQuery("p_User_delete");
            string sql = string.Format("delete from T_SYS_USER where USERNAME = '{0}'",UserName);
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

    //插入记录，返回记录的条数===================================================================================
    public int Insert(string UserName, string UserRealName, string UserId, string UserPass, string BranchCode, string UserRegIp, string UserPhone, string DisplayOrder, string IsUse, string IsVisible, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, ID));
            parameters.Add(DataFunction.SetParameter(":UserName", DbType.String, UserName));
            parameters.Add(DataFunction.SetParameter(":UserRealName", DbType.String, UserRealName));
            parameters.Add(DataFunction.SetParameter(":UserId", DbType.String, UserId));
            parameters.Add(DataFunction.SetParameter(":UserPass", DbType.String, UserPass));
            parameters.Add(DataFunction.SetParameter(":BranchCode", DbType.String, BranchCode));
            parameters.Add(DataFunction.SetParameter(":UserRegIp", DbType.String, UserRegIp));

            parameters.Add(DataFunction.SetParameter(":UserPhone", DbType.String, UserPhone));
            parameters.Add(DataFunction.SetParameter(":DisplayOrder", DbType.String, DisplayOrder));

            parameters.Add(DataFunction.SetParameter(":IsUse", DbType.String, IsUse));
            parameters.Add(DataFunction.SetParameter(":IsVisible", DbType.String, IsVisible));
            nResult = DataFunction.InsertData(parameters, "T_SYS_USER");


            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserRealName", UserRealName);
            //dbSys.AddParameter("@UserId", UserId);
            //dbSys.AddParameter("@UserPass", UserPass);
            //dbSys.AddParameter("@BranchCode", BranchCode);
            //dbSys.AddParameter("@UserRegIp", UserRegIp);
            //dbSys.AddParameter("@UserPhone", UserPhone);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_User_insert");
            //string sql = string.Format("insert into T_SYS_USER values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',to_date('{8}','YYYY-MM-DD HH24:MI:SS'),'{9}',to_date('{10}','YYYY-MM-DD HH24:MI:SS'),'{11}','{12}','{13}')", ID, UserName, UserId, UserRealName, UserPass, BranchCode, UserPhone, UserRegIp, DateTime.Now.ToString(), UserRegIp, DateTime.Now.ToString(), DisplayOrder, IsUse, IsVisible);
            //nResult = DataFunction.ExecuteNonQuery(sql);
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
    public int Update(string ID,string UserName, string UserRealName, string BranchCode, string UserPhone, string DisplayOrder, string IsUse, string IsVisible,string userid, out string strMsg)
    {
        int nResult = 0;

        try
        {
            ArrayList parameters = new ArrayList();
            parameters.Add(DataFunction.SetParameter(":ID", DbType.String, ID));
            parameters.Add(DataFunction.SetParameter(":UserName", DbType.String, UserName));
            parameters.Add(DataFunction.SetParameter(":UserRealName", DbType.String, UserRealName));

            parameters.Add(DataFunction.SetParameter(":BranchCode", DbType.String, BranchCode));

            parameters.Add(DataFunction.SetParameter(":UserPhone", DbType.String, UserPhone));
            parameters.Add(DataFunction.SetParameter(":UserId", DbType.String, userid));
            parameters.Add(DataFunction.SetParameter(":DisplayOrder", DbType.String, DisplayOrder));

            parameters.Add(DataFunction.SetParameter(":IsUse", DbType.String, IsUse));
            parameters.Add(DataFunction.SetParameter(":IsVisible", DbType.String, IsVisible));

            nResult = DataFunction.UpdateData(parameters, "T_SYS_USER", "ID='" + ID + "'");


            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserRealName", UserRealName);
            //dbSys.AddParameter("@BranchCode", BranchCode);
            //dbSys.AddParameter("@UserPhone", UserPhone);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_User_update");
            //string sql = string.Format("update T_SYS_USER set UserRealName='{0}',BranchCode='{1}',UserPhone='{2}',DisplayOrder='{3}',IsUse='{4}',IsVisible='{5}' where UserName='{6}'",UserRealName,BranchCode,UserPhone,DisplayOrder,IsUse,IsVisible,UserName);
            //nResult = DataFunction.ExecuteNonQuery(sql);
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
    public int UpdatePass(string UserName, string UserPass, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserPass", UserPass);

            //nResult = dbSys.ExecuteNonQuery("p_User_pass_update");
            string sql = string.Format("update T_SYS_USER set UserPass = '{0}' where UserName = '{1}'",UserPass,UserName);
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




    //返回用户组代码===================================================================================
    public string GetGroupCode(string UserName, out string strMsg)
    {
        string strReturn = "''";

        try
        {
            string strSql = " ";
            strSql += " SELECT ru.* FROM t_sys_r_UserGroup ru ";
            strSql += " WHERE ru.UserName = '" + UserName + "' ";

            DataSet ds = DataFunction.FillDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strReturn += ", '" + dr["GroupCode"].ToString().Trim() + "' ";
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

    //从用户名得到用户真名===================================================================================
    public string GetUserRealName(string strUserName, out string strMsg)
    {
        string strReturn = "";

        try
        {
            string strSql = " ";
            strSql += " SELECT u.* FROM t_sys_User u ";
            strSql += " WHERE u.UserName = '" + strUserName + "' ";

            DataRow dr = DataFunction.GetSingleRow(strSql);

            if (dr != null)
            {
                strReturn = dr["UserRealName"].ToString().Trim();
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
    public int DelUserGroup(string UserName, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            string str_sql = "";
            str_sql = " delete from t_sys_r_UserGroup ";
            str_sql += " WHERE UserName = '" + UserName + "' ";

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
    public int InsUserGroup(string UserName, string GroupCode, out string strMsg)
    {
        int nResult = 0;

        try
        {
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@GroupCode", GroupCode);

            //nResult = dbSys.ExecuteNonQuery("p_UserGroup_insert");
            string sql = string.Format("insert into T_SYS_R_USERGROUP values ('{0}','{1}','{2}')",ID,UserName,GroupCode);
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



    //更改密码，返回记录的条数===================================================================================
    public int UpdatePassSelf(string UserName, string UserPass, string UserPassOld, out string strMsg)
    {
        int nResult = 0;
        try
        {
            string strSql = " select * from t_sys_User where UserName = '" + UserName + "' and UserPass = '" + UserPassOld + "' ";

            if (DataFunction.HasRecord(strSql))
            {
                //正常代码
                //dbSys.Parameters.Clear();
                //dbSys.AddParameter("@UserName", UserName);
                //dbSys.AddParameter("@UserPass", UserPass);

                //nResult = dbSys.ExecuteNonQuery("p_User_pass_update");
                string sql = string.Format("update T_SYS_USER set USERPASS = '{0}' where USERNAME = '{1}'",UserPass,UserName);
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





    //批量更改用户密码，返回记录的条数===================================================================================
    public int UpdatePassAll(string UserName, string UserPass, out string strMsg)
    {
        int nResult = 0;

        try
        {
            /*
            dbSys.Parameters.Clear();
            dbSys.AddParameter("@UserName", UserName);
            dbSys.AddParameter("@UserPass", UserPass);

            nResult = dbSys.ExecuteNonQuery("p_User_pass_update_all");
            strMsg = "操作成功，" + nResult.ToString().Trim() + "条记录变更！";
            */


            string strSql = " update t_sys_User set UserPass = '" + UserPass + "' where UserName in ( " + UserName + " )";
            nResult = DataFunction.ExecuteNonQuery(strSql);
            strMsg = "操作成功，" + nResult.ToString().Trim() + "条记录变更！";


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


    //用户登陆保存信息===========================================================================================
    public int UserLogin(string UserName, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            string UserLoginIp = publ.GetClientIP();
            string UserLoginDate = DateTime.Now.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@UserName", UserName);
            //dbSys.AddParameter("@UserLoginIp", UserLoginIp);
            //dbSys.AddParameter("@UserLoginDate", UserLoginDate);

            //nResult = dbSys.ExecuteNonQuery("p_User_login");    //执行存储过程
            string sql = string.Format("update T_SYS_USER set UserLoginIp = '{0}',UserLoginDate = '{1}' where UserName = '{2}'",UserLoginIp,UserLoginDate,UserName);
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
}



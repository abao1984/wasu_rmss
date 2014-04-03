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
using System.Data.OracleClient;

public class classBranch
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT b.* FROM t_sys_Branch b ";
        StrSql += " WHERE 1 = 1 " + StrTemp;
        return StrSql;
    }
    public DataSet GetRootNodes()
    {
        return DataFunction.FillDataSet("SELECT * FROM t_sys_Branch where PBRANCHCODE = '0' order by displayorder");
    }
    //删除记录，返回记录的条数===================================================================================
    public int Delete(string BranchCode, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@BranchCode", BranchCode);

            //nResult = dbSys.ExecuteNonQuery("p_Branch_delete");
            string sql = string.Format("delete from T_SYS_BRANCH where BRANCHCODE = '{0}'",BranchCode);
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
    public int Insert(string BranchCode, string Jglx_DataDm, string BranchName, string PBranchCode, string BranchLevel, string DisplayOrder, string IsUse, string IsVisible, out string strMsg,string IsQY,string path)
    {
        int nResult = 0;

        try
        {
            //正常代码
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@BranchCode", BranchCode);
            //dbSys.AddParameter("@Jglx_DataDm", Jglx_DataDm);
            //dbSys.AddParameter("@BranchName", BranchName);
            //dbSys.AddParameter("@PBranchCode", PBranchCode);
            //dbSys.AddParameter("@BranchLevel", BranchLevel);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Branch_insert");
            string sql = string.Format("insert into T_SYS_BRANCH values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",ID,BranchCode,BranchName,PBranchCode,BranchLevel,Jglx_DataDm,DisplayOrder,IsUse,IsVisible,IsQY,path);
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


    //编辑记录，返回记录的条数===================================================================================
    public int Update(string BranchCode, string Jglx_DataDm, string BranchName, string PBranchCode, string BranchLevel, string DisplayOrder, string IsUse, string IsVisible, out string strMsg,string isqy,string path)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@BranchCode", BranchCode);
            //dbSys.AddParameter("@Jglx_DataDm", Jglx_DataDm);
            //dbSys.AddParameter("@BranchName", BranchName);
            //dbSys.AddParameter("@PBranchCode", PBranchCode);
            //dbSys.AddParameter("@BranchLevel", BranchLevel);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Branch_update");
            string sql = string.Format(@"update T_SYS_BRANCH set Jglx_DataDm='{0}',BranchName='{1}',PBranchCode='{2}',BranchLevel='{3}',DisplayOrder='{4}',IsUse='{5}',IsVisible='{6}',ISQY = '{8}',PATH='{9}' where BranchCode='{7}'", Jglx_DataDm,BranchName,PBranchCode,BranchLevel,DisplayOrder,IsUse,IsVisible,BranchCode,isqy,path);
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

    //获取子部门所有代码=====================================================================================================
    /* string str_tmp = GetChildBranchStr("3213");
     * 则得到"'3213','321301','321302'"样式的字符串
     */
    public string str_branch_tmp = "";
    public string GetChildBranchStr(string StrTemp)
    {
       
        string tmp_str = "";
        //string my_sql = " select * from Sys_Branch WHERE branch_dm_sj = '" + StrTemp + "' and xy_bj = '1' ORDER BY xh ASC ";
        string my_sql = " select * from t_sys_Branch WHERE IsUse = '1' ORDER BY DisplayOrder ASC ";
        DataSet ds = DataFunction.FillDataSet(my_sql);
        tmp_str = sysBindDataBranch(ds.Tables[0], StrTemp, "");

        return "'" + StrTemp + "'" + tmp_str;
    }
    private string sysBindDataBranch(DataTable dt, string str_branch_dm, string blank)
    {
        DataView dv = new DataView(dt);
        dv.RowFilter = " PBranchCode = '" + str_branch_dm.ToString() + "' ";
        foreach (DataRowView drv in dv)
        {
            str_branch_tmp += ",'" + drv["BranchCode"].ToString() + "'";
            sysBindDataBranch(dt, Convert.ToString(drv["BranchCode"]), blank);
        }
        return str_branch_tmp;
    }
    //==========================================================================================================================
    public string GetCode(string pcode)
    {
        string code = "";
        if (pcode == "root")
        {
            code = DataFunction.GetStringResult("select BranchCode from t_sys_Branch where PBranchCode = '0' order by BranchCode desc");
            if (code.Equals(""))
            {
                code = "10";
            }
            else
            {
                code = (Convert.ToInt32(code) + 1).ToString();
            }
        }
        else
        {
            code = DataFunction.GetStringResult(string.Format("select BranchCode from t_sys_Branch where PBranchCode = '{0}' order by BranchCode desc", pcode));
            if (code.Equals(""))
            {
                code = pcode + "01";
            }
            else
            {
                code = (Convert.ToInt32(code) + 1).ToString();
            }
        }
        return code;
    }

}

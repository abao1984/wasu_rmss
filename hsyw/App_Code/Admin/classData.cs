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

public class classData
{

    //返回查询语句===================================================================================
    public string GetQueryStr(string StrTemp)
    {
        string StrSql = " ";
        StrSql += " SELECT d.* FROM t_sys_Data d ";
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
            str_sql = " delete from t_sys_Data ";
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
    public int Insert(string DataCode, string DataName, string DataDm, string DataMc, string DisplayOrder, string IsUse, string IsVisible,string menu,string zdz, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            System.Guid guid = System.Guid.NewGuid();
            string ID = guid.ToString().Trim();

            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@DataCode", DataCode);
            //dbSys.AddParameter("@DataName", DataName);
            //dbSys.AddParameter("@DataDm", DataDm);
            //dbSys.AddParameter("@DataMc", DataMc);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Data_insert");
            nResult = DataFunction.ExecuteNonQuery(string.Format("insert into T_SYS_DATA values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ID, DataCode, DataName, DataDm, DataMc, DisplayOrder, IsUse, IsVisible,menu,zdz));
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
    public int Update(string ID, string DataMc, string DisplayOrder, string IsUse, string IsVisible,string menu,string zdz, out string strMsg)
    {
        int nResult = 0;

        try
        {
            //正常代码
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@ID", ID);
            //dbSys.AddParameter("@DataMc", DataMc);
            //dbSys.AddParameter("@DisplayOrder", DisplayOrder);
            //dbSys.AddParameter("@IsUse", IsUse);
            //dbSys.AddParameter("@IsVisible", IsVisible);

            //nResult = dbSys.ExecuteNonQuery("p_Data_update");
            nResult = DataFunction.ExecuteNonQuery(string.Format("update T_SYS_DATA set DataMc = '{0}',DisplayOrder = '{1}',IsUse='{2}',IsVisible='{3}',menu = '{5}',ZDZ='{6}' where Id = '{4}'",DataMc,DisplayOrder,IsUse,IsVisible,ID,menu,zdz));
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

    //编辑选用标记，返回记录的条数===================================================================================
    //public int UpdateXybj(string id, string xy_bj, out string strMsg)
    //{
    //    int nResult = 0;

    //    try
    //    {
    //        //正常代码
    //        dbSys.Parameters.Clear();
    //        dbSys.AddParameter("@id", id);
    //        dbSys.AddParameter("@xy_bj", xy_bj);

    //        nResult = dbSys.ExecuteNonQuery("Sys_Data_Update_Xybj");    //执行存储过程
    //        if (nResult == 0)
    //        {
    //            strMsg = "操作失败！";
    //        }
    //        else
    //        {
    //            strMsg = "操作成功，" + nResult.ToString().Trim() + "条记录变更！";

    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        //异常代码
    //        strMsg = publ.GetCatchMsg(e.Message.ToString().Trim());
    //    }
    //    finally
    //    {
    //        //无论异常发生与否，都会执行的代码
    //    }

    //    return nResult;
    //}
    public string GetZDZ(string datacode,string datadm)
    {
        return DataFunction.GetStringResult(string.Format("select ZDZ from T_SYS_DATA where DATACODE='{0}' and DATADM='{1}'",datacode,datadm));
    }

}


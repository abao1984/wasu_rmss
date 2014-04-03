using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Text.RegularExpressions;
//

/// <summary>
/// Public_LRMY 的摘要说明
/// </summary>
public class OleHelp
{
    public OleHelp()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 连接数据字符串
    /// </summary>
    /// <returns></returns>
    public static OleDbConnection OpenConn()
    {
        string ls_str = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + System.Web.HttpContext.Current.Server.MapPath("App_data\\freasy.mdb") + "";
        OleDbConnection ConnData = new OleDbConnection();
        ConnData.ConnectionString = ls_str;
        return ConnData;
    }
}

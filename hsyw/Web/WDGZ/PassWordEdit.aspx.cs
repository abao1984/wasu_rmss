using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

public partial class Web_WDGZ_PassWordEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Web_WDGZ_PassWordEdit));
        TxtUserName.Text=Convert.ToString(Session["USERNAME"]);
    }

    [Ajax.AjaxMethod()]
    public string CheckYsPwd(string pwd,string username)
    {
        string password = publ.MD5(pwd);
        string val = DataFunction.GetStringResult("select count(*) from t_sys_user where username='" + username + "' and userpass='" + password + "'");
        return val;
    }

    [Ajax.AjaxMethod()]
    public void SavePwd(string pwd,string username)
    {
        string password = publ.MD5(pwd);
        DataFunction.ExecuteNonQuery("update t_sys_user set userpass='" + password + "' where username='" + username + "'");
    }
}

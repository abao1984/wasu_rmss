using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


using System.Data.Common;

public partial class App_Ascx_windowHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Msg"] = "";        //系统提示的SESSION

        if (Session["UserName"].ToString().Trim().Length == 0 || Session["UserName"] == null)
        {
            Response.Redirect("../../Login.aspx", false);
        }

        string str_stu = "";
        str_stu += "<script>window.status='";
        str_stu += Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightCompany"].ToString().Trim();

        str_stu += "'</script>";
        Response.Write(str_stu);

    }

}
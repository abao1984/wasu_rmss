using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///AdminBasePage 的摘要说明
/// </summary>
public class AdminBasePage:System.Web.UI.Page
{
	public AdminBasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    #region 初始化
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //验证权限
        if (!this.IsPostBack)
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("~/Admin/login.aspx");
                //Response.Write(@"<script>parent.location='" + Request.ApplicationPath + "/Login.Aspx'</script>");
            }
            else
            {
                string sjggg = Request.QueryString["SJGGG"];
                if (sjggg == null || !sjggg.Trim().Equals(Session["SJGGG"].ToString().Trim()))
                {
                     Session["UserName"] = "";
                     Response.Redirect("~/Admin/login.aspx", true);
                }
            }

        }
        this.Load += new System.EventHandler(this.BasePage_Load);
    }

    #endregion 初始化
    private void BasePage_Load(object sender, System.EventArgs e)
    {
        //Response.Write("<script type='text/javascript' src='" + Request.ApplicationPath + "/config.js'></script>");
        ClientScript.RegisterStartupScript(this.GetType(), "aaa", "<script type='text/javascript' src='" + Request.ApplicationPath + "/config.js'></script>");
    }
    //private string GetIELocationUrl()
    //{
    //    string locationurl = "";
    //    SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();
    //    foreach (SHDocVw.InternetExplorer ie in shellWindows)
    //    {
    //        string filename = System.IO.Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
    //        if (filename.Equals("iexplore") )
    //        {
    //            if(ie.LocationURL.IndexOf("hsyw") > -1)
    //            {
    //                locationurl = ie.LocationURL;
    //            }
    //        }
    //    }
    //    return locationurl;
    //}
}

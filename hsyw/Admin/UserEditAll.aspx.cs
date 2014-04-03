using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserEditAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            OldBtnId.Text = "BtnXGPwd";
            BtnXGPwd.ForeColor = System.Drawing.Color.Red;
            MainFrame.Attributes.Add("src", "UserPassUpdateAll.aspx?code=" + Request.QueryString["code"]);
        }
    }
    //修改密码
    protected void BtnXGPwd_Click(object sender, EventArgs e)
    {
        LinkButton Btn = sender as LinkButton;
        Btn.ForeColor = System.Drawing.Color.Red;
        LinkButton oldBtn = Page.FindControl(OldBtnId.Text) as LinkButton;
        if (oldBtn != null)
        {
            oldBtn.ForeColor = System.Drawing.Color.Black;
        }
        OldBtnId.Text = Btn.ID;
        MainFrame.Attributes.Add("src", "UserPassUpdateAll.aspx?code=" + Request.QueryString["code"]);
    }
    //修改权限
    protected void BtnXGQX_Click(object sender, EventArgs e)
    {
        LinkButton Btn = sender as LinkButton;
        Btn.ForeColor = System.Drawing.Color.Red;
        LinkButton oldBtn = Page.FindControl(OldBtnId.Text) as LinkButton;
        if (oldBtn != null)
        {
            oldBtn.ForeColor = System.Drawing.Color.Black;
        }
        OldBtnId.Text = Btn.ID;
        string code = publ.GetUrlToReceive(Request.QueryString["code"]).Replace("'", "").Substring(1);
        MainFrame.Attributes.Add("src", "UserGroupUpdateAll.aspx?code=" +publ.GetUrlToSend(code));
    }
    //修改访问区域
    protected void BtnXGQY_Click(object sender, EventArgs e)
    {
        LinkButton Btn = sender as LinkButton;
        Btn.ForeColor = System.Drawing.Color.Red;
        LinkButton oldBtn = Page.FindControl(OldBtnId.Text) as LinkButton;
        if (oldBtn != null)
        {
            oldBtn.ForeColor = System.Drawing.Color.Black;
        }
        OldBtnId.Text = Btn.ID;
        MainFrame.Attributes.Add("src", "FWQYtree.aspx?code=" + Request.QueryString["code"] + "&fwqy=");
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserList.aspx", false);
    }
}

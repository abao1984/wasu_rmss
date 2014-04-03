using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MenuPrivate : System.Web.UI.Page
{
    public string BY_WHERE = "", str_sess_code = "", str_sql = "", str_link = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    ClassPrivate cprivate = new ClassPrivate();
    protected void Page_Load(object sender, EventArgs e)
    {
        string pcode = publ.GetUrlToReceive(Request.QueryString["pcode"] == null ? "" : Request.QueryString["pcode"]);
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    str_link = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(str_link, false);
        //}
        //Session["Msg"] = ""; Session["PageNavigator"] = "";
        if (pcode == string.Empty)
        {
            Session["PageSubTite"] = "增加权限";
        }
        else
        {
            Session["PageSubTite"] = "编辑权限";
        }
        Session["PageNavigator"] += "<a href='MenuList.aspx?SJGGG="+Session["SJGGG"]+"' target='_self'>菜单维护</a>";
        Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================
        if (!Page.IsPostBack)
        {
            MENUCODE.Text = publ.GetUrlToReceive(Request.QueryString["code"] == null ? "" : Request.QueryString["code"]);
            PCODE.Text = publ.GetUrlToReceive(Request.QueryString["pcode"] == null ? "" : Request.QueryString["pcode"]);
            FillPage();
        }
    }
    private void FillPage()
    {
        DataSet ds = cprivate.GetPrivateByPCODE(PCODE.Text);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid().ToString();
            string pcode = cprivate.GetPCODE(MENUCODE.Text);
            dr["PCODE"] = pcode;
            dr["MENUCODE"] = MENUCODE.Text;
            dr["MENUNAME"] = cprivate.GetMenuName(MENUCODE.Text);
            dr["PNAME"] = dr["MENUNAME"] + "打开权限";
            dr["XH"] = GetXH(pcode);
            dr["ISUSE"] = "1";
            dr["ISVISIBLE"] = "1";
            ds.Tables[0].Rows.Add(dr);
        }
        else {
            dr = ds.Tables[0].Rows[0];
        }
        ShareFunction.FillControlData(Page, dr);
    }
    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        str_link = "MenuList.aspx?SJGGG="+Session["SJGGG"];
        Response.Redirect(str_link, false);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = cprivate.GetPrivateByPCODE(PCODE.Text);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds,"T_SYS_PRIVATE");
        Session["Msg"] = "<script>alert('保存成功！');location.replace('MenuList.aspx?SJGGG="+Session["SJGGG"]+"');</script>";
    }
    private string GetXH(string pcode)
    {
        string xh = "";
        int len = pcode.Length;
        if (pcode.Substring(len - 2, 1).Equals("0"))
        {
            xh = pcode.Substring(len - 1, 1);
        }
        else
        {
            xh = pcode.Substring(len - 2, 2);
        }
        return xh;
    }
}

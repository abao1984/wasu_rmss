using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class Web_sysBranch_BranchUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classBranch branch = new classBranch();

    protected void Page_Load(object sender, EventArgs e)
    {
        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {
            DropData1.DataCode = "JGLX";
            DropData1.Top = false;

            strWhere = " and b.BranchCode = '" + strSessCode + "' ";

            DataRow dr = DataFunction.GetSingleRow(branch.GetQueryStr(strWhere));

            if (dr != null)
            {

                ZLTextBox_BranchCode.Text = dr["BranchCode"].ToString().Trim();
                ZLTextBox_BranchCode.Enabled = false;

                DropData1.Sel = dr["Jglx_DataDm"].ToString().Trim();
                ZLTextBox_BranchName.Text = dr["BranchName"].ToString().Trim();
                ZLTextBox_BranchLevel.Text = dr["BranchLevel"].ToString().Trim();
                ZLTextBox_DisplayOrder.Text = dr["DisplayOrder"].ToString().Trim();

                RadioButtonList_IsUse.SelectedValue = dr["IsUse"].ToString().Trim();
                RadioButtonList_IsVisible.SelectedValue = dr["IsVisible"].ToString().Trim();
                RadioButtonList_IsQY.SelectedValue = dr["ISQY"].ToString().Trim();

                PATH.Text = System.Web.HttpUtility.UrlDecode(Request.QueryString["path"],System.Text.Encoding.GetEncoding("GB2312"));

                Session["TEMP1"] = dr["PBranchCode"].ToString().Trim();

            }
            else
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");
                Response.Redirect(strLink, false);
            }

            ZLTextBox_BranchName.Focus();


        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "BranchList.aspx";
        Response.Redirect(strLink, false);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string BranchCode = ZLTextBox_BranchCode.Text.ToString().Trim();
        string BranchName = ZLTextBox_BranchName.Text.ToString().Trim();
        string PBranchCode = Session["TEMP1"].ToString().Trim();
        string BranchLevel = ZLTextBox_BranchLevel.Text.ToString().Trim();

        string Jglx_DataDm = DropData1.SelectedValue.ToString().Trim();

        string DisplayOrder = ZLTextBox_DisplayOrder.Text.ToString().Trim(); ;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();
        int index = PATH.Text.LastIndexOf('/');
        if (index == -1)
        {
            PATH.Text = BranchName;
        }
        else
        {
            PATH.Text = PATH.Text.Substring(0, index) + "/" + BranchName;
        }

        n = branch.Update(BranchCode, Jglx_DataDm, BranchName, PBranchCode, BranchLevel, DisplayOrder, IsUse, IsVisible, out strMsg,RadioButtonList_IsQY.SelectedValue,PATH.Text);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "编辑机构";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }
        ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(), "<script>alert('" + strMsg + "');location.replace('BranchList.aspx');</script>");
    }
}

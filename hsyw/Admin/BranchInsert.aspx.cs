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

public partial class Web_sysBranch_BranchInsert : System.Web.UI.Page
{
    public string BY_WHERE = "", str_sess_code = "", str_sql = "", path = "", strMsg = "";
    public int n = 0;
    classLog log = new classLog();
    classBranch branch = new classBranch();

    protected void Page_Load(object sender, EventArgs e)
    {
        str_sess_code = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());
        if (Request.QueryString["path"] != null)
        {
            path = System.Web.HttpUtility.UrlDecode(Request.QueryString["path"], System.Text.Encoding.GetEncoding("GB2312"));
        }
        if (!IsPostBack)
        {

            DropData1.DataCode = "JGLX";
            DropData1.Top = false;

            DropData1.Sel = "1";
            RadioButtonList_IsUse.SelectedValue = "1";
            RadioButtonList_IsVisible.SelectedValue = "1";
            if (str_sess_code == "root")
            {
                ZLTextBox_BranchLevel.Text = "0";
                RadioButtonList_IsQY.SelectedValue = "1";
            }
            else
            {
                int level = str_sess_code.Length / 2;
                ZLTextBox_BranchLevel.Text = level.ToString();
                if (level > 2)
                {
                    RadioButtonList_IsQY.SelectedValue = "0";
                }
                else
                {
                    RadioButtonList_IsQY.SelectedValue = "1";
                }
            }
            ZLTextBox_BranchCode.Text = branch.GetCode(str_sess_code);
        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("BranchList.aspx", false);
    }
    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string BranchCode = ZLTextBox_BranchCode.Text.ToString().Trim();
        if (DataFunction.HasRecord(string.Format("select BRANCHCODE from T_SYS_BRANCH where BRANCHCODE = '{0}'",BranchCode)))
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('该代码已存在，请重新输入！');</script>");
            return;
        }
        string BranchName = ZLTextBox_BranchName.Text.ToString().Trim();
        string BranchLevel = ZLTextBox_BranchLevel.Text.ToString().Trim();

        string Jglx_DataDm = DropData1.SelectedValue.ToString().Trim();

        string PBranchCode = str_sess_code.ToString().Trim();
        if (PBranchCode == "root")
        {
            PBranchCode = "0";
        }
      
        string DisplayOrder = BranchCode;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();
        if (path == "")
        {
            path = BranchName;
        }
        else
        {
            path = path + "/" + BranchName;
        }

        n = branch.Insert(BranchCode, Jglx_DataDm,  BranchName, PBranchCode, BranchLevel, DisplayOrder, IsUse, IsVisible, out strMsg,RadioButtonList_IsQY.SelectedValue,path);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "增加机构";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }
        ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(), "<script>alert('" + strMsg + "');location.replace('BranchList.aspx');</script>");
    }
}

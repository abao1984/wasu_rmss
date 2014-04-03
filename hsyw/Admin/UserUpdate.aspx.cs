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


public partial class Web_sysUser_UserUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classUser user = new classUser();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {
            try
            {
                strWhere = " and u.UserName = '" + strSessCode + "' ";

                DataRow dr = DataFunction.GetSingleRow(user.GetQueryStr(strWhere));


                if (dr != null)
                {

                    ID.Text = dr["ID"].ToString().Trim();
                    ZLTextBox_UserName.Text = dr["UserName"].ToString().Trim();
                   // ZLTextBox_UserName.Enabled = false;

                    ZLTextBox_UserRealName.Text = dr["UserRealName"].ToString().Trim();

                    ZLTextBox_UserRegIp.Text = dr["UserRegIp"].ToString().Trim();
                    ZLTextBox_UserRegIp.Enabled = false;

                    ZLTextBox_UserRegDate.Text = dr["UserRegDate"].ToString().Trim();
                    ZLTextBox_UserRegDate.Enabled = false;
                    BRANCH.Attributes.Add("readonly", "true");
                    BRANCH.Text = dr["PATH"].ToString().Trim();
                    BRANCHCODE.Text = dr["BranchCode"].ToString().Trim();

                    ZLTextBox_UserLoginIp.Text = dr["UserLoginIp"].ToString().Trim();
                    ZLTextBox_UserLoginIp.Enabled = false;

                    ZLTextBox_UserLoginDate.Text = dr["UserLoginDate"].ToString().Trim();
                    ZLTextBox_UserLoginDate.Enabled = false;

                    ZLTextBox_DisplayOrder.Text = dr["DisplayOrder"].ToString().Trim();
                    if (dr["UserPhone"] != DBNull.Value)
                    {
                        ZLTextBox_UserPhone.Text = dr["UserPhone"].ToString().Trim();
                    }
                    if (dr["USERID"] != DBNull.Value)
                    {
                        ZLTextBox_UserId.Text = dr["USERID"].ToString();
                    }
                    RadioButtonList_IsUse.SelectedValue = dr["IsUse"].ToString().Trim();
                    RadioButtonList_IsVisible.SelectedValue = dr["IsVisible"].ToString().Trim();

                    strLink = "";

                }
                else
                {

                    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("参数传递错误！");

                }
            }
            catch (Exception errorMsg)
            {
                strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend(publ.GetCatchMsg(errorMsg.ToString().Trim()));
            }
            finally
            {
                if (strLink.Length > 0)
                {
                    Response.Redirect(strLink, false);
                }
            }



            ZLTextBox_UserRealName.Focus();


        }


    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_UserName.Text.ToString().Trim();
        string UserRealName = ZLTextBox_UserRealName.Text.ToString().Trim();
        
        string BranchCode = BRANCHCODE.Text.Trim();
        string DisplayOrder = ZLTextBox_DisplayOrder.Text.ToString().Trim();

        string UserPhone = ZLTextBox_UserPhone.Text.ToString().Trim();

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();


        n = user.Update(ID.Text,UserName, UserRealName, BranchCode, UserPhone, DisplayOrder, IsUse, IsVisible,ZLTextBox_UserId.Text, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "编辑用户";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('操作成功！');</script>");
    }
}

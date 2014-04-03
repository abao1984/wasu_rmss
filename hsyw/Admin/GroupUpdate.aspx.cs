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

public partial class Web_sysGroup_GroupUpdate : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;

    classGroup group = new classGroup();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
       

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {

            strWhere = " and g.GroupCode = '" + strSessCode + "' ";

            DataRow dr = DataFunction.GetSingleRow(group.GetQueryStr(strWhere));
            try
            {

                if (dr != null)
                {

                    ZLTextBox_GroupCode.Text = dr["GroupCode"].ToString().Trim();
                    ZLTextBox_GroupCode.Enabled = false;

                    ZLTextBox_GroupName.Text = dr["GroupName"].ToString().Trim();
                    ZLTextBox_DisplayOrder.Text = dr["DisplayOrder"].ToString().Trim();

                    RadioButtonList_IsUse.SelectedValue = dr["IsUse"].ToString().Trim();
                    RadioButtonList_IsVisible.SelectedValue = dr["IsVisible"].ToString().Trim();

                    if(dr["GROUPMS"] != DBNull.Value)
                    {
                        GROUPMS.Text = dr["GROUPMS"].ToString();
                    }

                    Session["TEMP1"] = dr["PGroupCode"].ToString().Trim();

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

            ZLTextBox_GroupName.Focus();


        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "GroupList.aspx";
        Response.Redirect(strLink, false);
    }

    protected void Button_Update_Click(object sender, EventArgs e)
    {
        string GroupCode = ZLTextBox_GroupCode.Text.ToString().Trim();
        string GroupName = ZLTextBox_GroupName.Text.ToString().Trim();
        string PGroupCode = Session["TEMP1"].ToString().Trim();

        string DisplayOrder = ZLTextBox_DisplayOrder.Text.ToString().Trim();
        
        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        n = group.Update(GroupCode, GroupName, PGroupCode, DisplayOrder, IsUse, IsVisible,GROUPMS.Text, out strMsg);
        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "编辑系统角色";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('GroupList.aspx');</script>";
        return;
    }
}

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

public partial class Web_sysData_DataInsert : System.Web.UI.Page
{

    public string strWhere = "", strSql = "", strLink = "", strMsg = "", strSessCode = "";
    public int n = 0;
    classData data = new classData();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        ////导航栏=================================================================================
        //if (Session["UserGroup"].ToString().Trim() != "0")
        //{
        //    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
        //    Response.Redirect(strLink, false);
        //} 
        
        //Session["Msg"] = ""; Session["PageNavigator"] = ""; Session["PageSubTite"] = "增加";
        //Session["PageNavigator"] += "<a href='DataList.aspx' target='_self'>数据字典</a>";
        //Session["PageNavigator"] += "<a href='#' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        ////导航栏=================================================================================

        strSessCode = publ.GetUrlToReceive(Request.QueryString["code"].ToString().Trim());

        if (!IsPostBack)
        {

            if (strSessCode == "0")
            {

                ZLTextBox_DataCode.Enabled = true;
                ZLTextBox_DataName.Enabled = true;
                ZLTextBox_DataCode.Focus();

            }
            else
            {

                strWhere = " and ( d.DataCode = '" + strSessCode + "' ) ";

                DataRow dr = DataFunction.GetSingleRow(data.GetQueryStr(strWhere));

                if (dr != null)
                {

                    ZLTextBox_DataCode.Enabled = false;
                    ZLTextBox_DataName.Enabled = false;

                    ZLTextBox_DataCode.Text = dr["DataCode"].ToString().Trim();
                    ZLTextBox_DataName.Text = dr["DataName"].ToString().Trim();
                    ZLTextBox_DataDm.Text = "";
                    ZLTextBox_DataMc.Text = "";

                    ZLTextBox_DataDm.Focus();
                    
                }
                else
                {
                    strLink = "Msg.aspx?code=" + publ.GetUrlToSend("1") + "&msg=" + publ.GetUrlToSend("不允许你操作本模块！");
                    Response.Redirect(strLink, false);
                }

            }


            RadioButtonList_IsUse.SelectedValue = "1";
            RadioButtonList_IsVisible.SelectedValue = "1";

        }

    }

    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        strLink = "DataList.aspx";
        Response.Redirect(strLink, false);
    }
    protected void Button_Insert_Click(object sender, EventArgs e)
    {
        string DataCode = ZLTextBox_DataCode.Text.ToString().Trim();
        string DataName = ZLTextBox_DataName.Text.ToString().Trim();
        string DataDm = ZLTextBox_DataDm.Text.ToString().Trim();
        string DataMc = ZLTextBox_DataMc.Text.ToString().Trim();
        string menu = MENU.Text;
        string DisplayOrder = DataDm;

        string IsUse = RadioButtonList_IsUse.SelectedValue.ToString().Trim();
        string IsVisible = RadioButtonList_IsVisible.SelectedValue.ToString().Trim();

        n = data.Insert(DataCode, DataName, DataDm, DataMc, DisplayOrder, IsUse, IsVisible,menu,ZDZ.Text, out strMsg);

        if (n > 0)
        {
            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "增加数据字典字段";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================
        }

        Session["Msg"] = "<script>alert('" + strMsg + "');location.replace('DataList.aspx');</script>";
        return;
    }
}

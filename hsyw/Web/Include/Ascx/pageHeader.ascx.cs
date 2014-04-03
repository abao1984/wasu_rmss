using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.Common;
//using yueue.ADOKeycap;

public partial class pageHeader : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Msg"] = "";        //系统提示的SESSION

        if (Session["UserName"].ToString().Trim().Length == 0 || Session["UserName"] == null)
        {
            Response.Redirect("../../Login.aspx", false);
        }
        else
        {
            string str_stu = "";
            str_stu += "<script>window.status='";
            str_stu += Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightCompany"].ToString().Trim();

            str_stu += "'</script>";
            Response.Write(str_stu);
        }



        //===============================================================================
        //IP或域名绑定
        if (Session["BoolUrl"].ToString().Trim() == "1")
        {
            string host_ip = HttpContext.Current.Request.Url.Host.ToString().Trim();      //得到当前的域名或IP地址

            if (host_ip != Session["ServerUrl"].ToString().Trim())       //IP址或域名进行绑定处理    
            {
                Session["Msg"] = "<script>alert('" + host_ip + "," + Session["ServerUrl"].ToString().Trim() + "你为盗版软件的受害者! 若得到本系统正版软件，请与软件开发人员联系，谢谢！')</script>";
            }
        }

        //使用时间限制
        if (Session["BoolDate"].ToString().Trim() == "1")
        {
            TimeSpan tem_days = DateTime.Parse(Session["DateEnd"].ToString().Trim()) - DateTime.Now;
            int tem_int_ts = tem_days.Days;

            if (Session["BoolDateMsg"].ToString().Trim() == "1" && tem_int_ts < Convert.ToInt32(Session["DateMsg"].ToString().Trim()))
            {
                Session["Msg"] = "<script>alert('软件即将到期，您还有" + tem_int_ts.ToString().Trim() + "天使用时间！');</script>";
            }

            if (DateTime.Now > DateTime.Parse(Session["DateEnd"].ToString().Trim()))
            {
                Session["Msg"] = "<script>alert('软件使用到期，谢谢使用！');window.parent.location.replace('../../" + "Logout.aspx');</script>";
            }
        }
        //===============================================================================


        //权限控制=================================================================================

        string tmpFileName = "", tmpMenuCode = "", tmpGroupMenu = "", strLink = "";
        tmpFileName = System.IO.Path.GetFileName(Request.Path);    //得到目前执行的文件名
        Session["UrlName"] = tmpFileName;

        if (tmpFileName != "Msg.aspx" && tmpFileName != "Default.aspx" && tmpFileName != "MainPassSelf.aspx" && tmpFileName != "About.aspx")                //对系统提示文件,默认首页,用户更改自已密码,不做限制
        {
            tmpGroupMenu = Session["GroupMenu"].ToString().Trim();  //得到用户组菜单和用户菜单

            DataRow dr = DataFunction.GetSingleRow(" SELECT * FROM t_sys_Menu WHERE FileName = '" + tmpFileName + "' ");

            if (dr != null)
            {
                tmpMenuCode = "'" + dr["MenuCode"].ToString().Trim() + "'";             //加上引号是防止代码重复
                if (tmpGroupMenu.IndexOf(tmpMenuCode) > -1)                          //菜单验证
                {
                }
                else
                {
                    strLink = "Msg.aspx?flg=1&msg=" + "不允许你操作本模块！";
                    Response.Redirect(strLink, false);
                }
            }
            else
            {
                strLink = "Msg.aspx?flg=1&msg=" + "不允许你操作本模块！";
                Response.Redirect(strLink, false);
            }
        }

        //权限控制=================================================================================

    }

}
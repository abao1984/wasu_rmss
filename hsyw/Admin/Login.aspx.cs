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

public partial class Admin_AdminLogin : System.Web.UI.Page
{
    public string strWhere = "", strSessCode = "", strSql = "", strLink = "", strMsg = "";
    public int n = 0;
    classAdmin admin = new classAdmin();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        //导航栏=================================================================================
        Session["PageSubTite"] = "登陆系统";
        string this_filename = "";
        this_filename = System.IO.Path.GetFileName(Request.Path);    //得到目前执行的文件名
        Session["PageNavigator"] = "";
        Session["PageNavigator"] += "<a href='" + this_filename + "' target='_self'>" + Session["PageSubTite"].ToString().Trim() + "</a>";
        //导航栏=================================================================================

        if (Session["BoolYzm"].ToString().Trim() == "1")
        {
            Label3.Visible = true;
            IMG1.Src = "SysCheckCode.aspx";
            ZLTextBox_CheckCode.Visible = true;
            IMG1.Visible = true;
        }
        else
        {
            Label3.Visible = false;
            ZLTextBox_CheckCode.Visible = false;
            IMG1.Visible = false;
        }

        if (!IsPostBack)
        {

            Session["SJGGG"] = Guid.NewGuid().ToString();//随机产生的guid
            if (Session["UserName"].ToString().Trim().Length == 0 || Session["UserName"] == null)
            {

            }
            else
            {
                Response.Redirect("Main.aspx", false);
            }

        }

        ZLTextBox_UserName.Focus();

    }

    protected void Button_Post_Click(object sender, EventArgs e)
    {
        string UserName = ZLTextBox_UserName.Text.ToString().Trim();
        string AdminPass = ZLTextBox_UserPass.Text.ToString().Trim();

        AdminPass = publ.MD5(AdminPass);

        //验证码=====================================================================================
        if (Session["BoolYzm"].ToString().Trim() == "1")
        {
            if (Session["CheckCode"] == null)
            {
                strMsg = "<script>alert('系统错误，不能生成验证码！')</script>";
                return;
            }
            if (ZLTextBox_CheckCode.Text.ToString().Trim() == "")
            {
                strMsg = "<script>alert('请输入验证码！')</script>";
                ZLTextBox_CheckCode.Focus();
                return;
            }

            if (String.Compare(Session["CheckCode"].ToString(), ZLTextBox_CheckCode.Text.ToString().Trim(), true) != 0)
            {
                strMsg = "<script>alert('验证码错误，请输入正确的验证码！')</script>";
                ZLTextBox_CheckCode.Text = "";
                ZLTextBox_CheckCode.Focus();
                return;
            }
        }
        //验证码=====================================================================================

        strWhere = "";
        strWhere += " and a.UserName = '" + UserName + "' and a.AdminPass = '" + AdminPass + "'";
        strWhere += " order by a.DisplayOrder asc ";

        DataRow dr = DataFunction.GetSingleRow(admin.GetQueryStr(strWhere));
        if (dr != null)
        {

            //=============================================================== 
            Session["PageTiteSub"] = "";                //子标题所用
            Session["PageNavigator"] = "";              //导航栏
            Session["UrlName"] = "";                    //客户端所执行的文件
            //=============================================================== 

            //Session["UserGroup"] = "";                  //用户所在组(角色)

            Session["UserIP"] = publ.GetClientIP();                                 //客户端登陆的IP
            Session["UserName"] = dr["UserName"].ToString().Trim();		            //用户代码
            Session["UserRealName"] = dr["UserRealName"].ToString().Trim();	        //用户名称
            Session["BranchCode"] = dr["BranchCode"].ToString().Trim();	            //用户所属部门代码
            Session["BranchName"] = dr["BranchName"].ToString().Trim();	            //用户所属部门名称
            Session["UserGroup"] = dr["UserGroup"].ToString().Trim();	            //超管标志
            //=============================================================== 

            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "登陆后台";
                string LogMemo = "登陆后台";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================

            Response.Redirect("Main.aspx", false);
        }
        else
        {
            errMsg();
        }
    }

    public void errMsg()
    {

        //=============================================================== 
        Session["PageTiteSub"] = "";                //子标题所用
        Session["PageNavigator"] = "";              //导航栏
        Session["UrlName"] = "";                    //客户端所执行的文件
        //=============================================================== 

        Session["UserIP"] = "";         	        //客户端IP

        Session["UserName"] = "";                  //管理员用户名
        Session["UserRealName"] = "";              //管理员真实姓名
        Session["UserGroup"] = "";                 //管理员组
        Session["BranchCode"] = "";            //所管理辖的部门代码
        Session["BranchName"] = "";            //所管理辖的部门代码
        //=============================================================== 

        strMsg = "<script>alert('用户名或密码错误!')</script>";
        return;
    }
}
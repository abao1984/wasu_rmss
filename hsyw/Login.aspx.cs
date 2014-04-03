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
    classUser user = new classUser();
    classGroup group = new classGroup();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Session["SJGGG"] = Guid.NewGuid().ToString();//随机产生的guid
            if (Session["UserName"].ToString().Trim().Length == 0 || Session["UserName"] == null)
            {

            }
            else
            {
              //  Response.Redirect("Main.aspx?SJGGG=" + Session["SJGGG"].ToString(), false);
            }

        }

        ZLTextBox_UserName.Focus();

    }

    protected void Button_Post_Click(object sender, EventArgs e)
    {

       

        string UserName = ZLTextBox_UserName.Text.ToString().Trim();
        string UserPass = ZLTextBox_UserPass.Text.ToString().Trim();

        UserPass = publ.MD5(UserPass);
        //Response.Write(UserPass);
        strWhere = "";
        strWhere += " and u.UserName = '" + UserName + "' and u.UserPass = '" + UserPass + "'";
        strWhere += " order by u.DisplayOrder asc ";


        DataRow dr = DataFunction.GetSingleRow(user.GetQueryStr(strWhere));
        if (dr != null)
        {

            //=============================================================== 
            Session["PageTiteSub"] = "";                //子标题所用
            Session["PageNavigator"] = "";              //导航栏
            Session["UrlName"] = "";                    //客户端所执行的文件
            //=============================================================== 

            Session["TEMP1"] = "";                      //临时SESSION
            Session["TEMP2"] = "";                      //临时SESSION
            Session["TEMP3"] = "";                      //临时SESSION
            Session["TEMP4"] = "";                      //临时SESSION

            Session["PageSubTite"] = "";                //子标题所用
            Session["PageNavigator"] = "";              //导航栏
            Session["UrlName"] = "";                    //客户端所执行的文件
            //=============================================================== 

            Session["UserIP"] = publ.GetClientIP();                                 //客户端登陆的IP
            Session["UserName"] = dr["UserName"].ToString().Trim();		            //用户代码
            Session["UserID"] = dr["ID"].ToString().Trim();		                //用户ID
            Session["UserRealName"] = dr["UserRealName"].ToString().Trim();	        //用户名称
            Session["BranchCode"] = dr["BranchCode"].ToString().Trim();	            //用户所属部门代码
            Session["BranchName"] = dr["BranchName"].ToString().Trim();	            //用户所属部门名称
            
            Session["ISSUPER"] = dr["ISSUPER"].ToString();
            Session["FWQY"] = null;
            if (dr["FWQY"] != DBNull.Value)
            {
                Session["FWQY"] = dr["FWQY"].ToString();
            }
            //=============================================================== 

            //保存登陆时间
            n = user.UserLogin(Session["UserName"].ToString().Trim(), out strMsg);


            //获取用户组和相关菜单
            Session["UserGroup"] = user.GetGroupCode(UserName, out strMsg);
            Session["GroupMenu"] = group.GetGroupMenu(Session["UserGroup"].ToString().Trim(), out strMsg);


            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "登陆系统";
                string LogMemo = "登陆系统";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);
                
            }
            //写日志文件结束====================================================================
            //System.Web.Security.FormsAuthentication.SetAuthCookie("hsyw", false);

            //if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            //{
            //    Response.Redirect("Main.aspx?SJGGG=" + Session["SJGGG"].ToString(), false);
            //}
            //else
            //{
            //    Response.Redirect(Convert.ToString(Request.QueryString["ReturnUrl"]));
            //}
            Response.Redirect("Main.aspx?SJGGG=" + Session["SJGGG"].ToString(), false);
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

        Session["TEMP1"] = "";                      //临时SESSION
        Session["TEMP2"] = "";                      //临时SESSION
        Session["TEMP3"] = "";                      //临时SESSION
        Session["TEMP4"] = "";                      //临时SESSION

        Session["PageSubTite"] = "";                //子标题所用
        Session["PageNavigator"] = "";              //导航栏
        Session["UrlName"] = "";                    //客户端所执行的文件
        //=============================================================== 

        Session["UserGroup"] = "";                  //用户所在组(角色)
        Session["GroupMenu"] = "";                  //用户所在组菜单

        Session["UserIP"] = "";         	        //客户端IP
        Session["UserID"] = "";                     //用户ID
        Session["UserName"] = "";		            //用户代码
        Session["UserRealName"] = "";               //用户名称
        Session["BranchCode"] = "";                 //用户所属部门代码
        Session["BranchName"] = "";                 //用户所属部门名称
        //=============================================================== 
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('密码错误');</script>");
        return;
    }
}
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

public partial class Admin_Logout : System.Web.UI.Page
{
    classLog log = new classLog();
    protected void Page_Load(object sender, EventArgs e)
    {

        //写日志文件开始====================================================================
        if (Session["BoolLog"].ToString().Trim() == "1")
        {
            string LogStrMsg = "";
            //LogUserName-人员 LogTitle-标题  LogMemo-内容
            string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
            LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
            string LogTitle = "退出后台";
            string LogMemo = "";
            log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

        }
        //写日志文件结束====================================================================


        //=============================================================== 
        Session["TEMP1"] = "";                      //临时SESSION
        Session["TEMP2"] = "";                      //临时SESSION
        Session["TEMP3"] = "";                      //临时SESSION
        Session["TEMP4"] = "";                      //临时SESSION

        Session["PageSubTite"] = "";                //子标题所用
        Session["PageNavigator"] = "";              //导航栏
        Session["UrlName"] = "";                    //客户端所执行的文件

        Session["UserName"] = "";                  //管理员用户名
        Session["UserRealName"] = "";              //管理员真实姓名
        Session["UserGroup"] = "";                 //管理员组
        Session["BranchCode"] = "";            //所管理辖的部门代码
        Session["BranchName"] = "";            //所管理辖的部门代码
        //=============================================================== 

        Response.Redirect("Login.aspx");
    }
}

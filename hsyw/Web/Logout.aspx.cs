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

public partial class Web_Logout : System.Web.UI.Page
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
            string LogTitle = "退出系统";
            string LogMemo = "退出系统";
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
        //=============================================================== 

        Session["UserGroup"] = "";                  //用户所在组(角色)
        Session["GroupMenu"] = "";                  //用户所在组菜单

        Session["UserIP"] = "";         	        //客户端IP
        Session["UserID"] = "";                     //用户ID
        Session["UserName"] = "";		            //用户代码
        Session["UserRealName"] = "";               //用户名称
        Session["BranchCode"] = "";                 //用户所属部门代码
        Session["BranchName"] = "";                 //用户所属部门名称

        Response.Redirect("../Login.aspx");
    }
}

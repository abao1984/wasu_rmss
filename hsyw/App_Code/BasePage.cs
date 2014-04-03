using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.UI;

public class BasePage : System.Web.UI.Page
{

    protected bool validate = true;
    protected bool DemoCheck = true;
    protected bool _HasParentPage = true;
    protected bool _HasPageLoad = true;

    protected override void OnPreRender(EventArgs e)
    {
        //应用样式表
        //if(!this.IsClientScriptBlockRegistered("css"))
        //this.RegisterClientScriptBlock("css",@"<LINK href='../../Styles/blueformalToolbar.css' type='text/css' rel='stylesheet'>");

        base.OnPreRender(e);
    }

    #region 验证
    public bool DefaultValidate
    {
        get
        {
            return this.validate;
        }
        set
        {
            this.validate = value;
        }

    }
    #endregion

    #region 设置是否有框架既parent.page
    public bool HasParentPage
    {
        get
        {
            return this._HasParentPage;
        }
        set
        {
            this._HasParentPage = value;
        }

    }
    #endregion

    #region 设置是否要执行Page_Load
    public bool HasPageLoad
    {
        get
        {
            return this._HasPageLoad;
        }
        set
        {
            this._HasPageLoad = value;
        }

    }
    #endregion

    #region 初始化
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string url = " " + this.Request.Url.ToString().ToLower();
        //验证权限
        if (validate)
        {

            if (!this.IsPostBack)
            {
                if (Session["UserName"] == "")
                {
                    //Response.Redirect("~/login.aspx");
                   Response.Write(@"<script>window.top.location='" + Request.ApplicationPath + "/Login.Aspx'</script>");
                }
                else
                {
                    this.validate = true;
                    //string sjggg = Request.QueryString["SJGGG"];
                    //if (sjggg == null || !sjggg.Trim().Equals(Session["SJGGG"].ToString().Trim()))
                    //{
                    //    Session["UserName"] = "";
                    //    Response.Write(@"<script>window.top.location='" + Request.ApplicationPath + "/Login.Aspx'</script>");
                    //}
                    
                }
                
            }

        }

        this.Load += new System.EventHandler(this.BasePage_Load);
    }

    #endregion 初始化

    #region 页面出错处理
    protected void BasePage_Error(object sender, System.EventArgs e)
    {
        string errMsg;
        Exception currentError = Server.GetLastError();

        errMsg = "<link rel=\"stylesheet\" href=\"" + Request.ApplicationPath + "/Styles/mainStyle.css\">";
        errMsg += "<h1>页面出错[Page Error]</h1><hr/>本页发生一个异常 系统管理员请注意." +
            "请联系我们以便更好的解决此问题" +
            ".<br/>" +
            "An unexpected error has occurred on this page.The system administrators have been notified.Please feel free to contact us with the information surrounding this error." +
            "<br/>" +
            "页面发生于[The error occurred in]: " + Request.Url.ToString() + "<br/>" +
            "出错信息[Error Message]: <font class=\"Alert\">" + currentError.Message.ToString() + "</font><br/>" +
            "引发异常的方法[Error Method]:" + currentError.TargetSite.ToString() + "<hr/>" +

            "<b> 堆栈跟踪[Stack Trace]:</b><br/>" +
            currentError.ToString();

        Response.Write(errMsg);
        Server.ClearError();

    }

    #endregion 页面出错处理结束


 
    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BasePage_Load(object sender, System.EventArgs e)
    {
        Response.Write("<script type='text/javascript' src='" + Request.ApplicationPath + "/config.js'></script>");
        if (_HasPageLoad)
        {
            //定义CSS文件引用
            //string cssFile = "<link rel=\"stylesheet\" href=\"" + Request.ApplicationPath + "/Styles/mainStyle.css\">";
            //Response.Write(cssFile);
        }
    }

    #region 关闭本窗口









    /// <summary>
    /// 关闭本窗口,关闭前清楚页面中的Session
    /// </summary>
    protected virtual void Close()
    {
        ClearSession();
        this.Response.Write("<script language=javascript>self.close();</script>");
        //            if(!this.IsStartupScriptRegistered("closeWindow"))
        //            {
        //                this.RegisterStartupScript("closeWindow","<script language=javascript>self.close();</script>");
        //            }
    }
    /// <summary>
    /// 初始化Session中的数据,如果继承的窗体有session数据,请重载









    /// </summary>
    protected virtual void InitialSession()
    {

    }
    /// <summary>
    /// 关闭窗口时清楚Session中的数据,需要重载









    /// </summary>
    protected virtual void ClearSession()
    {
    }
    #endregion

    public virtual bool SavePage()
    {
        return true;
    }
    
    public void ClientAlert(Page page, string message, bool isClosePage)
    {
        string script = "<script>alert('" + message + "');";
        if (isClosePage)
            script += "window.close();";
        script += "</script>";
        page.ClientScript.RegisterStartupScript(page.GetType(), Guid.NewGuid().ToString(), script);
    }

    #region 获取当前用户
    public string GetUserName()
    {
        return "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
    }
    #endregion

    #region 获取客户端IP
    public  string GetClientIP()
    {
        string userIP;
        HttpRequest Request = HttpContext.Current.Request;
        // 如果使用代理，获取真实IP
        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
            userIP = Request.ServerVariables["REMOTE_ADDR"];
        else
            userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (userIP == null || userIP == "")
            userIP = Request.UserHostAddress;
        return userIP;
    }
    #endregion

    #region 判断当前用户权限编码
    public  bool IsHavePrivate( string pcode)
    {
        if (Session["ISSUPER"].ToString() == "1")
        {
            return true;
        }
        else
        {
            string username = Session["UserName"].ToString();
            return DataFunction.HasRecord(string.Format(@"select p.* from t_sys_private p left join t_sys_r_groupprivate gp on gp.pcode = p.pcode left join t_Sys_r_Usergroup ug on ug.groupcode = gp.groupcode where ug.username = '{0}' and p.pcode = '{1}'", username, pcode));
        }
    }
    #endregion
}
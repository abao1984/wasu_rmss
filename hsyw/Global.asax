<%@ Application Language="C#" %>
<%@ Import Namespace= "System.Web.Security" %> 

    
<script runat="server">

    //定义全开始时间和终止时间
    DateTime BeginTime, EndTime;
        
    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码

        try
        {
            Aspose.Cells.License cellLic = new Aspose.Cells.License();
            Aspose.Words.License wordLic = new Aspose.Words.License();
            string filePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "Aspose.Total.lic";
            cellLic.SetLicense(filePath);
            wordLic.SetLicense(filePath);

        }
        catch
        {
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
    
    
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {

        //防止SQL注入==========================================================================================
        //SQL防注入
        string Sql_1 = "exec|insert+|select+|delete|update|count|chr|mid|master+|truncate|char|declare|drop+|drop+table|creat+|creat+table";
        string Sql_2 = "exec+|insert+|delete+|update+|count(|count+|chr+|+mid(|+mid+|+master+|truncate+|char+|+char(|declare+|drop+|creat+|drop+table|creat+table";
        string[] sql_c = Sql_1.Split('|');
        string[] sql_c1 = Sql_2.Split('|');

        if (Request.QueryString != null)
        {
            foreach (string sl in sql_c)
            {
                if (Request.QueryString.ToString().ToLower().IndexOf(sl.Trim()) >= 0)
                {
                    Response.Write("警告！你的IP已经被记录!");//吓唬人的
                    Response.Write(sl);
                    Response.Write(Request.QueryString.ToString());
                    //System.Windows.Forms.MessageBox.Show("禁止提交外部数据","1",System.Windows.F
                    //orms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
                    //Response.Redirect("http://www.163.com");
                    Response.End();
                    break;
                }
            }
        }

        if (Request.Form.Count > 0)
        {

            string s1 = Request.ServerVariables["SERVER_NAME"].Trim();//服务器名称
            if (Request.ServerVariables["HTTP_REFERER"] != null)
            {
                string s2 = Request.ServerVariables["HTTP_REFERER"].Trim();//http接收的名称
                string s3 = "";
                if (s1.Length > (s2.Length - 7))
                {
                    s3 = s2.Substring(7);
                }
                else
                {
                    s3 = s2.Substring(7, s1.Length);
                }
                if (s3 != s1)
                {
                    Response.Write("你的IP已被记录！警告！");//吓人的
                    //System.Windows.Forms.MessageBox.Show("禁止提交外部数据","1",System.Windows.Forms.MessageBoxButtons.OK,Sy
                    //stem.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
                    //Response.Redirect("http://www.163.com");
                    Response.End();
                }
            }
        }
        //=======================================================================================================================================




        BeginTime = DateTime.Now; 
    }


    protected void Application_EndRequest(Object sender, EventArgs e)
    {

        EndTime = DateTime.Now;
        //Session["ProTime"] = (EndTime - BeginTime).Milliseconds.ToString().Trim();     //得到当面页面执行时间

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

        /*
    
        Exception objErr = Server.GetLastError().GetBaseException();
        string error = "发生异常页: " + Request.Url.ToString() + "<br>";
        error += "异常信息: " + objErr.Message + "<br>";
        Server.ClearError();
        Application["error"] = error;
        Response.Redirect("~/Include/ErrorPage.aspx"); 
        */

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

        //系统SESSION=======================================================
        //制作公司信息
        Session["CopyRightCompany"] = "";                                      //制作公司信息
        Session["CopyRightAuthor"] = "孙志华";                                                           //作者
        Session["CopyRightPhone"] = "13605803864";                                                  //联系电话
        Session["CopyRightQQ"] = "24558147";                                                  //联系电话
        
        //软件信息
        Session["PageTitle"] = "华数网通运维支撑系统";                                                 //软件名称
        Session["PageVersion"] = "2011-01-01";                                                           //软件版本
        
        //用户信息
        Session["ClientName"] = "华数网通公司";                                            //使用单位
        Session["ClientCode"] = "3213";                                                                //使用单位代码
        
        
        Session["DataBase"] = "ABIS_BASE";                                           		                   //数据库名称
        Session["PageSize"] = "10";                                     		                           //分页大小
        Session["Msg"] = "";                                                                               //提示全局变量
        //===============================================================

        //软件设置==========================================================
        Session["BoolLog"] = "1";												                            //是否记录系统日志 0－不记录  1－记录
        Session["BoolYzm"] = "0";                                                                           //后台登陆是否出现验证码 
        
        //使用IP或域名限制
        Session["BoolUrl"] = "0";												                            //是否域名绑定 0－不绑定  1－绑定 
        Session["ServerUrl"] = "127.0.0.1";                                   		                        //域名绑定地址

        //使用期限限制
        Session["BoolDate"] = "0";												                            //是否有使用期限 0－无限制 1－有试用期
        Session["DateEnd"] = "2100-01-01";                               		                            //软件到期时间 
        Session["BoolDateMsg"] = "1";												                        //是否事先提示 0－不提示 1－提示，此设置只有Session["BoolDate"] = "1"情况下有效
        Session["DateMsg"] = "30";												                            //在多少天犯围内提示？
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
        Session["UserID"] = "";                     //用户ID，用户操作标识
        Session["UserName"] = "";		            //用户代码
        Session["UserRealName"] = "";               //用户名称
        Session["BranchCode"] = "";                 //用户所属部门代码
        Session["BranchName"] = "";                 //用户所属部门名称

        //Session["UserName"] = "";                  //管理员用户名
        //Session["UserRealName"] = "";              //管理员真实姓名
        //Session["UserGroup"] = "";                 //管理员组
        //Session["BranchCode"] = "";            //所管理辖的部门代码
        //Session["BranchName"] = "";            //所管理辖的部门名称
        //=============================================================== 
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

        Response.Redirect("~/Login.aspx");

    }


    //以下代码段为在线人数
    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {

        string strUserName = string.Empty;
        /*
        if (Request.IsAuthenticated)
        {
            FormsIdentity identity = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = identity.Ticket;
            strUserName = User.Identity.Name;
            strUserName = Session["UserName"].ToString().Trim();
            
        }
        else
        {
            strUserName = Request.UserHostAddress;
            strUserName = "游客";
        }


        if (Session["UserName"].ToString().Trim() != "")
        {
            strUserName = Session["UserName"].ToString().Trim();
        }
        else
        {
            strUserName = "游客";
        }

        Response.Write(strUserName);
 */       
        /*
        MemberOnlineInfo objOnline = new MemberOnlineInfo(strUserID, Request.UserHostAddress, DateTime.Now.ToString(), Request.FilePath);

        MemberAccount account = new MemberAccount();
        if (!account.CheckUserOnline(strUserID))
            account.AddOnline(objOnline);
        else
            account.UpdateOnline(objOnline);

        //删除超时的会员
        account.DeleteOnline();

         */ 
    }


       
</script>

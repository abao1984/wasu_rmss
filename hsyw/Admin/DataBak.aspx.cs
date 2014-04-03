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

public partial class Admin_Sys_DataBase_Bak : System.Web.UI.Page
{
    public string str_BakFileName = "", str_DataBaseName = "", strLink = "";
    public string str_BakFileName_ls1 = "", str_BakFileName_ls2 = "";
    public int n = 0;
    //Database dbSys = DataBase.GetData();
    classLog log = new classLog();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //TextBox1.Text = DateTime.Now.ToShortDateString();
            TextBox1.Text = Session["DataBase"].ToString().Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".dat";
            Button2.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Label_BAK.Text = "正在备份请稍候... ...，备份可能需要几到十几分钟的时间(视你的数据量大小而定)！";
        Label_BAK.Visible = true;

        str_BakFileName = Server.MapPath("../UserFiles/") + TextBox1.Text.ToString().Trim();      //备份目标文件名
        str_DataBaseName = Session["DataBase"].ToString().Trim();                                                               //需要备份的数据库名称

        if (System.IO.File.Exists(str_BakFileName))
        {
            str_BakFileName_ls1 = TextBox1.Text.ToString().Trim();
            str_BakFileName_ls2 = Session["DataBase"].ToString().Trim() + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".dat";
            TextBox1.Text = str_BakFileName_ls2;
            Label_BAK.Text = "已经存在此文件：" + str_BakFileName_ls1 + "，备份文件已经被自动改名为：" + str_BakFileName_ls2 + "请重新备份！";
            Label_BAK.Visible = true;

            return;

        }

        try
        {
            //dbSys.Parameters.Clear();
            //dbSys.AddParameter("@BakFileName", str_BakFileName);
            //dbSys.AddParameter("@DataBaseName", str_DataBaseName);
            //dbSys.ExecuteNonQuery("p_DataBase_bak");    //执行存储过程


            //写日志文件开始====================================================================
            if (Session["BoolLog"].ToString().Trim() == "1")
            {
                string LogStrMsg = "";
                //LogUserName-人员 LogTitle-标题  LogMemo-内容
                string LogUserName = Session["UserRealName"].ToString().Trim() + "[" + Session["UserName"].ToString().Trim() + "]";
                LogUserName += "  " + Session["BranchName"].ToString().Trim() + "[" + Session["BranchCode"].ToString().Trim() + "]";
                string LogTitle = "数据备份";
                string LogMemo = "";
                log.Insert(LogUserName, LogTitle, LogMemo, out LogStrMsg);

            }
            //写日志文件结束====================================================================

            TextBox1.Enabled = false;
            Label_BAK.Text = "备份成功，服务器备份文件为：" + str_BakFileName;
            Button2.Visible = true;
            Session["Msg"] = "<script>alert('备份成功，请及时将备份文件异地再次备份！');</script>";
        }
        catch (Exception errorMsg)
        {
            Label_BAK.Text = "备份出错，请与系统管理员联系！";
            //Session["Msg"] = "<script>alert('备份出错，请与系统管理员联系！');</script>";
            Session["Msg"] = "<script>alert('" + publ.GetCatchMsg(errorMsg.ToString().Trim()) + "');</script>";
            Button2.Visible = false;

        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string host_ip = HttpContext.Current.Request.Url.Host.ToString().Trim();      //得到当前的域名或IP地址
        //string filename = "http://" + host_ip + "/UserFiles/" + TextBox1.Text.ToString().Trim();

        string filename = "http://";
        filename += HttpContext.Current.Request.Url.Host.ToString().Trim();        //得到域名或IP
        filename += ":";
        filename += HttpContext.Current.Request.Url.Port.ToString().Trim();        //得到端口号

        string floder = HttpRuntime.AppDomainAppVirtualPath.ToString().Trim();                    //得到虚拟目录
        if (floder != "/")
        {
            filename += floder + "/";
        }
        else
        {
            filename += "/";
        }

        filename += "UserFiles/" + TextBox1.Text.ToString().Trim();

        strLink = filename;
        Response.Redirect(strLink, false);

    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Admin_WindowMain : System.Web.UI.Page
{
    protected string str_sess_url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        str_sess_url = Request.QueryString["url"].ToString().Trim();
        str_sess_url = Server.UrlDecode(str_sess_url);

        if (!IsPostBack)
        {

            Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
            Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
            Response.Write("<head>\r\n");

            Response.Write("<meta http-equiv=\"Pragma\" content=\"no-cache\" />\r\n");
            Response.Write("<meta http-equiv=\"expires\" content=\"0\" />\r\n");
            
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />\r\n");
            Response.Write("\r\n");
            Response.Write("<title>" + Session["PageTitle"].ToString().Trim() + "</title>\r\n");
            Response.Write("</head>\r\n");

            Response.Write("<FRAMESET border=\"0\" FRAMESPACING=\"0\" TOPMARGIN=\"0\" LEFTMARGIN=\"0\" MARGINHEIGHT=\"0\" MARGINWIDTH=\"0\">");
            Response.Write("        <frame name=\"MainFrame\" src=\"" + str_sess_url + "\" SCROLLING=\"no\" FRAMEBORDER=\"0\" MARGINWIDTH=\"0\" MARGINHEIGHT=\"0\" TOPMARGIN=\"0\" LEFTMARGIN=\"0\" style=\"border: 1px solid #808080\" target=\"_self\"></frame>");
            Response.Write("</FRAMESET>");
            Response.Write("<noframes></noframes><noframes>您的浏览器不支持框架显示</noframes>");

            Response.Write("</html>");
        }
    }
}
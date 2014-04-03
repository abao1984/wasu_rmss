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

public partial class Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserName"].ToString().Trim().Length == 0 || Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx", false);
        }

        if (!IsPostBack)
        {

            Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
            Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
            Response.Write("<head>\r\n");

            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />\r\n");
            Response.Write("\r\n");
            Response.Write("<title>" + Session["PageTitle"].ToString().Trim()  + "</title>\r\n");
            Response.Write("</head>\r\n");

            Response.Write("<FRAMESET rows=\"31,*\" border=\"0\" frameborder=\"0\" FRAMESPACING=\"0\" TOPMARGIN=\"0\" LEFTMARGIN=\"0\" MARGINHEIGHT=\"0\" MARGINWIDTH=\"0\">");
            Response.Write("    <FRAME noresize name=\"TopFrame\" src=\"MainTop.aspx\" scrolling=\"no\" border=\"0\" frameborder=\"no\"  TOPMARGIN=\"0\" LEFTMARGIN=\"0\" MARGINHEIGHT=\"0\" MARGINWIDTH=\"0\" ></FRAME>");
            Response.Write("    <FRAMESET cols=\"200,*\" framespacing=\"4\" frameborder=\"1\" name=\"tFrame\"  TOPMARGIN=\"0\"  LEFTMARGIN=\"0\" MARGINHEIGHT=\"0\" MARGINWIDTH=\"0\" style=\"border: 0px solid #808080\" bordercolor=\"#ffffff\" >");
            Response.Write("        <frame src=\"MainLeft.aspx\" name=\"LeftFrame\" SCROLLING=\"NO\" FRAMEBORDER=\"0\" MARGINWIDTH=\"0\" MARGINHEIGHT=\"0\" TOPMARGIN=\"0\" LEFTMARGIN=\"0\" style=\"border: 1px solid #AAAAAA\" target=\"_self\"></frame>");
            Response.Write("        <frame id=\"MainFrame\" name=\"MainFrame\" src=\"Default.aspx\" SCROLLING=\"auto\" FRAMEBORDER=\"0\" MARGINWIDTH=\"0\" MARGINHEIGHT=\"0\" TOPMARGIN=\"0\" LEFTMARGIN=\"0\" BORDER=\"0\" style=\"border: 1px solid #AAAAAA\" ></FRAME>");
            Response.Write("    </FRAMESET>");
            Response.Write("</FRAMESET>");
            Response.Write("<noframes></noframes><noframes>您的浏览器不支持框架显示</noframes>");

            Response.Write("</html>");
        }
    }
}
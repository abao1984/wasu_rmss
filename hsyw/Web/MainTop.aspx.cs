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

public partial class MainTop : System.Web.UI.Page
{
    string[] week = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelDate.Text = DateTime.Now.ToLongDateString() + " " + week[Convert.ToInt16(DateTime.Now.DayOfWeek.ToString("D"))];
        LabelUser.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
    }
}

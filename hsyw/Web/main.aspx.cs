using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Web_main : System.Web.UI.Page
{
    public string user_id="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Session["UserId"].Equals(null))
        {
            user_id = Session["UserID"].ToString();
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_GZCL_GZBB_XuanZheDaYingZiDuan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnQR_Click(object sender, EventArgs e)
    {
        string dateName = "";
        string dateID = "";
        foreach(ListItem item in CheckBoxList1.Items)
        {
            if(item.Selected)
            {
                dateName += item.Text + ",";
                dateID += item.Value + ",";
            }
        }
        dateName = dateName.Substring(0, dateName.Length - 1);
        dateID = dateID.Substring(0, dateID.Length - 1);
        Session.Add("dateName", dateName);
        Session.Add("dateID", dateID);
        //string names = Request.QueryString["NAME"];
        //string codes = Request.QueryString["CODE"];
        string str = string.Format(@"<script> parent.WindowClose();</script>");
//             parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}';</script>", names, dateName, codes, dateID);
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), str);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_GZCL_FDZ_FanDanChuLi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string zbguid=((TextBox)Page.Parent.FindControl("txtGuids")).Text;
        //string txtName = Request.QueryString["name"];
        if (!string.IsNullOrEmpty(DropDownList1.SelectedValue))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
               "<script> window.close(); window.returnValue='" + DropDownList1.SelectedValue + "'</script>");
        }

    }
}

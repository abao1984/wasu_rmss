using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_XiuFuChuLiFangFa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtName.Text = Request.QueryString["NAME"];
            BindList();
        }
    }

    private void BindList()
    {
        string sql = string.Format(@"select t.codename from t_fau_lxsz t where t.lb='clff'");
        DataSet ds = DataFunction.FillDataSet(sql);
        DataList1.DataSource = ds;
        DataList1.DataBind();
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            LinkButton linkBtn = (LinkButton)e.Item.FindControl("LinkButton1");
            linkBtn.Attributes.Add("onclick", "onchange('" + linkBtn.Text + "')");
        }
    }
}

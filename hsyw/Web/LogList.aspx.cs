using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Web_LogList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LogGrid.Attributes.Add("BorderColor", "#5B9ED1");
            BindLogGrid();
        }
    }

    private void BindLogGrid()
    {
        string sql = "select * from t_sys_log t where t.pk_guid='"+Request.QueryString["PK_GUID"]+"' order by t.userdatetime desc";
        DataSet ds = DataFunction.FillDataSet(sql);
        LogGrid.DataSource = ds;
        LogGrid.DataBind();
    }
}

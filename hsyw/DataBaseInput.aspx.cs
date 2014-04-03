using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DataBaseInput : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GuGanButton_Click(object sender, EventArgs e)
    {
        string sql = "SELECT * FROM  Zypz_Ggywgl";
        DataSet ds = DataFunction.FillDataSet(sql, "Database SQL");
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}

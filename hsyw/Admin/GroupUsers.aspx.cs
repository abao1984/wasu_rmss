using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_GroupUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sql = string.Format(@"select u.*,(case when u.ISUSE = '1' then '可用' else '停用' end) as ZT,b.BRANCHNAME from T_SYS_USER u left join T_SYS_R_USERGROUP r on u.USERNAME = r.USERNAME left join T_SYS_BRANCH b on u.BRANCHCODE = b.BRANCHCODE where r.GROUPCODE = '{0}'", Request.QueryString["groupcode"]);
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                int nColumnCount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = nColumnCount;
                GridView1.Rows[0].Cells[0].Text = "暂时还没有用户使用该角色！";
                GridView1.RowStyle.Height = 30;

                GridView1.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            }
            else
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
    }
}

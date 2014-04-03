using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_YongYouRenXinXi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string sql = string.Format("select * from T_SYS_USER where USERNAME = '{0}'",Request.QueryString["YYR"]);
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            }
        }
    }
}

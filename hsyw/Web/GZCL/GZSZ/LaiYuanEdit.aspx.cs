using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_LaiYuanEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GUID.Text=Request.QueryString["GUID"];
            if(GUID.Text=="")
            {
                GUID.Text = Guid.NewGuid().ToString();
            }
            string sql = "select * from t_fau_lxsz where guid='"+GUID.Text+"'";
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["guid"] = GUID.Text;
                dr["LB"] = "GZLY";
                dr["SFQY"] = 1;
                ds.Tables[0].Rows.Add(dr);
            }
            ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_lxsz where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["guid"] = GUID.Text;
            dr["LB"] = "GZLY";
            dr["SFQY"] = 1;
            ds.Tables[0].Rows.Add(dr);
        }
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "t_fau_lxsz");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true'; window.close();</script>");
    }
    protected void BtnGB_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();</script>");
    }
}

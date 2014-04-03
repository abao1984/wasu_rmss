using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_GuZhangLaiYuan : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        string sql = "select * from T_FAU_LXSZ t where t.lb='GZLY'";
        DataSet ds = DataFunction.FillDataSet(sql);
        gzcl.BindGridView(GridView1, ds);
    }

    protected void CheckAll(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            ch.Checked = cbx.Checked;
        }
    }

    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            //ch.Checked = cbx.Checked;
            if (ch.Checked)
            {
                guids += ",'" + GridView1.DataKeys[gvr.RowIndex]["GUID"].ToString() + "'";
            }
        }

        if (guids != "''")
        {
            string sql = "delete T_FAU_LXSZ where guid in(" + guids + ")";
            DataFunction.ExecuteNonQuery(sql);
        }
        BindGrid();
    }
    protected void btnSX_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


}

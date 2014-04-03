﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_FAU_GRADE : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGD();
        }
    }
    private void BindGD()
    {
        string sql = "select * from T_FAU_GZDJ t where t.SFQY='1'";
        if (txtZYMC.Text != "")
        {
            sql += string.Format(" and (t.MC  like '%{0}%' or t.MS like '%{0}%')", txtZYMC.Text.Trim());
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        gzcl.BindGridView(GridView1, ds);
    }
    //刷新
    protected void btnSX_Click(object sender, EventArgs e)
    {
        BindGD();
    }
    //删除
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
            string sql = "delete T_FAU_GZDJ where guid in(" + guids + ")";
            DataFunction.ExecuteNonQuery(sql);
        }
        BindGD();
    }
    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGD();
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
}

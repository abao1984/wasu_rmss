using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_FDZ_FanDanGaoJing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindDrop();
            BindGrid();
        }
    }

    private void BindDrop()
    {
        string sql = "select t.codename from t_fau_lxsz t where t.lb='ywlb'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "codename";
        DropDownList1.DataBind();
    }
    private void BindGrid()
    {
        string sql = "select guid,case when t.sfqy=1 then 'true' else 'false' end as sfqy,t.codename,t.ms from t_fau_lxsz t where t.lb='fdjg' and t.parent_name='" + DropDownList1.SelectedValue + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if(ds.Tables[0].Rows.Count == 0)
        {
            newData(ds);
        }
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    private void newData(DataSet ds)
    {
        string[] lists = { "电话处理", "调度发单", "维修返单", "处理结束 " };
        //ds.Tables[0].PrimaryKey = new DataColumn[] { new DataColumn("guid") };
        foreach(string list in lists)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["guid"] = Guid.NewGuid().ToString();
            dr["sfqy"] = 0;
            dr["codename"] = list;
            dr["ms"] = 0;
            ds.Tables[0].Rows.Add(dr);
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_lxsz t where t.lb='fdjg' and t.parent_name='" + DropDownList1.SelectedValue + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["GUID"] };
        foreach (GridViewRow gv in GridView1.Rows)
        {
            if (gv.RowType == DataControlRowType.DataRow) //判断是否为行模式
            {
                DataRow dr = ds.Tables[0].Rows.Find(GridView1.DataKeys[gv.RowIndex]);
                if(dr==null)
                {
                    dr = ds.Tables[0].NewRow();
                    dr["guid"] = Guid.NewGuid().ToString();
                    dr["codename"] = gv.Cells[1].Text;
                    dr["ms"] = 0;
                    dr["lb"] = "fdjg";
                    dr["parent_name"] = DropDownList1.SelectedValue;
                    ds.Tables[0].Rows.Add(dr);
                }
                if (((CheckBox)gv.FindControl("CheckBox1")).Checked)
                {
                    dr["SFQY"] = 1;
                }
                else
                {
                    dr["SFQY"] = 0;
                }
                dr["ms"] = ((TextBox)gv.FindControl("TextBox1")).Text;
            }
        }
        DataFunction.SaveData(ds, "t_fau_lxsz");
    }
   
}

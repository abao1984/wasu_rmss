using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_XiuFuYuanYing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            txtName.Text = Request.QueryString["NAME"];
            BindList();
        }
    }

    private void BindList()
    {
        string lx = Request.QueryString["lx"];
        string sql = string.Format(@"select t.parent_name,t.parent_id from t_fau_lxsz t 
                where t.lb='yy' and sfqy='1' and t.parent_id='{0}' group by t.parent_name,t.parent_id", lx);
        DataSet ds = DataFunction.FillDataSet(sql);
        if(ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["parent_name"] = "无数据，请重新选择故障类型！";
            dr["parent_id"] = "";
            ds.Tables[0].Rows.Add(dr);
        }
        DataList1.DataSource = ds;
        DataList1.DataBind();
    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemIndex > -1)
        {
            DataList dataList = (DataList)e.Item.FindControl("DataList2");
            Label label = (Label)e.Item.FindControl("Label2");
            string sql = string.Format(@"select codename from t_fau_lxsz t where t.lb='yy' and  sfqy='1' and parent_id='" + label.Text + "' order by ms");
            DataSet ds = DataFunction.FillDataSet(sql);
            dataList.DataSource = ds;
            dataList.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void DataList2_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemIndex > -1)
        {
            LinkButton linkBtn = (LinkButton)e.Item.FindControl("LinkButton1");
            linkBtn.Attributes.Add("onclick", "onchange('"+linkBtn.Text+"')");
        }
    }
}

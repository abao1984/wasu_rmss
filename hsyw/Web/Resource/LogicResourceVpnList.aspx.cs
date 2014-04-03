using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceVpnList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GridViewLogicEquVpn.Attributes.Add("BorderColor", "#5B9ED1");
            GridViewLogicEquVpn.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGrid());
        }
    }
    private int BindGrid()
    {
        string sql = "select * from t_logic_equ_vpn t where 1=1";
        if (VPNMC.Text != "")
        {
            sql += " and VPNMC like '%"+VPNMC.Text+"%'";
        }
        if (SSQY.Text != "")
        {
            sql += " and SSQY like '%"+SSQY.Text+"%'";
        }
        if (SM.Text != "")
        {
            sql += " and SM like '%" + SM.Text + "%'";
        }
        if (RD.Text != "")
        {
            sql += " and RDZ like '%" + RD.Text + "%'";
        }
        if (PZSJ1.Text != "")
        {
            sql += " and PZSJ >=  to_date('" + PZSJ1.Text + "','YYYY-MM-DD')";
        }
        if (PZSJ2.Text != "")
        {
            sql += " and PZSJ <= to_date('" + PZSJ2.Text + "','YYYY-MM-DD')";
        }
        if (XGSJ1.Text != "")
        {
            sql += " and UPDATEDATETIME >= to_date('" + XGSJ1.Text + "','YYYY-MM-DD')";
        }
        if (XGSJ2.Text != "")
        {
            sql += " and UPDATEDATETIME <= to_date('" + XGSJ2.Text + "','YYYY-MM-DD')";
        }
        //DataSet ds = DataFunction.FillDataSet(sql);
        //int num = ds.Tables[0].Rows.Count;
        int num = DataFunction.GetIntResult(string.Format("select count(*) from ({0})", sql));
        int n = 0;
        if (GridPageList.SelectedIndex > -1)
        {
            n = GridPageList.SelectedIndex;
        }
        int s = n * Convert.ToInt32(PageSize.SelectedValue);
        int e = s + Convert.ToInt32(PageSize.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from  (select rownum as rn,a.* from ({0}) a ) where rn>{1} and rn<={2}", sql, s, e));

        if (num == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        GridViewLogicEquVpn.DataSource = ds;
        GridViewLogicEquVpn.DataBind();
        if (num == 0)
        {
            GridViewLogicEquVpn.Rows[0].Cells.Clear();
            return 0;
        }
        return num;
    }
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridViewLogicEquVpn.PageIndex = 0;
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        GridPageList.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
            GridPageList.Items.Add(LI);
        }
        DataCountLab.Text = DataCount.ToString();
        PageCountLab.Text = PageCount.ToString();
        PageIndexLab.Text = "1";
    }


    protected void PrevButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (PageIndex == 0)
        {
            GridPageList.SelectedIndex = GridPageList.Items.Count - 1;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex - 1;
        }
       // GridViewLogicEquVpn.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }

    protected void NextButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (GridPageList.Items.Count - 1 == PageIndex)
        {
            GridPageList.SelectedIndex = 0;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex + 1;
        }
       // GridViewLogicEquVpn.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewLogicEquVpn.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   GridViewLogicEquVpn.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }
    #endregion
    protected void GridViewLogicEquVpn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = GridViewLogicEquVpn.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','" + name + "')");
            e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=# onclick=\"windowOpen('" + guid + "','" + name + "')\">详细</a>";
        }
    }

    protected void CheckAll(object sender, EventArgs e)
    {
        CheckBox cbx = (CheckBox)sender;
        foreach (GridViewRow gvr in GridViewLogicEquVpn.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            ch.Checked = cbx.Checked;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow gvr in GridViewLogicEquVpn.Rows)
        {
            CheckBox ch = (CheckBox)gvr.FindControl("ItemCheckBox");
            //ch.Checked = cbx.Checked;
            if (ch.Checked)
            {
                guids += ",'" + GridViewLogicEquVpn.DataKeys[gvr.RowIndex]["GUID"].ToString() + "'";
            }
        }

        if (guids != "''")
        {
            string sql = "delete t_logic_equ_vpn where guid in(" + guids + ")";
            DataFunction.ExecuteNonQuery(sql);
        }
        BindGridPage(BindGrid());
    }
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceVpnSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            NAME.Text = Request.QueryString["NAME"];
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
            sql += " and VPNMC like '%" + VPNMC.Text + "%'";
        }
        if (SSQY.Text != "")
        {
            sql += " and SSQY like '%" + SSQY.Text + "%'";
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
        DataSet ds = DataFunction.FillDataSet(sql);
        int num = ds.Tables[0].Rows.Count;
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
        return ds.Tables[0].Rows.Count;
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
        GridViewLogicEquVpn.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridViewLogicEquVpn.PageIndex + 1);
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
        GridViewLogicEquVpn.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewLogicEquVpn.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewLogicEquVpn.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewLogicEquVpn.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewLogicEquVpn.PageIndex + 1);
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
            CheckBox ch = (CheckBox)gvr.FindControl("XZ");
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
    protected void OKButton_Click(object sender, EventArgs e)
    {
        string strVpn = "";
        string strVpnGuid = "";
        string strVpnCode = "";
        foreach (GridViewRow gr in GridViewLogicEquVpn.Rows)
        {
            CheckBox ch = (CheckBox)gr.FindControl("XZ");
            if (ch.Checked)
            {
                if (strVpnGuid != "")
                {
                    strVpn += ",";
                    strVpnGuid += ",";
                    strVpnCode += ",";
                }
                strVpn += gr.Cells[2].Text;
                strVpnGuid += GridViewLogicEquVpn.DataKeys[gr.RowIndex].Value;
                strVpnCode += gr.Cells[1].Text;
            }
        }

        string strScript = string.Format(@"<script> window.close();parent.WindowClose(); parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{0}_CODE').value = '{2}';parent.document.getElementById('{0}_GUID').value = '{3}';</script>",
            NAME.Text, strVpn, strVpnCode, strVpnGuid);
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
    }
}

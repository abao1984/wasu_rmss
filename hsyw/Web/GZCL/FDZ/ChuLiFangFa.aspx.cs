using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_FDZ_ChuLiFangFa : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //types.Text = Request.QueryString["type"];
            BindGridPage(BindGrid());
            //filePage();
        }
    }

    private int BindGrid()
    {
        string sql = "select * from T_FAU_LXSZ t where t.lb='" + types.Text + "' ";
        if (txtZYMC.Text != "")
        {
            sql += string.Format(" and (t.CODENAME  like '%{0}%' or t.ms like '%{0}%')", txtZYMC.Text.Trim());
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        // gzcl.BindGridView(GridView1, ds);

        return gzcl.BindGridView(GridView1, ds);
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
    protected void btnSX_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
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

            string[] sql = new string[2];
            sql[0] = "delete T_FAU_LXSZ where  parent_name in(select CODENAME from T_FAU_LXSZ  where guid in(" + guids + "))";
            sql[1] = "delete T_FAU_LXSZ where guid in(" + guids + ")";
            DataFunction.ExecuteTransaction(sql);
        }
        BindGridPage(BindGrid());
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = GridView1.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            if (!guid.Equals(""))
            {
                e.Row.Attributes.Add("ondblclick", "OpenNew('" + guid + "')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridView1.PageIndex = 0;
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
        //filePage();
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
        GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
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
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }
    #endregion

    private void filePage()
    {
        string CODENAME = "";
        string MS = "";
        if (types.Text == "zy")
        {
            SaveButton.Text = "新增故障专业";
            CODENAME = "专业名称";
            MS = "专业描述";
        }
        else if (types.Text == "lx")
        {
            SaveButton.Text = "新增故障类型";
            CODENAME = "类型名称";
            MS = "类型描述";
        }
        GridView1.HeaderRow.Cells[1].Text = CODENAME;
        GridView1.HeaderRow.Cells[2].Text = MS;
    }
}

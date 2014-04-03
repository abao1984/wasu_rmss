using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigCupboardList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            SB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,66a85a26-6e7b-42c6-ac01-678697ba3023";
            BindGridPage(BindGrid());
        }
    }

    private int BindGrid()
    {
        DataSet ds = DataFunction.FillDataSet(GetSql());
        gvCupboard.DataSource = ds;
        gvCupboard.DataBind();
        return ds.Tables[0].Rows.Count;
    }

    private string GetSql()
    {
        string sql = "select t.*,t.zrr||'/'||t.zrbm as zrrbm from t_con_cupboard t where 1=1 ";
        if (JFMC.Text!="")
        {
            sql += " and HOUSE_NAME like '%" + JFMC.Text + "%'";
        }
        if (SBMC_CODE.Text != "")
        {
            sql += " and SBMC_CODE like '%" + SBMC_CODE.Text + "%'";
        }
        if ( QYSJ2.Text != "")
        {
            sql += " and QYRQ<= to_date('" + QYSJ2.Text + "','yyyy-mm-dd')";
        }

        if (QYSJ1.Text != "")
        {
            sql += " and QYRQ>= to_date('" + QYSJ1.Text + "','yyyy-mm-dd') ";
        }
        sql += " order by t.QYRQ";
        return sql;
    }

    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string ywguids = "''";
        foreach (GridViewRow row in gvCupboard.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                ywguids += ",'" + gvCupboard.DataKeys[row.RowIndex]["GUID"].ToString() + "'";
            }
        }
        if (!ywguids.Equals("''"))
        {
            DataFunction.ExecuteNonQuery(string.Format("delete from t_con_cupboard where GUID in ({0})", ywguids));
            BindGridPage(BindGrid());
        }
    }
  
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvCupboard.PageIndex = 0;
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
        gvCupboard.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvCupboard.PageIndex + 1);
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
        gvCupboard.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvCupboard.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCupboard.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCupboard.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvCupboard.PageIndex + 1);
        BindGrid();
    }
    #endregion

    protected void gvCupboard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = gvCupboard.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "')");
        }
    }

    protected void gvCupboard_Sorting(object sender, GridViewSortEventArgs e)
    {
        string order = e.SortExpression;
        string sql = GetSql();
        DataView dv = DataFunction.FillDataSet(sql).Tables[0].DefaultView;
        if (this.SortAscending)
        {
            this.SortAscending = false;
            order = e.SortExpression + " asc ";
        }
        else
        {
            this.SortAscending = true;
            order = e.SortExpression + " desc ";
        }
        dv.Sort = order;
        ViewState["SortName"] = order;
        gvCupboard.DataSource = dv;
        gvCupboard.DataBind();
        BindGridPage(dv.Table.Rows.Count);
    }

    bool SortAscending
    {
        get
        {
            object o = ViewState["SortAscending"];
            if (o == null)
            {
                return true;
            }
            return (bool)o;
        }
        set
        {
            ViewState["SortAscending"] = value;
        }
    }

    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_JianKongSbQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.QueryString["SBMC"].IsNullOrEmpty())
            {
                SBMC.Text = Convert.ToString(Request.QueryString["SBMC"]);
            }
            if (!Request.QueryString["SBID"].IsNullOrEmpty())
            {
                SBID.Text = Convert.ToString(Request.QueryString["SBID"]);
            }
          BindGridPage(BindGrid());
        }
    }

    #region 得到SQL语句
    private string getSql()
    {
        string sql = "select guid,house_area,equ_code,equ_name,sblx from T_RES_EQU_JKSB where 1=1";

        if (!TxtSbBh.Text.IsNullOrEmpty())
        {
            sql += " and equ_code like '%" + TxtSbBh.Text + "%'";
        }
        if (!TxtSsqy.Text.IsNullOrEmpty())
        {
            sql += " and house_area like '%" + TxtSsqy.Text + "%'";
        }
        if (!TxtSbMc.Text.IsNullOrEmpty())
        {
            sql += " and equ_name like '%" + TxtSbMc.Text + "%'";
        }
        return sql;
    }
    #endregion

    #region 绑定数据
    private int BindGrid()
    {
        string sql = getSql();
        DataSet ds = DataFunction.FillDataSet(sql);
        try
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int col = GridView1.Columns.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = col;
                GridView1.Rows[0].Cells[0].Text = "没有相关数据！";
                GridView1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }
        return ds.Tables[0].Rows.Count;
    }
    #endregion

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
        // GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
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
        // GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }
    #endregion

    #region 查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    #endregion

    #region 确认
    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string sbmc = string.Empty, sbid = string.Empty;
        int idx = 0;
        GridView1.Rows.Cast<GridViewRow>().ForEach(dr => {
            bool ch = ((CheckBox)dr.Cells[0].FindControl("CheckBox1")).Checked;
            if (ch == true)
            {
                if (!sbmc.IsNullOrEmpty())
                {
                    sbmc += ",";
                }
                sbmc += Convert.ToString(GridView1.DataKeys[idx].Values["EQU_NAME"]);
                if (!sbid.IsNullOrEmpty())
                {
                    sbid += ",";
                }
                sbid += Convert.ToString(GridView1.DataKeys[idx].Values["GUID"]);
            }
            ++idx;
        });

        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script> parent.document.getElementById('"+SBMC.Text+"').value='" + sbmc + "';parent.document.getElementById('"+SBID.Text+"').value='" + sbid + "';parent.WindowClose();</script>");
    }
    #endregion


   
}

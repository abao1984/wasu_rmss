using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;

public partial class Web_Resource_JianKongPzd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridPage(BindGrid());
        }
    }

    #region 得到SQL语句
    private string getSql()
    {
        string sql = "";
        sql = "select * from t_con_jkpzb where 1=1";
        if (!TxtKhBh.Text.IsNullOrEmpty())
        {
            sql += " and khbh like '%" + TxtKhBh.Text + "%'";
        }
        if (!TxtKhMc.Text.IsNullOrEmpty())
        {
            sql += " and khmc like '%"+TxtKhMc.Text+"%' ";
        }
        if (!TxtKhQy.Text.IsNullOrEmpty())
        {
            sql += " and khqy like '%" + TxtKhQy.Text + "%'";
        }
        return sql;
    }
    #endregion

    #region 绑定数据
    private int BindGrid()
    {
        int count = 0;
        try
        {
            string sql = getSql();
            DataSet ds = DataFunction.FillDataSet(sql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridViewJK.DataSource = ds;
                GridViewJK.DataBind();
                int col = GridViewJK.Columns.Count;
                GridViewJK.Rows[0].Cells.Clear();
                GridViewJK.Rows[0].Cells.Add(new TableCell());
                GridViewJK.Rows[0].Cells[0].ColumnSpan = col;
                GridViewJK.Rows[0].Cells[0].Text = "没有相关数据！";
                GridViewJK.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
            else
            {
                count = ds.Tables[0].Rows.Count;
                GridViewJK.DataSource = ds;
                GridViewJK.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }
        return count;
    }
    #endregion

    #region 按钮方法
    //删除
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int idx = 0;
        GridViewJK.Rows.Cast<GridViewRow>().ForEach(dr => {
            bool ch = ((System.Web.UI.WebControls.CheckBox)dr.Cells[0].FindControl("CheckBox1")).Checked;
            if (ch == true)
            {
                string guid = Convert.ToString(GridViewJK.DataKeys[idx].Value);
                DataFunction.ExecuteNonQuery("delete from t_con_jkpzb t where t.guid='"+guid+"'");
                DataFunction.ExecuteNonQuery("delete from t_con_jkpzb_cb t where t.lsid='" + guid + "'");
            }
            ++idx;
        });
        BindGrid();
    }
    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridViewJK.PageIndex = 0;
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
        GridViewJK.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridViewJK.PageIndex + 1);
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
        GridViewJK.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewJK.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewJK.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewJK.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewJK.PageIndex + 1);
        BindGrid();
    }
    #endregion

    #region 刷新
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    #endregion

    #region 生成行事件
    protected void GridViewJK_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string guid = GridViewJK.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','编辑')");

        }
    }
    #endregion

    protected void BtnExport_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        Worksheet ws = book.Worksheets[0];
        int col = -1;
        int row = 0;
        //导出表头
        GridViewJK.Columns.Cast<DataControlField>().ForEach(con =>
        {
            if (col > -1)
            {

                ws.Cells[row, col].PutValue(con.HeaderText);
                ws.Cells[row, col].Style.Borders.SetStyle(CellBorderType.Thin);
                ws.Cells[row, col].Style.Borders.DiagonalStyle = CellBorderType.None;
                ws.Cells[row, col].Style.HorizontalAlignment = TextAlignmentType.Center;
                ws.Cells[row, col].Style.Font.IsBold = true;
                ws.Cells[row, col].Style.Font.Size = 9;
            }
            ++col;
        });

        //导出表头
        GridViewJK.Rows.Cast<GridViewRow>().ForEach(dr =>
        {
            ++row;
            int idx = 0;
            col = -1;
            GridViewJK.Columns.Cast<DataControlField>().ForEach(con =>
            {
                if (col > -1)
                {
                    ws.Cells[row, col].PutValue(dr.Cells[idx].Text);
                    ws.Cells[row, col].Style.Borders.SetStyle(CellBorderType.Thin);
                    ws.Cells[row, col].Style.Borders.DiagonalStyle = CellBorderType.None;
                    ws.Cells[row, col].Style.Font.Size = 8;


                }
                ++idx;
                ++col;
            });
        });
        ws.AutoFitColumns();
        MemoryStream ms = new MemoryStream();

        byte[] by = null;
        book.Save(ms, FileFormatType.Excel2003);
        by = ms.ToArray();
        IDP.Common.WebUtils.ResponseWriteBinary(by, "监控配置单.xls");
    }
}

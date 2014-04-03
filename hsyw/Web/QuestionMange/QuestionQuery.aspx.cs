using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;

public partial class Web_QuestionMange_QuestionQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDDL();
            GVQuestion.Attributes.Add("BorderColor", "#5B9ED1");
            GVQuestion.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGrid());
        }
    }
    #region 绑定DropDownList
    private void BindDDL()
    {
        ShareFunction.BindEnumDropList(WTLY, "WTLY");
        ShareFunction.BindEnumDropList(ZXTLB, "ZXTLB");
        ShareFunction.BindEnumDropList(WTYXJ, "WTYXJ");
        ShareFunction.BindEnumDropList(WTZT, "WTZT");
    }
    #endregion

    #region 得到SQL语句
    private string getSql()
    {
        //因为绑定数据和导出数据的要显示列是不一样的，但条件是一样的，所以得到SQL语句只是条件公用，主语句是分开写的 罗耀斌 2011-6-14 10：00
        string sql = "";
        if (WTMC.Text != "")
        {
            sql += " and WTMC like '%" + WTMC.Text + "%'";
        }
        if (WTLY.SelectedValue != "")
        {
            sql += " and WTLY = '" + WTLY.SelectedValue + "'";
        }
        if (ZXTLB.SelectedValue != "")
        {
            sql += " and ZXTLB = '" + ZXTLB.SelectedValue + "'";
        }
        if (WTYXJ.SelectedValue != "")
        {
            sql += " and WTYXJ = '" + WTYXJ.SelectedValue + "'";
        }
        if (WTZT.SelectedValue != "")
        {
            sql += " and WTZT = '" + WTZT.SelectedValue + "'";
        }
        if (WCSJ1.Text != "")
        {
            sql += " and WCSJ >=  to_date('" + WCSJ1.Text + "','YYYY-MM-DD')";
        }
        if (WCSJ2.Text != "")
        {
            sql += " and WCSJ <= to_date('" + WCSJ2.Text + "','YYYY-MM-DD')";
        }
        if (FZBM.Text != "")
        {
            sql += " and fzbm like '%" + FZBM.Text + "%'";
        }
        sql += " order by t.wcsj desc";
        return sql;
    }
    #endregion

    #region 绑定数据
    private int BindGrid()
    {
        string sql = "select t.* from t_ques_info t where 1=1";
        sql += getSql();
        DataSet ds = DataFunction.FillDataSet(sql);
        int num = ds.Tables[0].Rows.Count;
        if (num == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        GVQuestion.DataSource = ds;
        GVQuestion.DataBind();
        if (num == 0)
        {
            int count = GVQuestion.Columns.Count;
            GVQuestion.Rows[0].Cells.Clear();
            GVQuestion.Rows[0].Cells.Add(new TableCell());
            GVQuestion.Rows[0].Cells[0].ColumnSpan = count;
            GVQuestion.Rows[0].Cells[0].Text = "没有相关数据！";
            GVQuestion.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            return 0;
        }
        return ds.Tables[0].Rows.Count;
    }
    #endregion

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GVQuestion.PageIndex = 0;
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
        GVQuestion.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GVQuestion.PageIndex + 1);
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
        GVQuestion.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GVQuestion.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVQuestion.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVQuestion.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GVQuestion.PageIndex + 1);
        BindGrid();
    }
    #endregion

    #region 查询事件
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    #endregion

    #region 导出事件
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        WorkbookDesigner designer1 = new WorkbookDesigner();
        Workbook book = designer1.Workbook;
        Worksheet ws = book.Worksheets[0];

        string sql = "select t.wtly 问题来源,t.fzbm 负责部门, t.zxtlb 子系统类别, t.wtyxj 优先级, t.wtmc 问题名称, t.wtms 问题描述, t.yxd 影响度, t.wtzt 问题状态,t.fzr 负责人,t.wtfx 问题分析, t.lsjjcs 临时解决措施, t.jjbf 解决办法, t.psr 评审人, t.pssj 评审时间 , t.pf 评分, t.py 评语 from t_ques_info t where 1=1";
        sql += getSql();

        DataTable dt = DataFunction.FillDataSet(sql).Tables[0];

        int idx = 0;

        //导出表头
        for (int col = 0; col < dt.Columns.Count; col++)
        {
            ws.Cells[idx, col].PutValue(dt.Columns[col].ColumnName);
            ws.Cells[idx, col].Style.Borders.SetStyle(CellBorderType.Thin);
            ws.Cells[idx, col].Style.Borders.DiagonalStyle = CellBorderType.None;
            ws.Cells[idx, col].Style.BackgroundColor = System.Drawing.Color.Blue;
            ws.Cells[idx, col].Style.HorizontalAlignment = TextAlignmentType.Center;
            ws.Cells[idx, col].Style.VerticalAlignment = TextAlignmentType.Center;
            ws.Cells[idx, col].Style.IsTextWrapped = true;
            ws.Cells[idx, col].Style.Font.IsBold = true;
            ws.Cells.SetColumnWidth(col, 25);
        }

        //导出内容
        for (int row = 0; row < dt.Rows.Count; row++)
        {
            ++idx;
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                ws.Cells[idx, col].PutValue(Convert.ToString(dt.Rows[row][col]));
                ws.Cells[idx, col].Style.IsTextWrapped = true;
                ws.Cells[idx, col].Style.VerticalAlignment = TextAlignmentType.Center;
            }
        }
        ws.AutoFitRows();
        designer1.Process();
        designer1.Save(System.Web.HttpUtility.UrlEncode("问题信息.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
        Response.End();
    }
    #endregion

    #region GridView行创建事件
    protected void GVQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string id = GVQuestion.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + id + "','" + name + "','Query')");
            string ph = DataFunction.GetStringResult("select pf from t_ques_info t where t.id='" + id + "'");
            string url = "", tooltip = "";
            if (string.IsNullOrEmpty(ph))
            {
                url = "../Images/Small/y.gif";
                tooltip = "未评审";
            }
            else
            {
                url = "../Images/Small/success-sm.gif";
                tooltip = "已评审";
            }
            ((Image)e.Row.Cells[e.Row.Cells.Count - 1].FindControl("imgPs")).ImageUrl = url;
            ((Image)e.Row.Cells[e.Row.Cells.Count - 1].FindControl("imgPs")).ToolTip = tooltip;
        }
    }
    #endregion
}

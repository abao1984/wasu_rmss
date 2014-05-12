using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;

public partial class Web_GZCL_GZBB_LiShiJiLuChaXun : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindDrop();
            //GridView1.Sorting += new GridViewSortEventHandler(GridView1_Sorting);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        GridView1.Columns[4].Visible = true;
        GridView1.Columns[5].Visible = true;
        GridView1.Columns[6].Visible = true;

        BindGridPage(BindGrid());

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = false;
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[5].Visible = false;
        GridView1.Columns[6].Visible = false;
    }

    private void bindDrop()
    {
        dropYWZT.Items.Clear();

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywzt' and sfqy=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        dropYWZT.DataSource = ds;
        dropYWZT.DataTextField = "codename";
        dropYWZT.DataValueField = "guid";
        dropYWZT.DataBind();
        ListItem itmes = new ListItem("---请选择---", "");
        dropYWZT.Items.Add(itmes);
        dropYWZT.SelectedValue = "";
    }
    private DataSet GetData()
    {
        //string sql = string.Format("select rownum as xh,t.* from t_fau_zb t where t.fdzzt='结单'");
        string sql = string.Format(@"select rownum as xh,
       t.*,
       (select to_char(max(clsj), 'yyyy-mm-dd hh24:mi')
          from t_fau_cllc
         where zbguid = t.zbguid
           and lccz = '故障申告') as gzsgsj,
       (select to_char(max(clsj), 'yyyy-mm-dd hh24:mi')
          from t_fau_cllc
         where zbguid = t.zbguid
           and lccz = '送调度发单') as sddfdsj,
       (select to_char(max(clsj), 'yyyy-mm-dd hh24:mi')
          from t_fau_cllc
         where zbguid = t.zbguid
           and lccz = '发单') as fdsj,
       (select to_char(max(clsj), 'yyyy-mm-dd hh24:mi')
          from t_fau_cllc
         where zbguid = t.zbguid
           and lccz = '故障修复') as gzxfsj
  from t_fau_zb t
 where t.gzzt = '结单'");
        //by hangyt@2012.2.29
        if (dropYWZT.SelectedValue != "")
        {
            sql += " and t.ywzt='" + dropYWZT.SelectedItem.Text + "'";
            string ywlx = "";
            foreach (ListItem box in CheckBoxList1.Items)
            {
                if (box.Selected)
                {
                    ywlx += "'" + box.Text + "',";
                }
            }
            if (ywlx != "")
            {
                ywlx = ywlx.Substring(0, ywlx.Length - 1);
                sql += " and t.ywlx  in (" + ywlx + ")";
            }
        }

        if (TSSJ1.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd')>='" + TSSJ1.Text + "'";
        }

        if (TSSJ2.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd')<='" + TSSJ2.Text + "'";
        }

        if (JDSJ1.Text != "")
        {
            sql += " and to_char(t.jdsj,'yyyy-mm-dd')>='" + JDSJ1.Text + "'";
        }

        if (JDSJ2.Text != "")
        {
            sql += " and to_char(t.jdsj,'yyyy-mm-dd')<='" + JDSJ2.Text + "'";
        }

        if (txtNR.Text != "" && dropNR.SelectedValue != "----请选择----")
        {
            sql += " and " + dropNR.SelectedValue + " like '%" + txtNR.Text + "%'";
        }

        if (txt_GZBH.Text.Trim() != "")
        {
            sql += " and t.gzbh like '%" + txt_GZBH.Text.Trim() + "%' ";
        }

        DataSet ds = DataFunction.FillDataSet(sql);
        return ds;
    }
    private int BindGrid()
    {
        DataSet ds = GetData();
        return gzcl.BindGridView(GridView1, ds);
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

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        GridView1.Columns[4].Visible = true;
        GridView1.Columns[5].Visible = true;
        GridView1.Columns[6].Visible = true;

        BindGrid();

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = false;
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[5].Visible = false;
        GridView1.Columns[6].Visible = false;
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

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        GridView1.Columns[4].Visible = true;
        GridView1.Columns[5].Visible = true;
        GridView1.Columns[6].Visible = true;

        BindGrid();

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = false;
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[5].Visible = false;
        GridView1.Columns[6].Visible = false;
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        GridView1.Columns[4].Visible = true;
        GridView1.Columns[5].Visible = true;
        GridView1.Columns[6].Visible = true;

        BindGridPage(BindGrid());

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = false;
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[5].Visible = false;
        GridView1.Columns[6].Visible = false;
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        GridView1.Columns[4].Visible = true;
        GridView1.Columns[5].Visible = true;
        GridView1.Columns[6].Visible = true;

        BindGrid();

        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = false;
        GridView1.Columns[4].Visible = false;
        GridView1.Columns[5].Visible = false;
        GridView1.Columns[6].Visible = false;
    }
    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string zbguid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            //            string sql = string.Format(@"select * from (
            //                select case when t.lccz='故障申告' then '投诉' when t.lccz='送调度发单' then '电话处理' 
            //                 when t.lccz='发单' then '发单' when  t.lccz='返调度发单' then '返单' end as clcz,
            //                 to_char(t.clsj, 'yyyy-mm-dd hh24:mi') as clsj,t.clry,t.clsm,t.sjclry
            //                  from t_fau_cllc t
            //                 where t.zbguid = '{0}' ) where clcz is not null order by clsj", zbguid);
            string sql = string.Format(@"select *
                 from (select case
                 when t.lccz = '故障申告' then
                  '投诉'
                 when t.lccz = '故障移交' then
                  '移交'
                 when t.lccz = '送调度发单' then
                  '电话处理'
                 when t.lccz = '发单' then
                  '发单'
                 when t.lccz = '返调度发单' then
                  '返单'
                 when t.lccz = '故障修复' then
                  '故障修复'
                 when t.lccz = '留单' then
                  '留单'
               end as clcz,
               to_char(t.clsj, 'yyyy-mm-dd hh24:mi') as clsj,
               t.clry,
               t.clsm,
               t.sjclry
          from t_fau_cllc t
         where t.zbguid = '{0}')
 where clcz is not null
 order by clsj", zbguid);
            DataSet ds = DataFunction.FillDataSet(sql);
            GridView grv = (GridView)e.Row.FindControl("GridView2");
            grv.DataSource = ds;
            grv.DataBind();
            grv.ShowHeader = false;

            e.Row.Attributes.Add("ondblclick", "windowOpen('" + zbguid + "','')");
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
        }
    }
    #region gridView排序
    // GridView1 += new GridViewSortEventHandler(gvData_Sorting);
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortExpression = e.SortExpression.ToUpper();
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            //排序並重新綁定
            bindData(sortExpression, "DESC");
        }
        else if (GridViewSortDirection == SortDirection.Descending)
        {
            GridViewSortDirection = SortDirection.Ascending;
            //排序並重新綁定
            bindData(sortExpression, "ASC");
        }
    }

    /// <summary>
    /// 排序方向屬性
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }

    /// <summary>
    /// 排序並綁定數據
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="sortDirection"></param>
    protected void bindData(string sortExpression, string sortDirection)
    {
        string sql = string.Format("select rownum as xh,t.* from t_fau_zb t where t.gzzt = '结单'");
        if (dropYWZT.SelectedValue != "")
        {
            sql += " and t.ywlb='" + dropYWZT.SelectedItem.Text + "'";
        }

        if (TSSJ1.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd')>='" + TSSJ1.Text + "'";
        }

        if (TSSJ2.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd')<='" + TSSJ2.Text + "'";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        DataView dv = new DataView(ds.Tables[0]);
        dv.Sort = sortExpression;
        if (sortDirection != String.Empty)
        {
            dv.Sort = sortExpression + " " + sortDirection;
        }
        BindGridPage(gzcl.BindGridView(GridView1, dv.Table.DataSet));
    }

    #endregion


    protected void dropYWZT_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = string.Format(@"select codename from t_fau_lxsz t where t.parent_id='{0}' and  t.lb='ywlx'", dropYWZT.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        CheckBoxList1.DataSource = ds;
        CheckBoxList1.DataTextField = "codename";
        CheckBoxList1.DataBind();
        foreach (ListItem box in CheckBoxList1.Items)
        {
            box.Selected = true;
        }
    }
    //导出Excel
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (GridView1.Columns[2].Visible == true)
        {
            WorkbookDesigner designer1 = new WorkbookDesigner();
            object filePath = Server.MapPath("LSJLCX.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Workbook workbook = designer1.Workbook;
                DataSet ds = GetData();
                int num = 0;
                int length = ds.Tables[0].Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    //dsh 3.19
//                    string sql = string.Format(@"select * from (select case when t.lccz='故障申告' then '投诉' when t.lccz='送调度发单' then '电话处理' 
//                                                               when t.lccz='发单' then '发单' when  t.lccz='返调度发单' then '返单' end as clcz,
//                                                                to_char(t.clsj, 'yyyy-mm-dd hh24:mi') as clsj,t.clry,t.clsm,t.sjclry
//                                                                from t_fau_cllc t where t.zbguid = '{0}' ) 
//                                                where clcz is not null order by clsj", dr["ZBGUID"].ToString());

                    string sql = string.Format(@"select *
                 from (select case
                 when t.lccz = '故障申告' then
                  '投诉'
                 when t.lccz = '故障移交' then
                  '移交'
                 when t.lccz = '送调度发单' then
                  '电话处理'
                 when t.lccz = '发单' then
                  '发单'
                 when t.lccz = '返调度发单' then
                  '返单'
                 when t.lccz = '故障修复' then
                  '故障修复'
                 when t.lccz = '留单' then
                  '留单'
               end as clcz,
               to_char(t.clsj, 'yyyy-mm-dd hh24:mi') as clsj,
               t.clry,
               t.clsm,
               t.sjclry
          from t_fau_cllc t
         where t.zbguid = '{0}')
 where clcz is not null
 order by clsj", dr["ZBGUID"].ToString());
                    DataSet ds1 = DataFunction.FillDataSet(sql);
                    int count = ds1.Tables[0].Rows.Count;
                    if (count == 0)
                    {
                        count = 1;
                    }
                    else
                    {
                        for (int j = 0; j < count; j++)
                        {
                            DataRow dr1 = ds1.Tables[0].Rows[j];
                            workbook.Worksheets[0].Cells[2 + num + j, 2].PutValue(dr1["clsj"].ToString());
                            workbook.Worksheets[0].Cells[2 + num + j, 3].PutValue(dr1["clcz"].ToString());
                            workbook.Worksheets[0].Cells[2 + num + j, 4].PutValue(dr1["clry"].ToString());
                            workbook.Worksheets[0].Cells[2 + num + j, 5].PutValue(dr1["SJCLRY"].ToString());
                            workbook.Worksheets[0].Cells[2 + num + j, 6].PutValue(dr1["clsm"].ToString());
                        }
                    }
                    workbook.Worksheets[0].Cells[2 + num, 0].PutValue(dr["XH"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 1].PutValue(dr["GZBH"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 7].PutValue(dr["YWZT"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 8].PutValue(dr["GZMC"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 9].PutValue(dr["LXDH"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 10].PutValue(dr["KHDZ"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 11].PutValue(dr["YWLB"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 12].PutValue(dr["HYFL"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 13].PutValue(dr["GZLY"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 14].PutValue(dr["FDZZT"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 15].PutValue(dr["GZCC"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 16].PutValue(dr["GZLX"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 17].PutValue(dr["GZYY"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 18].PutValue(dr["ZJYY"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 19].PutValue(dr["GZCLFF"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 20].PutValue(dr["GZFFMS"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 21].PutValue(dr["XFRY"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 22].PutValue(dr["GZMS"].ToString());
                    workbook.Worksheets[0].Cells[2 + num, 23].PutValue(dr["CUSTOMER_LEVEL"].ToString());


                    workbook.Worksheets[0].Cells.Merge(2 + num, 0, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 1, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 7, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 8, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 9, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 10, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 11, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 12, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 13, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 14, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 15, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 16, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 17, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 18, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 19, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 20, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 21, count, 1);
                    workbook.Worksheets[0].Cells.Merge(2 + num, 22, count, 1);

                    num = num + count;
                }
                designer1.Save(System.Web.HttpUtility.UrlEncode("历史记录查询.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
                Response.End();
            }
        }
        else
        {
            WorkbookDesigner designer1 = new WorkbookDesigner();
            object filePath = Server.MapPath("LSJLCX1.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Workbook workbook = designer1.Workbook;
                DataSet ds = GetData();
                int length = ds.Tables[0].Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    workbook.Worksheets[0].Cells[1 + i, 0].PutValue(dr["XH"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 1].PutValue(dr["GZBH"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 2].PutValue(dr["gzsgsj"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 3].PutValue(dr["sddfdsj"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 4].PutValue(dr["fdsj"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 5].PutValue(dr["gzxfsj"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 6].PutValue(dr["YWZT"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 7].PutValue(dr["GZMC"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 8].PutValue(dr["LXDH"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 9].PutValue(dr["KHDZ"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 10].PutValue(dr["YWLB"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 11].PutValue(dr["HYFL"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 12].PutValue(dr["GZLY"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 13].PutValue(dr["FDZZT"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 14].PutValue(dr["GZCC"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 15].PutValue(dr["GZLX"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 16].PutValue(dr["GZYY"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 17].PutValue(dr["ZJYY"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 18].PutValue(dr["GZCLFF"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 19].PutValue(dr["GZFFMS"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 20].PutValue(dr["XFRY"].ToString());
                    workbook.Worksheets[0].Cells[1 + i, 21].PutValue(dr["GZMS"].ToString());

                }
                designer1.Save(System.Web.HttpUtility.UrlEncode("历史记录查询1.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
                Response.End();
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (GridView1.Columns[2].Visible == true)
        {
            GridView1.Columns[2].Visible = false;
            GridView1.Columns[3].Visible = true;
            GridView1.Columns[4].Visible = true;
            GridView1.Columns[5].Visible = true;
            GridView1.Columns[6].Visible = true;
        }
        else
        {
            GridView1.Columns[2].Visible = true;
            GridView1.Columns[3].Visible = false;
            GridView1.Columns[4].Visible = false;
            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = false;
        }
    }
}

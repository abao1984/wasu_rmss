﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;
using System.Text.RegularExpressions;

public partial class Web_INFO_DiZiTouSu : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            BindGridPage(BindGrid());

        }
    }

    #region 绑定数据
    private int BindGrid()
    {
        string sql = getSql();
        DataSet ds = DataFunction.FillDataSet(sql);
        return gzcl.BindGridView(GridView1, ds);
    }
    #endregion

    #region 得到SQL语句，绑定数据和导出数据时用
    private string getSql()
    {
     
        string sql = "select guid, cxnr, cxjg, yhlx, tslx, hffkxx, gzyy, slrq, hfrq, gzrq, ljcs, bz from t_info_gzts where 1=1";
        if (!string.IsNullOrEmpty(TxtCxNr.Text))
        {
            sql += " and cxnr like '%" + TxtCxNr.Text + "%'";
        }
        if (!string.IsNullOrEmpty(TxtCxJg.Text))
        {
            sql += " and cxjg like '%" + TxtCxJg.Text + "%'";
        }
        if (!string.IsNullOrEmpty(TxtLjCs.Text))
        {
            sql += " and ljcs='" + TxtLjCs.Text + "'";
        }
        if (!string.IsNullOrEmpty(Kssj.Text))
        {
            sql += " and slrq>=to_date('" + Kssj.Text + "','yyyy-MM-dd')";
        }
        if (!string.IsNullOrEmpty(Jssj.Text))
        {
            sql += " and slrq<=to_date('" + Jssj.Text + "','yyyy-MM-dd')";
        }
        return sql;
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

    #region 按钮事件
    //查询按钮
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    //刷新按钮
    protected void BtnRfh_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    //全选
    protected void BtnAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = true;
        }
        
    }
    //取消
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            ((System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1")).Checked = false;
        }
    }

    //编辑
    protected void BtnEdit_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            string id = Convert.ToString(GridView1.DataKeys[i].Value);
            System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
            if (box.Checked == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>windowOpen('Edit','"+id+"')</script>");
                return;
            }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择要编辑的行');</script>");

    }

    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
      
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            System.Web.UI.WebControls.CheckBox box = (System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
            if (box.Checked == true)
            {
                string id = Convert.ToString(GridView1.DataKeys[i]["GUID"]);
                DataFunction.ExecuteNonQuery("delete from T_INFO_GZTS where guid='" + id + "'");
            }
        }
        BindGridPage(BindGrid());
    }

    //导出
    protected void BtnExl_Click(object sender, EventArgs e)
    {
        ExportExcel();
    }

    //导入数据方法
    protected void BtnOk_Click(object sender, EventArgs e)
    {
        try
        {
            //得到表结构用的
            DataTable dt = DataFunction.FillDataSet("select guid,cxnr, cxjg, yhlx, tslx, hffkxx, gzyy, slrq, hfrq, gzrq, ljcs, bz from T_INFO_GZTS where guid=''").Tables[0];

            Stream sm = FileUpLoad1.FileContent;
            License lic = new License();
            lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
            WorkbookDesigner designer1 = new WorkbookDesigner();
            Workbook book = designer1.Workbook;
            book.LoadData(sm);
            Worksheet ws = book.Worksheets[0];
            string mc = ws.Cells[0, 0].StringValue;
            if (mc.Trim() != "查询内容")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('模板不能更改!');</script>");
                return;
            }
            DateTime slrq, hfrq, gzrq;
            int idx = 0;
            for (int row = 0; row < ws.Cells.MaxRow; row++)
            {
                ++idx;
                DataRow dr = dt.NewRow();
                dr["guid"] = System.Guid.NewGuid().ToString().ToUpper();
                dr["cxnr"] = ws.Cells[idx, 0].StringValue;
                dr["cxjg"] = ws.Cells[idx, 1].StringValue;
                dr["yhlx"] = ws.Cells[idx, 2].StringValue;
                dr["tslx"] = ws.Cells[idx, 3].StringValue;
                dr["hffkxx"] = ws.Cells[idx, 4].StringValue;
                dr["gzyy"] = ws.Cells[idx, 5].StringValue;
                if (DateTime.TryParse(ws.Cells[idx, 6].StringValue, out slrq))
                {
                    dr["slrq"] = slrq;
                }
                //dr["slrq"] = ws.Cells[row + 1, 6].StringValue;
                if (DateTime.TryParse(ws.Cells[idx, 7].StringValue, out hfrq))
                {
                    dr["hfrq"] = hfrq;
                }
                //dr["hfrq"] = ws.Cells[row + 1, 7].StringValue;
                if (DateTime.TryParse(ws.Cells[idx, 8].StringValue, out gzrq))
                {
                    dr["gzrq"] = gzrq;
                }
                //dr["gzrq"] = ws.Cells[row + 1, 8].StringValue;
                if (string.IsNullOrEmpty(ws.Cells[idx, 9].StringValue))
                {
                    dr["ljcs"] = "0";
                }
                else
                {
                    if (!Regex.IsMatch(ws.Cells[idx, 9].StringValue, @"^[+-]?\d*$"))
                    {
                        dr["ljcs"] = "0";
                    }
                    else
                    {
                        dr["ljcs"] = ws.Cells[idx, 9].StringValue;
                    }
                }

                dr["bz"] = ws.Cells[idx, 10].StringValue;
                dt.Rows.Add(dr);
            }
            DataFunction.SaveData(dt.DataSet, "T_INFO_GZTS");
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('保存成功!');</script>");
            BindGridPage(BindGrid());
        }
        catch (Exception)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('导入发生错误!');</script>");
        }
    }
    #endregion

    #region 行生成事件
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion

    #region 导出方法
    private void ExportExcel()
    {
        string sql =getSql();
        sql="select tslx 投诉类型,cxnr 查询内容, cxjg 查询结果,slrq 受理日期,hfrq 回访日期,ljcs 累计次数,gzrq 整改日期 from ("+sql+")";
        DataTable dt = DataFunction.FillDataSet(sql).Tables[0];
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        WorkbookDesigner designer1 = new WorkbookDesigner();
        Workbook book = designer1.Workbook;
        Worksheet ws = book.Worksheets[0];
        int row = 0;
        //表头
        ws.Cells.SetRowHeight(0, 20);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            ws.Cells[row, i].PutValue(dt.Columns[i].ColumnName);
            ws.Cells[row, i].Style.Borders.SetStyle(CellBorderType.Thin);
            ws.Cells[row, i].Style.Borders.DiagonalStyle = CellBorderType.None;
            ws.Cells[row, i].Style.BackgroundColor = System.Drawing.Color.Blue;
            ws.Cells[row, i].Style.HorizontalAlignment = TextAlignmentType.Center;
            ws.Cells[row, i].Style.VerticalAlignment = TextAlignmentType.Center;
            ws.Cells[row, i].Style.IsTextWrapped = true;
            ws.Cells[row, i].Style.Font.IsBold = true;
        }
        ws.Cells.SetColumnWidth(3, 30);
        ws.Cells.SetColumnWidth(4, 30);
        ws.Cells.SetColumnWidth(6, 30);
        //数据
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ++row;
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                ws.Cells[row, col].PutValue(Convert.ToString(dt.Rows[i][col]));
                ws.Cells[row, col].Style.Borders.SetStyle(CellBorderType.Thin);
                ws.Cells[row, col].Style.Borders.DiagonalStyle = CellBorderType.None;
                ws.Cells[row, col].Style.BackgroundColor = System.Drawing.Color.Blue;
            }
        }
        designer1.Process();
        designer1.Save(ReturnUrlEncode("地址投诉信息.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
        Response.End();
            
    }
    public static string ReturnUrlEncode(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return "";
        return System.Web.HttpUtility.UrlEncode(fileName);
    }
    #endregion
  
}

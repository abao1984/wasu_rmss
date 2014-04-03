using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;

public partial class Web_Resource_JianKongSbEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.QueryString["guid"].IsNullOrEmpty())
            {
                GUID.Text = Convert.ToString(Request.QueryString["guid"]);
            }
            BindGrid();
        }
    }

    #region 绑定数据
      void BindGrid()
    {
        try
        {
            DataSet ds = DataFunction.FillDataSet("SELECT * FROM T_CON_JKPZB where guid='" + GUID.Text + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            }

            ds = DataFunction.FillDataSet("SELECT * FROM T_CON_JKPZB_cb where lsid='" + GUID.Text + "'");
            if (!ds.Tables[0].IsNullOrEmpty())
            {
                GridViewCb.DataSource = ds;
                GridViewCb.DataBind();
            }
            else
            {
                DataRow dr = ds.Tables[0].NewRow();
                ds.Tables[0].Rows.Add(dr);
                GridViewCb.DataSource = ds;
                GridViewCb.DataBind();
                int col = GridViewCb.Columns.Count;
                GridViewCb.Rows[0].Cells.Clear();
                GridViewCb.Rows[0].Cells.Add(new TableCell());
                GridViewCb.Rows[0].Cells[0].ColumnSpan = col;
                GridViewCb.Rows[0].Cells[0].Text = "没有相关数据！";
                GridViewCb.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }

    }
    #endregion


    #region 刷新客户信息
    protected void BtnSetRmss_Click(object sender, EventArgs e)
    {
        string khbh = KHBH.Text;
        DataSet ds = DataFunction.FillDataSet("select CUSTOMER_CODE KHBH,customer_name KHMC,region KHQY from rmss where subscriber_id='" + khbh + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
        }
    }
    #endregion

    #region 删除主表数据
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataFunction.ExecuteNonQuery("delete from T_CON_JKPZB where GUID='" + GUID.Text + "'");
            DataFunction.ExecuteNonQuery("delete from T_CON_JKPZB_CB where lsid='" + GUID.Text + "'");
            BindGrid();
            this.Alert("删除成功...");
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }
    }
    #endregion

    #region 保存主表数据
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = DataFunction.FillDataSet("SELECT * FROM T_CON_JKPZB where guid='" + GUID.Text + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                GUID.Text = System.Guid.NewGuid().ToString().ToUpper(); ;
                ds.Tables[0].Rows.Add(dr);
                ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
                DataFunction.SaveData(ds, "T_CON_JKPZB");
                
            }
            else
            {
                ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
                DataFunction.SaveData(ds, "T_CON_JKPZB");
            }
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }
    }
    #endregion

    #region 删除从表数据
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        try
        {
            int idx = 0;
            GridViewCb.Rows.Cast<GridViewRow>().ForEach(dr =>
            {
                bool ch = ((System.Web.UI.WebControls.CheckBox)dr.Cells[0].FindControl("CheckBox1")).Checked;
                if (ch == true)
                {
                    string guid = GridViewCb.DataKeys[idx].Values["GUID"].ToString();
                    DataFunction.ExecuteNonQuery("delete from T_CON_JKPZB_cb where guid='" + guid + "'");
                }
                ++idx;
            });
            BindGrid();
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }
    }
    #endregion

    #region 刷新
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    #endregion

    #region 行生成事件
    protected void GridViewCb_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string guid = GridViewCb.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "checkZbGuid('" + guid + "')");

        }
    }
    #endregion

    #region 导出方法
    protected void BtnExp_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        Worksheet ws = book.Worksheets[0];

        int rowindex = 0;
        int colindex = 0;
        //导主表表头
        DataTable zbdt = DataFunction.FillDataSet("select t.khbh 客户编号,t.khmc 客户名称,t.khqy 客户所属区域,t.xmfl 项目分类,t.jkptxx 监控平台信息,t.ywbm 基础链路编码,t.ipdz IP地址,t.jksb 监控设备  from t_con_jkpzb t  where t.guid='" + GUID.Text + "'").Tables[0];
        zbdt.Columns.Cast<DataColumn>().ForEach(col =>
        {
            ws.Cells[rowindex, colindex].PutValue(col.ColumnName);
            ws.Cells[rowindex, colindex].Style.Font.IsBold = true;
            ws.Cells.SetColumnWidth(colindex, 18);
            ws.Cells[rowindex, colindex].Style.HorizontalAlignment = TextAlignmentType.Center;
            ws.Cells[rowindex, colindex].Style.Borders.SetStyle(CellBorderType.Thin);
            ws.Cells[rowindex, colindex].Style.Borders.DiagonalStyle = CellBorderType.None;
            ++colindex;
        });
        ws.Cells.SetRowHeight(0, 20);

        //导主表数据
        zbdt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            ++rowindex;
            colindex = 0;
            zbdt.Columns.Cast<DataColumn>().ForEach(col =>
               {
                   ws.Cells[rowindex, colindex].PutValue(Convert.ToString(dr[col]));
                   ws.Cells[rowindex, colindex].Style.Borders.SetStyle(CellBorderType.Thin);
                   ws.Cells[rowindex, colindex].Style.Borders.DiagonalStyle = CellBorderType.None;
                   ++colindex;
               });
        });

        colindex = 1;
        //导从表表头
        DataTable cbdt = DataFunction.FillDataSet(@"select a.tdh 通道号,a.jkywbm 监控点业务编码,a.jkdmc 监控点名称,a.jkazdd 监控点安装地址,a.sxjlx 摄像机类型,a.sxjxh 摄像机型号,a.sccs 生产厂商,
                                a.sxjbh 摄像机编号,a.szbm 数据编码,a.db33bm DB33编码,a.ssjd 所属街道,a.lx 类型,a.lgxh 立杆型号,a.lgcs 立杆厂商,a.kgrq 开工日期,a.tswcrq 调试完成日期,
                                a.sgdw 施工单位,a.gxdw 共享单位,a.whdw 维护单位,a.swys 是否验收,a.bz 备注 from t_con_jkpzb_cb a where a.lsid='" + GUID.Text + "'").Tables[0];




        //导从表表头 
        ++rowindex;
        cbdt.Columns.Cast<DataColumn>().ForEach(col =>
        {
            ws.Cells.SetColumnWidth(colindex, 18);
            ws.Cells[rowindex, colindex].PutValue(col.ColumnName);
            ws.Cells[rowindex, colindex].Style.Font.IsBold = true;
            ws.Cells[rowindex, colindex].Style.HorizontalAlignment = TextAlignmentType.Center;
            ws.Cells[rowindex, colindex].Style.Borders.SetStyle(CellBorderType.Thin);
            ws.Cells[rowindex, colindex].Style.Borders.DiagonalStyle = CellBorderType.None;
            ++colindex;
        });
        ws.Cells.SetRowHeight(rowindex, 20);

        //导从表数据
        int idx = 0;
        colindex = 1;
        cbdt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            ++rowindex;
           
            ++idx;
            ws.Cells[rowindex, colindex - 1].PutValue(idx + "、");
            ws.Cells[rowindex, colindex - 1].Style.HorizontalAlignment = TextAlignmentType.Right;
            cbdt.Columns.Cast<DataColumn>().ForEach(col =>
            {
                ws.Cells[rowindex, colindex].PutValue(Convert.ToString(dr[col]));
                ws.Cells[rowindex, colindex].Style.Borders.SetStyle(CellBorderType.Thin);
                ws.Cells[rowindex, colindex].Style.Borders.DiagonalStyle = CellBorderType.None;
                ++colindex;
            });
            colindex = 1;
        });

        MemoryStream ms = new MemoryStream();
        book.Save(ms, FileFormatType.Excel2003);
        IDP.Common.WebUtils.ResponseWriteBinary(ms.ToArray(), "监控配置单.xls");


    }
    #endregion
}

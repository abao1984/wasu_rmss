using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;

public partial class Web_Resource_ConfigBackBoneList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZYHS_BJ.Text = Request.QueryString["ZYHS_BJ"];
            //SQSJ1.Attributes.Add("readonly","true");
            //SQSJ2.Attributes.Add("readonly", "true");
            //QYSJ1.Attributes.Add("readonly", "true");
            //QYSJ2.Attributes.Add("readonly", "true");
            if (ZYHS_BJ.Text == "0")
            {
                BtnAdd.Visible = false;
            }
            BindDDL();
            gvBoneList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGV());
        }
    }
    private void BindDDL()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'YWLX' order by sequence");
        YWLX.DataSource = ds;
        YWLX.DataTextField = "ENUM_NAME";
        YWLX.DataValueField = "ENUM_NAME";
        YWLX.DataBind();
        YWLX.Items.Add(new ListItem("", ""));
        YWLX.SelectedValue = "";
    }
    private int BindGV()
    {
        int count = 0;
        string sql = "select t.* from T_CON_BONE_BUSINESS t where ZYHS_BJ='" + ZYHS_BJ.Text + "'";
        if (YWBM.Text!="")
        {
            sql += " and t.YWBM like '%" + YWBM.Text.Trim() + "%'";
        }
        if (!string.IsNullOrEmpty(YWLX.SelectedValue))
        {
            sql += " and t.YWLX = '" + YWLX.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(LLMC.Text))
        {
            sql += " and t.LLMC like '%" + LLMC.Text + "%'";
        }
        if (!string.IsNullOrEmpty(WZXH.Text))
        {
            sql += " and t.WZXH like '%" + WZXH.Text + "%'";
        }
        if (!string.IsNullOrEmpty(JDJRJF_GUID.Text))
        {
            sql += " and t.JDJRJF_GUID = '" + JDJRJF_GUID.Text + "'";
        }
        else
        {
            if (!string.IsNullOrEmpty(JDJRJF_CODE.Text))
            {
                sql += " and t.JDJRJF_CODE like '%" + JDJRJF_CODE.Text + "%'";
            }
            if (!string.IsNullOrEmpty(JDJRJF.Text))
            {
                sql += " and t.JDJRJF like '%" + JDJRJF.Text + "%'";
            }
        }
        if (!string.IsNullOrEmpty(JDSBLX.Text))
        {
            sql += " and t.JDSBLX = '" + JDSBLX.Text + "'";
        }
        if (!string.IsNullOrEmpty(JDSB_GUID.Text))
        {
            sql += " and t.JDSB_GUID = '" + JDSB_GUID.Text + "'";
        }
        else
        {
            if (!string.IsNullOrEmpty(JDSB_CODE.Text))
            {
                sql += " and t.JDSB_CODE like '%" + JDSB_CODE.Text + "%'";
            }
            if (!string.IsNullOrEmpty(JDSB.Text))
            {
                sql += " and t.JDSB like '%" + JDSB.Text + "%'";
            }
        }

        if (!string.IsNullOrEmpty(JDSBDK_GUID.Text))
        {
            sql += " and t.JDSBDK_GUID = '" + JDSBDK_GUID.Text + "'";
        }
        else
        {
            if (!string.IsNullOrEmpty(JDSBDK.Text))
            {
                sql += " and t.JDSBDK like '%" + JDSBDK.Text + "%'";
            }
        }

        if (!string.IsNullOrEmpty(YDJRJF_CODE.Text))
        {
            sql += " and t.YDJRJF_CODE  like '%" + YDJRJF_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(YDSBLX.Text))
        {
            sql += " and t.YDSBLX = '" + YDSBLX.Text + "'";
        }
        if (!string.IsNullOrEmpty(YDSB_GUID.Text))
        {
            sql += " and t.YDSB_GUID = '" + YDSB_GUID.Text + "'";
        }
        else
        {
            if (!string.IsNullOrEmpty(YDSB_CODE.Text))
            {
                sql += " and t.YDSB_CODE like '%" + YDSB_CODE.Text + "%'";
            }
            if (!string.IsNullOrEmpty(YDSB.Text))
            {
                sql += " and t.YDSB like '%" + YDSB.Text + "%'";
            }
        }
        if (!string.IsNullOrEmpty(YDSBDK_GUID.Text))
        {
            sql += " and t.YDSBDK_GUID = '" + YDSBDK_GUID.Text + "'";
        }
        else
        {
            if (!string.IsNullOrEmpty(YDSBDK.Text))
            {
                sql += " and t.YDSBDK like '%" + YDSBDK.Text + "%'";
            }
        }

        if (!string.IsNullOrEmpty(QYSJ1.Text))
        {
            sql += " and t.QDSJ >= to_date('" + QYSJ1.Text + "','YYYY-MM-DD')";
        }
        if (!string.IsNullOrEmpty(QYSJ2.Text))
        {
            sql += " and t.QDSJ <= to_date('" + QYSJ2.Text + "','YYYY-MM-DD')";
        }
        if (!string.IsNullOrEmpty(SQSJ1.Text))
        {
            sql += " and t.QDSJ >= to_date('" + SQSJ1.Text + "','YYYY-MM-DD')";
        }
        if (!string.IsNullOrEmpty(SQSJ2.Text))
        {
            sql += " and t.QDSJ <= to_date('" + SQSJ2.Text + "','YYYY-MM-DD')";
        }
        sql += " order by t.createdatetime desc";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvBoneList.DataSource = ds;
            gvBoneList.DataBind();
            int count1 = gvBoneList.Columns.Count;
            gvBoneList.Rows[0].Cells.Clear();
            //gvBoneList.Rows[0].Cells.Add(new TableCell());
            //gvBoneList.Rows[0].Cells[0].Text = "没有相关的信息！";
            //gvBoneList.Rows[0].Cells[0].ColumnSpan = count1;
            //gvBoneList.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            count = ds.Tables[0].Rows.Count;
            gvBoneList.DataSource = ds;
            gvBoneList.DataBind();
        }
        return count;
    }
    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }
    //新增
    protected void BtnAdd_Click(object sender, EventArgs e)
    {

    }
    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string ywguids = "''";
        foreach (GridViewRow row in gvBoneList.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                ywguids += ",'" + gvBoneList.DataKeys[row.RowIndex]["YWGUID"].ToString() + "'";
            }
        }
        if (!ywguids.Equals("''"))
        {
            DataFunction.ExecuteNonQuery(string.Format("delete from T_CON_BONE_BUSINESS where YWGUID in ({0})", ywguids));
            BindGV();
        }
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvBoneList.PageIndex = 0;
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
        gvBoneList.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvBoneList.PageIndex + 1);
        BindGV();
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
        gvBoneList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvBoneList.PageIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvBoneList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvBoneList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvBoneList.PageIndex + 1);
        BindGV();
    }
    #endregion

    protected void Btn_Click(object sender, EventArgs e)
    {
        int DataCount = BindGV();
        DataCountLab.Text = DataCount.ToString();
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        PageCountLab.Text = PageCount.ToString();
    }
    protected void gvBoneList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string ywguid = gvBoneList.DataKeys[e.Row.RowIndex]["YWGUID"].ToString();
            if (!ywguid.Equals(""))
            {
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }


    protected void BtnExp_Click(object sender, EventArgs e)
    {
       

    }



    #region 导出Excel
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        Worksheet ws = book.Worksheets[0];
        int col = -1;
        int row = 0;
        //导出表头
        gvBoneList.Columns.Cast<DataControlField>().ForEach(con =>
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
        gvBoneList.Rows.Cast<GridViewRow>().ForEach(dr =>
        {
            ++row;
            int idx = 0;
            col = -1;
            gvBoneList.Columns.Cast<DataControlField>().ForEach(con =>
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
        IDP.Common.WebUtils.ResponseWriteBinary(by, "骨干业务管理.xls");
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;

public partial class Web_GZCL_FDZ_FanDanZiQuery : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            KHQY.Attributes.Add("ReadOnly", "true");
            
            //TSSJ1.Text = DateTime.Now.ToString("yyyy-MM-dd");

            string type=Request.QueryString["type"];
            if(type == "fdcl")
            {
                GridView1.Columns[0].Visible = true;
                BtnXG.Style.Add("display","bolck");
            }
            //BindGridPage(BindGrid());
        }
    }

    #region 得到SQL语句
    private string getSql()
    {
        string sql = "";

        string type = dropZT.SelectedValue.Trim();
        string userID = Session["UserID"].ToString();
        DateTime datetime = DateTime.Now;

        switch (type)
        {
            case "电话受理":
                sql = string.Format("select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='电话处理' and ( ldsj is null or to_char(t.ldsj,'yyyy-mm-dd')>='{0}')", datetime.AddDays(-1).ToString("yyyy-MM-dd"));
                //per.Text = "dhsl";
                break;
            case "调度发单":
                sql = "select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='调度发单'";
                //per.Text = "ddfd";
                break;
            case "维修返单":
                sql = "select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='维修返单'";
                //per.Text = "wxfd";
                //GridView1.Columns[7].Visible = true;
                break;
            case "遗单":
                sql = "select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='遗单'";
                break;
            case "留单":
                sql = string.Format("select t.*, t.rowid from t_fau_zb2 t where t.fdzzt='电话处理' and ( ldsj is not null or to_char(t.ldsj,'yyyy-mm-dd') > '{0}')", datetime.AddDays(-1).ToString("yyyy-MM-dd"));
                break;
            case "":
                sql = string.Format("select t.*  from t_fau_zb2 t where t.fdzzt is not null ");
                //if (Request.QueryString["type"] == "fdcl")
                //{
                //    sql += " and fdzzt <> '结单'";
                //}
                break;
        }

        if (KHMC.Text.Trim() != "")
        {
            sql += " and GZMC like '%" + KHMC.Text.Trim() + "%'";
        }

        if (TSBH.Text != "")
        {
            sql += " and GZBH like '%" + TSBH.Text.Trim() + "%'";
        }

        if (KHQY.Text != "")
        {
            sql += " and KHQY='" + KHQY.Text.Trim() + "'";
        }

        if (TSSJ1.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd')>='" + TSSJ1.Text + "'";
        }

        if (TSSJ2.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd')<='" + TSSJ2.Text + "'";
        }

        //if (Session["ISSUPER"].ToString() != "1")
        //{
        //    if (Session["FWQY"] != null)
        //    {
        //        string sql1 = "";
        //        string[] fwqy = Session["FWQY"].ToString().Split(',');
        //        foreach (string fwqy1 in fwqy)
        //        {

        //            if (sql1 != "")
        //            {
        //                sql1 += " or t.KHQYID like '" + fwqy1 + "%'";
        //            }
        //            else
        //            {
        //                sql1 += "t.KHQYID like '" + fwqy1 + "%'";
        //            }
        //        }
        //        sql += " and (" + sql1 + ")";
        //    }
        //    else
        //    {
        //        sql += " and 1<>1";
        //    }
        //}

        if (type == "dhsl")
        {
            sql += " order by tssj";
        }
        return sql;
    }
    #endregion

    #region 绑定数据
    private int BindGrid()
    {
        string sql = getSql();
        DataSet ds = DataFunction.FillDataSet(sql);
        return gzcl.BindGridView(GridView1, ds);
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

    protected void BtnJS_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    protected void CheckAll(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)sender;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("ItemCheckBox");
            ch.Checked = cbx.Checked;
        }
    }

    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowIndex > -1)
    //    {
    //        string sql = "select CLRY from t_fau_cllc2 t where zbguid='" + GridView1.DataKeys[e.Row.RowIndex].Value + "' and LCCZ ='发单'";
    //        System.Web.UI.WebControls.Label lable = e.Row.Cells[7].FindControl("Lable1") as System.Web.UI.WebControls.Label;
    //        lable.Text = DataFunction.GetStringResult(sql);
    //    }
    //}
    //protected void BtnXG_Click(object sender, EventArgs e)
    //{

    //}
    protected void btn_Click(object sender, EventArgs e)
    {
        if (xggzzt.Text == "")
        {
            // ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('修改失败！')</script>");
            return;
        }
        string guids = "''";
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("ItemCheckBox");
            //ch.Checked = cbx.Checked;
            if (ch.Checked)
            {
                guids += ",'" + GridView1.DataKeys[gvr.RowIndex]["ZBGUID"].ToString() + "'";
            }
        }

        if (guids != "''")
        {

            string sql = "";//string.Format(@"update t_fau_zb2 t set t.fdzzt='{0}' ", xggzzt.Text);
            if (xggzzt.Text == "留单")
            {
                sql = string.Format(@"update t_fau_zb2 t set t.fdzzt='电话处理',t.ldsj=to_date('{0}','yyyy-mm-dd hh24:mi:ss') where zbguid in ({1})", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), guids);
            }
            else
            {
                sql = string.Format(@"update t_fau_zb2 t set t.fdzzt='{0}',t.ldsj=null where zbguid in ({1})", xggzzt.Text, guids);
            }
            DataFunction.ExecuteNonQuery(sql);
            
        }
        BindGridPage(BindGrid());
        
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string clr = e.Row.Cells[11].Text;
            string clsm = e.Row.Cells[12].Text;
            string zbguid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();

            string sql = string.Format(@"select t.* from t_fau_cllc2 t where t.clsj=(select max(clsj) from t_fau_cllc2  where zbguid='{0}') and t.zbguid='{0}'", zbguid);
            DataRow dr = DataFunction.GetSingleRow(sql);
            if (clr == "" || clr == "&nbsp;")
            {
                e.Row.Cells[11].Text = dr["clry"].ToString();
            }
            if (clsm == "" || clsm == "&nbsp;")
            {
                e.Row.Cells[12].Text = dr["clsm"].ToString();
            }
        }
    }
    protected void btn_Click1(object sender, EventArgs e)
    {
        if (xggzzt.Text == "")
        {
            // ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('修改失败！')</script>");
            return;
        }
        string guids = "''";
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gvr.FindControl("ItemCheckBox");
            //ch.Checked = cbx.Checked;
            if (ch.Checked)
            {
                guids += ",'" + GridView1.DataKeys[gvr.RowIndex]["ZBGUID"].ToString() + "'";
            }
        }

        if (guids != "''")
        {
            //dsh t.gzzt='处理中' and  3.30
            string sql = "";//string.Format(@"update t_fau_zb2 t set t.fdzzt='{0}' ", xggzzt.Text);
            if (xggzzt.Text == "留单")
            {
                sql = string.Format(@"update t_fau_zb2 t set t.gzzt='处理中' ,t.fdzzt='电话处理',t.ldsj=to_date('{0}','yyyy-mm-dd hh24:mi:ss'),SFSD='1',SDRY='',gzsdsj =null where zbguid in ({1})", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), guids);
            }
            else
            {
                sql = string.Format(@"update t_fau_zb2 t set t.gzzt='处理中' ,t.fdzzt='{0}',t.ldsj=null,SFSD='1',SDRY='',gzsdsj =null where zbguid in ({1})", xggzzt.Text, guids);
            }
            DataFunction.ExecuteNonQuery(sql);
            xggzzt.Text = "";
        }
        BtnJS_Click(null, null);

        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('修改成功！')</script>");
    }

    #region 导出Excel
    protected void BtnExp_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../../Aspose.Total.lic"));
        WorkbookDesigner designer1 = new WorkbookDesigner();
        Workbook book = designer1.Workbook;
        Worksheet ws = book.Worksheets[0];

        string sql = "select gzbh 故障编号,t.tssj 投诉时间,t.dhslsj 电话处理时间,t.ywzt 业务主体,t.hyfl 行业分类,t.customer_level 客户等级,t.gzdj 故障等级,t.gzmc 故障名称,t.khdz 地址,t.LXRNAME 联系人,t.DDFDR 处理人员,t.fdylsm 处理说明,t.FDZZT 故障状态,zbguid id from (" + getSql() + ") t";
  

        DataTable dt = DataFunction.FillDataSet(sql).Tables[0];

        int idx = 0;

        //导出表头
        for (int col = 0; col < dt.Columns.Count-1; col++)
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
            //应为绑定GridView时也是这样处理的，如果主表里没有处理人员和处理说明，就到符表中得  罗耀斌  2011-6-14 12:00
            string zbguid = Convert.ToString(dt.Rows[row][dt.Columns.Count - 1]);
            DataTable fbdt = DataFunction.FillDataSet(@"select t.* from t_fau_cllc2 t where t.clsj=(select max(clsj) from t_fau_cllc2  where zbguid='" + zbguid + "') and t.zbguid='" + zbguid + "'").Tables[0];
            for (int col = 0; col < dt.Columns.Count-1; col++)
            {
                if (dt.Columns[col].ColumnName == "处理人员")
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[row][col])) && fbdt.Rows.Count>0)
                    {
                        dt.Rows[row][col] = fbdt.Rows[0]["clry"];
                    }
                }
                else if (dt.Columns[col].ColumnName == "处理说明")
                {
                    if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[row][col])) && fbdt.Rows.Count > 0)
                    {
                        dt.Rows[row][col] = fbdt.Rows[0]["clsm"];
                    }
                }
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
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string order = e.SortExpression;
        string sql = getSql();
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
        GridView1.DataSource = dv;
        GridView1.DataBind();
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
}

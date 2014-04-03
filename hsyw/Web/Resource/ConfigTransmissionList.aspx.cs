using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Aspose.Cells;

public partial class Web_Resource_ConfigTransmissionList : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ZYHS_BJ.Text = Request.QueryString["ZYHS_BJ"];
            ShareFunction.BindEnumDropList(ZWFS, "ZWFS");
            ShareFunction.BindEnumDropList(LLDK, "LLDK");
            if (ZYHS_BJ.Text == "0")
            {
                BtnAdd.Visible = false;
            }
            JRSB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            //PZRQ1.Attributes.Add("readonly", "true");
            //PZRQ2.Attributes.Add("readonly", "true");
            gvCSYWList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGV());
        }
    }
    //private void BindDDL()
    //{
    //    DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'DKLX' order by sequence");
    //    DKLX.DataSource = ds;
    //    DKLX.DataTextField = "ENUM_NAME";
    //    DKLX.DataValueField = "ENUM_NAME";
    //    DKLX.DataBind();
    //    DKLX.Items.Insert(0, new ListItem("", ""));
    //    DKLX.SelectedIndex = 0;

    //    //ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'KHJB' order by sequence");
    //    //KHJB.DataSource = ds;
    //    //KHJB.DataTextField = "ENUM_NAME";
    //    //KHJB.DataValueField = "ENUM_NAME";
    //    //KHJB.DataBind();
    //    //KHJB.Items.Insert(0, new ListItem("", ""));
    //    //KHJB.SelectedIndex = 0;
    //}
    private int BindGV()
    {
        int count = 0;
        string sql = getQuerySql();
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvCSYWList.DataSource = ds;
            gvCSYWList.DataBind();
            int count1 = gvCSYWList.Columns.Count;
            gvCSYWList.Rows[0].Cells.Clear();
            gvCSYWList.Rows[0].Cells.Add(new TableCell());
        }
        else
        {
            count = ds.Tables[0].Rows.Count;
            //DataView dv = ds.Tables[0].DefaultView;
            //if (txtorder.Text != "")
            //{
            //    dv.Sort = txtorder.Text;
            //    //ViewState["SortName"] = txtorder.Text;
            //}
            gvCSYWList.DataSource = ds;
            gvCSYWList.DataBind();
        }
        return count;
    }
    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }
    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string ywguids = "''";
        foreach (GridViewRow row in gvCSYWList.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                ywguids += ",'" + gvCSYWList.DataKeys[row.RowIndex]["YWGUID"].ToString() + "'";
            }
        }
        if (!ywguids.Equals("''"))
        {
            string[] sql = new string[2];
            sql[0] = string.Format("delete from T_CON_TRANSM_BUSSINESS where YWGUID in ({0})", ywguids);
            sql[1] = string.Format("delete from T_CON_TRANSM_BUSSINESS_CB where YWGUID in ({0})", ywguids);
            DataFunction.ExecuteTransaction(sql);
            BindGridPage(BindGV());
        }
    }
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvCSYWList.PageIndex = 0;
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
        gvCSYWList.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvCSYWList.PageIndex + 1);
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
        gvCSYWList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvCSYWList.PageIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCSYWList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCSYWList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvCSYWList.PageIndex + 1);
        BindGV();
    }
    #endregion
    protected void gvCSYWList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            //添加甲、乙端联系方式
            
            string ywguid = gvCSYWList.DataKeys[e.Row.RowIndex]["YWGUID"].ToString();
            if (!ywguid.Equals(""))
            {
                string ywlx = e.Row.Cells[2].Text;
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }
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
    //protected void Btn1_Click(object sender, EventArgs e)
    //{
    //    DropDownList ddl = Page.FindControl(DDLID.Text) as DropDownList;
    //    string sv = ddl.SelectedValue;
    //    DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_RES_SYS_ENUMDATA where enum_sort = '{0}' order by sequence", DDLLX.Text));
    //    if (ds.Tables[0].Select("ENUM_NAME = '" + sv + "'").Length == 0)
    //    {
    //        sv = "";
    //    }
    //    ddl.DataSource = ds;
    //    ddl.DataTextField = "ENUM_NAME";
    //    ddl.DataValueField = "ENUM_NAME";
    //    ddl.DataBind();
    //    ddl.Items.Insert(0, new ListItem("", ""));
    //    ddl.SelectedValue = sv;

    //    if (gvCSYWList.Rows.Count > 0 && gvCSYWList.Rows[0].Cells[0].Text == "没有相关的信息！")
    //    {
    //        int count1 = gvCSYWList.Columns.Count;
    //        gvCSYWList.Rows[0].Cells.Clear();
    //        gvCSYWList.Rows[0].Cells.Add(new TableCell());
    //        gvCSYWList.Rows[0].Cells[0].Text = "没有相关的信息！";
    //        gvCSYWList.Rows[0].Cells[0].ColumnSpan = count1;
    //        gvCSYWList.Rows[0].Cells[0].Style.Add("text-align", "center");
    //    }
    //}
    protected void BtnRmss_Click(object sender, EventArgs e)
    {

        FillRmssPage(SUBSCRIBER_ID.Text, "");

    }

   

    /// <summary>
    /// 填充业务编码内容
    /// </summary>
    /// <param name="SUBSCRIBER_ID">业务ID</param>
    /// <param name="JYD">甲乙端</param>
    private void FillRmssPage(string str_SUBSCRIBER_ID, string JYD)
    {
        string sql = string.Format("select * from rmss t where t.SUBSCRIBER_ID='{0}'", str_SUBSCRIBER_ID);
        DataRow DR = DataFunction.GetSingleRow(sql);
        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName = JYD + DC.ColumnName;
            string strValue = DR[DC.ColumnName].ToString(); ;
            System.Web.UI.Control webControl = this.Page.FindControl(ControlName);
            if (webControl != null)
            {
                string controlType = webControl.GetType().FullName;
                switch (controlType)
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((System.Web.UI.WebControls.TextBox)webControl).Text = strValue;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        ShareFunction.SetDropListSelectedValue(((System.Web.UI.WebControls.DropDownList)webControl), strValue);
                        break;
                }
            }
        }
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        Worksheet ws = book.Worksheets[0];
        int col = -1;
        int row = 0;
        //导出表头
        gvCSYWList.Columns.Cast<DataControlField>().ForEach(con =>
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
        string sql = getQuerySql();
        DataSet ds = DataFunction.FillDataSet(sql);
        ds.Tables[0].Rows.Cast<DataRow>().ForEach(dr =>
        {
            ++row;
            int idx = 0;
            col = -1;
            gvCSYWList.Columns.Cast<DataControlField>().ForEach(con =>
            {
                if (col > -1)
                {
                    string sort = con.SortExpression;
                    if (sort != "")
                    {

                        ws.Cells[row, col].PutValue(dr[sort]);
                        ws.Cells[row, col].Style.Borders.SetStyle(CellBorderType.Thin);
                        ws.Cells[row, col].Style.Borders.DiagonalStyle = CellBorderType.None;
                        ws.Cells[row, col].Style.Font.Size = 8;
                    }
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
        IDP.Common.WebUtils.ResponseWriteBinary(by, "传输业务配置.xls");
    }

    private string getQuerySql()
    {
        string sql = @"select distinct t.*,
jd.REGION as REGION,
jd.SUBSCRIBER_CODE as JD_SUBSCRIBER_CODE,
jd.SUB_NAME as JD_SUB_NAME,
yd.SUBSCRIBER_CODE as YD_SUBSCRIBER_CODE,
yd.SUB_NAME as YD_SUB_NAME,
jd.CUSTOMER_LEVEL as JD_CUSTOMER_LEVEL,
jd.LINKMAN as JD_LINKMAN,yd.LINKMAN as YD_LINKMAN, jd.PHONE_NO, jd.MOBILE_NO,
jd.MOBILE_NO||case when jd.MOBILE_NO is not null and jd.MOBILE_NO<> ' ' and jd.PHONE_NO is not null and  jd.PHONE_NO <>' '  then '/' end||jd.PHONE_NO as JD_LINK,
yd.MOBILE_NO||case when yd.MOBILE_NO is not null and yd.MOBILE_NO<>' ' and yd.PHONE_NO is not null and yd.PHONE_NO <>' ' then '/' end||yd.PHONE_NO as YD_LINK
from T_CON_TRANSM_BUSSINESS t
left join T_CON_TRANSM_BUSSINESS_CB c on t.YWGUID = c.YWGUID 
left join rmss jd on t.jd_subscriber_id=jd.SUBSCRIBER_ID
left join rmss yd on t.yd_subscriber_id=yd.SUBSCRIBER_ID
where ZYHS_BJ='" + ZYHS_BJ.Text + "' ";
        if (!string.IsNullOrEmpty(ZWFS.Text))
        {
            sql += " and t.ZWFS like '%" + ZWFS.Text + "%'";
        }
        if (!string.IsNullOrEmpty(LLDK.Text))
        {
            sql += " and t.LLDK like '%" + LLDK.Text + "%'";
        }
        if (!string.IsNullOrEmpty(PZKSRQ.Text))
        {
            sql += " and t.PZRQ >= to_date('" + PZKSRQ.Text + "','YYYY-MM-DD') ";
        }
        if (!string.IsNullOrEmpty(PZJSRQ.Text))
        {
            sql += " and c.PZRQ <= to_date('" + PZJSRQ.Text + "','YYYY-MM-DD') ";
        }
        if (!string.IsNullOrEmpty(SUBSCRIBER_CODE.Text))
        {
            sql += " and (jd.SUBSCRIBER_CODE like '%" + SUBSCRIBER_CODE.Text + "%' or yd.SUBSCRIBER_CODE like '%" + SUBSCRIBER_CODE.Text + "%' )";
        }
        if (!string.IsNullOrEmpty(SUB_NAME.Text))
        {
            sql += " and (jd.SUB_NAME like '%" + SUB_NAME.Text + "%' or yd.SUB_NAME like '%" + SUB_NAME.Text + "%' )";
        }
        if (!string.IsNullOrEmpty(REGION.Text))
        {
            sql += " and (jd.REGION like '%" + REGION.Text + "%' or yd.REGION like '%" + REGION.Text + "%')";
        }
        if (!string.IsNullOrEmpty(CUSTOMER_NAME.Text))
        {
            sql += " and (jd.CUSTOMER_NAME like '%" + CUSTOMER_NAME.Text + "%' or yd.CUSTOMER_NAME like '%" + CUSTOMER_NAME.Text + "%' )";
        }
        if (!string.IsNullOrEmpty(CUSTTYPE1.Text))
        {
            sql += " and (jd.CUSTTYPE1 like '%" + CUSTTYPE1.Text + "%' or yd.CUSTTYPE1 like '%" + CUSTTYPE1.Text + "%')";
        }

        if (!string.IsNullOrEmpty(JRJF_GUID.Text))
        {
            sql += " and (c.JDJRJF_GUID = '" + JRJF_GUID.Text + "' or c.YDJRJF_GUID = '" + JRJF_GUID.Text + "')";
        }
        else if (!string.IsNullOrEmpty(JRJF.Text))
        {
            sql += " and (c.JDJRJF like '%" + JRJF.Text + "%' or c.YDJRJF like '%" + JRJF.Text + "%' )";
        }
        if (!string.IsNullOrEmpty(SBDK_GUID.Text))
        {
            sql += " and (c.JDSBDK_GUID = '" + SBDK_GUID.Text + "' or c.YDSBDK_GUID = '" + SBDK_GUID.Text + "')";
        }
        else if (!string.IsNullOrEmpty(SBDK.Text))
        {
            sql += " and (c.JDSBDK like '%" + SBDK.Text + "%' or c.YDSBDK like '%" + SBDK.Text + "%')";
        }

        //if (!string.IsNullOrEmpty(JRSB_GUID.Text))
        //{
        //    sql += "and (c.JDJRSB_GUID =')" + JRSB_GUID.Text + "' or c.YDJRSB_GUID ='" + JRSB_GUID.Text + "')";
        //}
        //else 
        if (!string.IsNullOrEmpty(JRSB.Text))
        {
            sql += "and (c.JDJRSB  like '%)" + JRSB.Text + "%' or c.YDJRSB like'%" + JRSB.Text + "%')";
        }
        sql += " order by t.createdatetime desc";
        return sql;
    }
    protected void gvCSYWList_Sorting(object sender, GridViewSortEventArgs e)
    {
        int num = 0;
        string order = e.SortExpression;
        string sql = getQuerySql();
        if (!sql.IsNullOrEmpty())
        {
            DataView dv = DataFunction.FillDataSet(sql).Tables[0].DefaultView;
            if (this.SortAscending)
            {
                this.SortAscending = false;
                order = e.SortExpression + " desc ";
            }
            else
            {
                this.SortAscending = true;
                order = e.SortExpression + " asc ";
            }
            dv.Sort = order;
            txtorder.Text = order;
            ViewState["SortName"] = order;
            gvCSYWList.DataSource = dv;
            gvCSYWList.DataBind();

            num = dv.Count;
        }
        BindGridPage(num);
       
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;

public partial class Web_Resource_ConfigLightList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            JRSB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898,66a85a26-6e7b-42c6-ac01-678697ba3023";
            
            ZYHS_BJ.Text = Request.QueryString["ZYHS_BJ"];
            if (ZYHS_BJ.Text == "0")
            {
                BtnAdd.Visible = false;
            }
            BindDDL();
            gvLightList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGV());
          
        }
    }
    private void BindDDL()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'GLYWLX' order by sequence");
        YWLX.DataSource = ds;
        YWLX.DataTextField = "ENUM_NAME";
        YWLX.DataValueField = "ENUM_NAME";
        YWLX.DataBind();
        YWLX.Items.Insert(0, new ListItem("", ""));
        YWLX.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'KHLB' order by sequence");
        CUSTTYPE.DataSource = ds;
        CUSTTYPE.DataTextField = "ENUM_NAME";
        CUSTTYPE.DataValueField = "ENUM_NAME";
        CUSTTYPE.DataBind();
        CUSTTYPE.Items.Insert(0, new ListItem("", ""));
        CUSTTYPE.SelectedIndex = 0;
    }
    private string GetQuerySql()
    {
        string sql = "select distinct t.*,nvl(t.VJF,nvl(t.JDJF,'')||'  '||nvl(t.YDJF,'')) as JFMC ,R.* from T_CON_LIGHT_BUSINESS t left join T_CON_LIGHT_BUSINESS_CABLE c on t.YWGUID = c.LIGHTGUID left join RMSS R on t.subscriber_id = R.SUBSCRIBER_ID  where  ZYHS_BJ='" + ZYHS_BJ.Text + "'";
       
        if (!string.IsNullOrEmpty(YWLX.SelectedValue))
        {
            if (YWLX.SelectedValue == "骨干业务")
            {
                sql += " and t.YWLX like '%骨干%'";
            }
            else
            {
                sql += " and t.YWLX = '" + YWLX.SelectedValue + "'";
            }
        }
        if (!string.IsNullOrEmpty(SUBSCRIBER_CODE.Text))
        {
            sql += " and t.SUBSCRIBER_CODE  like '%" + SUBSCRIBER_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(CUSTTYPE.SelectedValue))
        {
            sql += " and r.CUSTTYPE = '" + CUSTTYPE.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(CUSTOMER_NAME.Text))
        {
            sql += " and r.CUSTOMER_NAME like '%" + CUSTOMER_NAME.Text + "%'";
        }
        if (!string.IsNullOrEmpty(ADDRESS.Text))
        {
            sql += " and r.ADDRESS like '%" + ADDRESS.Text + "%'";
        }
        if (!string.IsNullOrEmpty(JFMC_CODE.Text))
        {
            sql += " and ( t.JDJF_CODE = '" + JFMC_CODE.Text + "' or t.YDJF_CODE = '" + JFMC_CODE.Text + "' or t.VJF_CODE = '" + JFMC_CODE.Text + "')";
        }
        if (!string.IsNullOrEmpty(JFMC.Text))
        {
            sql += " and (t.JDJF like '%" + JFMC.Text + "%' or t.YDJF like  '%" + JFMC.Text + "%' or t.VJF like  '%" + JFMC.Text + "%')";
        }
        if (!string.IsNullOrEmpty(GXH.Text))
        {
            sql += " and c.GXH = '" + GXH.Text + "'";
        }
        if (!string.IsNullOrEmpty(GLDMC_CODE.Text))
        {
            sql += " and c.GLDMC_CODE like '%" + GLDMC_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(GLDMC.Text))
        {
            sql += " and c.GXH = '" + GXH.Text + "'";
        }
        
        if (!string.IsNullOrEmpty(SBMC.Text))
        {
            sql += " and (t.jdjrsb  like '%" + SBMC.Text + "%' or t.ydjrsb like '%" + SBMC.Text + "%' or  t.vjrsb like '%" + SBMC.Text + "%')";
        }

        if (!string.IsNullOrEmpty(SBDK.Text))
        {
            sql += " and (t.jdjrsbdk  like '%" + SBDK.Text + "%' or t.ydjrsbdk like '%" + SBDK.Text + "%' or t.vjrsbdk like '%"+SBDK.Text+"%')";
        }
        
        sql += " order by t.createdatetime desc";
        //DataSet ds = DataFunction.FillDataSet(sql);
        return sql;
    }
    private int BindGV()
    {
        int count = 0;
        string sql = GetQuerySql();
        //string sql = "select distinct t.*,nvl(t.VJF,nvl(t.JDJF,'')||'  '||nvl(t.YDJF,'')) as JFMC ,R.* from T_CON_LIGHT_BUSINESS t left join T_CON_LIGHT_BUSINESS_CABLE c on t.YWGUID = c.LIGHTGUID left join RMSS R on t.subscriber_id = R.SUBSCRIBER_ID　where  ZYHS_BJ='" + ZYHS_BJ.Text + "'";
        //if (!string.IsNullOrEmpty(SUBSCRIBER_CODE.Text))
        //{
        //    sql += " and t.SUBSCRIBER_CODE  like '%" + SUBSCRIBER_CODE.Text + "%'";
        //}
        //if (!string.IsNullOrEmpty(YWLX.SelectedValue))
        //{
        //    sql += " and t.YWLX = '"+YWLX.SelectedValue+"'";
        //}
        //if (!string.IsNullOrEmpty(CUSTTYPE.SelectedValue))
        //{
        //    sql += " and r.CUSTTYPE = '" + CUSTTYPE.SelectedValue + "'";
        //}
        //if (!string.IsNullOrEmpty(CUSTOMER_NAME.Text))
        //{
        //    sql += " and r.CUSTOMER_NAME like '%" + CUSTOMER_NAME.Text + "%'";
        //}
        //if (!string.IsNullOrEmpty(ADDRESS.Text))
        //{
        //    sql += " and r.ADDRESS like '%" + ADDRESS.Text + "%'";
        //}
        //if (!string.IsNullOrEmpty(JFMC_CODE.Text))
        //{
        //    sql += " and ( t.JDJF_CODE = '" + JFMC_CODE.Text + "' or t.YDJF_CODE = '" + JFMC_CODE.Text + "' or t.VJF_CODE = '"+JFMC_CODE.Text+"')";
        //}
        //if (!string.IsNullOrEmpty(JFMC.Text))
        //{
        //    sql += " and (t.JDJF like '%" + JFMC.Text + "%' or t.YDJF like  '%" + JFMC.Text + "%' or t.VJF like  '%" + JFMC.Text + "%')";
        //}
        //if (!string.IsNullOrEmpty(GXH.Text))
        //{
        //    sql += " and c.GXH = '" + GXH.Text + "'";
        //}
        //if (!string.IsNullOrEmpty(GLDMC_CODE.Text))
        //{
        //    sql += " and c.GLDMC_CODE like '%" + GLDMC_CODE.Text + "%'";
        //}
        //if (!string.IsNullOrEmpty(GLDMC.Text))
        //{
        //    sql += " and c.GXH = '" + GXH.Text + "'";
        //}
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvLightList.DataSource = ds;
            gvLightList.DataBind();
            int count1 = gvLightList.Columns.Count;
            gvLightList.Rows[0].Cells.Clear();
            gvLightList.Rows[0].Cells.Add(new TableCell());
            //gvLightList.Rows[0].Cells[0].Text = "没有相关的信息！";
            //gvLightList.Rows[0].Cells[0].ColumnSpan = count1;
            //gvLightList.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            count = ds.Tables[0].Rows.Count;
            gvLightList.DataSource = ds;
            gvLightList.DataBind();
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
        foreach (GridViewRow row in gvLightList.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                ywguids += ",'" + gvLightList.DataKeys[row.RowIndex]["YWGUID"].ToString() + "'";
            }
        }
        if (!ywguids.Equals("''"))
        {
            string[] sql = new string[2];
            sql[0] = string.Format("delete from T_CON_LIGHT_BUSINESS where YWGUID in ({0})", ywguids);
            sql[1] = string.Format("delete from T_CON_LIGHT_BUSINESS_CABLE where LIGHTGUID in ({0})",ywguids);
            DataFunction.ExecuteTransaction(sql);
            BindGV();
        }
    }
    //刷新下拉列表
    protected void Btn_Click(object sender, EventArgs e)
    {
        DropDownList ddl = Page.FindControl(DDLID.Text) as DropDownList;
        string sv = ddl.SelectedValue;
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_RES_SYS_ENUMDATA where enum_sort = '{0}' order by sequence", DDLLX.Text));
        if (ds.Tables[0].Select("ENUM_NAME = '" + sv + "'").Length == 0)
        {
            sv = "";
        }
        ddl.DataSource = ds;
        ddl.DataTextField = "ENUM_NAME";
        ddl.DataValueField = "ENUM_NAME";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("", ""));
        ddl.SelectedValue = sv;

        //if (gvLightList.Rows[0].Cells[0].Text == "没有相关的信息！")
        //{
        //    int count1 = gvLightList.Columns.Count;
        //    gvLightList.Rows[0].Cells.Clear();
        //    gvLightList.Rows[0].Cells.Add(new TableCell());
        //    gvLightList.Rows[0].Cells[0].Text = "没有相关的信息！";
        //    gvLightList.Rows[0].Cells[0].ColumnSpan = count1;
        //    gvLightList.Rows[0].Cells[0].Style.Add("text-align", "center");
        //}
    }
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvLightList.PageIndex = 0;
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
        gvLightList.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvLightList.PageIndex + 1);
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
        gvLightList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvLightList.PageIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLightList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLightList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvLightList.PageIndex + 1);
        BindGV();
    }
    #endregion
    protected void gvLightList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string ywguid = gvLightList.DataKeys[e.Row.RowIndex]["YWGUID"].ToString();
            if (!ywguid.Equals(""))
            {
                string ywlx = e.Row.Cells[2].Text;
                if (ywlx.IndexOf("VPN") > -1)
                {
                    e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "','vpn')");
                }
                else if (ywlx.IndexOf("骨干") > -1)
                {
                    string llmc = DataFunction.GetStringResult("select llmc from t_con_bone_business t where t.ywguid='"+ywguid+"'");
                    e.Row.Cells[4].Text = llmc;
                    e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "','gg')");
                }
                else
                {
                    e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "','')");
                }
                
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }
    //刷新
    protected void Btn1_Click(object sender, EventArgs e)
    {
        BindDDL();
        int DataCount = BindGV();
        DataCountLab.Text = DataCount.ToString();
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        PageCountLab.Text = PageCount.ToString();
    }

    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        string sql = "select * from rmss t where t.SUBSCRIBER_ID='" + SUBSCRIBER_ID.Text + "'";
        ShareFunction.FillControlData(this.Page, DataFunction.GetSingleRow(sql));        
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
        gvLightList.Columns.Cast<DataControlField>().ForEach(con =>
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
        string sql = GetQuerySql();
        DataSet ds = DataFunction.FillDataSet(sql);
        //导出表头
        ds.Tables[0].Rows.Cast<DataRow>().ForEach(dr =>
        {
            ++row;
            int idx = 0;
            col = -1;
            gvLightList.Columns.Cast<DataControlField>().ForEach(con =>
            {
                if (col > -1)
                {
                    string sort = con.SortExpression;
                    ws.Cells[row, col].PutValue(dr[sort]);
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
        IDP.Common.WebUtils.ResponseWriteBinary(by, "光缆资源配置.xls");
    }
    protected void gvLightList_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
}

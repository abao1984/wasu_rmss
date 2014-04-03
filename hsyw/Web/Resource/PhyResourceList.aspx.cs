using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using Aspose.Cells;

public partial class Web_Resource_PhyResourceList : BasePage
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            RemoveResourceSession();
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            UNIT_NAME.Text = Request.QueryString["UNIT_NAME"];
            LabelTitle.Text ="物理资源初始化---"+ UNIT_NAME.Text;
            NAME_FILED.Text = Request.QueryString["NAME_FILED"];
           // LabelHead.Text = UNIT_NAME.Text;
            GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        }  
        shareResource.CreateResourceTable(TD_QUERY, UNIT_ID.Text, "QUERY");
        shareResource.CreateResourceGrid(GridViewPhyResource, UNIT_ID.Text,false);
        if (!this.IsPostBack)
        {
            BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text));
        }
        else
        {
            shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text);
        }

    }

    private void RemoveResourceSession()
    {
        DataSet ds = shareResource.GetListUnitData();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            Session.Remove(dr["UNIT_ID"]+"_GUID");
            Session.Remove(dr["UNIT_ID"].ToString());
        }
        //if (UNIT_ID.Text == shareResource.comp_house_unit_id || UNIT_ID.Text==shareResource.house_unit_id)
        //{
        //Session.Remove("HOUSE_AREA");
        //Session.Remove("HOUSE_AREA_CODE");
        //Session.Remove("HOUSE_ID");
        //Session.Remove("HOUSE_NAME");
        //Session.Remove("CUPBOARD_C_ID");
        //Session.Remove("CUPBOARD_C_NAME");
        //Session.Remove("CUPBOARD_M_ID");
        //Session.Remove("CUPBOARD_M_NAME");
        //}
    }

    protected void GridViewPhyResource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            //光芯
            if (UNIT_NAME.Text == "光芯")
            {
                string gxywbm = e.Row.Cells[5].Text;
                DataSet gxds = DataFunction.FillDataSet(string.Format("select ywlx,ywguid from t_con_light_business t where t.subscriber_code='{0}'", gxywbm));
                if (gxds.Tables[0].Rows.Count > 0)
                {
                    DataRow gxdr = gxds.Tables[0].Rows[0];
                    string strScript2 = "";
                    if (!gxdr[1].Equals(""))
                    {
                        string ywlx = gxdr[0].ToString();
                        if (ywlx.IndexOf("VPN") > -1)
                        {
                            strScript2 = "windowOpenGX('" + gxdr[1].ToString() + "','vpn')";
                            //e.Row.Attributes.Add("ondblclick", "windowOpenGX('" + gxdr[1].ToString() + "','vpn')");
                        }
                        else if (ywlx.IndexOf("骨干") > -1)
                        {
                            strScript2 = "windowOpenGX('" + gxdr[1].ToString() + "','gg')";
                            //e.Row.Attributes.Add("ondblclick", "windowOpenGX('" + gxdr[1].ToString() + "','gg')");
                        }
                        else
                        {
                            strScript2 = "windowOpenGX('" + gxdr[1].ToString() + "','')";
                            //e.Row.Attributes.Add("ondblclick", "windowOpenGX('" + gxdr[1].ToString() + "','')");
                        }

                    }
                    e.Row.Cells[5].Text = "<a href=#  onclick=\"" + strScript2 + "\">" + e.Row.Cells[5].Text + "</a>";
                }
            }
            DataRowView dv = (DataRowView)e.Row.DataItem;
            if (dv["UPDATEDATETIME"].ToString() == "")
            {
                e.Row.Cells.Clear();
            }
            else
            {
                string strGuid = GridViewPhyResource.DataKeys[e.Row.RowIndex].Value.ToString();
                string strName = "";
                if (!string.IsNullOrEmpty(NAME_FILED.Text))
                {
                    strName = dv[NAME_FILED.Text].ToString();
                }
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')");
                int n = 0;
                if (GridPageList.SelectedIndex > -1)
                {
                    n = GridPageList.SelectedIndex;
                }
                e.Row.Cells[1].Text = Convert.ToString(e.Row.RowIndex + 1 +Convert.ToInt32( PageSize.SelectedValue) * n);
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
            }
         }
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        //GridViewPhyResource.PageIndex = 0;
        //BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text));
    }
    protected void AddButton_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>windowOpen('" + Guid.NewGuid().ToString() + "','新增','" + NAME_FILED.Text + "');</script>");
    }


    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridViewPhyResource.PageIndex = 0;
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
       // GridViewPhyResource.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
       shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text);
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
       // GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
       shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text);
    }
   
    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text));
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
     //  GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text);
    }
    #endregion

    #region 排序
    protected void GridViewPhyResource_Sorting(object sender, GridViewSortEventArgs e)
    {
        string order = e.SortExpression;
        int count = 0;
        DataView dv = shareResource.GetResourceUnitData(this.Page,UNIT_ID.Text, shareResource.GetQueryStr(this.Page, UNIT_ID.Text),ref count).Tables[0].DefaultView;
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
        GridViewPhyResource.DataSource = dv;
        GridViewPhyResource.DataBind();       
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
    #endregion

    #region 删除
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        string tableName = shareResource.GetTableName(UNIT_ID.Text);
        int i = 0;
        foreach (GridViewRow gr in GridViewPhyResource.Rows)
        {
            Control cn = gr.FindControl("XZ");
            if (cn != null)
            {
                if (((System.Web.UI.WebControls.CheckBox)cn).Checked)
                {
                    string sql = "delete from " + tableName + " where guid='" + GridViewPhyResource.DataKeys[i].Value + "'";
                    DataFunction.ExecuteNonQuery(sql);
                }
            }
            i++;
        }
        BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text));
    }
    #endregion

    protected void QueryButton_Click(object sender, EventArgs e)
    {
        GridViewPhyResource.PageIndex = 0;
        BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, UNIT_ID.Text));
    }
    protected void BtnExp_Click(object sender, EventArgs e)
    {
        string ColumMc = Session["ColumMc"].ToString();
        string ColumZd= Session["ColumZd"].ToString();
        if (ColumMc != "" && ColumZd!="")
        {
            ExpExcel(ColumMc, ColumZd);
        }
    }

    private void ExpExcel(string ColumMc, string ColumZd)
    {
        string[] mc = ColumMc.Split(',');
        string[] zd = ColumZd.Split(',');
        try
        {
            License lic = new License();
            lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
            WorkbookDesigner designer1 = new WorkbookDesigner();
            Workbook book = designer1.Workbook;
            DataSet ds = shareResource.GetResourceUnitData(Page, UNIT_ID.Text, shareResource.GetQueryStr(Page, UNIT_ID.Text));
            ds.Tables[0].TableName = "T1";
            object filePath = Server.MapPath("WLZY.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Cells cells = book.Worksheets[0].Cells;
                //int i = 1;
                for (int i = 0; i < mc.Length-1; i++ )
                {
                    cells[0, i].PutValue(mc[i]);
                    cells[0, i].Style = cells[0,0].Style;
                    cells[1, i].PutValue("&=[T1]." + zd[i]);
                }
                //foreach (ListItem item in CheckBoxList1.Items)
                //{
                //    if (item.Selected)
                //    {
                //        cells[0, i].PutValue(item.Text);
                //        cells[0, i].Style.BackgroundColor = Color.PowderBlue;
                //        cells[1, i].PutValue("&=[T1]." + item.Value);
                //        i++;
                //    }
                //}
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    designer1.SetDataSource(ds);
                }
                else
                {
                    return;
                }
                designer1.Process();
                designer1.Save(HttpUtility.UrlEncode(LabelTitle.Text+".xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
                Response.End();
            }
            else
                return;
        }
        catch (Exception ex)
        {
            string aa = ex.Message;
        }
    }
}

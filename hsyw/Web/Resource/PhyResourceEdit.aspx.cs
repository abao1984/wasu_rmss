using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Drawing;
using IDP.Common;

public partial class Web_Resource_PhyResourceEdit : BasePage
{
    private ShareResource shareResource = new ShareResource();

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(ShareResource));
        if (!this.IsPostBack)
        {

            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            PARENT_UNIT_ID.Text = Request.QueryString["PARENT_UNIT_ID"];
            GUID.Text = Request.QueryString["GUID"];
            if (string.IsNullOrEmpty(GUID.Text))
            {
                GUID.Text = Guid.NewGuid().ToString();
            }
            // NAME_FILED.Text = Request.QueryString["NAME_FILED"];
            NAME_FILED_GUID.Text = Request.QueryString["NAME_FILED_GUID"];
            NAME_FILED_NAME.Text = Request.QueryString["NAME_FILED_NAME"];
            DataRow drt = shareResource.GetResUnitData(UNIT_ID.Text);
            NAME_FILED.Text = drt["NAME_FILED"].ToString();
            UNIT_NAME.Text = drt["UNIT_NAME"].ToString();
            TABLE_NAME.Text = drt["TABLE_NAME"].ToString(); //shareResource.GetTableName(UNIT_ID.Text);

            GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);

        }


        shareResource.CreateResourceTable(TD_PROPERTY, UNIT_ID.Text, "PAGE");

        Control drtCn = Page.FindControl("SSLX");
        if (drtCn != null)
        {
            ((DropDownList)drtCn).AutoPostBack = true;
            ((DropDownList)drtCn).SelectedIndexChanged += new System.EventHandler(SslxChanged);
        }


        Control ch = Page.FindControl("EXIST_GROOVY");
        if (ch != null)
        {
            ((CheckBox)ch).AutoPostBack = true;
            ((CheckBox)ch).CheckedChanged += new EventHandler(Groovy_CheckedChanged);
        }
        //CreateMenuTable();  
        if (!this.IsPostBack)
        {
            FillPage();
        }



        GetChildUnitId();
        string mc = CHILD_UNIT_ID.Text;
        BatchButton.Visible = true;
        if (mc == "光缆段")
        {
            BatchButton.Visible = false;
            return;
        }
        ChangeGridPage();
        if (!string.IsNullOrEmpty(CHILD_UNIT_ID.Text))
        {
            if (!this.IsPostBack)
            {
                BindGridPage(BindGrid());
            }
            else
            {
                if (ISAdd.Text == "y")//新增
                {
                    BindGridPage(BindGrid());
                }
                else
                {
                    BindGrid();
                }
            }
        }
        InitPageContrl();

        //复制按钮权限分配
        if (UNIT_NAME.Text == "传输设备" || UNIT_NAME.Text == "网络设备" || UNIT_NAME.Text == "光设备")
        {
            CoppyButton.Visible = true;
        }
    }

    private void ChangeGridPage()
    {
        if (!string.IsNullOrEmpty(CHILD_UNIT_ID.Text))
        {
            DataRow drt = shareResource.GetResUnitData(CHILD_UNIT_ID.Text);
            CHILD_UNIT_NAME.Text = drt["UNIT_NAME"].ToString();
            CHILD_NAME_FILED.Text = drt["NAME_FILED"].ToString();
            CHILD_GRID_MODE.Text = drt["GRID_MODE"].ToString();
            string codeFiled = drt["CODE_FILED"].ToString();
            //  shareResource.CreateResourceGrid(GridViewPhyResource, CHILD_UNIT_ID.Text, false);
            if (string.IsNullOrEmpty(codeFiled))
            {
                BatchButton.Style.Add("display", "none");
            }
            else
            {
                BatchButton.Style.Add("display", "block");
            }
            // BindGridPage(BindGrid());          
        }
    }

    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, shareResource.GetResourceDataRow(TABLE_NAME.Text, GUID.Text));
        if (!string.IsNullOrEmpty(NAME_FILED.Text))
        {
            string nameFiled = getNameFiled();
            if (!string.IsNullOrEmpty(nameFiled))
            {
                SetControlData(nameFiled, NAME_FILED_NAME.Text);
                SetControlData(nameFiled + "_GUID", NAME_FILED_GUID.Text);
            }
        }
        //dsh 12.19 新增网络设备的时候第二次新增不需要默认选择上条机房
        //Session[UNIT_ID.Text + "_GUID"] = GUID.Text;
        //Session[UNIT_ID.Text] = ShareFunction.GetControlValue(Page.FindControl(NAME_FILED.Text));
        //DataSet ds = shareResource.GetSessionUnitData(UNIT_ID.Text);
        //foreach (DataRow DR in ds.Tables[0].Rows)
        //{
        //    string filedValue = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"].ToString())).ToString();
        //    if (Session[DR["UNIT_ID"].ToString()] == null)
        //    {
        //        Session[DR["UNIT_ID"] + "_GUID"] = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"] + "_GUID"));
        //        Session[DR["UNIT_ID"].ToString()] = filedValue;
        //    }
        //    else //if(Request.QueryString["PAGE_TYPE"]==null)
        //    {
        //        if (string.IsNullOrEmpty(filedValue))
        //        {
        //            ShareFunction.SetControlValue(Page.FindControl(DR["FILED_NAME"] + "_GUID"), Session[DR["UNIT_ID"] + "_GUID"]);
        //            ShareFunction.SetControlValue(Page.FindControl(DR["FILED_NAME"].ToString()), Session[DR["UNIT_ID"].ToString()]);
        //        }
        //        else
        //        {
        //            Session[DR["UNIT_ID"] + "_GUID"] = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"] + "_GUID"));
        //            Session[DR["UNIT_ID"].ToString()] = filedValue;
        //        }
        //    }
        //}
        GetHouseAreaSession();
        TeSuJiSuanGongShi();
        //if (UNIT_ID.Text == shareResource.comp_house_unit_id || UNIT_ID.Text == shareResource.house_unit_id)
        //{
        //    Session["HOUSE_AREA"] = ((TextBox)Page.FindControl("HOUSE_AREA")).Text;
        //    Session["HOUSE_AREA_CODE"] = ((TextBox)Page.FindControl("HOUSE_AREA_CODE")).Text;
        //    Session["HOUSE_ID"] = GUID.Text;
        //    Session["HOUSE_NAME"] = ((TextBox)Page.FindControl(NAME_FILED.Text)).Text;
        //    Session.Remove("CUPBOARD_C_ID");
        //    Session.Remove("CUPBOARD_C_NAME");
        //    Session.Remove("CUPBOARD_M_ID");
        //    Session.Remove("CUPBOARD_M_NAME");
        //}
        //else if (UNIT_ID.Text == shareResource.cupboard_unit_id)
        //{
        //    string house_Name_id = ((TextBox)Page.FindControl("HOUSE_NAME_GUID")).Text;
        //    if (!string.IsNullOrEmpty(house_Name_id))
        //    {
        //        Session["HOUSE_ID"] = ((TextBox)Page.FindControl("HOUSE_NAME_GUID")).Text;
        //        GetHouseArea(Session["HOUSE_ID"].ToString());
        //        Session["HOUSE_NAME"] = ((TextBox)Page.FindControl("HOUSE_NAME")).Text;
        //        Session["CUPBOARD_C_ID"] = GUID.Text;
        //        Session["CUPBOARD_C_NAME"] = ((TextBox)Page.FindControl(NAME_FILED.Text)).Text;
        //        Session.Remove("CUPBOARD_M_ID");
        //        Session.Remove("CUPBOARD_M_NAME");
        //    }
        //}



    }
    private void GetHouseAreaSession()
    {
        string sql = "select t.filed_name from t_res_sys_property t where t.data_type='组织机构' and t.unit_id='" + UNIT_ID.Text + "'";
        string FiledName = DataFunction.GetStringResult(sql);
        if (!string.IsNullOrEmpty(FiledName))
        {
            string AreaCode = ((TextBox)Page.FindControl(FiledName + "_CODE")).Text;
            string AreaName = ((TextBox)Page.FindControl(FiledName)).Text;
            if (Session["HOUSE_AREA_CODE"] == null)
            {
                Session["HOUSE_AREA"] = AreaName;
                Session["HOUSE_AREA_CODE"] = AreaCode;
            }
            else
            {
                if (string.IsNullOrEmpty(AreaCode))
                {
                    ShareFunction.SetControlValue(Page.FindControl(FiledName + "_CODE"), Session["HOUSE_AREA_CODE"]);
                    ShareFunction.SetControlValue(Page.FindControl(FiledName), Session["HOUSE_AREA"]);
                }
                else
                {
                    Session["HOUSE_AREA"] = AreaName;
                    Session["HOUSE_AREA_CODE"] = AreaCode;
                }
            }
        }
    }


    private void GetHouseArea(string houseGuid)
    {
        string sql = string.Format(@"select t.house_area,t.house_area_code from t_res_house_common t where t.guid='{0}' union
            select t.house_area,t.house_area_code from t_res_house_COMPUTER t where t.guid='{0}'", houseGuid);
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Session["HOUSE_AREA"] = ds.Tables[0].Rows[0]["HOUSE_AREA"];
            Session["HOUSE_AREA_CODE"] = ds.Tables[0].Rows[0]["HOUSE_AREA_CODE"];
        }
    }

    private string getNameFiled()
    {
        string sql = string.Format(@"select p.filed_name from t_res_sys_property_unit up,T_RES_SYS_PROPERTY p 
        where up.propery_id=p.propery_id and up.unit_id='{0}' and p.unit_id='{1}'", PARENT_UNIT_ID.Text, UNIT_ID.Text);
        return DataFunction.GetStringResult(sql);
    }

    private void SetControlData(string filed_name, string filed_value)
    {
        ShareFunction.SetControlValue(Page.FindControl(filed_name), filed_value);
    }
    private int BindGrid()
    {
        int count = 0;
        //当资源类别为光设备时，给他加上一个光缆段 
        string mc = CHILD_UNIT_ID.Text;
        if (mc == "光缆段")
        {
            shareResource.CreateResourceGldGrid(GridViewPhyResource);
            DataSet ds = DataFunction.FillDataSet("select guid, lsid, gldguid, gldmc, gldcode, gxhguid, gxhcode, gldxh, gldcd from t_res_gld where lsid='" + GUID.Text + "'");
            count = ds.Tables[0].Rows.Count;
            try
            {
                GridViewPhyResource.DataSource = ds;
                GridViewPhyResource.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return count;

        }
        else
        {
            shareResource.CreateResourceGrid(GridViewPhyResource, CHILD_UNIT_ID.Text, false);
            DataSet ds = shareResource.GetResourceUnitData(this.Page, CHILD_UNIT_ID.Text, shareResource.GetChildQueryStr(CHILD_UNIT_ID.Text, GUID.Text), ref count);
            try
            {
                GridViewPhyResource.DataSource = ds;
                GridViewPhyResource.DataBind();
            }
            catch
            {
            }
            if (ds.Tables[0].Rows[0]["UPDATEDATETIME"].ToString() == "")
            {
                return 0;
            }
            else
            {
                return count;
            }
        }

    }

    private bool CheckPageData()
    {
        string Message = "";
        string Message1 = "";
        string sql = "select * from t_res_sys_property t where t.unit_id='" + UNIT_ID.Text + "' and (t.isempty=0 or ISREPEAT=1)  order by t.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string strValue = ShareFunction.GetControlValue(Page.FindControl(dr["FILED_NAME"].ToString())).ToString();
            if (string.IsNullOrEmpty(strValue) && dr["isempty"].ToString() == "0")
            {
                if (!string.IsNullOrEmpty(Message))
                {
                    Message += "、";
                }
                Message += dr["PROPERY_NAME"].ToString();
            }

            if (dr["ISREPEAT"].ToString() == "1")
            {
                sql = string.Format("select * from {0} t where t.guid<>'{1}' and {2}='{3}'",
                   TABLE_NAME.Text, GUID.Text, dr["FILED_NAME"].ToString(), strValue);
                if (DataFunction.HasRecord(sql))
                {
                    Message1 += dr["PROPERY_NAME"].ToString() + "不允许重复，";
                }
            }
        }
        if (string.IsNullOrEmpty(Message) && string.IsNullOrEmpty(Message1))
        {
            return true;
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + Message + "不能为空！" + Message1 + "');</script>");
            return false;
        }
    }


    #region 创建菜单标签
    private void GetChildUnitId()
    {
        MenuTr.Cells.Clear();
        Control ch = Page.FindControl("EXIST_GROOVY");
        if (ch == null)
        {
            CreateMenuTable();
        }
        else
        {
            CheckBox chb = (CheckBox)ch;
            chb.AutoPostBack = true;
            if (chb.Checked)
            {
                Menu_TR.Style.Add("display", "block");
                Menu_Button_TR.Style.Add("display", "block");
                CHILD_UNIT_ID.Text = shareResource.groove_unit_id;
                CreateMenuCell(CHILD_UNIT_ID.Text, "槽位", 0);
            }
            else
            {
                CreateMenuTable();
                //当资源类别为光设备时，给他加上一个光缆段 
                if (UNIT_ID.Text == "66a85a26-6e7b-42c6-ac01-678697ba3023")
                {

                    CreateMenuCell("光缆段", "光缆段", 1);
                }
            }
        }

    }
    private DataSet GetMenuData()
    {
        string sql = "select z.* from T_RES_SYS_UNIT_RELATION c,t_res_sys_unit z where z.unit_id=c.child_unit_id  and c.father_unit_id='" + UNIT_ID.Text + "' order by z.sequence";
        return DataFunction.FillDataSet(sql);
    }
    private void CreateMenuTable()
    {
        DataSet ds = GetMenuData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            Menu_TR.Style.Add("display", "block");
            Menu_Button_TR.Style.Add("display", "block");
            TR_PAGE.Style.Add("display", "block");
            int i = 0;
            foreach (DataRow DR in ds.Tables[0].Rows)
            {
                CreateMenuCell(DR["UNIT_ID"].ToString(), DR["UNIT_NAME"].ToString(), i);
                i++;
            }
            if (string.IsNullOrEmpty(CHILD_UNIT_ID.Text))
            {
                CHILD_UNIT_ID.Text = ds.Tables[0].Rows[0]["UNIT_ID"].ToString();
            }

        }
        else
        {
            Menu_TR.Style.Add("display", "none");
            Menu_Button_TR.Style.Add("display", "none");
            TR_PAGE.Style.Add("display", "none");
        }
    }
    private void CreateMenuCell(string Unit_Id, string Unit_Name, int i)
    {
        HtmlTableCell TD = new HtmlTableCell();
        TD.InnerHtml = "<a href=# onclick='changeMenu(\"" + Unit_Id + "\")'>" + Unit_Name + "</a>";
        if (CHILD_UNIT_ID.Text == "" && i == 0)
        {
            TD.Attributes.Add("class", "nav01");
        }
        else if (CHILD_UNIT_ID.Text == Unit_Id)
        {
            TD.Attributes.Add("class", "nav01");
        }
        else
        {
            TD.Attributes.Add("class", "nav02");
        }
        MenuTr.Cells.Add(TD);
    }
    #endregion

    protected void GridViewPhyResource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            //光芯
            if(CHILD_UNIT_NAME.Text=="光芯")
            {
                string gxywbm = e.Row.Cells[5].Text;
                DataSet gxds = DataFunction.FillDataSet(string.Format("select ywlx,ywguid from t_con_light_business t where t.subscriber_code='{0}'", gxywbm));
                if(gxds.Tables[0].Rows.Count>0)
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
            if (CHILD_UNIT_ID.Text == "光缆段")
            {
                string strGuid = GridViewPhyResource.DataKeys[e.Row.RowIndex].Value.ToString();
                string strScript2 = "AddGld('" + strGuid + "','" + GUID.Text + "')";
                e.Row.Attributes.Add("ondblclick", strScript2);
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");

            }
            else
            {
                DataRowView dv = (DataRowView)e.Row.DataItem;
                if (dv["UPDATEDATETIME"].ToString() == "")
                {
                    e.Row.Cells.Clear();
                }
                else
                {
                    string strGuid = GridViewPhyResource.DataKeys[e.Row.RowIndex].Value.ToString();
                    string strName = "";
                    string name_filed = CHILD_NAME_FILED.Text;
                    string childUnitId = CHILD_UNIT_ID.Text;
                    string childUnitName = CHILD_UNIT_NAME.Text;
                    string filedGuid = GUID.Text;
                    string filedName = "";// dv[NAME_FILED.Text].ToString();
                    string parent_unit_id = UNIT_ID.Text;
                    if (!string.IsNullOrEmpty(CHILD_NAME_FILED.Text))
                    {
                        strName = dv[CHILD_NAME_FILED.Text].ToString();
                    }
                    string strScript1 = "windowOpen('" + childUnitId + "','" + childUnitName + "','" + strGuid + "','" + strName + "','" + name_filed + "','" + filedGuid + "','" + filedName + "','" + parent_unit_id + "')";
                    if (!string.IsNullOrEmpty(CHILD_GRID_MODE.Text))
                    {
                        strGuid = dv["equ_guid"].ToString();
                        childUnitId = dv["unit_id"].ToString();
                        childUnitName = dv["unit_name"].ToString();
                        strName = dv["equ_name"].ToString();
                        name_filed = dv["name_filed"].ToString();
                        filedGuid = dv["guid"].ToString();
                        if (!string.IsNullOrEmpty(name_filed))
                        {
                            filedName = dv[name_filed].ToString();
                        }
                        parent_unit_id = CHILD_UNIT_ID.Text;
                    }
                    string strScript2 = "windowOpen('" + childUnitId + "','" + childUnitName + "','" + strGuid + "','" + strName + "','" + name_filed + "','" + filedGuid + "','" + filedName + "','" + parent_unit_id + "')";
                    e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
                    int n = 0;
                    if (GridPageList.SelectedIndex > -1)
                    {
                        n = GridPageList.SelectedIndex;
                    }
                    e.Row.Cells[1].Text = Convert.ToString(e.Row.RowIndex + 1 + Convert.ToInt32(PageSize.SelectedValue) * n);
                    if (!string.IsNullOrEmpty(childUnitId))
                    {
                        e.Row.Attributes.Add("ondblclick", strScript2);

                    }
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"" + strScript1 + "\">详细</a>";
                }
            }
        }
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
        //  GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGrid();
    }

    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }
    #endregion

    #region 排序
    protected void GridViewPhyResource_Sorting(object sender, GridViewSortEventArgs e)
    {
        int count = 0;
        string order = e.SortExpression;
        DataView dv = shareResource.GetResourceUnitData(this.Page, CHILD_UNIT_ID.Text, shareResource.GetChildQueryStr(CHILD_UNIT_ID.Text, GUID.Text), ref count).Tables[0].DefaultView;
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

    protected void Groovy_CheckedChanged(object sender, EventArgs e)
    {
        GetChildUnitId();
        ChangeGridPage();
    }

    #region 保存IP资源配置
    private void SaveIpPz()
    {
        string sql = "select * from t_res_sys_property t where t.data_type='IP资源' and t.unit_id='" + UNIT_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        sql = "delete from t_logic_equ_ip_pz where pk_guid='" + GUID.Text + "'";
        DataFunction.ExecuteNonQuery(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            string ipdz = Convert.ToString(ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"].ToString())));
            if (!string.IsNullOrEmpty(ipdz))
            {
                SaveT_logic_equ_ip_pz(GUID.Text, ipdz);
            }
        }
    }
    private void SaveT_logic_equ_ip_pz(string pk_guid, string ipdz)
    {
        string[] ips = ipdz.Split('.');
        string sql = string.Format("insert into t_logic_equ_ip_pz (GUID,PK_GUID,IP1,IP2,IP3,IP4,IPFD,IPDZ) values('{0}','{1}',{2},{3},{4},{5},{6},'{7}')",
            Guid.NewGuid(), pk_guid, ips[0], ips[1], ips[2], ips[3], 32, ipdz);
        DataFunction.ExecuteNonQuery(sql);
    }
    #endregion

    #region 计算公式
    private void SaveDataByFormula()
    {
        string sql = "select * from t_res_sys_property t where t.formula is not null and t.unit_id='" + UNIT_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            string strValue = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"].ToString())).ToString();
            string strCheck = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"].ToString() + "_CHECK")).ToString();
            if (string.IsNullOrEmpty(strValue) || strCheck == "0")
            {
                sql = " update " + TABLE_NAME.Text + " set " + DR["FILED_NAME"].ToString() + "=" + DR["FORMULA"].ToString() + " where guid='" + GUID.Text + "'";
                DataFunction.ExecuteNonQuery(sql);
                sql = " update " + TABLE_NAME.Text + "_LS set " + DR["FILED_NAME"].ToString() + "=" + DR["FORMULA"].ToString() + " where guid='" + GUID.Text + "'";
                DataFunction.ExecuteNonQuery(sql);
            }
        }
        FillPage();
    }
    #endregion

    #region 同步设备名称
    private void UpdateEquName(string unit_id, string guid, string name, string isGroovy)
    {
        string sql = string.Format(@"select p.filed_name, u.Table_Name,u.Unit_id,u.NAME_FILED from t_res_sys_property_unit up,t_res_sys_property p,t_res_sys_unit u
 where up.propery_id=p.propery_id and p.unit_id=u.unit_id and up.unit_id='{0}'", unit_id);
        if (isGroovy == "1")
        {
            sql += " and u.Unit_id='00c3a457-24eb-4c00-a7e0-5e7f0116fe68'";
        }
        else if (isGroovy == "0")
        {
            sql += " and u.Unit_id in (select t.unit_id from t_res_sys_unit t where t.parent_unit_id='" + unit_id + "')";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            string Table_Name = DR["Table_Name"].ToString();
            string filed_name = DR["filed_name"].ToString();
            sql = string.Format("update {0} set {1}='{2}' where {1}_GUID='{3}'", Table_Name, filed_name, name, guid);
            DataFunction.ExecuteNonQuery(sql);
            sql = string.Format("update {0}_LS set {1}='{2}' where {1}_GUID='{3}'", Table_Name, filed_name, name, guid);
            DataFunction.ExecuteNonQuery(sql);

            sql = "select * from t_res_sys_property t where t.formula is not null and t.unit_id='" + DR["Unit_id"] + "'";
            DataSet dst = DataFunction.FillDataSet(sql);
            foreach (DataRow drt in dst.Tables[0].Rows)
            {
                sql = " update " + Table_Name + " set " + drt["FILED_NAME"].ToString() + "=" + drt["FORMULA"].ToString() + " where " + filed_name + "_GUID='" + guid + "'";
                DataFunction.ExecuteNonQuery(sql);
                sql = " update " + Table_Name + "_LS set " + drt["FILED_NAME"].ToString() + "=" + drt["FORMULA"].ToString() + " where " + filed_name + "_GUID='" + guid + "'";
                DataFunction.ExecuteNonQuery(sql);
            }
            sql = string.Format("select * from {0} where {1}_guid='{2}'", Table_Name, filed_name, guid);
            DataSet dsU = DataFunction.FillDataSet(sql);
            bool isExist = dsU.Tables[0].Columns.Contains("EXIST_GROOVY");
            filed_name = DR["NAME_FILED"].ToString();
            foreach (DataRow drU in dsU.Tables[0].Rows)
            {
                if (isExist)
                {
                    UpdateEquName(DR["Unit_id"].ToString(), drU["GUID"].ToString(), drU[filed_name].ToString(), drU["EXIST_GROOVY"].ToString());
                }
                else
                {
                    UpdateEquName(DR["Unit_id"].ToString(), drU["GUID"].ToString(), drU[filed_name].ToString(), null);
                }
            }
        }

    }
    #endregion

    #region 按钮方法
    protected void AddButton_Click(object sender, EventArgs e)
    {
        string mc = CHILD_UNIT_ID.Text;
        if (mc == "光缆段") //AddGld
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
                "<script>AddGld('','" + GUID.Text + "')</script>");
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
                "<script>windowOpen('" + CHILD_UNIT_ID.Text + "','" + CHILD_UNIT_NAME.Text + "','" + Guid.NewGuid().ToString() + "','新增','" + CHILD_NAME_FILED.Text + "','" + GUID.Text + "','','" + UNIT_ID.Text + "')</script>");
        }

    }
    protected void DeleButton_Click(object sender, EventArgs e)
    {


        string mc = CHILD_UNIT_ID.Text;
        if (mc == "光缆段")
        {
            string gsb_guid = GUID.Text;
            int i = 0;
            foreach (GridViewRow gr in GridViewPhyResource.Rows)
            {
                Control cn = gr.FindControl("XZ");
                if (cn != null)
                {
                    if (((CheckBox)cn).Checked)
                    {
                        string sql = "delete from t_res_gld where guid='" + GridViewPhyResource.DataKeys[i].Value + "'";
                        DataFunction.ExecuteNonQuery(sql);
                    }
                }
                i++;
            }

        }
        else
        {
            string tableName = shareResource.GetTableName(CHILD_UNIT_ID.Text);
            int i = 0;
            string idList = "";
            foreach (GridViewRow gr in GridViewPhyResource.Rows)
            {
                Control cn = gr.FindControl("XZ");
                if (cn != null)
                {
                    if (((CheckBox)cn).Checked)
                    {
                        string guid = Convert.ToString(GridViewPhyResource.DataKeys[i].Value);
                        if (!IsDelete(CHILD_UNIT_ID.Text, guid))
                        {
                            string sql = "delete from " + tableName + " where guid='" + guid + "'";
                            DataFunction.ExecuteNonQuery(sql);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(idList))
                            {
                                idList += ",";
                            }
                            idList += "'" + guid + "'";
                        }
                    }
                }
                i++;
            }
            if (!string.IsNullOrEmpty(idList))
            {
                //string NameFiled = DataFunction.GetStringResult("select NAME_FILED from t_res_sys_unit where UNIT_ID='" + CHILD_UNIT_ID.Text + "'");
                //DataTable dt = DataFunction.FillDataSet("select " + NameFiled + " from " + tableName + " where guid in(" + idList + ") ").Tables[0];
                //string msg = dt.Rows.Cast<DataRow>().JoinString(" , ", dr => Convert.ToString(dr[NameFiled]));
                this.Alert("资源数据下有子资源，请先删除子资源");
            }
        }
        BindGridPage(BindGrid());

    }
    protected void MenuButton_Click(object sender, EventArgs e)
    {
        string mc = CHILD_UNIT_ID.Text;
        if (mc == "光缆段")
        {
            BindGridPage(BindGrid());
        }
        else
        {
            ChangeGridPage();
            BindGridPage(BindGrid());
        }
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(CHILD_UNIT_ID.Text))
        //{
        //   // shareResource.CreateResourceGrid(GridViewPhyResource, CHILD_UNIT_ID.Text, false);
        //    ChangeGridPage();

        //    BindGridPage(BindGrid());
        //}
        string mc = CHILD_UNIT_ID.Text;
        //因为光缆段在pageLoad里下面是不执行的。所以无法刷新，具体看PageLoad方法
        if (mc == "光缆段")
        {
            BindGrid();
        }
    }
    protected void changeButton_Click(object sender, EventArgs e)
    {
        if (changeButton.Text == "隐藏基本属性")
        {
            changeButton.Text = "显示基本属性";
            TR_PROPERTY.Style.Add("display", "none");
        }
        else
        {
            changeButton.Text = "隐藏基本属性";
            TR_PROPERTY.Style.Add("display", "block");
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (!CheckPageData())
        {
            return;
        }
        string strValue = ShareFunction.GetControlValue(Page.FindControl("GLDBH")).ToString();
        DataRow DR = shareResource.GetResourceDataRow(TABLE_NAME.Text, GUID.Text);
        DataRow DRLS = shareResource.GetResourceDataRowLS(TABLE_NAME.Text, GUID.Text);
        
        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        string oldName = DR[NAME_FILED.Text].ToString();
        string strComment = ShareFunction.GetControlData(this.Page, DR, TABLE_NAME.Text);
        string strTitle = "修改" + UNIT_NAME.Text + "资源";
        if (DR.RowState == DataRowState.Added)
        {
            strTitle = "新增" + UNIT_NAME.Text + "资源";
            strComment = "新增";
        }
        ShareFunction.GetControlData(this.Page, DRLS);
        DataFunction.SaveData(DR.Table.DataSet, TABLE_NAME.Text);
        TeSuJiSuanGongShi();
        // TongBuJiGuiU(DRLS);
        DataFunction.SaveData(DRLS.Table.DataSet, TABLE_NAME.Text + "_LS");
        Control cn = Page.FindControl(NAME_FILED.Text);
        string name = "";
        if (cn != null)
        {
            name = ShareFunction.GetControlValue(cn).ToString();
            if (!string.IsNullOrEmpty(name))
            {
                shareResource.SaveT_RES_EQUMENT_ALL(GUID.Text, name, UNIT_ID.Text);
            }
        }
        shareResource.SaveT_RES_EQUMENT_RELATION(Page, UNIT_ID.Text, GUID.Text);


        ShareFunction.InsertLog(this.Page, GUID.Text, strTitle, strComment);
        SaveIpPz();
        
        name = ShareFunction.GetControlValue(cn).ToString();
        if (name != oldName)
        {
            Control ch = Page.FindControl("EXIST_GROOVY");
            if (ch == null)
            {
                UpdateEquName(UNIT_ID.Text, GUID.Text, name, null);
            }
            else if (((CheckBox)ch).Checked)
            {
                UpdateEquName(UNIT_ID.Text, GUID.Text, name, "1");
            }
            else
            {
                UpdateEquName(UNIT_ID.Text, GUID.Text, name, "0");
            }
            ChangeGridPage();
        }
        if (!string.IsNullOrEmpty(name))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.SetLabelHead('" + UNIT_NAME.Text + "-----" + name + "');</script>");
        }
        SaveDataByFormula();
        //光缆时清空没有编号的数据
        if (UNIT_ID.Text == "53d347a2-5be1-4b85-af53-ffd35b3ccfc7")
        {
            ClearEmptyGl();
            ShareFunction.FillControlData(this.Page, shareResource.GetResourceDataRow(TABLE_NAME.Text, GUID.Text));
        }

        //保存网络设备时把设备管理地址保存到配置单中
        if (UNIT_ID.Text == "64602091-d4fe-4c89-ac6a-52f6acdd836d")
        {
            SaveIp();
        }

        //更新IP资源配置单，设备关联信息
        if (UNIT_NAME.Text == "网络设备")
        {
            string strSql = string.Format(@"update t_con_logic_equ_ip p set ( sbmc,sbmc_code,sbpzxx) = (select t.equ_name,t.equ_code,t.equ_code||substr(p.sbpzxx,instr(p.sbpzxx,'.')) from t_res_equ_net t where p.sbmc_guid=t.guid and p.sbmc_guid='{0}'  )",GUID.Text);
            DataFunction.ExecuteNonQuery(strSql);
        }
       
    }

    #region 保存时把光缆中没有编号的数据清空
    private void ClearEmptyGl()
    {
        DataFunction.ExecuteNonQuery("delete from T_RES_EQU_LIGHT_CABLE where gldbh is null");
    }
    #endregion

    #region 保存网络设备时把设备管理地址保存到配置单中
    private void SaveIp()
    {
        string sbgldz = ((TextBox)Page.FindControl("SBGLDZ")).Text;
        if (!string.IsNullOrEmpty(sbgldz))
        {
            string ip1, ip2, ip3, ip4, ipfd;
            Int64 ipdz1 = 0;
            Int64 ipdz2 = 0;
            string[] ip = sbgldz.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            ip1 = ip[0];
            ip2 = ip[1];
            ip3 = ip[2];
            ip4 = ip[3];
            ipfd = "32";
            GetIpdz(ipfd, ip1, ip2, ip3, ip4,ref ipdz1,ref ipdz2);
            string sql = "";
            if (DataFunction.GetIntResult("select count(*) from t_logic_equ_ip_pz t where pk_guid='" + GUID.Text + "'") > 1)
            {
                string guid = System.Guid.NewGuid().ToString();
                sql = @"insert into t_logic_equ_ip_pz
              (guid, pk_guid, ip1, ip2, ip3, ip4, ipfd, ipdz, ipdz1, ipdz2, createdatetime, updatedatetime, updateusername, pz_ipywlx)
            values
              ('" + guid + "', '" + GUID.Text + "', '" + ip1 + "', '" + ip2 + "', '" + ip3 + "', '" + ip4 + "', '" + ipfd + "', '" + sbgldz + "', '" + ipdz1 + "', '" + ipdz2 + "',sysdate, sysdate, '" + Convert.ToString(Session["USERNAME"]) + "', '');";
            }
            else
            {
                sql = "update t_logic_equ_ip_pz set ip1='" + ip1 + "',ip2='" + ip2 + "',ip3='" + ip3 + "',ip4='" + ip4 + "',ipdz='" + sbgldz + "',ipdz1='" + ipdz1 + "',ipdz2='" + ipdz2 + "',updatedatetime=sysdate,updateusername='" + Convert.ToString(Session["USERNAME"]) + "' where pk_guid='" + GUID.Text + "'";
            }
            DataFunction.ExecuteNonQuery(sql);

            DataRow dr = DataFunction.GetSingleRow(@"select case when p.cn=t.ipdz2-t.ipdz1 then '已分配'  when p.cn>0 then '部分分配' else '未分配' end as fpqk ,
                                p.cn, t.*,t.rowid from t_logic_equ_ip t  join
                                (select a.guid,sum(b.ipdz2-b.ipdz1) as cn from t_logic_equ_ip a,t_logic_equ_ip_pz b
                                where a.ipdz1<=b.ipdz1 and a.ipdz2>=b.ipdz2 and b.pk_guid='" + GUID.Text + "' and ( b.SFHS<>'1' or b.SFHS is null) group by a.guid) p on  t.guid=p.guid ");
            DataFunction.ExecuteNonQuery("update t_logic_equ_ip set ipfpzt='" + Convert.ToString(dr["fpqk"]) + "' where guid='"+Convert.ToString(dr["GUID"])+"'");

        }
        
    }
    #endregion

    #region 设置IP
     public static void GetIpdz(string IPFD, string IP1, string IP2, string IP3, string IP4, ref Int64 IPDZ1, ref Int64 IPDZ2)
        {
            Int64 ipfd = Convert.ToInt64(IPFD);
            Int64 ipdz11 = Convert.ToInt64(IP1), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
            Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
            if (ipfd > 16)
            {
                ipdz12 = Convert.ToInt64(IP2);
                ipdz22 = ipdz12;
                if (ipfd > 24)
                {
                    ipdz13 = Convert.ToInt64(IP3);
                    ipdz23 = ipdz13;
                    getIp(ref ipdz14, ref ipdz24, Convert.ToInt64(IP4), 32 - ipfd);
                }
                else
                {
                    getIp(ref ipdz13, ref ipdz23, Convert.ToInt64(IP3), 24 - ipfd);
                }
            }
            else if (ipfd > 8)
            {
                getIp(ref ipdz12, ref ipdz22, Convert.ToInt64(IP2), 16 - ipfd);
            }
            IPDZ1 = Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14;
            IPDZ2 = Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24;
        }
     private static void getIp(ref Int64 ips1, ref Int64 ips2, Int64 ipdz, Int64 ipfd)
     {
         Int64[] ips = new Int64[8];
         ips[0] = ipdz % 2;
         ips[1] = ipdz / 2 % 2;
         ips[2] = ipdz / 4 % 2;
         ips[3] = ipdz / 8 % 2;
         ips[4] = ipdz / 16 % 2;
         ips[5] = ipdz / 32 % 2;
         ips[6] = ipdz / 64 % 2;
         ips[7] = ipdz / 128 % 2;
         Int64 ip = 0;
         for (Int64 i = 7; i >= ipfd; i--)
         {
             ip += ips[i] * Convert.ToInt64(Math.Pow(2, i));
         }
         ips1 = ip;
         ips2 = ip + Convert.ToInt64(Math.Pow(2, ipfd));
     }
    #endregion

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        if (!IsDelete(UNIT_ID.Text, GUID.Text))
        {
            string sql = "delete from " + TABLE_NAME.Text + " where guid='" + GUID.Text + "'";
            DataFunction.ExecuteNonQuery(sql);
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.close();parent.WindowClose();</script>");
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('该资源下有子资源，请先删除子资源');</script>");
        }

    }
    #endregion

    #region 删除时判断下面是否有数据，有的话不能删除 罗耀斌
    private bool IsDelete(string UnitId, string GUID)
    {

        int idx = 0;
        //得到下属资源
        DataTable dtList = DataFunction.FillDataSet("select CHILD_UNIT_ID from t_res_sys_unit_relation where father_unit_id='" + UnitId + "'").Tables[0];
        foreach (DataRow dr in dtList.Rows)
        {
            string ChildUnitId = Convert.ToString(dr["CHILD_UNIT_ID"]);
            DataTable childDt = DataFunction.FillDataSet("select unit_name,table_name from t_res_sys_unit where unit_id='" + ChildUnitId + "'").Tables[0];
            foreach (DataRow childDr in childDt.Rows)
            {
                string tableName = Convert.ToString(childDr["TABLE_NAME"]);

                //得到子资源表中的字段
                DataTable childZd = DataFunction.FillDataSet("select Filed_name ZD from t_res_sys_property where Unit_id='" + ChildUnitId + "'").Tables[0];
                foreach (DataRow zdDr in childZd.Rows)
                {
                    string zd = Convert.ToString(zdDr["ZD"]) + "_GUID";
                    //昨到子资源表的表结构，判断列名是否存在
                    DataTable dt = DataFunction.FillDataSet("select * from " + tableName + " where guid='aa'").Tables[0];
                    if (dt.Columns.Contains(zd))
                    {
                        idx += DataFunction.GetIntResult("select count(*) from " + tableName + " where " + zd + "='" + GUID + "'");
                    }

                }
            }
        }
        string mc = UNIT_NAME.Text;
        //因为光设备下特殊处理了下，加了光缆段，所以也要加上光缆段
        if (mc == "光设备" || mc=="网络设备" || mc=="传输设备")
        {
            idx += DataFunction.GetIntResult("select count(*) from t_res_gld where lsid='" + GUID + "'");
            idx += DataFunction.GetIntResult("select count(*) from T_RES_CHILD_GROOVE where equ_name_guid='" + GUID + "'");
        }
        if (idx == 0)
        {
            return false;
        }
        return true;
    }
    #endregion

    [Ajax.AjaxMethod]
    public DataSet getEnumData(string enumSort, string pEnumName)
    {
        string sql = "select * from T_RES_SYS_ENUMDATA where ENUM_SORT='" + enumSort + "' ";
        if (!string.IsNullOrEmpty(pEnumName))
        {
            sql += " and P_ENUM_NAME='" + pEnumName + "'";
        }
        sql += " order by SEQUENCE";
        return DataFunction.FillDataSet(sql);
    }



    #region 特殊计算公式
    private void TeSuJiSuanGongShi()
    {
        string sql = string.Format(@"select p.filed_name,p.tsgs from t_res_sys_property p where p.tsgs is not null and p.unit_id='{0}'",
            UNIT_ID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            sql = DR["TSGS"].ToString();
            string strValue = DataFunction.GetStringResult(string.Format(sql, GUID.Text));
            string strCheck = ShareFunction.GetControlValue(Page.FindControl(DR["FILED_NAME"].ToString() + "_CHECK")).ToString();
            if (string.IsNullOrEmpty(strValue) || strCheck == "0")
            {
                ShareFunction.SetControlValue(Page.FindControl(DR["FILED_NAME"].ToString()), strValue);
                DataFunction.ExecuteNonQuery("update " + TABLE_NAME.Text + " set " + DR["FILED_NAME"].ToString() + "='" + strValue + "' where guid='" + GUID.Text + "'");
            }
        }
    }
    #endregion

    protected void SslxChanged(object sender, EventArgs e)
    {
        InitPageContrl();
    }

    private void InitPageContrl()
    {
        if (UNIT_ID.Text == "53d347a2-5be1-4b85-af53-ffd35b3ccfc7")
        {
            string sslx = ShareFunction.GetControlValue(Page.FindControl("SSLX")).ToString();
            TextBox qsd = (TextBox)(Page.FindControl("QSD"));
            TextBox zzd = (TextBox)(Page.FindControl("ZZD"));
            if (sslx.IndexOf("用户端") > -1)
            {
                qsd.Attributes.Add("readonly", "true");
                qsd.BackColor = Color.WhiteSmoke;

                zzd.Attributes.Remove("readonly");
                zzd.BackColor = Color.White;
            }

            else if (sslx == "其它直连光缆")
            {

                qsd.Attributes.Remove("readonly");
                qsd.BackColor = Color.White;


                zzd.Attributes.Remove("readonly");
                zzd.BackColor = Color.White;
            }
            else
            {

                qsd.Attributes.Add("readonly", "true");
                qsd.BackColor = Color.WhiteSmoke;


                zzd.Attributes.Add("readonly", "true");
                zzd.BackColor = Color.WhiteSmoke;
            }
        }
    }

    #region 复制数据方法
    protected void BtnCopy_Click(object sender, EventArgs e)
    {
        //T_RES_EQU_NET          网络设备
        //T_RES_EQU_TRANSFERS    传输设备
        //T_RES_EQU_LIGHT        光设备
        //T_RES_CHILD_GROOVE     槽位
        //T_RES_CHILD_BOARD      板卡
        //T_RES_CHILD_PORT       端口

        string guid = GUID.Text;
        DataTable dt = null;
        //得到命名规则
        string dkmm = DataFunction.GetStringResult("select FORMULA from t_res_sys_property where unit_id =(select unit_id from  t_res_sys_unit where unit_name='端口') and propery_name='端口名称'");
        string bkmm = DataFunction.GetStringResult("select formula from t_res_sys_property where unit_id =(select unit_id from  t_res_sys_unit where unit_name='板卡') and propery_name='设备名称'");
        string cwmm = DataFunction.GetStringResult("select formula from t_res_sys_property where unit_id =(select unit_id from  t_res_sys_unit where unit_name='槽位') and propery_name='槽位名称'");
        if (dkmm.IsNullOrEmpty() || bkmm.IsNullOrEmpty() || cwmm.IsNullOrEmpty())
        {
            this.Alert("t_res_sys_property表中配置数据有误");
            return;
        }

        //新、老的编码、名称、GUID等
        string EquCode = ((TextBox)Page.FindControl("EQU_CODE")).Text;
        string EquName = ((TextBox)Page.FindControl("EQU_NAME")).Text;


        string NewEquCode = New_Equ_Code.Text;
        string NewEquName = New_Equ_Name.Text;
        string NewEquGuid = DateTime.Now.ToString("MMdd") + System.Guid.NewGuid().ToString("N");
        string TableName = "";

        if (UNIT_NAME.Text == "网络设备")
        {
            TableName = "T_RES_EQU_NET";
        }
        else if (UNIT_NAME.Text == "传输设备")
        {
            TableName = "T_RES_EQU_TRANSFERS";
        }
        else if (UNIT_NAME.Text == "光设备")
        {
            TableName = "T_RES_EQU_LIGHT";
        }
        if (TableName.IsNullOrEmpty())
        {
            this.Alert("没找到表名，程序配置错误");
            return;
        }
      
        DataTable sbdt = DataFunction.FillDataSet("select * from " + TableName + " where guid='" + guid + "'").Tables[0];
        if (sbdt.Rows.Count > 0)
        {
            dt = DataFunction.FillDataSet("select * from " + TableName + " where guid=''").Tables[0];
            DataRow sbdr = dt.NewRow();
            sbdr.ItemArray = sbdt.Rows[0].ItemArray;
            sbdr["GUID"] = NewEquGuid;
            sbdr["EQU_CODE"] = NewEquCode;
            sbdr["EQU_NAME"] = NewEquName;
            dt.Rows.Add(sbdr);
            DataFunction.SaveData(dt.DataSet, TableName);

            //复制槽位数据 如果没有槽位数据就复制端口数据
            DataTable cwdt = DataFunction.FillDataSet("select * from T_RES_CHILD_GROOVE where equ_name_guid='" + guid + "'").Tables[0];
            if (cwdt.Rows.Count > 0)
            {
                cwdt.Rows.Cast<DataRow>().ForEach(dr =>
                {
                    dt = DataFunction.FillDataSet("select * from T_RES_CHILD_GROOVE where equ_name_guid=''").Tables[0];
                    DataRow cwdrtemp = dt.NewRow();
                    cwdrtemp.ItemArray = dr.ItemArray;
                    dt.Rows.Add(cwdrtemp);

                    string cwid = Convert.ToString(dr["GUID"]);
                    string cwnewid = DateTime.Now.ToString("MMhh") + Guid.NewGuid().ToString("N");
                    cwdrtemp["GUID"] = cwnewid;
                    cwdrtemp["EQU_NAME_GUID"] = NewEquGuid;
                    cwdrtemp["EQU_NAME"] = NewEquName;
                    DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_GROOVE");
                    string cwnewname = DataFunction.GetStringResult("select " + cwmm + " from T_RES_CHILD_GROOVE a where a.guid='" + cwnewid + "'");
                    cwdrtemp["GROOVE_NAME"] = cwnewname;
                    DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_GROOVE");


                    //复制板卡数据
                    DataTable bkdt = DataFunction.FillDataSet("select * from T_RES_CHILD_BOARD  where GROOVE_NAME_GUID='" + cwid + "'").Tables[0];
                    bkdt.Rows.Cast<DataRow>().ForEach(bkdr =>
                    {
                        dt = DataFunction.FillDataSet("select * from T_RES_CHILD_BOARD  where GROOVE_NAME_GUID=''").Tables[0];
                        DataRow bkdrtemp = dt.NewRow();
                        bkdrtemp.ItemArray = bkdr.ItemArray;
                        dt.Rows.Add(bkdrtemp);

                        string bkid = Convert.ToString(bkdr["GUID"]);
                        string bknewid = DateTime.Now.ToString("MMhh") + Guid.NewGuid().ToString("N");
                        bkdrtemp["GUID"] = bknewid;
                        bkdrtemp["GROOVE_NAME_GUID"] = cwnewid;
                        bkdrtemp["GROOVE_NAME"] = cwnewname;
                        DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_BOARD");
                        string bknewname = DataFunction.GetStringResult("select " + bkmm + " from T_RES_CHILD_BOARD a where a.guid='" + bknewid + "'");
                        bkdrtemp["BOARD_NAME"] = bknewname;
                        DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_BOARD");

                        //查找端口,如果没有就查找槽位
                        DataTable dkdt = DataFunction.FillDataSet("select * from T_RES_CHILD_PORT where equ_name_guid='" + bkid + "'").Tables[0];
                        if (dkdt.Rows.Count > 0)
                        {
                            //再端口
                            dkdt.Rows.Cast<DataRow>().ForEach(dkdr =>
                            {
                                dt = DataFunction.FillDataSet("select * from T_RES_CHILD_PORT where equ_name_guid=''").Tables[0];
                                DataRow dkdrtemp = dt.NewRow();
                                dkdrtemp.ItemArray = dkdr.ItemArray;
                                dt.Rows.Add(dkdrtemp);

                                string dknewid = DateTime.Now.ToString("MMdd") + System.Guid.NewGuid().ToString("N");
                                dkdrtemp["GUID"] = dknewid;
                                dkdrtemp["EQU_NAME_GUID"] = bknewid;
                                dkdrtemp["EQU_NAME"] = bknewname;
                                DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_PORT");
                                dkdrtemp["PORT_NAME"] = DataFunction.GetStringResult("select " + dkmm + " from T_RES_CHILD_PORT a where a.guid='" + dknewid + "'");
                                DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_PORT");
                            });
                        }

                        //找槽位
                        DataTable cwdt2 = DataFunction.FillDataSet("select * from T_RES_CHILD_GROOVE where equ_name_guid='" + bkid + "'").Tables[0];
                        if (cwdt2.Rows.Count > 0)
                        {
                            cwdt2.Rows.Cast<DataRow>().ForEach(cw2 =>
                            {
                                dt = DataFunction.FillDataSet("select * from T_RES_CHILD_GROOVE where equ_name_guid=''").Tables[0];
                                DataRow cw2temp = dt.NewRow();
                                cw2temp.ItemArray = cw2.ItemArray;
                                dt.Rows.Add(cw2temp);


                                string cwid2 = Convert.ToString(cw2["GUID"]);
                                string cwnewid2 = DateTime.Now.ToString("MMhh") + Guid.NewGuid().ToString("N");
                                cw2temp["GUID"] = cwnewid2;
                                cw2temp["EQU_NAME_GUID"] = bknewid;
                                cw2temp["EQU_NAME"] = bknewname;

                                DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_GROOVE");
                                string cwnewname2 = DataFunction.GetStringResult("select " + cwmm + " from T_RES_CHILD_GROOVE a where a.guid='" + cwnewid2 + "'");
                                cw2temp["GROOVE_NAME"] = cwnewname2;
                                DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_GROOVE");


                                //再找板卡
                                DataTable bkdt2 = DataFunction.FillDataSet("select * from T_RES_CHILD_BOARD  where GROOVE_NAME_GUID='" + cwid2 + "'").Tables[0];
                                bkdt2.Rows.Cast<DataRow>().ForEach(bk2 =>
                                 {

                                     dt = DataFunction.FillDataSet("select * from T_RES_CHILD_BOARD  where GROOVE_NAME_GUID=''").Tables[0];
                                     DataRow bk2temp = dt.NewRow();
                                     bk2temp.ItemArray = bk2.ItemArray;
                                     dt.Rows.Add(bk2temp);

                                     string bkid2 = Convert.ToString(bk2["GUID"]);
                                     string bknewid2 = DateTime.Now.ToString("MMhh") + Guid.NewGuid().ToString("N");
                                     bk2temp["GUID"] = bknewid2;
                                     bk2temp["GROOVE_NAME_GUID"] = cwnewid2;
                                     bk2temp["GROOVE_NAME"] = cwnewname2;
                                     DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_BOARD");
                                     string bknewname2 = DataFunction.GetStringResult("select " + bkmm + " from T_RES_CHILD_BOARD a where a.guid='" + bknewid2 + "'");
                                     bk2temp["BOARD_NAME"] = bknewname2;
                                     DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_BOARD");

                                     //再找端口
                                     DataTable dkdt2 = DataFunction.FillDataSet("select * from T_RES_CHILD_PORT where equ_name_guid='" + bkid2 + "'").Tables[0];
                                     dkdt2.Rows.Cast<DataRow>().ForEach(dk2 =>
                                     {

                                         dt = DataFunction.FillDataSet("select * from T_RES_CHILD_PORT where equ_name_guid=''").Tables[0];
                                         DataRow dk2temp = dt.NewRow();
                                         dk2temp.ItemArray = dk2.ItemArray;
                                         dt.Rows.Add(dk2temp);

                                         string dknewid2 = DateTime.Now.ToString("MMdd") + System.Guid.NewGuid().ToString("N");
                                         dk2temp["GUID"] = dknewid2;
                                         dk2temp["EQU_NAME_GUID"] = bknewid2;
                                         dk2temp["EQU_NAME"] = bknewname2;
                                         DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_PORT");
                                         string dknewname2 = DataFunction.GetStringResult("select " + dkmm + " from T_RES_CHILD_PORT a where a.guid='" + dknewid2 + "'");
                                         dk2temp["PORT_NAME"] = dknewname2;
                                         DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_PORT");
                                     });

                                 });

                            });

                        }
                    });
                });
            }
            //复制端口
            DataTable dk = DataFunction.FillDataSet("select * from T_RES_CHILD_PORT where equ_name_guid='" + guid + "'").Tables[0];

            dk.Rows.Cast<DataRow>().ForEach(dr =>
            {
                dt = DataFunction.FillDataSet("select * from T_RES_CHILD_PORT where equ_name_guid=''").Tables[0];
                DataRow dtdr = dt.NewRow();
                dtdr.ItemArray = dr.ItemArray;
                dt.Rows.Add(dtdr);

                string dknewid = DateTime.Now.ToString("MMdd") + System.Guid.NewGuid().ToString("N");
                dtdr["GUID"] = dknewid;
                dtdr["EQU_NAME_GUID"] = NewEquGuid;
                dtdr["EQU_NAME"] = NewEquName;

                DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_PORT");
                string dknewname = DataFunction.GetStringResult("select " + dkmm + " from T_RES_CHILD_PORT a where a.guid='" + dknewid + "'");
                dtdr["PORT_NAME"] = dknewname;
                DataFunction.SaveData(dt.DataSet, "T_RES_CHILD_PORT");
            });
            this.Alert("生成成功...");
        }
    #endregion

    }
}

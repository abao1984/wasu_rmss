using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_PhyResourceSelect : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OKButton.Attributes.Add("onclick", "getSelectRowId()");
            PROPERY_ID.Text = Request.QueryString["PROPERY_ID"];
            TXT_NAME.Text = Request.QueryString["TXT_NAME"];
            TXT_GUID.Text = Request.QueryString["TXT_GUID"];
            TXT_CODE.Text = Request.QueryString["TXT_CODE"];
            RES_GUID.Text = Request.QueryString["RES_GUID"];
            RES_CODE.Text = Request.QueryString["RES_CODE"];
            RES_NAME.Text = Request.QueryString["RES_NAME"];
            NAME_FILED.Text = Request.QueryString["NAME_FILED"];
            ISEQUCODE.Text = Request.QueryString["ISEQUCODE"];           
            GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindRadioResource();
        }
        GetUnitName();
        shareResource.CreateResourceTable(TD_QUERY, RadioResource.SelectedValue, "QUERY");
        if (!this.IsPostBack)
        {
            FillPage();
        } 
        
             //GridViewPhyResource.Columns.Clear(); 
       
            shareResource.CreateResourceGrid(GridViewPhyResource, RadioResource.SelectedValue, true);
            if (!this.IsPostBack)
            {
                BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue));
            }
            else
            {
              //  shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
            }
        
        // ((TemplateField)GridViewPhyResource.Columns[0]).ItemTemplate = new CheckBoxTemplate();
    }
    public class CheckBoxTemplate : System.Web.UI.ITemplate
    {
        public CheckBoxTemplate()
        {
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            CheckBox ch = new CheckBox();
            ch.ID = "XZ";
            container.Controls.Add(ch);
        }
    }
    private void FillPage()
    {

        string sql = "select up.unit_id,p.filed_name from t_res_sys_property p,T_RES_SYS_PROPERTY_UNIT up where p.propery_id=up.propery_id and p.unit_id='" + RadioResource.SelectedValue + "'  and ISQUERY=1";
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            try
            {
                if (dr["unit_id"].ToString() == shareResource.comp_house_unit_id || dr["unit_id"].ToString() == shareResource.house_unit_id)
                {
                    ((TextBox)Page.FindControl(dr["filed_name"].ToString() + "_GUID")).Text = Convert.ToString(Session["HOUSE_ID"]);
                    ((TextBox)Page.FindControl(dr["filed_name"].ToString())).Text = Convert.ToString(Session["HOUSE_NAME"]);
                }
                else if (dr["unit_id"].ToString() == shareResource.cupboard_unit_id)
                {
                    ((TextBox)Page.FindControl(dr["filed_name"].ToString() + "_GUID")).Text = Convert.ToString(Session["CUPBOARD_C_ID"]);
                    ((TextBox)Page.FindControl(dr["filed_name"].ToString())).Text = Convert.ToString(Session["CUPBOARD_C_NAME"]);
                }
            }
            catch { }
        }

        //Control cn = Page.FindControl("HOUSE_AREA");
        //if (cn != null && Session["HOUSE_AREA"] != null)
        //{
        //    ((TextBox)cn).Text =Convert.ToString( Session["HOUSE_AREA"]);
        //    ((TextBox)Page.FindControl("HOUSE_AREA_CODE")).Text = Convert.ToString(Session["HOUSE_AREA_CODE"]);
        //}
        if (!string.IsNullOrEmpty(NAME_FILED.Text))
        {
            string nameFiled = getNameFiled();
            SetControlData(nameFiled, RES_NAME.Text);
            SetControlData(nameFiled + "_GUID", RES_GUID.Text);
        }
      //  GetHouseAreaSession();
    }

    private void GetHouseAreaSession()
    {
        string sql = "select t.filed_name from t_res_sys_property t where t.data_type='组织机构' and ISQUERY=1 and t.unit_id='" + RadioResource.SelectedValue + "'";
        string FiledName = DataFunction.GetStringResult(sql);
        if (!string.IsNullOrEmpty(FiledName))
        {
            string AreaCode = ((TextBox)Page.FindControl(FiledName + "_CODE")).Text;
            string AreaName = ((TextBox)Page.FindControl(FiledName)).Text;
            if (Session["HOUSE_AREA_CODE"] == null)
            {
                //Session["HOUSE_AREA"] = AreaName;
                //Session["HOUSE_AREA_CODE"] = AreaCode;
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
                    //Session["HOUSE_AREA"] = AreaName;
                    //Session["HOUSE_AREA_CODE"] = AreaCode;
                }
            }
        }
    }




    private string getNameFiled()
    {
        string sql = string.Format(@"select p1.filed_name from t_res_sys_property p1,T_RES_SYS_PROPERTY_UNIT rp1,t_res_sys_property p2,T_RES_SYS_PROPERTY_UNIT rp2
 where p1.unit_id='{0}' and p1.propery_id=rp1.propery_id and p2.filed_name='{1}' and p2.propery_id=rp2.propery_id
 and rp1.unit_id=rp2.unit_id", RadioResource.SelectedValue, NAME_FILED.Text);
        return DataFunction.GetStringResult(sql);
    }
    private void SetControlData(string filed_name,string filed_value)
    {
        Control cn = Page.FindControl(filed_name);
        if (cn != null)
        {
            ((TextBox)cn).Text = filed_value;
        }
    }

    private void BindRadioResource()
    {
        RadioResource.Items.Clear();
        string sql = "select u.* from t_res_sys_property_unit t,t_res_sys_unit u where t.unit_id=u.unit_id and t.propery_id='" + PROPERY_ID.Text + "' order by u.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            RadioResource.DataSource = ds;
            RadioResource.DataBind();
            RadioResource.Items[0].Selected = true;
        }
    }

    private void GetUnitName()
    {
        //string sql = "select * from t_res_sys_unit t where t.unit_id='" + RadioResource.SelectedValue + "'";
        //UNIT_NAME.Text = DataFunction.GetStringResult(sql);
        DataRow drt = shareResource.GetResUnitData(RadioResource.SelectedValue);
        UNIT_NAME.Text = drt["NAME_FILED"].ToString();
        if (string.IsNullOrEmpty(UNIT_NAME.Text))
        {
            GridViewPhyResource.DataKeyNames = new string[] { "GUID"};
        }
        else if (ISEQUCODE.Text == "1")
        {
            GridViewPhyResource.DataKeyNames = new string[] { "GUID", UNIT_NAME.Text, drt["EQU_CODE_FILED"].ToString() };
        }
        else
        {
            GridViewPhyResource.DataKeyNames = new string[] { "GUID", UNIT_NAME.Text };
        }
    }
    protected void OKButton_Click(object sender, EventArgs e)
    {
        string txtGuid = "";
        string txtCode = "";
        string txtName = "";
        if (string.IsNullOrEmpty(TXT_ROWID.Text))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择需要的设备！');</script>");
            return;
        }
        string[] rowids = TXT_ROWID.Text.Split(',');
        foreach (string rowid in rowids)
        {
            GridViewRow gr = GridViewPhyResource.Rows[Convert.ToInt32(rowid)];
            if (txtGuid != "") 
            {
                txtGuid += ",";                    
                txtName += ",";
                txtCode += ",";
            }
            txtGuid += GridViewPhyResource.DataKeys[gr.RowIndex].Values[0].ToString();
           txtName += GridViewPhyResource.DataKeys[gr.RowIndex].Values[1].ToString();
           if (ISEQUCODE.Text == "1")
           {
               txtCode += GridViewPhyResource.DataKeys[gr.RowIndex].Values[2].ToString();
           }
        }
        string str = "";
        if (ISEQUCODE.Text == "1")
        {
            str = string.Format(" parent.document.getElementById('{0}').value = '{1}';", TXT_CODE.Text, txtCode);
        }
        else
        {
            SetHouseNameLj(txtGuid);
        }
        string strScript = string.Format(@"<script> window.close();parent.WindowClose();
             parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}';{4}</script>"
         , TXT_NAME.Text, txtName, TXT_GUID.Text, txtGuid, str);       
       this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
        SetEquNameByLinkageCode( RadioResource.SelectedValue,PROPERY_ID.Text, txtGuid);
    }

    private void SetHouseNameLj(string strGuid)
    {
        if (TXT_NAME.Text == "HOUSE_NAME_LJ" || string.IsNullOrEmpty( strGuid))
        {
            return;
        }
        string sql = "select * from t_res_sys_property t where t.filed_name='HOUSE_NAME_LJ' and t.unit_id='" + RadioResource.SelectedValue + "'";
        if (DataFunction.HasRecord(sql))
        {
            sql = string.Format("select * from t_res_house_computer t where guid in ('{0}')",strGuid.Replace(",","','"));
            DataRow dr = DataFunction.GetSingleRow(sql);
            string strScript = string.Format(@" parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}';"
        , "HOUSE_NAME_LJ", dr["HOUSE_NAME_LJ"], "HOUSE_NAME_LJ_GUID", dr["HOUSE_NAME_LJ_GUID"]);
            strScript = "<script  language='javascript'>try{" + strScript + "} catch(e) {}</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
        }
    }

    private void SetEquNameByLinkageCode(string unit_id,string popery_id,string guid)
    {
        string sql = string.Format(@"select distinct p2.propery_id,p3.filed_name,u.table_name,up1.unit_id from t_res_sys_property p1,t_res_sys_property p2,T_RES_SYS_PROPERTY_UNIT up1,
t_res_sys_property p3,T_RES_SYS_PROPERTY_UNIT up2,T_RES_SYS_UNIT u
 where p1.propery_id='{0}' and p3.unit_id='{1}'
 and p1.unit_id=p2.unit_id and p1.linkage_code=p2.filed_name and p2.propery_id=up1.propery_id 
 and p3.propery_id=up2.propery_id and up1.unit_id=up2.unit_id and p3.unit_id=u.unit_id", popery_id, unit_id);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {

            sql = string.Format("select {0}_GUID,{0} from {1} where guid in ('{2}')", DR["filed_name"], DR["table_name"], guid.Replace(",","','"));
            DataSet equDs = DataFunction.FillDataSet(sql);
            if (equDs.Tables[0].Rows.Count > 0)
            {
                DataRow equDr = equDs.Tables[0].Rows[0];
                string strScript = string.Format(@"<script>parent.document.getElementById('{0}_GUID').value = '{1}'; parent.document.getElementById('{0}').value = '{2}';</script>"
                       , DR["filed_name"], equDr[0],equDr[1]);
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
                SetEquNameByLinkageCode(DR["unit_id"].ToString(), DR["PROPERY_ID"].ToString(), equDr[0].ToString());
            }
           
        }
    }



    protected void GridViewPhyResource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            CheckBox ch = (CheckBox)e.Row.FindControl("XZ");
            ch.Attributes.Add("onclick", "changeCheck(this);");
        }
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        oldCheckID.Text = "";
        GridViewPhyResource.PageIndex = 0;
       // GridViewPhyResource.PageIndex = -1;
        //shareResource.CreateResourceGrid(GridViewPhyResource, RadioResource.SelectedValue, true);
        BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue));
    }
    protected void RadioResource_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue));
    }
    //protected void RadioResource_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetUnitName();
    //}

    protected void GridViewPhyResource_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
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
      //  GridViewPhyResource.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
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
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
    }

    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
       // GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
    }
    #endregion
}

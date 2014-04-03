using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_PhyEquSelect : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            OKButton.Attributes.Add("onclick", "getSelectRowId()");
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            LJJF_NAME.Text = Request.QueryString["LJJF_NAME"];
            TXT_NAME.Text = Request.QueryString["TXT_NAME"];
            TXT_GUID.Text = Request.QueryString["TXT_GUID"];
            TXT_CODE.Text = Request.QueryString["TXT_CODE"];
            RES_GUID.Text = Request.QueryString["RES_GUID"];
            RES_CODE.Text = Request.QueryString["RES_CODE"];
            RES_NAME.Text = Request.QueryString["RES_NAME"];
            NAME_FILED.Text = Request.QueryString["NAME_FILED"];
            ISEQUCODE.Text = Request.QueryString["ISEQUCODE"];
            P_TXT_NAME.Text = Request.QueryString["P_TXT_NAME"];
            //BRANCH.Text = Request.QueryString["BRANCH"];
            //BRANCH_CODE.Text = Request.QueryString["BRANCH_CODE"];
            ZYSX.Text = Request.QueryString["ZYSX"];
            GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindRadioResource();
        }
        GetUnitName();
        shareResource.CreateResourceTable(TD_QUERY, RadioResource.SelectedValue, "QUERY");
        FillPage();

        //GridViewPhyResource.Columns.Clear(); 

        shareResource.CreateResourceGrid(GridViewPhyResource, RadioResource.SelectedValue, true);
        if (!this.IsPostBack)
        {
            BindGridPage(shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue));
        }
        //else
        //{
        //    shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
        //}

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
        if (!string.IsNullOrEmpty(NAME_FILED.Text))
        {
            SetControlData(NAME_FILED.Text, RES_NAME.Text);
            SetControlData(NAME_FILED.Text + "_CODE", RES_CODE.Text);
            SetControlData(NAME_FILED.Text + "_GUID", RES_GUID.Text);
        }
        //if (!string.IsNullOrEmpty(BRANCH.Text))
        //{
        //    string branchName = GetBranchName();
        //    if (!string.IsNullOrEmpty(branchName))
        //    {
        //        SetControlData(branchName, BRANCH.Text);
        //        SetControlData(branchName + "_CODE", BRANCH_CODE.Text);
        //    }
        //}
    }
    private string getNameFiled()
    {
        string sql = string.Format(@"select p1.filed_name from t_res_sys_property p1,T_RES_SYS_PROPERTY_UNIT rp1,t_res_sys_property p2,T_RES_SYS_PROPERTY_UNIT rp2
 where p1.unit_id='{0}' and p1.propery_id=rp1.propery_id and p2.filed_name='{1}' and p2.propery_id=rp2.propery_id
 and rp1.unit_id=rp2.unit_id", RadioResource.SelectedValue, NAME_FILED.Text);
        return DataFunction.GetStringResult(sql);
       
    }

    private string GetBranchName()
    {
        string sql = "select t.filed_name from t_res_sys_property t where t.data_type='组织机构' and t.unit_id='" + RadioResource.SelectedValue + "'";
        return DataFunction.GetStringResult(sql);
    }
    private void SetControlData(string filed_name, string filed_value)
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
        string sql = string.Format("select u.* from t_res_sys_unit u where u.unit_id in ('{0}') order by u.sequence",
            UNIT_ID.Text.Replace(",","','"));
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            RadioResource.DataSource = ds;
            RadioResource.DataBind();
            //RadioResource.Items[0].Selected = true;
            RadioResource.SelectedValue = UNIT_ID.Text.Split(',')[0];
        }
      
    }

    private void GetUnitName()
    {
        DataRow drt = shareResource.GetResUnitData(RadioResource.SelectedValue);
        UNIT_NAME.Text = drt["NAME_FILED"].ToString();
        TABLE_NAME.Text = drt["TABLE_NAME"].ToString();
        if (string.IsNullOrEmpty(UNIT_NAME.Text))
        {
            GridViewPhyResource.DataKeyNames = new string[] { "GUID" };
        }
        else if (ISEQUCODE.Text == "1" && !string.IsNullOrEmpty(drt["EQU_CODE_FILED"].ToString()))
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
            //this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择需要的设备！');");
            //this.ClientScript
        }
        else
        {
            string[] rowids = TXT_ROWID.Text.Split(',');
            string dkguids = "";
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
                //如果该设备是端口号，那就取端口编码，不取端口名称  罗耀斌
                if (UNIT_ID.Text == "bfc13d2d-eab8-4784-a96a-b8ffc21b4e88")
                {
                    txtName += GridViewPhyResource.Rows[gr.RowIndex].Cells[GridViewPhyResource.Columns.Count - 1].Text;
                    dkguids += "'" + GridViewPhyResource.DataKeys[gr.RowIndex].Values[0].ToString() + "',";
                }
                else
                {
                    txtName += GridViewPhyResource.DataKeys[gr.RowIndex].Values[1].ToString();
                }
                if (ISEQUCODE.Text == "1")
                {
                    txtCode += GridViewPhyResource.DataKeys[gr.RowIndex].Values[2].ToString();
                }
            }
            if (dkguids!="")
            {
                //如端口已启用,并且占用数量为1
                string names=getDKSFQY(dkguids);
                if (names!="")
                {
                    ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>sfqy('" + names + "')</script>");
                }
                
            }
        }
        string str = "";
        if (ISEQUCODE.Text == "1")
        {
            str = string.Format(" parent.document.getElementById('{0}').value = '{1}'", TXT_CODE.Text, txtCode);
        }
        SetHouseNameLj(txtGuid);
        SetZysx(txtGuid);
//        string strScript = string.Format(@"<script>var qy=document.getElementById('{5}').value;alert(qy);if(qy==null)window.close();parent.WindowClose();parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}';{4}</script>"
//, TXT_NAME.Text, txtName, TXT_GUID.Text, txtGuid, str, "SFFY");

        string strScript ="";
        if (ISEQUCODE.Text == "1")
        {
            strScript = string.Format(@"<script>test('{0}','{1}','{2}','{3}','{4}','{5}')</script>"
, TXT_NAME.Text, txtName, TXT_GUID.Text, txtGuid, TXT_CODE.Text, txtCode);
        }
        else
        {
            strScript = string.Format(@"<script>test('{0}','{1}','{2}','{3}')</script>"
, TXT_NAME.Text, txtName, TXT_GUID.Text, txtGuid);
        }
        
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
        if (RadioResource.SelectedValue != shareResource.port_unit_id)
        {
            SetEquNameByLinkageCode(RadioResource.SelectedValue, txtGuid);
        }
        shareResource.CreateResourceGrid(GridViewPhyResource, RadioResource.SelectedValue, true);
    }

    /// <summary>
    /// 端口是否启用
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    private string getDKSFQY(string guid)
    {
        string sql = "select port_name,guid from T_RES_CHILD_PORT where guid in (" + guid.Substring(0, guid.Length - 1) + ") and  DKZT='启用'";
        string strSql = "select count(*) from T_CON_LOGIC_EQU_IP t where t.zyhs_bj<>0 and jrdk_guid like '%{0}%'";
        DataSet ds = DataFunction.FillDataSet(sql);
        string name = "";
        int dkzynum = 0;
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            strSql = string.Format(strSql, dr[1].ToString());
            dkzynum = DataFunction.GetIntResult(strSql);
            if (dkzynum==1)
            {
                name += dr[0].ToString() + ",";
            }
            dkzynum = 0;
        }
        if (name!="")
        {
            name = name.Substring(0, name.Length - 1);
        }
        return name;
    }
    private void SetHouseNameLj(string strGuid)
    {
        if (string.IsNullOrEmpty(LJJF_NAME.Text) || LJJF_NAME.Text == "undefined" || string.IsNullOrEmpty(strGuid))
        {
            return;
        }
        string sql = "select * from t_res_sys_property t where t.filed_name='HOUSE_NAME_LJ' and t.unit_id='" + RadioResource.SelectedValue + "'";
        if (DataFunction.HasRecord(sql))
        {
            sql = string.Format("select * from t_res_house_computer t where guid in ('{0}')", strGuid.Replace(",", "','"));
            DataRow dr = DataFunction.GetSingleRow(sql);
            if (DataFunction.FillDataSet(sql).Tables[0].Rows.Count==0)
            {
                try
                {
                    sql = string.Format("select * from t_res_equ_net t where guid in ('{0}')", strGuid.Replace(",", "','"));
                    dr = DataFunction.GetSingleRow(sql);
                }
                catch
                {
                
                }
            }
            string strScript = string.Format(@"<script> parent.document.getElementById('{0}').value = '{1}'; parent.document.getElementById('{2}').value = '{3}';</script>"
        , LJJF_NAME.Text, dr["HOUSE_NAME_LJ"], LJJF_NAME.Text + "_GUID", dr["HOUSE_NAME_LJ_GUID"]);
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
        }
    }

    private void SetZysx(string strGuid)
    {
        if (string.IsNullOrEmpty(ZYSX.Text) || ZYSX.Text == "undefined" || string.IsNullOrEmpty(strGuid))
        {
            return;
        }
        string[] zysxs = ZYSX.Text.Split(':');
        string sql = string.Format("select {0} from {1} where guid in ('{2}')", zysxs[1],TABLE_NAME.Text,strGuid.Replace(",", "','"));
        DataRow dr = DataFunction.GetSingleRow(sql);
        string[] sxs = zysxs[0].Split(',');
        for (int i=0;i<sxs.Length;i++)
        {
            string strScript = string.Format(@"<script> parent.document.getElementById('{0}').value = '{1}';</script>"
    , sxs[i],dr[i].ToString());
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
        }
    }


    private void SetEquNameByLinkageCode(string unit_id, string guid)
    {
        string sql = string.Format(@"select p.filed_name,u.table_name from t_res_sys_property p,T_RES_SYS_UNIT u where p.unit_id=u.unit_id 
and p.filed_name='{1}' and p.unit_id='{0}'", unit_id,NAME_FILED.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            try
            {
                sql = string.Format("select {0}_GUID,{0} from {1} where guid in ('{2}')", DR["filed_name"], DR["table_name"], guid.Replace(",", "','"));
                DataSet equDs = DataFunction.FillDataSet(sql);
                if (equDs.Tables[0].Rows.Count > 0)
                {
                    DataRow equDr = equDs.Tables[0].Rows[0];
                    string strScript = string.Format(@"<script>parent.document.getElementById('{0}_GUID').value = '{1}'; parent.document.getElementById('{0}').value = '{2}';</script>"
                           , P_TXT_NAME.Text, equDr[0], equDr[1]);
                    this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);
                }
            }
            catch { }
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
        GridViewPhyResource.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridViewPhyResource.PageIndex + 1);
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
        GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewPhyResource.PageIndex + 1);
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewPhyResource.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
    }

    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewPhyResource.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewPhyResource.PageIndex + 1);
        shareResource.BindGrid(this.Page, GridViewPhyResource, RadioResource.SelectedValue);
    }
    #endregion
}

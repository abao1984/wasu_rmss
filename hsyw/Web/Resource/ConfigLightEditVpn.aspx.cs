using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigLightEditVpn : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SLSB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            JRSB_UNIT_ID.Text = "66a85a26-6e7b-42c6-ac01-678697ba3023,64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            YWGUID.Text = Request.QueryString["YWGUID"];
            SetContrlReadonly();
            BindDDL();
            FillPage();
            YWLX.SelectedValue = Request.QueryString["YWLX"];
            if (Request.QueryString["query"] != null)
            {
                tr_gld.Visible = false;
                query.Visible = false;
            }
            //YWLX.SelectedValue = "VPN";

            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
            //DataRow DR = null;   
            if (ds.Tables[0].Rows.Count > 0)
            {
                BtnAdd.Enabled = true;
                BtnDel.Enabled = true;
                
            }
            else
            {
                BtnAdd.Enabled = false;
                BtnDel.Enabled = false;

            }
        }
    }

    private void SetContrlReadonly()
    {
        VJF_CODE.Attributes.Add("readonly", "true");
        VJF.Attributes.Add("readonly", "true");
        VJRSB_CODE.Attributes.Add("readonly", "true");
        VJRSB.Attributes.Add("readonly", "true");
        VJRSBDK.Attributes.Add("readonly", "true");
        V_ODF_J.Attributes.Add("readonly", "true");
        V_ODF_K.Attributes.Add("readonly", "true");
        V_ODF_P.Attributes.Add("readonly", "true");
        V_ODF_FL.Attributes.Add("readonly", "true");
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

        //ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'DKXH' order by sequence");
        //VDKXH.DataSource = ds;
        //VDKXH.DataTextField = "ENUM_NAME";
        //VDKXH.DataValueField = "ENUM_NAME";
        //VDKXH.DataBind();
        //VDKXH.Items.Insert(0, new ListItem("", ""));
        //VDKXH.SelectedIndex = 0;
    }
    private void FillPage()
    {
        if (string.IsNullOrEmpty(YWGUID.Text))
        {
            YWGUID.Text = Guid.NewGuid().ToString();
        }
        else
        {
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
                //if (VGGSJ.Text != "") { VGGSJ.Text = VGGSJ.Text.Substring(0, 10); }
              
                BindGV();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('您打开的页面没有数据！');</script>");
                BtnSave.Enabled = false;
            }
        }
        FillRmssPage(SUBSCRIBER_ID.Text, "");
    }
    private void BindGV()
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS_CABLE where LIGHTGUID = '{0}' and LB = '0'", YWGUID.Text));
     
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvVGLD.DataSource = ds;
            gvVGLD.DataBind();
            int count1 = gvVGLD.Columns.Count;
            gvVGLD.Rows[0].Cells.Clear();
            gvVGLD.Rows[0].Cells.Add(new TableCell());
            gvVGLD.Rows[0].Cells[0].Text = "没有相关的信息！";
            gvVGLD.Rows[0].Cells[0].ColumnSpan = count1;
            gvVGLD.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            gvVGLD.DataSource = ds;
            gvVGLD.DataBind();
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        SaveData(false);
    }
    private void SaveData(bool isZyhs)
    {  
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
        string strTitle = "修改光缆资源配置";
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            CREATEDATETIME.Text = DateTime.Now.ToString();
            ZYHS_BJ.Text = "1";
            strTitle = "新增光缆资源配置";
        }
        if (isZyhs)
        {
            if (ZYHS_BJ.Text == "0")
            {
                strTitle = "回收光缆资源配置";
            }
            else
            {
                strTitle = "恢复光缆资源配置";
            }
        }

        DataRow DR = ds.Tables[0].Rows[0];
        string strPortGuid = DR["VJRSBDK_GUID"].ToString()  + "," + VJRSBDK_GUID.Text ;
        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        SaveRmssData(SUBSCRIBER_ID.Text, SUBSCRIBER_CODE.Text, "", SUBSCRIBER_GDLY.SelectedValue);
        string strComment = ShareFunction.GetControlData(Page, DR, "T_CON_LIGHT_BUSINESS");
        DataFunction.SaveData(ds, "T_CON_LIGHT_BUSINESS");
        shareResource.SetResourcePort(strPortGuid);


        string strSql = @"select b.subscriber_code,c.SUB_NAME,a.gxh_guid
                         from T_CON_LIGHT_BUSINESS_CABLE a,
                              T_CON_LIGHT_BUSINESS b,
                              rmss c
                        where a.lightguid = b.ywguid
                          and c.subscriber_id = b.SUBSCRIBER_ID";
        DataSet gxds = DataFunction.FillDataSet(strSql);
        foreach (DataRow dr in gxds.Tables[0].Rows)
        {
            strSql = string.Format(@"update T_RES_CHILD_CORE set ywbm='{0}' ,yhmc='{1}' where guid='{2}'",dr[0].ToString(),dr[1].ToString(),dr[2].ToString());
            DataFunction.ExecuteNonQuery(strSql);
        }
        ShareFunction.InsertLog(this.Page, YWGUID.Text, strTitle, strComment);
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.close();parent.WindowClose();</script>");
        //FillPage();
    }
    private void SetResChildPortZt(string dkGuid, string dkzt)
    {
        string sql = string.Format("update t_res_child_port t set t.dkzt='{1}' where t.guid in ('{0}') ",
            dkGuid.Replace(",", "','"), dkzt);
        DataFunction.ExecuteNonQuery(sql);
    }

    protected void YWLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (YWLX.SelectedValue.IndexOf("骨干") > -1)
        {
            Response.Redirect("ConfigLightEditBone.aspx?YWLX=" + YWLX.SelectedValue);
        }
        else if (YWLX.SelectedValue.IndexOf("VPN") == -1)
        {
            Response.Redirect("ConfigLightEdit.aspx?YWLX="+YWLX.SelectedValue);
        }
        
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow row in gvVGLD.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as CheckBox).Checked)
            {
                guids += ",'" + gvVGLD.DataKeys[row.RowIndex]["GUID"].ToString() + "'";
                SetResChildCoreZt(gvVGLD.DataKeys[row.RowIndex]["GXH_GUID"].ToString(), "未使用");
            }
        }
        if (!guids.Equals("''"))
        {
            string sql = string.Format("delete from T_CON_LIGHT_BUSINESS_CABLE where GUID in ({0})", guids);
            DataFunction.ExecuteNonQuery(sql);
            BindGV();
        }
    }
    protected void gvVGLD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = gvVGLD.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            if (!guid.Equals(""))
            {
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','0','VJF')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }
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
    }
    protected void Btn1_Click(object sender, EventArgs e)
    {
        BindGV();
    }
    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");

    }
    protected void SUBSCRIBER_GDLY_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeSUBSCRIBER_CODE();
        SUBSCRIBER_ID.Text = "";
        FillRmssPage(SUBSCRIBER_ID.Text, "");
    }

    private void ChangeSUBSCRIBER_CODE()
    {
        if (SUBSCRIBER_GDLY.SelectedValue == "BOSS")
        {
            TQ.Visible = true;
            SelectBOSS.Visible = true;
        }
        else
        {
            TQ.Visible = false;
            SelectBOSS.Visible = false;
        }
    }
    protected void TQ_Click(object sender, ImageClickEventArgs e)
    {
        if (SUBSCRIBER_CODE.Text != "")
        {

            string sql = "select * from rmss t where 1=1";

            sql += " and SUBSCRIBER_CODE  like '%" + SUBSCRIBER_CODE.Text + "%'";
            DataSet dt = DataFunction.FillDataSet(sql);
            if (dt.Tables[0].Rows.Count == 1)
            {
                SUBSCRIBER_ID.Text = dt.Tables[0].Rows[0]["SUBSCRIBER_ID"].ToString();
                FillRmssPage(SUBSCRIBER_ID.Text, "");
            }
            else if (dt.Tables[0].Rows.Count > 1)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpenRmssTQ()</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('没有查到相应的用户信息！');</script>");
            }

        }
    }

    #region 业务单管理
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

    /// <summary>
    /// 保存手工业务单
    /// </summary>
    /// <param name="str_SUBSCRIBER_ID">业务ID</param>
    /// <param name="JYD">甲乙端</param>
    /// <param name="str_SUBSCRIBER_GDLY">工单来源</param>
    private void SaveRmssData(string str_SUBSCRIBER_ID, string str_SUBSCRIBER_CODE, string JYD, string str_SUBSCRIBER_GDLY)
    {
        if (str_SUBSCRIBER_GDLY == "BOSS")
        { return; }

        if (string.IsNullOrEmpty(str_SUBSCRIBER_CODE))
        { return; }

        string sql = string.Format("select * from RMSS_SGD t where t.SUBSCRIBER_ID='{0}'", str_SUBSCRIBER_ID.Replace("SGD_", ""));
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            string sId = GetSubscriberId();
            ((System.Web.UI.WebControls.TextBox)this.Page.FindControl(JYD + "SUBSCRIBER_ID")).Text = "SGD_" + sId;
            DR["SUBSCRIBER_ID"] = sId;
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            DR = ds.Tables[0].Rows[0];
        }

        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName = JYD + DC.ColumnName;
            string strValue = null;
            System.Web.UI.Control webControl = this.Page.FindControl(ControlName);
            if (webControl != null)
            {
                string controlType = webControl.GetType().FullName;
                switch (controlType)
                {
                    case "System.Web.UI.WebControls.TextBox":
                        strValue = ((System.Web.UI.WebControls.TextBox)webControl).Text.Replace("SGD_", "");
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        strValue = ((System.Web.UI.WebControls.DropDownList)webControl).SelectedValue;
                        break;
                }
            }
            if (string.IsNullOrEmpty(strValue))
            {
                DR[DC.ColumnName] = Convert.DBNull;
            }
            else
            {
                DR[DC.ColumnName] = strValue;
            }
        }
        DataFunction.SaveData(ds, "RMSS_SGD");
    }

    private string GetSubscriberId()
    {
        string sql = "select nvl(max(t.subscriber_id),0)+1 as ID from rmss_sgd t";
        return DataFunction.GetStringResult(sql);
    }
    #endregion
    protected void BtnZyhs_Click(object sender, EventArgs e)
    {
        if (ZYHS_BJ.Text == "1")
        {
            ZYHS_BJ.Text = "0";
        }
        else
        {
            ZYHS_BJ.Text = "1";
        }
        SaveData(true);
    }

    private void SetResChildCoreZt(string gxhGuid, string gxzt)
    {
        string sql = string.Format("update t_res_child_core t set t.gxzt='{1}',ywbm='',ywmc='' where t.guid in ('{0}') ",
            gxhGuid.Replace(",", "','"), gxzt);
        DataFunction.ExecuteNonQuery(sql);
    }
}

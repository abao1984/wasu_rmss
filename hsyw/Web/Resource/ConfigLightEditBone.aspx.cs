using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigLightEditBone : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            YWGUID.Text = Request.QueryString["YWGUID"];
            if (Request.QueryString["query"] != null)
            {
                query.Visible = false;
                tr_gld.Visible = false;
            }
            BindDrp();
            //BindGV();
            FillPage();
            YWLX1.SelectedValue = "骨干业务";
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
            //DataRow DR = null;   
            if (ds.Tables[0].Rows.Count > 0)
            {
                BtnAddJDGLD.Enabled = true;
                BtnDelJDGLD.Enabled = true;
                BtnAddYDGLD.Enabled = true;
                BtnDelYDGLD.Enabled = true;
            }
            else
            {
                BtnAddJDGLD.Enabled = false;
                BtnDelJDGLD.Enabled = false;
                BtnAddYDGLD.Enabled = false;
                BtnDelYDGLD.Enabled = false;

            }
        }
    }

    private void FillPage()
    {
        if (string.IsNullOrEmpty(YWGUID.Text))
        {
            YWGUID.Text = Guid.NewGuid().ToString();
        }
        //else
        //{
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count > 0)
        {
            DR = ds.Tables[0].Rows[0];
        }
        else
        {
            DR = ds.Tables[0].NewRow();
            DR["YWGUID"] = YWGUID.Text;
            DR["ZYHS_BJ"] = "1";
            ds.Tables[0].Rows.Add(DR);
        }

        ShareFunction.FillControlData(Page, DR);
        
        BindGV("1");
        BindGV("2");
        //}
        FillBonePage(SUBSCRIBER_CODE.Text, "");
        if (ZYHS_BJ.Text == "1")
        {
            BtnZyhs.Text = "资源回收";
        }
        else
        {
            BtnZyhs.Text = "资源恢复";
        }
    }

    protected void TQ_Click(object sender, ImageClickEventArgs e)
    {
        if (YWBM.Text != "")
        {

            string sql = "select * from t_con_bone_business t where 1=1";

            sql += " and ywbm  like '%" + YWBM.Text + "%'";
            DataSet dt = DataFunction.FillDataSet(sql);
            if (dt.Tables[0].Rows.Count == 1)
            {
                YWBM.Text = dt.Tables[0].Rows[0]["YWBM"].ToString();
                FillBonePage(YWBM.Text, "");
            }
            else if (dt.Tables[0].Rows.Count > 1)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpenBoneTQ()</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('没有查到相应的骨干业务信息！');</script>");
            }
        }
    }

    private void FillBonePage(string ywbm, string JYD)
    {
        string sql = string.Format("select * from t_con_bone_business t where t.ywbm='{0}'", ywbm);
        DataRow DR = DataFunction.GetSingleRow(sql);
        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName = JYD + DC.ColumnName.Replace("JR","");
            string strValue = DR[DC.ColumnName].ToString(); 
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
        YWBM1.Text = YWBM.Text;
        SUBSCRIBER_CODE.Text = YWBM.Text;
    }

    private void BindDrp()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'GLYWLX' order by sequence");
        YWLX1.DataSource = ds;
        YWLX1.DataTextField = "ENUM_NAME";
        YWLX1.DataValueField = "ENUM_NAME";
        YWLX1.DataBind();
        YWLX1.Items.Insert(0, new ListItem("", ""));

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'YWLX' order by sequence");
        YWLX.DataSource = ds;
        YWLX.DataTextField = "ENUM_NAME";
        YWLX.DataValueField = "ENUM_NAME";
        YWLX.DataBind();
        YWLX.Items.Insert(0, new ListItem("", ""));
        YWLX.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'WXZL' order by sequence");
        JDWXZL.DataSource = ds;
        JDWXZL.DataTextField = "ENUM_NAME";
        JDWXZL.DataValueField = "ENUM_NAME";
        JDWXZL.DataBind();
        JDWXZL.Items.Insert(0, new ListItem("", ""));
        JDWXZL.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'WXZL' order by sequence");
        YDWXZL.DataSource = ds;
        YDWXZL.DataTextField = "ENUM_NAME";
        YDWXZL.DataValueField = "ENUM_NAME";
        YDWXZL.DataBind();
        YDWXZL.Items.Insert(0, new ListItem("", ""));
        YDWXZL.SelectedIndex = 0;
        //YWLX.SelectedIndex = 0;
    }

    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillBonePage(YWBM.Text, "");
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
        //string strTitle = "修改光缆资源配置";
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            CREATEDATETIME.Text = DateTime.Now.ToString();
            ZYHS_BJ.Text = "1";
            //strTitle = "新增光缆资源配置";
        }
       
        DataRow DR = ds.Tables[0].Rows[0];

        string strPortGuid = DR["JDJRSBDK_GUID"].ToString() + "," + DR["YDJRSBDK_GUID"].ToString() + "," + JDSBDK_GUID.Text + "," + YDSBDK_GUID.Text;

        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        //SaveRmssData(SUBSCRIBER_ID.Text, SUBSCRIBER_CODE.Text, "", SUBSCRIBER_GDLY.SelectedValue);
        string strComment = ShareFunction.GetControlData(Page, DR, "T_CON_LIGHT_BUSINESS");
        DR["YWLX"] = YWLX1.SelectedValue;
        DR["JDJRSB_GUID"] = JDSB_GUID.Text;
        DR["JDJRSB_CODE"] = JDSB_CODE.Text;
        DR["JDJRSB"] = JDSB.Text;
        DR["JDJRSBDK_GUID"] = JDSBDK_GUID.Text;
        DR["JDJRSBDK"] = JDSBDK.Text;

        DR["YDJRSB_GUID"] = YDSB_GUID.Text;
        DR["YDJRSB_CODE"] = YDSB_CODE.Text;
        DR["YDJRSB"] = YDSB.Text;
        DR["YDJRSBDK_GUID"] = YDSBDK_GUID.Text;
        DR["YDJRSBDK"] = YDSBDK.Text;
        DataFunction.SaveData(ds, "T_CON_LIGHT_BUSINESS");
        //shareResource.SetResourcePort(strPortGuid);
        //ShareFunction.InsertLog(this.Page, YWGUID.Text, strTitle, strComment);
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.close();parent.WindowClose();</script>");
       // FillPage();
    }
    protected void BtnZyhs_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExp_Click(object sender, EventArgs e)
    {

    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {

    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGV("1");
        BindGV("2");
    }
    protected void YWLX1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (YWLX.SelectedValue.IndexOf("VPN") >= -1)
        {
            Response.Redirect("ConfigLightEditVpn.aspx?YWLX=" + YWLX1.SelectedValue);
        }
        else if (YWLX.SelectedValue.IndexOf("骨干") == -1)
        {
            Response.Redirect("ConfigLightEdit.aspx?YWLX=" + YWLX1.SelectedValue);
        }
    }

    private void BindGV(string lb)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS_CABLE where LIGHTGUID = '{0}' and LB = '{1}' order by GLDXH", YWGUID.Text, lb));
        GridView gv = gvJDGLD;
        if (lb.Equals("2"))
        {
            gv = gvYDGLD;
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gv.DataSource = ds;
            gv.DataBind();
        }
        else
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
    }

    protected void BtnDelJDGLD_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow row in gvJDGLD.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                guids += ",'" + gvJDGLD.DataKeys[row.RowIndex]["GUID"].ToString() + "'";
                SetResChildCoreZt(gvJDGLD.DataKeys[row.RowIndex]["GXH_GUID"].ToString(), "未使用");
            }
        }
        if (!guids.Equals("''"))
        {
            string sql = string.Format("delete from T_CON_LIGHT_BUSINESS_CABLE where GUID in ({0})", guids);
            DataFunction.ExecuteNonQuery(sql);
        }
        BindGV("1");
        //Btn1_Click(null, null);
    }

    protected void BtnDelYDGLD_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow row in gvYDGLD.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox2") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                guids += ",'" + gvYDGLD.DataKeys[row.RowIndex]["GUID"].ToString() + "'";
                SetResChildCoreZt(gvJDGLD.DataKeys[row.RowIndex]["GXH_GUID"].ToString(), "未使用");
            }
        }
        if (!guids.Equals("''"))
        {
            string sql = string.Format("delete from T_CON_LIGHT_BUSINESS_CABLE where GUID in ({0})", guids);
            DataFunction.ExecuteNonQuery(sql);
        }
        BindGV("2");
    }

    private void SetResChildCoreZt(string gxhGuid, string gxzt)
    {
        string sql = string.Format("update t_res_child_core t set t.gxzt='{1}',ywbm='',ywmc='' where t.guid in ('{0}') ",
            gxhGuid.Replace(",", "','"), gxzt);
        DataFunction.ExecuteNonQuery(sql);
    }

    protected void gvJDGLD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = gvJDGLD.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            if (string.IsNullOrEmpty(guid))
            {
                // e.Row.Cells.Clear();
            }
            else
            {
                //if (!guid.Equals(""))
                //{
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','1','JDJF','" + CUSTOMER_NAME.Text + "')");
                //}
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
                //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
                //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
            }
        }
    }
    //乙端
    protected void gvYDGLD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = gvYDGLD.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            if (string.IsNullOrEmpty(guid))
            {
                // e.Row.Cells.Clear();
            }
            else
            {
                //if (!guid.Equals(""))
                //{
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','2','YDJF','" + CUSTOMER_NAME.Text + "')");
                //}
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
                //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
                //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
            }
        }
    }
}

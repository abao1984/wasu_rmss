using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Aspose.Cells;

public partial class Web_Resource_ConfigLightEdit : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SLSB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            JRSB_UNIT_ID.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            YWGUID.Text = Request.QueryString["YWGUID"];
            //JDGGSJ.Attributes.Add("readonly","true");
            //YDGGSJ.Attributes.Add("readonly", "true");
            SetContrlReadonly();
            gvJDGLD.Attributes.Add("BorderColor", "#5B9ED1");
            gvYDGLD.Attributes.Add("BorderColor", "#5B9ED1");
            BindDDL();
            FillPage();
            if (Request.QueryString["YWLX"] != null)
            {
                YWLX.SelectedValue = Request.QueryString["YWLX"];
            }
            if (Request.QueryString["query"]!=null)
            {
                tr_gld.Visible = false;
                query.Visible = false;
            }
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
    private void SetContrlReadonly()
    {
        JDJF_CODE.Attributes.Add("readonly", "true");
        JDJF.Attributes.Add("readonly", "true");
        YDJF_CODE.Attributes.Add("readonly", "true");
        YDJF.Attributes.Add("readonly", "true");
        JDJRSB_CODE.Attributes.Add("readonly", "true");
        JDJRSB.Attributes.Add("readonly", "true");
        YDJRSB_CODE.Attributes.Add("readonly", "true");
        YDJRSB.Attributes.Add("readonly", "true");
        JDJRSBDK.Attributes.Add("readonly", "true");
        YDJRSBDK.Attributes.Add("readonly", "true");

        JD_ODF_J.Attributes.Add("readonly", "true");
        JD_ODF_K.Attributes.Add("readonly", "true");
        JD_ODF_P.Attributes.Add("readonly", "true");
        JD_ODF_FL.Attributes.Add("readonly", "true");

        YD_ODF_J.Attributes.Add("readonly", "true");
        YD_ODF_K.Attributes.Add("readonly", "true");
        YD_ODF_P.Attributes.Add("readonly", "true");
        YD_ODF_FL.Attributes.Add("readonly", "true");
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
        //JDDKXH.DataSource = ds;
        //JDDKXH.DataTextField = "ENUM_NAME";
        //JDDKXH.DataValueField = "ENUM_NAME";
        //JDDKXH.DataBind();
        //JDDKXH.Items.Insert(0, new ListItem("", ""));
        //JDDKXH.SelectedIndex = 0;

        //ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'DKXH' order by sequence");
        //YDDKXH.DataSource = ds;
        //YDDKXH.DataTextField = "ENUM_NAME";
        //YDDKXH.DataValueField = "ENUM_NAME";
        //YDDKXH.DataBind();
        //YDDKXH.Items.Insert(0, new ListItem("", ""));
        //YDDKXH.SelectedIndex = 0;
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
               DR= ds.Tables[0].Rows[0];              
            }
            else
            {
                DR=ds.Tables[0].NewRow();
                DR["YWGUID"]=YWGUID.Text;
                DR["ZYHS_BJ"] = "1";
                ds.Tables[0].Rows.Add(DR);
            }
           
        ShareFunction.FillControlData(Page,DR);
        BindGV("1");
        BindGV("2");
        //}
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        if (ZYHS_BJ.Text == "1")
        {
            BtnZyhs.Text = "资源回收";
        }
        else
        {
            BtnZyhs.Text = "资源恢复";
        }
    }
    private void BindGV(string lb)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS_CABLE where LIGHTGUID = '{0}' and LB = '{1}'",YWGUID.Text,lb));
        GridView gv = gvJDGLD;
        if(lb.Equals("2"))
        {
            gv = gvYDGLD;
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            
            gv.DataSource = ds;
            gv.DataBind();
            //int count1 = gv.Columns.Count;
            //gv.Rows[0].Cells.Clear();
            //gv.Rows[0].Cells.Add(new TableCell());
            //gv.Rows[0].Cells[0].Text = "没有相关的信息！";
            //gv.Rows[0].Cells[0].ColumnSpan = count1;
            //gv.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            gv.DataSource = ds;
            gv.DataBind();
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
        //string zyZt = "启用";
        //if (ZYHS_BJ.Text == "0")
        //{
        //    zyZt = "未启用";
        //}
        //else
        //{
        //    SetResChildPortZt(DR["JDJRSBDK_GUID"].ToString(), "未启用");
        //    SetResChildPortZt(DR["YDJRSBDK_GUID"].ToString(), "未启用");

        //    SetResChildPortZt(DR["JD_ODF_FL_GUID"].ToString(), "未启用");
        //    SetResChildPortZt(DR["YD_ODF_FL_GUID"].ToString(), "未启用");
        //}
        //SetResChildPortZt(JDJRSBDK_GUID.Text, zyZt);
        //SetResChildPortZt(YDJRSBDK_GUID.Text, zyZt);
        //SetResChildPortZt(JD_ODF_FL_GUID.Text, zyZt);
        //SetResChildPortZt(YD_ODF_FL_GUID.Text, zyZt);
        string strPortGuid = DR["JDJRSBDK_GUID"].ToString() + "," + DR["YDJRSBDK_GUID"].ToString() + "," + JDJRSBDK_GUID.Text + "," + YDJRSBDK_GUID.Text;

        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        SaveRmssData(SUBSCRIBER_ID.Text, SUBSCRIBER_CODE.Text, "", SUBSCRIBER_GDLY.SelectedValue);
       string strComment= ShareFunction.GetControlData(Page, DR, "T_CON_LIGHT_BUSINESS");
        DataFunction.SaveData(ds, "T_CON_LIGHT_BUSINESS");
        shareResource.SetResourcePort(strPortGuid);
        ShareFunction.InsertLog(this.Page, YWGUID.Text, strTitle, strComment);
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.close();parent.WindowClose();</script>");
        //FillPage();
    }

    protected void YWLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (YWLX.SelectedValue.IndexOf("VPN") > -1)
        {
            Response.Redirect("ConfigLightEditVpn.aspx?YWLX=" + YWLX.SelectedValue);
        }
        else if (YWLX.SelectedValue.IndexOf("骨干") > -1)
        {
            Response.Redirect("ConfigLightEditBone.aspx?YWLX=" + YWLX.SelectedValue);
        }
    }
    //甲端
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
                    e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "','2','YDJF')");
                //}
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
                //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
                //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
            }
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
                SetResChildCoreZt(gvJDGLD.DataKeys[row.RowIndex]["GXH_GUID"].ToString(),"未使用");
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

    private void SetResChildCoreZt(string gxhGuid, string gxzt)
    {
        string sql = string.Format("update t_res_child_core t set t.gxzt='{1}',ywbm='',ywmc='' where t.guid in ('{0}') ",
            gxhGuid.Replace(",", "','"), gxzt);
        DataFunction.ExecuteNonQuery(sql);
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
        BindGV("1");
        BindGV("2");
    }
    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");

    }
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

            sql += " and SUBSCRIBER_CODE like '%" + SUBSCRIBER_CODE.Text.Trim() + "%'";
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
    private void SetResChildPortZt(string dkGuid, string dkzt)
    {
        string sql = string.Format("update t_res_child_port t set t.dkzt='{1}' where t.guid in ('{0}') ",
            dkGuid.Replace(",", "','"), dkzt);
        DataFunction.ExecuteNonQuery(sql);
    }

    protected void BtnExp_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        book.Open(Server.MapPath("../../template/光缆资源配置模板.xls"));
        Worksheet ws = book.Worksheets["Sheet1"];
        DataTable dt = DataFunction.FillDataSet(@"select yh.SUB_NAME B3,yh.CUSTOMER_CODE D3,yh.CUSTOMER_NAME F3,yh.CUSTTYPE1 B4,yh.CUSTTYPE D4,
                                             yh.REGION F4,yh.ADDRESS B5,yh.FAX_NO B6,yh.ZIP_CODE D6,yh.LINKMAN F6,yh.CUSTOMER_LEVEL H6,
                                             yh.PHONE_NO B7,yh.EMAIL D7,yh.SALE_NAME F7,yh.MOBILE_NO H7,
                                             t.ywlx B1,t.subscriber_code E1,case when t.subscriber_id like 'SGD%' then '手工单' else 'BOSS' end H1,
                                             t.jdjf_code B9,t.jdjf D9,t.jdjrsb_code B10,t.jdjrsb D10,t.jdjrsbdk B11,t.jdglkhdsz C12,t.jdcssbxgjl C13,
                                             t.ydjf_code F9,t.ydjf H9,t.ydjrsb_code F10,t.ydjrsb H10,t.ydjrsbdk F11,t.ydglkhdsz G12,t.ydcssbxgjl G13,
                                             t.jd_odf_j B15,t.jd_odf_k D15,t.jd_odf_p B16,t.jd_odf_fl D16,t.yd_odf_j F15,t.yd_odf_k H15,t.yd_odf_p F16,
                                             t.yd_odf_fl H16,t.ztll B18,case t.llfx when -1 then '甲端←乙端' when 1 then '甲端→乙端' when 0 then '甲端～乙端' else '' end B19,
                                             t.llcd E19,t.qcshz G19,t.pzjl_bz B20
                                             from T_CON_LIGHT_BUSINESS T
                                             left join rmss yh on t.subscriber_id=yh.SUBSCRIBER_ID
                                             where ywguid='" + YWGUID.Text + "'").Tables[0];
        //导入基本数据
        dt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            dt.Columns.Cast<DataColumn>().ForEach(col =>
            {
                ws.Cells[col.ColumnName].PutValue(Convert.ToString(dr[col]));
            });
        });
        for (int i = 1; i <= 2; i++)
        {
            int idx = 22;//模板中的光缆段数据是从第22开始导的，所以默认22 罗耀斌

            //导入甲光缆段数据
            int colidx = 0;
            if (i % 2 == 0)
            {
                colidx = 4;
            }
            DataTable jdgld = DataFunction.FillDataSet("select GLDXH,gldmc_code,GLDMC,GXH from T_CON_LIGHT_BUSINESS_CABLE where lightguid='" + YWGUID.Text + "' and lb='" + i + "'").Tables[0];
            jdgld.Rows.Cast<DataRow>().ForEach(dr =>
            {
                jdgld.Columns.Cast<DataColumn>().ForEach(col =>
                {
                    ws.Cells[idx, colidx].PutValue(Convert.ToString(dr[col]));
                    ++colidx;
                });
                ++idx;
                if (i % 2 == 0)
                {
                    colidx = 4;
                }
                else
                {
                    colidx = 0;
                }
            });
        }



        ////导入乙光缆段数据
        //colidx = 4;
        //idx = 0;
        //DataTable ydgld = DataFunction.FillDataSet("select GLDXH,gldmc_code,GLDMC,GXH from T_CON_LIGHT_BUSINESS_CABLE where lightguid='" + YWGUID.Text + "' and lb='2'").Tables[0];
        //ydgld.Rows.Cast<DataRow>().ForEach(dr =>
        //{
        //    ydgld.Columns.Cast<DataColumn>().ForEach(col =>
        //    {
        //        ws.Cells[idx, colidx].PutValue(Convert.ToString(dr[col]));
        //        ++colidx;
        //    });
        //    ++idx;
        //    colidx = 4;
        //});
        MemoryStream ms = new MemoryStream();
        book.Save(ms, FileFormatType.Excel2003);
        IDP.Common.WebUtils.ResponseWriteBinary(ms.ToArray(), "光缆资源配置.xls");
    }
}

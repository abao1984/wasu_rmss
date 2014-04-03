using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;

public partial class Web_Resource_ConfigTransmissionEdit : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {

        Ajax.Utility.RegisterTypeForAjax(typeof(Web_Resource_ConfigTransmissionEdit));
        if (!Page.IsPostBack)
        {
            BindDDL();
            //PZRQ.Attributes.Add("readonly","true");
            YWGUID.Text = Request.QueryString["YWGUID"];
            PZR.Text = Session["UserRealName"].ToString();
            FillPage();
            
        }
    }
    private void BindDDL()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'ZWFS' order by sequence");
        ZWFS.DataSource = ds;
        ZWFS.DataTextField = "ENUM_NAME";
        ZWFS.DataValueField = "ENUM_NAME";
        ZWFS.DataBind();
        ZWFS.Items.Insert(0, new ListItem("", ""));
        ZWFS.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'LLDK' order by sequence");
        LLDK.DataSource = ds;
        LLDK.DataTextField = "ENUM_NAME";
        LLDK.DataValueField = "ENUM_NAME";
        LLDK.DataBind();
        LLDK.Items.Insert(0, new ListItem("", ""));
        LLDK.SelectedIndex = 0;

        //ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'KHJB' order by sequence");
        //KHJB.DataSource = ds;
        //KHJB.DataTextField = "ENUM_NAME";
        //KHJB.DataValueField = "ENUM_NAME";
        //KHJB.DataBind();
        //KHJB.Items.Insert(0, new ListItem("", ""));
        //KHJB.SelectedIndex = 0;
    }
    private void FillPage()
    {
        if (string.IsNullOrEmpty(YWGUID.Text))
        {
            YWGUID.Text = Guid.NewGuid().ToString();
            BH.Text = GetBH().ToString();
            JD_SUBSCRIBER_GDLY.SelectedValue = "BOSS";
            YD_SUBSCRIBER_GDLY.SelectedValue = "BOSS";
            ZYHS_BJ.Text = "1";
            CREATEDATETIME.Text = DateTime.Now.ToString();
            PZRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_TRANSM_BUSSINESS where YWGUID = '{0}'", YWGUID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
                //if (PZRQ.Text != "") { PZRQ.Text = PZRQ.Text.Substring(0, 10); }
                BindGV();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('您打开的页面没有数据！');</script>");
                BtnSave.Enabled = false;
            }

            FillRmssPage(JD_SUBSCRIBER_ID.Text, "JD_");
            FillRmssPage(YD_SUBSCRIBER_ID.Text, "YD_");
        }
        ChangeSUBSCRIBER_CODE();
        if (ZYHS_BJ.Text == "1")
        {
            BtnZyhs.Text = "资源回收";
        }
        else
        {
            BtnZyhs.Text = "资源恢复";
        }
    }

    private void ChangeSUBSCRIBER_CODE()
    { 
        if (JD_SUBSCRIBER_GDLY.SelectedValue == "BOSS")
        {
            JD_TQ.Visible = true ;
            JD_Select.Visible = true;
        }
        else
        {
            JD_TQ.Visible = false;
            JD_Select.Visible = false;
        }

        if (YD_SUBSCRIBER_GDLY.SelectedValue == "BOSS")
        {
            YD_TQ.Visible = true;
            YD_Select.Visible = true;
        }
        else
        {
            YD_TQ.Visible = false;
            YD_Select.Visible = false;
        }
    }

    private void BindGV()
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_TRANSM_BUSSINESS_CB where YWGUID = '{0}' order by BH", YWGUID.Text));
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvCSYWList.DataSource = ds;
            gvCSYWList.DataBind();
            int count1 = gvCSYWList.Columns.Count;
            gvCSYWList.Rows[0].Cells.Clear();
            gvCSYWList.Rows[0].Cells.Add(new TableCell());
            gvCSYWList.Rows[0].Cells[0].Text = "没有相关的信息！";
            gvCSYWList.Rows[0].Cells[0].ColumnSpan = count1;
            gvCSYWList.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            gvCSYWList.DataSource = ds;
            gvCSYWList.DataBind();
        }
    }
    //保存
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        
        SaveData(false);
        FillPage();
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {

    }
    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string guids = "''";
        foreach (GridViewRow row in gvCSYWList.Rows)
        {
            if (row.RowIndex > -1 && (row.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
            {
                guids += ",'" + gvCSYWList.DataKeys[row.RowIndex]["GUID"].ToString() + "'";
            }
        }
        if (!guids.Equals("''"))
        {
            string sql = string.Format("delete from T_CON_TRANSM_BUSSINESS_CB where GUID in ({0})", guids);
            DataFunction.ExecuteNonQuery(sql);
            BindGV();
        }
        else
        { 
            
        }
    }
    //刷新
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGV();
    }
    protected void gvCSYWList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[1].Text = Convert.ToString(e.Row.RowIndex + 1);
            string guid = gvCSYWList.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            if (!guid.Equals(""))
            {
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }
    private int GetBH()
    {
        int bh = DataFunction.GetIntResult("select BH from T_CON_TRANSM_BUSSINESS order by BH desc");
        if (bh == -1)
        {
            bh = 1;
        }
        else
        {
            ++bh;
        }
        return bh;
    }
    //下拉列表刷新
    protected void Btn1_Click(object sender, EventArgs e)
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

        if (gvCSYWList.Rows.Count >0 && gvCSYWList.Rows[0].Cells[0].Text == "没有相关的信息！")
        {
            int count1 = gvCSYWList.Columns.Count;
            gvCSYWList.Rows[0].Cells.Clear();
            gvCSYWList.Rows[0].Cells.Add(new TableCell());
            gvCSYWList.Rows[0].Cells[0].Text = "没有相关的信息！";
            gvCSYWList.Rows[0].Cells[0].ColumnSpan = count1;
            gvCSYWList.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
    }

    #region 业务单管理
    /// <summary>
    /// 填充业务编码内容
    /// </summary>
    /// <param name="SUBSCRIBER_ID">业务ID</param>
    /// <param name="JYD">甲乙端</param>
    private void FillRmssPage(string str_SUBSCRIBER_ID,string JYD)
    {
        string sql = string.Format("select * from rmss t where t.SUBSCRIBER_ID='{0}'", str_SUBSCRIBER_ID);
        DataRow DR = DataFunction.GetSingleRow(sql);
        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName=JYD+ DC.ColumnName;
            string strValue=DR[DC.ColumnName].ToString(); ;
          System.Web.UI.Control webControl= this.Page.FindControl(ControlName);
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
    private void SaveRmssData(string str_SUBSCRIBER_ID,string str_SUBSCRIBER_CODE,string JYD, string str_SUBSCRIBER_GDLY)
    {
        if (str_SUBSCRIBER_GDLY == "BOSS")
        {return;}

        if (string.IsNullOrEmpty(str_SUBSCRIBER_CODE))
        {return;}

        string sql = string.Format("select * from RMSS_SGD t where t.SUBSCRIBER_ID='{0}'", str_SUBSCRIBER_ID.Replace("SGD_", ""));
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        int idx = 0;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            string sId=GetSubscriberId();
            ((System.Web.UI.WebControls.TextBox)this.Page.FindControl(JYD+"SUBSCRIBER_ID")).Text="SGD_"+sId;
            DR["SUBSCRIBER_ID"] = sId;
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            DR = ds.Tables[0].Rows[0];
            idx = 1;
        }

        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName = JYD  + DC.ColumnName;
            string strValue =null; 
            System.Web.UI.Control webControl = this.Page.FindControl(ControlName);
            if (webControl != null)
            {
                string controlType = webControl.GetType().FullName;
                switch (controlType)
                {
                    case "System.Web.UI.WebControls.TextBox":
                       strValue=((System.Web.UI.WebControls.TextBox)webControl).Text.Replace("SGD_","");
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                       strValue =((System.Web.UI.WebControls.DropDownList)webControl).SelectedValue;
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
        DataFunction.SaveData(ds,"RMSS_SGD");
    }

    private string GetSubscriberId()
    {
        string sql = "select nvl(max(t.subscriber_id),0)+1 as ID from rmss_sgd t";
        return DataFunction.GetStringResult(sql);
    }
    #endregion


    protected void JD_SUBSCRIBER_GDLY_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeSUBSCRIBER_CODE();
       JD_SUBSCRIBER_ID.Text="";
       FillRmssPage(JD_SUBSCRIBER_ID.Text, "JD_");
      
    }
    protected void YD_SUBSCRIBER_GDLY_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeSUBSCRIBER_CODE();
        YD_SUBSCRIBER_ID.Text = "";
        FillRmssPage(YD_SUBSCRIBER_ID.Text, "YD_");
        
    }

    protected void BtnRmssJD_Click(object sender, EventArgs e)
    {
        
        FillRmssPage(JD_SUBSCRIBER_ID.Text, "JD_");

    }

    protected void BtnRmssYD_Click(object sender, EventArgs e)
    {
       
        FillRmssPage(YD_SUBSCRIBER_ID.Text, "YD_");
    }
    protected void JD_TQ_Click(object sender, ImageClickEventArgs e)
    {
        if (JD_SUBSCRIBER_CODE.Text != "")
        {
            
            string sql = "select * from rmss t where 1=1";
            
            sql += " and SUBSCRIBER_CODE  like '%" + JD_SUBSCRIBER_CODE.Text + "%'";
            DataSet dt = DataFunction.FillDataSet(sql);
            if (dt.Tables[0].Rows.Count == 1)
            {
                JD_SUBSCRIBER_ID.Text = dt.Tables[0].Rows[0]["SUBSCRIBER_ID"].ToString();
                FillRmssPage(JD_SUBSCRIBER_ID.Text, "JD_");
            }
            else if(dt.Tables[0].Rows.Count>1)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpenRmssJDTQ()</script>");
            }
            
        }
    }
    protected void YD_TQ_Click(object sender, ImageClickEventArgs e)
    {
        if (YD_SUBSCRIBER_CODE.Text != "")
        {

            string sql = "select * from rmss t where 1=1";

            sql += " and SUBSCRIBER_CODE like '%" + YD_SUBSCRIBER_CODE.Text + "%'";
            DataSet dt = DataFunction.FillDataSet(sql);
            if (dt.Tables[0].Rows.Count == 1)
            {
                YD_SUBSCRIBER_ID.Text = dt.Tables[0].Rows[0]["SUBSCRIBER_ID"].ToString();
                FillRmssPage(YD_SUBSCRIBER_ID.Text, "YD_");
            }
            else if (dt.Tables[0].Rows.Count > 1)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpenRmssYDTQ()</script>");
            }

        }
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
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }

    private void SaveData(bool isZyhs)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_TRANSM_BUSSINESS where YWGUID = '{0}'", YWGUID.Text));
        string strTitle = "修改传输资源配置";
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            strTitle = "新增传输资源配置";
            string strCodes="('" + JD_SUBSCRIBER_CODE.Text + "','" + YD_SUBSCRIBER_CODE.Text +"')";
            string sqlStr = "select * from T_CON_TRANSM_BUSSINESS t where t.jd_subscriber_code in " + strCodes + " or t.yd_subscriber_code in " + strCodes;
            if (DataFunction.HasRecord(sqlStr))
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('该业务编码已配置，请选择其它业务编码');</script>");
                return;
            }
        }
        SaveRmssData(JD_SUBSCRIBER_ID.Text, JD_SUBSCRIBER_CODE.Text, "JD_", JD_SUBSCRIBER_GDLY.SelectedValue);
        SaveRmssData(YD_SUBSCRIBER_ID.Text, YD_SUBSCRIBER_CODE.Text, "YD_", YD_SUBSCRIBER_GDLY.SelectedValue);
        DataRow DR = ds.Tables[0].Rows[0];
      

        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        string strComment = ShareFunction.GetControlData(Page, DR, "T_CON_TRANSM_BUSSINESS");
        DataFunction.SaveData(ds, "T_CON_TRANSM_BUSSINESS");
        ShareFunction.InsertLog(this.Page, YWGUID.Text, strTitle, strComment);
    }

    private void UpdateChildPortZt()
    {
        string sql="";
    }

    private void SetResChildPortZt(string dkGuid, string dkzt)
    {
        string sql = string.Format("update t_res_child_port t set t.dkzt='{1}' where t.guid in ('{0}') ",
            dkGuid.Replace(",", "','"), dkzt);
        DataFunction.ExecuteNonQuery(sql);
    }
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gvCSYWList.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gr.FindControl("XZ1");
            if (ch.Checked)
            {
                //gvCSYWList.Rows[gr.RowIndex].Cells[]
                string YDSBDK_GUID = gvCSYWList.DataKeys[gr.RowIndex][2].ToString();
                string JDSBDK_GUID = gvCSYWList.DataKeys[gr.RowIndex][1].ToString();
                shareResource.SetResourcePort(YDSBDK_GUID + "," + JDSBDK_GUID);
                string guid = gvCSYWList.DataKeys[gr.RowIndex][0].ToString();
                string sql = "delete from T_CON_TRANSM_BUSSINESS_CB where GUID='" + guid + "'";
                DataFunction.ExecuteNonQuery(sql);
            }
        }
        BindGV(); 
    }

    //判断业务编号是否有重复
    [Ajax.AjaxMethod()]
    public int getKhBhRow(string bh)
    {
        int idx = 0;
        idx  = Convert.ToInt32(DataFunction.GetStringResult("select count(*) from RMSS_SGD t where t.subscriber_code='" + bh + "'"));
        return idx;
    }

    #region 导出Excel
    protected void BtnExp_Click(object sender, EventArgs e)
    {
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
        Workbook book = new Workbook();
        book.Open(Server.MapPath("../../template/传输业务配置模板.xls"));
        Worksheet ws = book.Worksheets[0];

        DataTable dt = DataFunction.FillDataSet(@"select ZWFS B1,LLDK D1,PZRQ F1,PZR H1,VLANID B2,XGJL D2,DSFYYS B3,LXR D3,LXDH F3,case when JD_SUBSCRIBER_GDLY='SGD' then '手工单' else JD_SUBSCRIBER_GDLY end D5,
                        case when JD_SUBSCRIBER_GDLY='SGD' then '手工单' else JD_SUBSCRIBER_GDLY end H5 from T_CON_TRANSM_BUSSINESS  where ywguid='" + YWGUID.Text + "'").Tables[0];
       
        if (dt.IsNullOrEmpty())
        {
            this.Alert("找不到记录，可能没有保存或已删除!");
            return;
        }
        string jd = DataFunction.GetStringResult("select JD_SUBSCRIBER_ID JD from T_CON_TRANSM_BUSSINESS  where ywguid='" + YWGUID.Text + "'");
        string yd = DataFunction.GetStringResult("select YD_SUBSCRIBER_ID YD from T_CON_TRANSM_BUSSINESS  where ywguid='" + YWGUID.Text + "'");

        DataTable jdDt = DataFunction.FillDataSet(@"select jd.SUBSCRIBER_CODE B5,jd.LINKMAN B6,jd.REGION D6,
        jd.SUB_NAME B7,jd.CUSTOMER_CODE B8,jd.CUSTOMER_LEVEL ||'级' D8,
        jd.CUSTTYPE1 B9,jd.CUSTTYPE1 D9,jd.CUSTOMER_NAME B10,
        jd.ADDRESS B11,jd.EMAIL B12,jd.PHONE_NO D12,jd.MOBILE_NO B13,jd.fax_no D13,jd.ZIP_CODE B14,jd.SALE_NAME D14 from rmss jd where subscriber_id='" + jd + "'").Tables[0];

        DataTable ydDt = DataFunction.FillDataSet(@"select jd.SUBSCRIBER_CODE F5,jd.LINKMAN F6,jd.REGION H6,
        jd.SUB_NAME F7,jd.CUSTOMER_CODE F8,jd.CUSTOMER_LEVEL ||'级' H8,
        jd.CUSTTYPE1 F9,jd.CUSTTYPE1 H9,jd.CUSTOMER_NAME F10,
        jd.ADDRESS F11,jd.EMAIL F12,jd.PHONE_NO H12,jd.MOBILE_NO F13,jd.fax_no H13,jd.ZIP_CODE F14,jd.SALE_NAME H14 from rmss jd where subscriber_id='" + yd + @"'").Tables[0];
        
  
        //导入基本数据
        dt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            dt.Columns.Cast<DataColumn>().ForEach(cl =>
            {
                ws.Cells[cl.ColumnName].PutValue(Convert.ToString(dr[cl]));
            });
        });

 
        jdDt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            jdDt.Columns.Cast<DataColumn>().ForEach(cl =>
            {
                ws.Cells[cl.ColumnName].PutValue(Convert.ToString(dr[cl]));
            });
        });

        ydDt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            ydDt.Columns.Cast<DataColumn>().ForEach(cl =>
            {
                ws.Cells[cl.ColumnName].PutValue(Convert.ToString(dr[cl]));
            });
        });



        dt = DataFunction.FillDataSet(@"select rownum A17,case llfx when -1 then  '甲端←乙端' when 1 then '甲端→乙端' when 0 then '甲端～乙端' else '' end 
                    ,JDJRJF B17,JDJRSB C17,JDSBDK D17,JDJRWLSX E17,YDJRJF F17,YDJRSB G17,YDSBDK H17,YDJRWLSX I17  from T_CON_TRANSM_BUSSINESS_CB cb where ywguid='" + YWGUID.Text + "'").Tables[0];
        
        int row = 16;
        dt.Rows.Cast<DataRow>().ForEach(dr =>
        {
            int idx = 0;
            dt.Columns.Cast<DataColumn>().ForEach(cl =>
            {
                ws.Cells[row, idx].PutValue(Convert.ToString(dr[cl]));
                ws.Cells[row, idx].Style.Borders.SetStyle(CellBorderType.Hair);
                ws.Cells[row, idx].Style.Borders.DiagonalStyle = CellBorderType.None;
                ws.Cells[row, idx].Style.Font.Size = 9;
                ++idx;
            });
            ++row;
            idx = 0;
        });

        
        MemoryStream ms = new MemoryStream();

        book.Save(ms, FileFormatType.Excel2003);

        IDP.Common.WebUtils.ResponseWriteBinary(ms.ToArray(), "传输资源配置.xls");

    }
    #endregion
}

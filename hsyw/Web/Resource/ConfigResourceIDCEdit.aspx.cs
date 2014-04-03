using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigResourceIDCEdit : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SetContrlReadonly();
            gvLogicEquIp.Attributes.Add("BorderColor", "#5B9ED1");
            GUID.Text = Request.QueryString["GUID"];
            if (GUID.Text == "")
            {
                ISNEW.Text = "1";
            }
            ShareFunction.BindEnumDropList(DK, "DK");
            ShareFunction.BindEnumDropList(YWLX, "IDC_YWLX");
            FillPage();
        }
    }

    private void SetContrlReadonly()
    {
        //ONUMAC.Attributes.Add("readonly", "true");
        //VLAN.Attributes.Add("readonly", "true");
        //LJJF.Attributes.Add("readonly", "true");
        //HTVPN_CODE.Attributes.Add("readonly", "true");
        //HTVPN.Attributes.Add("readonly", "true");
        WLJF.Attributes.Add("readonly", "true");
        SBMC.Attributes.Add("readonly", "true");
        JRDK.Attributes.Add("readonly", "true");

        
    }

    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, GetT_CON_LOGIC_EQU_IDCData());
        //设备名称或者编号修改后，修改设备配置信息
        //FillSB();
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        BindgvLogicEquIp();
        BindgvLogicEquVlan();
        //InitPageContrl();
        if (ZYHS_BJ.Text == "1")
        {
            BtnZyhs.Text = "资源回收";
        }
        else
        {
            BtnZyhs.Text = "资源恢复";
        }
    }
    //private void InitPageContrl()
    //{
    //    //tr_DK.Style.Add("display", "none");
    //    //tr_JF.Style.Add("display", "none");
    //    tr_DDXX.Style.Add("display", "none");
    //    Btntr.Style.Add("display", "block");
    //    //YHZYIP.ReadOnly = false;
    //    //YHZYIP.BackColor = System.Drawing.Color.White;
    //    //YHLYWD.ReadOnly = false;
    //    //YHLYWD.BackColor = System.Drawing.Color.White;
    //    //HTVPN_CODE.ReadOnly = false;
    //    //HTVPN_CODE.BackColor = System.Drawing.Color.White;
    //    SBMC.ReadOnly = false;
    //    SBMC.BackColor = System.Drawing.Color.White;
    //    td_VPN.Attributes.Add("display", "block");
    //    td_SBMC.Attributes.Add("display", "block");
        
    //}
    #region 保存操作
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        SaveData(false);
        getDEVCODE();
    }

    private void getDEVCODE()
    {
        //WLJF_JG_U1U2_0
        if(WLJF.Text!=""&&JG.Text!=""&&U1.Text!=""&&U2.Text!="")
        {
            DEV_CODE.Text = WLJF.Text + "" + JG.Text + "" + Convert.ToInt32(U1.Text).ToString("00") + Convert.ToInt32(U2.Text).ToString("00") + "_0";
        }
    }

    private void SaveData(bool isZyhs)
    {

        if (string.IsNullOrEmpty(YWLX.SelectedValue))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务类型不能为空！');</script>");
            return;
        }

        if (ISNEW.Text == "1" && SUBSCRIBER_CODE.Text != "")
        {
            string sql = "select * from T_CON_LOGIC_EQU_IP t where t.subscriber_code='" + SUBSCRIBER_CODE.Text + "'";
            if (DataFunction.HasRecord(sql))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务编码不能重复！请重新选择');</script>");
                return;
            }
        }

        if(U1.Text=="" || U2.Text=="")
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('U编号不全！');</script>");
            return;
        }

        if(Convert.ToInt32(U1.Text) >Convert.ToInt32(U2.Text))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('U编号范围错误！');</script>");
            return;
        }
        DataRow dr = GetT_CON_LOGIC_EQU_IDCData();
        string strTitle = "修改IDC资源配置";
        if (dr.RowState == DataRowState.Added)
        {
            strTitle = "新增IDC资源配置";
        }
        if (isZyhs)
        {
            if (ZYHS_BJ.Text == "0")
            { strTitle = "回收IDC资源配置"; }
            else
            { strTitle = "恢复IDC资源配置"; }
        }

        //by hangyt@8.15
        if (!string.IsNullOrEmpty(SBMC_CODE.Text) && !string.IsNullOrEmpty(JRDK.Text))
        {
            //SBPZXX.Text = "设备名称：" + SBMC.Text + "，" + "接入端口：" + JRDK.Text;
            //if (SBPZXX.Text.Trim() == string.Empty)
            //{
            //    SBPZXX.Text = SBMC.Text + "." + JRDK.Text.Replace(',','.');
            //}
            SBPZXX.Text = SBMC_CODE.Text + "." + JRDK.Text.Replace(',', '.');
        }

        string strPortGuid = dr["JRDK_GUID"].ToString() + "," + JRDK_GUID.Text;

        //UPDATEDATETIME.Text = DateTime.Now.ToString();
        //GetPvcid();

        ////工单来源：手工单，业务编码为空，更具业务类型生成业务编码
        //if (SUBSCRIBER_GDLY.SelectedValue == "SGD" && SUBSCRIBER_CODE.Text == "")
        //{
        //    string strywbm = "";
        //    //更具业务类型生成业务编码
        //    switch (YWLX.Text)
        //    {
        //        case "PB":
        //        case "XPB":
        //            strywbm = "Z_XZL_";
        //            break;
        //        case "PPPOE":
        //        case "HDTV":
        //        case "WIRELESS":
        //        case "VOIP":
        //        case "MPLS/L3VPN":
        //        case "INTERNET":
        //        case "VPDN":
        //        case "SSLVPN":
        //            strywbm = "Z_P_";
        //            break;
        //        case "IDC托管":
        //            strywbm = "Z_IDC_";
        //            break;
        //        case "MPLS/L2VPN":
        //        case "LAN/L2VPN":
        //            strywbm = "Z_SL2_";
        //            break;
        //        case "VPN_GAJK":
        //            strywbm = "Z_GAJK_";
        //            break;
        //        case "IPCAM":
        //        case "DVS":
        //        case "DVR":
        //            strywbm = "Z_SPJK_";
        //            break;
        //        default:
        //            break;

        //    }

        //    if (strywbm != "")
        //    {
        //        string strSql = string.Format(@"select nvl(substr(max(t.SUBSCRIBER_CODE),LENGTH(max(t.SUBSCRIBER_CODE))-3),0)+1 as num from RMSS_SGD t where t.SUBSCRIBER_CODE like '{0}%'", strywbm);
        //        int strNum = DataFunction.GetIntResult(strSql);
        //        SUBSCRIBER_CODE.Text = strywbm + strNum.ToString("0000");
        //    }
        //}

        SaveRmssData(SUBSCRIBER_ID.Text, SUBSCRIBER_CODE.Text, "", SUBSCRIBER_GDLY.SelectedValue);
        string strComment = ShareFunction.GetControlData(this.Page, dr, "T_CON_LOGIC_IDC");
        ShareFunction.InsertLog(this.Page, GUID.Text, strTitle, strComment);
        DataFunction.SaveData(dr.Table.DataSet, "T_CON_LOGIC_IDC");
        shareResource.SetResourcePort(strPortGuid);
        FillPage();
        //修改端口复用
        hsdk();
    }
    #endregion

    private DataRow GetT_CON_LOGIC_EQU_IDCData()
    {
        string sql = "select * from T_CON_LOGIC_IDC where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["ZYHS_BJ"] = "1";
            //dr["CREATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        return dr;
    }

    private void BindgvLogicEquIp()
    {
        string sql = @"select t.guid,t.ipdz,t.pz_ipywlx,ip.wljf from t_logic_equ_ip_pz t 
                               left join t_con_logic_idc ip on t.pk_guid=ip.guid
                               where t.pk_guid='" + GUID.Text + "' order by t.ip1,t.ip2,t.ip3,t.ip4,t.ipfd ";
        DataSet ds = DataFunction.FillDataSet(sql);
        gvLogicEquIp.DataSource = ds;
        gvLogicEquIp.DataBind();

    }

    private void BindgvLogicEquVlan()
    {
        //以前写的SQL 修改如下
        //string sql = string.Format("select * from t_con_logic_equ_vlan t where t.pk_guid='{0}' order by t.vlanbh ",GUID.Text);
        //所属机房因为逻辑机房
        string sql = string.Format(@"select t.guid,t.vlanbh,vl.wljf,vl.ljjf,t.* from t_con_logic_equ_vlan t
              left join t_con_logic_idc vl on t.pk_guid=vl.guid  where t.pk_guid='{0}'  order by t.vlanbh", GUID.Text);

        DataSet ds = DataFunction.FillDataSet(sql);
        gvLogicEquVlan.DataSource = ds;
        gvLogicEquVlan.DataBind();
    }

    protected void BtnZyhs_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExp_Click(object sender, EventArgs e)
    {

    }
    protected void deleteButton_Click(object sender, EventArgs e)
    {
        string[] ipdzs = new string[gvLogicEquIp.Rows.Count * 2];
        int i = 0;
        foreach (GridViewRow gr in gvLogicEquIp.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gr.FindControl("XZ");
            if (ch.Checked)
            {

                string guid = gvLogicEquIp.DataKeys[gr.RowIndex].Value.ToString();
                string strSql = "select t.ipdz1,t.ipdz2 from t_logic_equ_ip_pz t where guid='" + guid + "'";
                DataRow dr = DataFunction.GetSingleRow(strSql);
                ipdzs[i] = dr["ipdz1"].ToString();
                ipdzs[i + 1] = dr["ipdz2"].ToString();
                string sql = "delete from t_logic_equ_ip_pz where GUID='" + guid + "'";
                DataFunction.ExecuteNonQuery(sql);
                i++;
            }
        }
        BindgvLogicEquIp();
        shareResource.updateIP();
    }
    protected void deleteVlanButton_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gvLogicEquVlan.Rows)
        {
            System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gr.FindControl("XZ1");
            if (ch.Checked)
            {
                //9.5 dsh
                string guid = gvLogicEquVlan.DataKeys[gr.RowIndex][0].ToString();
                string vlanguid = gvLogicEquVlan.DataKeys[gr.RowIndex][1].ToString();
                string sql = "delete from T_CON_LOGIC_EQU_VLAN where GUID='" + guid + "'";
                DataFunction.ExecuteNonQuery(sql);

                //by hangyt 8.15
                string vlanbh = gr.Cells[2].Text;
                string vlanjf = gr.Cells[1].Text;
                // 9.5 dsh
                sql = "update t_logic_equ_vlan set zyzt ='空闲' where vlanbh='" + vlanbh + "' and ssjf='" + vlanjf + "'";
                DataFunction.ExecuteNonQuery(sql);
                //

            }
        }
        BindgvLogicEquVlan();
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindgvLogicEquIp();
    }
    protected void BtnDk_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(DK, "DK");
    }
    protected void BtnVlan_Click(object sender, EventArgs e)
    {
        BindgvLogicEquVlan();
    }
    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");
    }
    protected void BtnView_Click(object sender, EventArgs e)
    {

    }
    protected void YWLX_SelectedIndexChanged(object sender, EventArgs e)
    {

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



    protected void BtnYwfl_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(YWLX, "IDC_YWLX");
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
            //DR["CUSTOMER_CODE"] = str_SUBSCRIBER_CODE;
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

    #region 回收资源
    /// <summary>
    /// 回收端口
    /// </summary>
    private void hsdk()
    {
        string strSql = "select count(*) from T_CON_LOGIC_EQU_IP t where t.zyhs_bj<>0 and jrdk_guid like '%{0}%'";
        //保存时回收历史端口
        if (oldJRDK_GUID.Text != "" && oldJRDK_GUID.Text != JRDK_GUID.Text)
        {
            JRDK_GUID.Text += "," + oldJRDK_GUID.Text;
            oldJRDK_GUID.Text = "";
        }
        if (JRDK_GUID.Text.IsNullOrEmpty())
        {
            return;
        }
        string[] dkguids = JRDK_GUID.Text.Split(',');
        int dkzynum = 0;
        string strUpdate = "";
        foreach (string dkid in dkguids)
        {
            strSql = string.Format(strSql, dkid);
            dkzynum = DataFunction.GetIntResult(strSql);
            if (dkzynum == 0)
            {
                strUpdate = " DKZT='未启用' ";
            }
            else if (dkzynum == 1)
            {
                strUpdate = " DKZT='启用',sfkfy=0 ";
            }
            else
            {
                strUpdate = " DKZT='启用',sfkfy=1 ";
            }

            if (strUpdate != "")
            {
                DataFunction.ExecuteNonQuery(string.Format(@"update T_RES_CHILD_PORT set {0} where guid='{1}' ", strUpdate, dkid));
            }
            strUpdate = "";
        }
    }

    /// <summary>
    /// 回收Vlan
    /// </summary>
    private void hsVlan()
    {
        foreach (GridViewRow gr in gvLogicEquVlan.Rows)
        {
            //是否回收
            string guid = gvLogicEquVlan.DataKeys[gr.RowIndex][0].ToString();
            string vlanguid = gvLogicEquVlan.DataKeys[gr.RowIndex][1].ToString();
            string vlanbh = gr.Cells[2].Text;
            string vlanjf = gr.Cells[1].Text;
            string sql = "update  T_CON_LOGIC_EQU_VLAN set SFHS='1' where GUID='" + guid + "'";
            DataFunction.ExecuteNonQuery(sql);

            //根据Vlan占用情况，修改Vlan状态
            sql = string.Format(@"select * from T_CON_LOGIC_EQU_VLAN t where VLANGUID='{0}' and  (SFHS <> '1' or SFHS is null)", vlanguid);
            if (!DataFunction.HasRecord(sql))
            {
                sql = "update t_logic_equ_vlan set zyzt ='空闲' where vlanbh='" + vlanbh + "' and ssjf='" + vlanjf + "'";
                DataFunction.ExecuteNonQuery(sql);
            }
        }
    }

    /// <summary>
    /// 回收IP
    /// </summary>
    private void hsIPDZ()
    {
        string guids = "";
        foreach (GridViewRow gr in gvLogicEquIp.Rows)
        {
            guids += "'" + gvLogicEquIp.DataKeys[gr.RowIndex].Value.ToString() + "',";
        }
        if (guids != "")
        {
            guids = guids.Substring(0, guids.Length - 1);
            string sql = "update  t_logic_equ_ip_pz set sfhs='1' where GUID in (" + guids + ")";
            DataFunction.ExecuteNonQuery(sql);
        }
    }
    #endregion


}

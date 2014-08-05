using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.IO;
public partial class Web_Resource_ConfigResourceIpEdit : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SetContrlReadonly();
            gvLogicEquIp.Attributes.Add("BorderColor", "#5B9ED1");
            GUID.Text = Request.QueryString["GUID"];
            if(GUID.Text=="")
            {
                ISNEW.Text = "1";
            }
            ShareFunction.BindEnumDropList(DK, "DK");
            ShareFunction.BindEnumDropList(YWLX, "IP_YWLX");
            FillPage();
        }
    }
    private void SetContrlReadonly()
    {
        //ONUMAC.Attributes.Add("readonly", "true");
        //VLAN.Attributes.Add("readonly", "true");
        WLJF.Attributes.Add("readonly", "true");
        LJJF.Attributes.Add("readonly", "true");
        HTVPN_CODE.Attributes.Add("readonly", "true");
        HTVPN.Attributes.Add("readonly", "true");
        SBMC.Attributes.Add("readonly", "true");
        JRDK.Attributes.Add("readonly", "true");

        DDWLJF.Attributes.Add("readonly", "true");
        DDLJJF.Attributes.Add("readonly", "true");
        DDSBMC.Attributes.Add("readonly", "true");
        DDSBDK.Attributes.Add("readonly", "true");
        DDVLAN.Attributes.Add("readonly", "true");

        PVCID.Attributes.Add("onchange", "checkTxt(this);");
        PVCID.Attributes.Add("onKeyPress", "return limitNum(this);");
    }

    #region 初始化界面
    private void BindgvLogicEquIp()
    {
        string sql = @"select t.guid,t.ipdz,t.pz_ipywlx,ip.wljf from t_logic_equ_ip_pz t 
                               left join t_con_logic_equ_ip ip on t.pk_guid=ip.guid
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
        //string sql = string.Format(@"select t.guid,t.vlanbh,vl.wljf,vl.ljjf,t.* from t_con_logic_equ_vlan t
        //      left join t_con_logic_equ_ip vl on t.pk_guid=vl.guid  where t.pk_guid='{0}'  order by t.vlanbh", GUID.Text);

        string sql = string.Format(@"select t.guid,t.vlanbh,vl.wljf,case when t.vlanguid is null then  vl.ljjf else v.ssjf end as ljjf,t.* from t_con_logic_equ_vlan t
              left join t_con_logic_equ_ip vl on t.pk_guid=vl.guid left join t_logic_equ_vlan v on v.guid=t.vlanguid where t.pk_guid='{0}'  order by t.vlanbh", GUID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        gvLogicEquVlan.DataSource = ds;
        gvLogicEquVlan.DataBind();
    }
    private DataRow GetT_CON_LOGIC_EQU_IPData()
    {
        string sql = "select * from T_CON_LOGIC_EQU_IP where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["ZYHS_BJ"] = "1";
            dr["CREATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        return dr;
    }

    private DataRow GetRMSS_SGDDataRow()
    {
        string sql = "select t.* from rmss_sgd t where SUBSCRIBER_ID = '" + SUBSCRIBER_ID.Text.Replace("SGD_", "") + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["CREATTIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        return dr;
    }

    private DataRow GetRMSSDataRow()
    {
        string sql = "select t.* from rmss t where SUBSCRIBER_ID = '" + SUBSCRIBER_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count != 0)
        {
            dr = ds.Tables[0].Rows[0];
            dr["SUBSCRIBER_ID"] = SUBSCRIBER_ID.Text;
        }

        return dr;
    }

    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, GetT_CON_LOGIC_EQU_IPData());
        //设备名称或者编号修改后，修改设备配置信息
        FillSB();
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        BindgvLogicEquIp();
        BindgvLogicEquVlan();
        InitPageContrl();
        BtnJRDK_Click(null,null);
        if (ZYHS_BJ.Text == "1")
        {
            BtnZyhs.Text = "资源回收";
            JXXZButton.Style.Add("display","block");
        }
        else
        {
            BtnZyhs.Text = "资源恢复";
            JXXZButton.Style.Add("display", "none");
        }

    }

    private void FillSB()
    {
        string sbSql = "select t.equ_code,t.equ_name from T_RES_EQU_NET t where t.guid='" + SBMC_GUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sbSql);
        if (dr["equ_code"].ToString() != SBMC_CODE.Text || dr["equ_name"].ToString() != SBMC.Text)
        {
            SBMC.Text = dr["equ_name"].ToString();
            SBMC_CODE.Text = dr["equ_code"].ToString();
            SaveData(false);
        }
    }
    #endregion

    #region 保存操作
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        SaveData(false);
    }

    private void SaveData(bool isZyhs)
    {
        
        if (string.IsNullOrEmpty(YWLX.SelectedValue))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务类型不能为空！');</script>");
            return;
        }

        if (ISNEW.Text=="1"&&SUBSCRIBER_CODE.Text!="")
        {
            string sql = "select * from T_CON_LOGIC_EQU_IP t where t.subscriber_code='" + SUBSCRIBER_CODE.Text + "'";
            if(DataFunction.HasRecord(sql))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务编码不能重复！请重新选择');</script>");
                return;
            }
        }

        DataRow dr = GetT_CON_LOGIC_EQU_IPData();
        //更新虚拟端口ONU状态
        if (dr["temp_xndk"] == DBNull.Value || dr["temp_xndk"].ToString()=="")
        {
            if(temp_xndk.Text!="")
            {
                updatexndk(temp_xndk.Text,0);
            }
        }
        else
        {
            if(dr["temp_xndk"].ToString()!=temp_xndk.Text)
            {
                updatexndk(temp_xndk.Text,0);
                updatexndk(dr["temp_xndk"].ToString(),1);
            }
             
        }
        string strTitle = "修改IP资源配置";
        if (dr.RowState == DataRowState.Added)
        {
            strTitle = "新增IP资源配置";
        }
        if (isZyhs)
        {
            if (ZYHS_BJ.Text == "0")
            { strTitle = "回收IP资源配置"; }
            else
            { strTitle = "恢复IP资源配置"; }
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
        //
        string strPortGuid = dr["JRDK_GUID"].ToString() + "," + JRDK_GUID.Text;

        UPDATEDATETIME.Text = DateTime.Now.ToString();
        GetPvcid();

        //工单来源：手工单，业务编码为空，更具业务类型生成业务编码
        if (SUBSCRIBER_GDLY.SelectedValue == "SGD" && SUBSCRIBER_CODE.Text == "")
        {
            string strywbm = "";
            //更具业务类型生成业务编码
            switch (YWLX.Text)
            {
                case "PB":
                case "XPB":
                    strywbm = "Z_XZL_";
                    break;
                case "PPPOE":
                case "HDTV":
                case "WIRELESS":
                case "VOIP":
                case "MPLS/L3VPN":
                case "INTERNET":
                case "VPDN":
                case "SSLVPN":
                    strywbm = "Z_P_";
                    break;
                case "IDC托管":
                    strywbm = "Z_IDC_";
                    break;
                case "MPLS/L2VPN":
                case "LAN/L2VPN":
                    strywbm = "Z_SL2_";
                    break;
                case "VPN_GAJK":
                    strywbm = "Z_GAJK_";
                    break;
                case "IPCAM":
                case "DVS":
                case "DVR":
                    strywbm = "Z_SPJK_";
                    break;
                default:
                    break;

            }

            if (strywbm !="" )
            {
                string strSql = string.Format(@"select nvl(substr(max(t.SUBSCRIBER_CODE),LENGTH(max(t.SUBSCRIBER_CODE))-3),0)+1 as num from RMSS_SGD t where t.SUBSCRIBER_CODE like '{0}%'", strywbm);
                int strNum = DataFunction.GetIntResult(strSql);
                SUBSCRIBER_CODE.Text = strywbm + strNum.ToString("0000");
            }
        }
       

        SaveRmssData(SUBSCRIBER_ID.Text, SUBSCRIBER_CODE.Text, "", SUBSCRIBER_GDLY.SelectedValue);
        string strComment = ShareFunction.GetControlData(this.Page, dr, "T_CON_LOGIC_EQU_IP");
        ShareFunction.InsertLog(this.Page, GUID.Text, strTitle, strComment);
        DataFunction.SaveData(dr.Table.DataSet, "T_CON_LOGIC_EQU_IP");
        shareResource.SetResourcePort(strPortGuid);
        FillPage();
        //修改端口复用
        hsdk();
    }
    private void updatexndk(string sndk,int zt)
    {
        if(sndk!="")
        {
            string [] dk=sndk.Split('-');
            DataFunction.ExecuteNonQuery(string.Format(@"update T_RES_EQU_OLT_PORT t set onu"+dk[1]+"={2} where t.port_guid='{0}' and t.virtualport='{1}'",JRDK_GUID.Text,dk[0],zt));
        }
    }

    #endregion

    private void GetPvcid()
    {
        if (Getywfl() == "MPLS/L2VPN")
        {
            if (string.IsNullOrEmpty(PVCID.Text))
            {
                string sql = "select nvl(max(t.pvcid),0)+1 as pvcid from t_con_logic_equ_ip t where t.ywlx='MPLS/L2VPN'";
                string strPvcid = DataFunction.GetStringResult(sql);
                if (strPvcid.Trim() == "1")
                {
                    sql = "select zdz from t_sys_data t where t.datadm='PVCID'";
                    strPvcid = DataFunction.GetStringResult(sql);
                }
                PVCID.Text = strPvcid;
            }
        }
        else
        {
            PVCID.Text = "";
        }
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindgvLogicEquIp();
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        string[] ipdzs = new string[gvLogicEquIp.Rows.Count*2];
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
                ipdzs[i+1] = dr["ipdz2"].ToString();
                string sql = "delete from t_logic_equ_ip_pz where GUID='" + guid + "'";
                DataFunction.ExecuteNonQuery(sql);
                i++;
            }
        }
        BindgvLogicEquIp();
        shareResource.updateIP();
        //for (int j = 0; j < ipdzs.Length; j=j+2 )
        //{
        //    if (ipdzs[j].IsNullOrEmpty())
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        UpdateIPZT(ipdzs[j], ipdzs[j+1]);
        //    }
        //}
        
    }

//    private void UpdateIPZT(string ipdz1,string ipdz2)
//    {
//        string strSql = string.Format(@"select case when p.cn=t.ipdz2-t.ipdz1 then '已分配'  when p.cn>0 then '部分分配' else '未分配' end as fpqk ,
//                                    p.cn, t.*,t.rowid from t_logic_equ_ip t  join
//                                    (select a.guid,sum({1}-{0}) as cn from t_logic_equ_ip a
//                                    where a.ipdz1<={0} and a.ipdz2>={1}  group by a.guid) p on  t.guid=p.guid", ipdz1, ipdz2);
//        DataRow ipdr = DataFunction.GetSingleRow(strSql);
//        DataFunction.ExecuteNonQuery("update t_logic_equ_ip set ipfpzt='" + Convert.ToString(ipdr["fpqk"]) + "' where guid='" + Convert.ToString(ipdr["GUID"]) + "'");
//    }
    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");

    }
    protected void BtnDk_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(DK, "DK");
    }

    protected void BtnVlan_Click(object sender, EventArgs e)
    {
        BindgvLogicEquVlan();
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
    protected void SUBSCRIBER_GDLY_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeSUBSCRIBER_CODE();
        SUBSCRIBER_ID.Text = "";
        FillRmssPage(SUBSCRIBER_ID.Text, "");
    }
    protected void BtnZyhs_Click(object sender, EventArgs e)
    {
        if (ZYHS_BJ.Text == "1")
        {
            ZYHS_BJ.Text = "0";
            //回收IP
            hsIPDZ();
            //回收Vlan
            hsVlan();
        }
        else //恢复资源
        {
            string dkGuid = "";
            string VlanGuids = "";
            string IpGuids = "";
            string VlanGuid = "";
            //接入端口
            string dkSql = "select count(*) from T_CON_LOGIC_EQU_IP t where t.zyhs_bj<>0 and jrdk_guid like '%{0}%'";
            string[] dkguids = JRDK_GUID.Text.Split(',');
            int dkzynum = 0;
            foreach (string dkid in dkguids)
            {
                dkSql = string.Format(dkSql, dkid);
                dkzynum = DataFunction.GetIntResult(dkSql);
                if (dkzynum > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('端口已被占用，资源恢复失败！')</script>");
                    return;
                }
                dkGuid += "'" + dkid + "',";
            }

            //Vlan
            foreach (GridViewRow gr in gvLogicEquVlan.Rows)
            {
                //根据Vlan占用情况，修改Vlan状态
                string vguid = gvLogicEquVlan.DataKeys[gr.RowIndex][0].ToString();
                string vlanguid = gvLogicEquVlan.DataKeys[gr.RowIndex][1].ToString();
                string vlanSql = string.Format(@"select * from T_CON_LOGIC_EQU_VLAN t where VLANGUID='{0}' and  (SFHS <> '1' or SFHS is null)", vlanguid);
                if (DataFunction.HasRecord(vlanSql))
                {
                    ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('Vlan已被占用，资源恢复失败！')</script>");
                    return;
                }
                VlanGuids += "'" + vlanguid + "',";
                VlanGuid += "'" + vguid + "',";
            }

            //IP
            foreach (GridViewRow gr in gvLogicEquIp.Rows)
            {
                string ipdz = gr.Cells[3].Text;
                string ipSql = "select * from t_logic_equ_ip_pz where ipdz='" + ipdz + "' and (SFHS <> '1' or sfhs is null)";
                if (DataFunction.HasRecord(ipSql))
                {
                    ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('Ip已被占用，资源恢复失败！')</script>");
                    return;
                }
                IpGuids += "'" + gvLogicEquIp.DataKeys[gr.RowIndex].Value.ToString() + "',";

            }
            
            //恢复资源
            string strSql = "";
            //if (dkGuid!="")
            //{
            //    dkGuid = dkGuid.Substring(0,dkGuid.Length - 1);
            //    strSql = "update T_RES_CHILD_PORT set  DKZT='启用' where guid in (" + dkGuid + ")";
            //    DataFunction.ExecuteNonQuery(strSql);
            //}

            if (VlanGuids!="")
            {
                VlanGuids = VlanGuids.Substring(0, VlanGuids.Length - 1);
                VlanGuid = VlanGuid.Substring(0, VlanGuid.Length - 1);
                strSql = "update t_logic_equ_vlan set zyzt = '占用' where guid in (" + VlanGuids + ")";
                DataFunction.ExecuteNonQuery(strSql);
                strSql = "update T_CON_LOGIC_EQU_VLAN set sfhs='0' where guid in (" + VlanGuid + ")";
                DataFunction.ExecuteNonQuery(strSql);
            }

            if (IpGuids!="")
            {
                IpGuids = IpGuids.Substring(0, IpGuids.Length - 1);
                strSql = "update t_logic_equ_ip_pz set sfhs = '0' where guid in (" + IpGuids + ")";
                DataFunction.ExecuteNonQuery(strSql);
            }
            ZYHS_BJ.Text = "1";
        }
        SaveData(true);
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }

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
        if(JRDK_GUID.Text.IsNullOrEmpty())
        {
            return ;
        }
        string[] dkguids = JRDK_GUID.Text.Split(',');
        int dkzynum = 0;
        string strUpdate="";
        foreach (string dkid in dkguids)
        {
            strSql=string.Format(strSql, dkid);
            dkzynum = DataFunction.GetIntResult(strSql);
            if(dkzynum==0)
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

            if (strUpdate!="")
            {
                DataFunction.ExecuteNonQuery(string.Format(@"update T_RES_CHILD_PORT set {0} where guid='{1}' ",strUpdate,dkid));
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

    protected void YWLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitPageContrl();
    }

    private string Getywfl()
    {
        string sql = string.Format(@"select t.ywfl from t_res_sys_enumdata t where t.enum_sort='IP_YWLX' and t.enum_name='{0}'", YWLX.SelectedValue);
        return DataFunction.GetStringResult(sql);
    }

    private void InitPageContrl()
    {
        tr_DK.Style.Add("display", "none");
        tr_JF.Style.Add("display", "none");
        tr_DDXX.Style.Add("display", "none");
        Btntr.Style.Add("display", "block");
        YHZYIP.ReadOnly = false;
        YHZYIP.BackColor = System.Drawing.Color.White;
        YHLYWD.ReadOnly = false;
        YHLYWD.BackColor = System.Drawing.Color.White;
        HTVPN_CODE.ReadOnly = false;
        HTVPN_CODE.BackColor = System.Drawing.Color.White;
        SBMC.ReadOnly = false;
        SBMC.BackColor = System.Drawing.Color.White;
        td_VPN.Attributes.Add("display", "block");
        td_SBMC.Attributes.Add("display", "block");
        string ip_ywlx = Getywfl();
        if (ip_ywlx == "MPLS/L2VPN")
        {
            //原来为隐藏，客户需要显示新增等按钮
            //Btntr.Style.Add("display", "none");
            YHZYIP.ReadOnly = true;
            YHZYIP.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            YHLYWD.ReadOnly = true;
            YHLYWD.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            HTVPN_CODE.ReadOnly = true;
            HTVPN_CODE.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            //SBMC.ReadOnly = true;
            //SBMC.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            //td_VPN.Style.Add("display", "none");
            //td_SBMC.Style.Add("display", "none");
            tr_DK.Style.Add("display", "block");
            tr_JF.Style.Add("display", "block");
            tr_DDXX.Style.Add("display", "block");
        }
        if (ip_ywlx == "XPB")
        {
            YHZYIP.ReadOnly = true;
            YHZYIP.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            YHLYWD.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            YHLYWD.ReadOnly = true;
            YHLYWD.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            HTVPN_CODE.ReadOnly = true;
            HTVPN_CODE.BackColor = System.Drawing.Color.FromArgb(0xF0F0F0);
            //SBMC.ReadOnly = true;
            //td_VPN.Style.Add("display", "none");
            td_SBMC.Style.Add("display", "none");
        }
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
    protected void BtnYwfl_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(YWLX, "IP_YWLX");
    }

    protected void BtnExp_Click(object sender, EventArgs e)
    {
        try
        {
            License lic = new License();
            lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
            Workbook book = new Workbook();
            book.Open(Server.MapPath("../../template/IP资源配置模板.xls"));
            Worksheet ws = book.Worksheets["Sheet1"];

            DataTable dt = DataFunction.FillDataSet(@"select t.subscriber_code B1,case when subscriber_gdly = 'BOSS' then 'BOSS' else '手工单' end D1,
                                                t.ywlx F1,t.sbpzxx B2,t.jrdw D2,onu_code F2,WLJF B3,LJJF D3,DK F3,YHZYIP B4,htvpn_code D4,htvpn F4,YHLYWD B5,
                                                SBMC D5,JRDK F5,pzr D13,pzsj F13,bz B14 from  T_CON_LOGIC_EQU_IP t where guid='" + GUID.Text + "'").Tables[0];
            //导基本信息
            dt.Rows.Cast<DataRow>().ForEach(dr =>
            {
                dt.Columns.Cast<DataColumn>().ForEach(col =>
                {
                    ws.Cells[col.ColumnName].PutValue(Convert.ToString(dr[col]));
                });
            });

            string id = DataFunction.GetStringResult("select SUBSCRIBER_ID from T_CON_LOGIC_EQU_IP where guid='" + GUID.Text + "'");
            dt = DataFunction.FillDataSet(@"select region B7,customer_code D7,customer_name F7,custtype1 B8,custtype D8,customer_level ||'级' F8,
                            sub_name B9,linkman D9,email F9,phone_no B10,fax_no D10,mobile_no F10,zip_code B11,address B12,
                            sale_name B13 from rmss where subscriber_id='" + id + "'").Tables[0];
            //导用户信息
            dt.Rows.Cast<DataRow>().ForEach(dr =>
            {
                dt.Columns.Cast<DataColumn>().ForEach(col =>
                {
                    ws.Cells[col.ColumnName].PutValue(Convert.ToString(dr[col]));
                });
            });

            dt = DataFunction.FillDataSet(@"select rownum xh,IPDZ from T_LOGIC_EQU_IP_PZ where pk_guid='" + GUID.Text + "'").Tables[0];
            //导IP
            int row = 16;
            dt.Rows.Cast<DataRow>().ForEach(dr =>
            {
                int idx = 0;
                dt.Columns.Cast<DataColumn>().ForEach(col =>
                {
                    ws.Cells[row, idx].PutValue(Convert.ToString(dr[col]));
                    ++idx;
                });
                idx = 0;
                ++row;
            });

            dt = DataFunction.FillDataSet(@"select rownum xh,vlanbh from t_con_logic_equ_vlan where pk_guid='" + GUID.Text + "'").Tables[0];
            //导VLAN
            row = 16;
            dt.Rows.Cast<DataRow>().ForEach(dr =>
            {
                int idx = 4;
                dt.Columns.Cast<DataColumn>().ForEach(col =>
                {
                    ws.Cells[row, idx].PutValue(Convert.ToString(dr[col]));
                    ++idx;
                });
                idx = 4;
                ++row;
            });
            MemoryStream ms = new MemoryStream();
            book.Save(ms, FileFormatType.Excel97To2003);
            IDP.Common.WebUtils.ResponseWriteBinary(ms.ToArray(), "IP资源配置.xls");
        }
        catch (Exception)
        {
            this.Alert("模板配置错误");
        }

    }
    protected void BtnJRDK_Click(object sender, EventArgs e)
    {
        string strSql = "select  * from t_res_child_port t where guid='" + JRDK_GUID.Text + "' and dklx='OLT'";
        if (DataFunction.HasRecord(strSql))
        {
            xndk.Style.Add("display", "block");
            xndk1.Style.Add("display", "block");
        }
        else
        {
            xndk.Style.Add("display", "none");
            xndk1.Style.Add("display", "none");
        }
    }
    protected void JXXZButton_Click(object sender, EventArgs e)
    {
        SaveData(false);
        GUID.Text = "";
        SUBSCRIBER_CODE.Text = "";
        SUBSCRIBER_ID.Text = "";
        CREATEDATETIME.Text = DateTime.Now.ToString();
    }
}

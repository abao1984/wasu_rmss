using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceIpEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            // START_FD.Text = Request.QueryString["START_FD"];
            GridViewLogicEquIP.Attributes.Add("BorderColor", "#5B9ED1");
            SSQY.Attributes.Add("readonly", "true");
            GUID.Text = Request.QueryString["GUID"];

            GridViewLogicEquIP.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            GridView1.PageSize = Convert.ToInt32(PageSize1.SelectedValue);

            if (string.IsNullOrEmpty(GUID.Text))
            {
                GUID.Text = Guid.NewGuid().ToString();
            }

            P_GUID.Text = Request.QueryString["P_GUID"];
            if (string.IsNullOrEmpty(P_GUID.Text))
            {
                P_GUID.Text = GetP_GUID();
            }
            if (!string.IsNullOrEmpty(P_GUID.Text))
            {
                InitPaterEquIp();
            }

            if (string.IsNullOrEmpty(START_FD.Text))
            {
                START_FD.Text = "7";
            }
            ShareFunction.BindEnumDropList(DZFL, "DZFL");

            SSJF.Attributes.Add("readonly", "true");
            BindDropIPFD();
            FillPage();

            //if (!string.IsNullOrEmpty(P_GUID.Text))
            //{
            //    YWDL.Enabled = false;
            //}
            BindDropIpList(true);
            BindGridPage(BindGrid());
            BindPzdGrid();


        }
    }


    private int BindGrid()
    {
        string sql = string.Format(@"select p.ip as p_ip,t.*
          from t_logic_equ_ip t
          left join t_logic_equ_ip p on t.p_guid = p.guid where t.p_guid='{0}'", GUID.Text);
        sql += " order by t.ip1,t.ip2,t.ip3,t.ip4,t.ipfd ";
        DataSet ds = DataFunction.FillDataSet(sql);
        GridViewLogicEquIP.DataSource = ds;
        GridViewLogicEquIP.DataBind();
        BtnIpCsh.Text = "IP分配数据(" + ds.Tables[0].Rows.Count + ")条";

        return ds.Tables[0].Rows.Count;
    }

    private void BindPzdGrid()
    {
        //by hangyt@2012.2.16
        //string sql = string.Format(@"select distinct  b.ipdz,p.vlan,p.sbpzxx,p.guid,r.SUBSCRIBER_ID,p.Subscriber_Code,p.ywlx,p.wljf,
        string sql = string.Format(@"select distinct  b.ipdz,(select WMSYS.WM_CONCAT(vlanbh) from t_con_logic_equ_vlan v where p.guid=v.pk_guid) as vlan,p.sbpzxx,p.guid,r.SUBSCRIBER_ID,p.Subscriber_Code,p.ywlx,p.wljf, 
                                r.CUSTOMER_NAME,
                                r.customer_code,
                                r.CUSTOMER_LEVEL,
                                r.CUSTTYPE1,
                                r.CUSTTYPE,
                                r.region,
                                r.sub_name,
                                r.LINKMAN
                  from t_logic_equ_ip     a,
                       t_logic_equ_ip_pz  b,
                       t_con_logic_equ_ip p,
                       RMSS               r
                 where a.ipdz1 <= b.ipdz1
                   and a.ipdz2 >= b.ipdz2
                   and b.pk_guid = p.guid
                   and p.Subscriber_ID = r.SUBSCRIBER_ID
                   and a.guid = '{0}'
                   and (b.SFHS <> '1' or b.SFHS is null) ", GUID.Text);

        string sqlCount = "select count(*) from (" + sql + ")";
        Pager1.RowCount = Convert.ToInt32(DataFunction.GetIntResult(sqlCount));
        sql = "select t.*, row_number() over (order by ywlx) idx from (" + sql + ") t";
        sql = "select * from (" + sql + ") where idx <= " + Pager1.PageSize;
        DataSet ds = DataFunction.FillDataSet(sql);

        gvLogicEquIp.DataSource = ds;
        gvLogicEquIp.DataBind();
        BtnIpPzd.Text = "IP配置单数据(" + Pager1.RowCount + ")条";


    }

    protected void GridViewLogicEquIP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = GridViewLogicEquIP.DataKeys[e.Row.RowIndex].Value.ToString();
            string name = e.Row.Cells[1].Text;
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen1('" + guid + "','" + name + "')");
            e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=# onclick=\"windowOpen1('" + guid + "','" + name + "')\">详细</a>";
        }
    }

    #region 初始化分段下拉框
    private void BindDropIPFD()
    {
        string str_Ipfd = IPFD.SelectedValue;
        IPFD.Items.Clear();
        int sFd = Convert.ToInt32(START_FD.Text);
        for (int i = sFd + 1; i <= 32; i++)
        {
            IPFD.Items.Add(i.ToString());
        }
        IPFD.SelectedValue = str_Ipfd;
    }
    private string GetP_GUID()
    {
        string sql = "select P_GUID from t_logic_equ_ip where guid='" + GUID.Text + "'";
        return DataFunction.GetStringResult(sql);
    }
    #endregion

    #region 初始化IP地址段
    private void BindDropIpList(bool isSetOld)
    {
        int ipfd = Convert.ToInt32(IPFD.SelectedValue);
        int sfd = Convert.ToInt32(START_FD.Text);
        int pr_ip2 = Convert.ToInt32(P_IP2.Text);
        int pr_ip3 = Convert.ToInt32(P_IP3.Text);
        int pr_ip4 = Convert.ToInt32(P_IP4.Text);
        if (!string.IsNullOrEmpty(P_GUID.Text))
        {
            IP1.Enabled = false;
            if (sfd > 16)
            {
                IP2.Enabled = false;
                if (sfd > 24)
                {
                    IP3.Enabled = false;
                }
            }
        }
        if (ipfd > 16)
        {
            InitDropIpList(IP2, 0, 16 - sfd, pr_ip2, isSetOld);
            GetIPState2();
            if (ipfd > 24)
            {
                InitDropIpList(IP3, 0, 24 - sfd, pr_ip3, isSetOld);
                GetIPState3();
                InitDropIpList(IP4, 32 - ipfd, 32 - sfd, pr_ip4, isSetOld);
                GetIPState4();
            }
            else
            {
                InitDropIpList(IP3, 24 - ipfd, 24 - sfd, pr_ip3, isSetOld);
                GetIPState3();
                InitDropIpList(IP4, 0, 0, 0, isSetOld);
            }
        }
        else if (ipfd > 8)
        {
            InitDropIpList(IP2, 16 - ipfd, 16 - sfd, pr_ip2, isSetOld);
            GetIPState2();
            InitDropIpList(IP3, 0, 0, 0, isSetOld);
            InitDropIpList(IP4, 0, 0, 0, isSetOld);
        }
    }
    private void InitDropIpList(DropDownList IpList, int sFd, int eFd, int sIp, bool isSetOld)
    {
        if (eFd > 8)
        {
            eFd = 8;
        }
        string strIp = IpList.SelectedValue;
        IpList.Items.Clear();
        ListItem li = new ListItem(sIp.ToString() + "【可选】", sIp.ToString());
        IpList.Items.Add(li);
        for (int i = sFd; i < eFd; i++)
        {
            int count = IpList.Items.Count;
            for (int j = 0; j < count; j++)
            {
                int ipNum = Convert.ToInt32(IpList.Items[j].Value) + Convert.ToInt32(Math.Pow(2, i));
                li = new ListItem(ipNum.ToString() + "【可选】", ipNum.ToString());
                IpList.Items.Add(li);
            }
        }
        if (isSetOld || IpList.Items.FindByValue(strIp) != null)
        {
            IpList.SelectedValue = strIp;
        }
    }
    private void GetIPState2()
    {
        if (string.IsNullOrEmpty(YWDL.SelectedValue))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务大类不能为空！');</script>");
            return;
        }
        foreach (ListItem li in IP2.Items)
        {
            GetIpdzState(li, IP1.Text, li.Value, "0", "0", IPFD.SelectedValue);
        }
    }
    private void GetIPState3()
    {
        if (string.IsNullOrEmpty(YWDL.SelectedValue))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务大类不能为空！');</script>");
            return;
        }
        int i = 0;
        foreach (ListItem li in IP3.Items)
        {
            if (GetIpdzState(li, IP1.Text, IP2.SelectedValue, li.Value, "0", IPFD.SelectedValue))
            {
                i++;
            }
        }
        if (i == IP3.Items.Count)
        {
            IP2.SelectedItem.Text = IP2.SelectedValue + "【可选】";
            IP2.SelectedItem.Attributes.Add("style", "color:black");
        }
        else if (i > 0)
        {
            IP2.SelectedItem.Text = IP2.SelectedValue + "〖部分分配〗";
            IP2.SelectedItem.Attributes.Add("style", "color:blue");
        }
        else
        {
            IP2.SelectedItem.Text = IP2.SelectedValue + "[已分配]";
            IP2.SelectedItem.Attributes.Add("style", "color:red");
        }
    }
    private void GetIPState4()
    {
        if (string.IsNullOrEmpty(YWDL.SelectedValue))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('业务大类不能为空！');</script>");
            return;
        }
        int i = 0;
        foreach (ListItem li in IP4.Items)
        {

            if (GetIpdzState(li, IP1.Text, IP2.SelectedValue, IP3.SelectedValue, li.Value, IPFD.SelectedValue))
            {
                i++;
            }
        }
        if (i == IP4.Items.Count)
        {
            IP3.SelectedItem.Text = IP3.SelectedValue + "【可选】";
            IP3.SelectedItem.Attributes.Add("style", "color:black");
        }
        else if (i > 0)
        {
            IP3.SelectedItem.Text = IP3.SelectedValue + "〖部分分配〗";
            IP3.SelectedItem.Attributes.Add("style", "color:blue");
        }
        else
        {
            IP3.SelectedItem.Text = IP3.SelectedValue + "[已分配]";
            IP3.SelectedItem.Attributes.Add("style", "color:red");
        }
    }
    #endregion

    #region 初始化父节点信息
    private void InitPaterEquIp()
    {
        string sql = "select * from T_LOGIC_EQU_IP where guid='" + P_GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            P_IP1.Text = dr["IP1"].ToString();
            P_IP2.Text = dr["IP2"].ToString();
            P_IP3.Text = dr["IP3"].ToString();
            P_IP4.Text = dr["IP4"].ToString();

            START_FD.Text = dr["IPFD"].ToString();
        }

    }
    #endregion

    #region 填充界面
    private void FillPage()
    {
        DataRow dr = Gett_Logic_equ_ipData();
        ShareFunction.FillControlData(this.Page, dr);
        try
        {
            ShareFunction.BindEnumDropList(YWDL, "YWDL", DZFL.SelectedValue);
            YWDL.SelectedValue = Convert.ToString(dr["YWDL"]);
            ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX", YWDL.SelectedValue);
            IPYWLX.SelectedValue = Convert.ToString(dr["IPYWLX"]);
        }
        catch
        {
        }

        if (SFQW.Checked)
        {
            TD_SSJF.Style.Add("display", "none");
        }
        else
        {
            TD_SSJF.Style.Add("display", "block");
        }
    }
    #endregion


    protected void IPFD_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropIpList(false);
    }

    #region 获取IP地址
    private void GetIpdz()
    {

        Int64 ipfd = Convert.ToInt64(IPFD.SelectedValue);
        Int64 ipdz11 = Convert.ToInt64(IP1.Text), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
        Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
        Int64 zwip1 = 255, zwip2 = 0, zwip3 = 0, zwip4 = 0;
        if (ipfd > 16)
        {
            zwip2 = 255;
            ipdz12 = Convert.ToInt64(IP2.SelectedValue);
            ipdz22 = ipdz12;
            if (ipfd > 24)
            {
                zwip3 = 255;
                ipdz13 = Convert.ToInt64(IP3.SelectedValue);
                ipdz23 = ipdz13;
                getIp(ref ipdz14, ref ipdz24, ref zwip4, Convert.ToInt64(IP4.SelectedValue), 32 - ipfd);
            }
            else
            {
                getIp(ref ipdz13, ref ipdz23, ref zwip3, Convert.ToInt64(IP3.SelectedValue), 24 - ipfd);
            }
        }
        else if (ipfd > 8)
        {
            getIp(ref ipdz12, ref ipdz22, ref zwip2, Convert.ToInt64(IP2.SelectedValue), 16 - ipfd);
        }
        ZWYM.Text = zwip1 + "." + zwip2 + "." + zwip3 + "." + zwip4;
        IPDZ1.Text = Convert.ToString(Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14);
        IPDZ2.Text = Convert.ToString(Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24);
    }

    private bool GetIpdzState(ListItem li, string s_ip1, string s_ip2, string s_ip3, string s_ip4, string s_ipfd)
    {
        bool isState = false;
        Int64 ipfd = Convert.ToInt64(s_ipfd);
        Int64 ipdz11 = Convert.ToInt64(s_ip1), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
        Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
        Int64 zwip2 = 0, zwip3 = 0, zwip4 = 0;
        if (ipfd > 16)
        {
            zwip2 = 255;
            ipdz12 = Convert.ToInt64(s_ip2);
            ipdz22 = ipdz12;
            if (ipfd > 24)
            {
                zwip3 = 255;
                ipdz13 = Convert.ToInt64(s_ip3);
                ipdz23 = ipdz13;
                getIp(ref ipdz14, ref ipdz24, ref zwip4, Convert.ToInt64(s_ip4), 32 - ipfd);
            }
            else
            {
                getIp(ref ipdz13, ref ipdz23, ref zwip3, Convert.ToInt64(s_ip3), 24 - ipfd);
            }
        }
        else if (ipfd > 8)
        {
            getIp(ref ipdz12, ref ipdz22, ref zwip2, Convert.ToInt64(s_ip2), 16 - ipfd);
        }

        string s_ipdz1 = Convert.ToString(Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14);
        string s_ipdz2 = Convert.ToString(Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24);
        string sql = string.Format(@"select * from t_logic_equ_ip t where t.p_guid='{0}' and t.guid<>'{1}' and t.ipdz1<{2} and t.ipdz2>{3} and t.ipfd<={4} and t.ywdl='{5}'", P_GUID.Text, GUID.Text, s_ipdz2, s_ipdz1, IPFD.SelectedValue, YWDL.SelectedValue);
        if (string.IsNullOrEmpty(P_GUID.Text))
        {
            sql = string.Format(@"select * from t_logic_equ_ip t where t.p_guid is null and t.guid<>'{1}' and t.ipdz1<{2} and t.ipdz2>{3} and t.ipfd<={4} and t.ywdl='{5}'", P_GUID.Text, GUID.Text, s_ipdz2, s_ipdz1, IPFD.SelectedValue, YWDL.SelectedValue);
        }
        if (DataFunction.HasRecord(sql))
        {
            li.Text = li.Value + "［已分配］";
            li.Attributes.Add("style", "color:red");
        }
        else
        {
            sql = string.Format(@"select * from t_logic_equ_ip t where t.p_guid='{0}' and t.guid<>'{1}' and t.ipdz1<{2} and t.ipdz2>{3} and t.ywdl='{4}'", P_GUID.Text, GUID.Text, s_ipdz2, s_ipdz1, YWDL.SelectedValue);
            if (string.IsNullOrEmpty(P_GUID.Text))
            {
                sql = string.Format(@"select * from t_logic_equ_ip t where t.p_guid is null and t.guid<>'{1}' and t.ipdz1<{2} and t.ipdz2>{3} and t.ywdl='{4}'", P_GUID.Text, GUID.Text, s_ipdz2, s_ipdz1, YWDL.SelectedValue);
            }
            if (DataFunction.HasRecord(sql))
            {
                li.Text = li.Value + "〖部分分配〗";
                li.Attributes.Add("style", "color:blue");
            }
            else
            {
                li.Text = li.Value + "【可选】";
                isState = true;
            }
        }
        return isState;
    }
    private void InitIpListColor(DropDownList IpList)
    {
        foreach (ListItem li in IpList.Items)
        {
            if (li.Text.IndexOf("已分配") > -1)
            {
                li.Attributes.Add("style", "color:red");
            }
            else if (li.Text.IndexOf("部分分配") > -1)
            {
                li.Attributes.Add("style", "color:blue");
            }
        }
    }

    private void getIp(ref Int64 ips1, ref Int64 ips2, ref Int64 zwip, Int64 ipdz, Int64 ipfd)
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
            zwip += Convert.ToInt64(Math.Pow(2, i));
        }
        ips1 = ip;
        ips2 = ip + Convert.ToInt64(Math.Pow(2, ipfd));
    }
    #endregion

    #region 获取IP数据
    private DataRow Gett_Logic_equ_ipData()
    {
        string sql = "select * from t_logic_equ_ip where GUID='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = GUID.Text;
            if (!string.IsNullOrEmpty(P_GUID.Text))
            {
                dr["P_GUID"] = P_GUID.Text;
                dr["YWDL"] = Request.QueryString["YWDL"];
                dr["SSQY"] = Request.QueryString["SSQY"];
                dr["SSQY_CODE"] = Request.QueryString["SSQY_CODE"];
            }
            dr["IP1"] = P_IP1.Text;
            dr["IP2"] = P_IP2.Text;
            dr["IP3"] = P_IP3.Text;
            dr["IP4"] = P_IP4.Text;
            dr["IPFD"] = IPFD.SelectedValue;
            dr["CREATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        return dr;
    }
    #endregion

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        GetIpdz();
        if (string.IsNullOrEmpty(IP1.Text))
        {
            IP.Text = "";
        }
        else
        {
            IP.Text = IP1.Text + "." + IP2.SelectedValue + "." + IP3.SelectedValue + "." + IP4.SelectedValue + "/" + IPFD.SelectedValue;
        }
        DataRow dr = Gett_Logic_equ_ipData();
        UPDATEDATETIME.Text = DateTime.Now.ToString();
        ShareFunction.GetControlData(this.Page, dr);
        DataFunction.SaveData(dr.Table.DataSet, "t_logic_equ_ip");
        GetIPZDS(dr["GUID"].ToString(), "");
        GetIPZDS(dr["P_GUID"].ToString(), "p");
        string s_ipdz = IP1.Text + "." + IP2.SelectedValue + "." + IP3.SelectedValue + "." + IP4.SelectedValue + "/" + IPFD.SelectedValue;
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.SetLabelHead('IP地址：" + s_ipdz + "');</script>");

    }

    private void GetIPZDS(string guid, string p)
    {
        string sql = "select IPDZ2 - IPDZ1 from t_logic_equ_ip where guid='" + guid + "'";
        int zdzs = DataFunction.GetIntResult(sql);
        sql = "select sum(IPDZ2 - IPDZ1) from t_logic_equ_ip where p_guid='" + guid + "'";
        int dzs = DataFunction.GetIntResult(sql);
        if (dzs == 0)
        {
            sql = string.Format(@"select sum(b.ipdz2-b.ipdz1) from t_logic_equ_ip  a, t_logic_equ_ip_pz  b,t_con_logic_equ_ip p,RMSS r
where a.ipdz1 <= b.ipdz1 and a.ipdz2 >= b.ipdz2  and b.pk_guid = p.guid and p.Subscriber_ID = r.SUBSCRIBER_ID
 and a.guid = '{0}'and (b.SFHS <> '1' or b.SFHS is null)", guid);
            dzs = DataFunction.GetIntResult(sql);
        }

        int kydzs = zdzs - dzs;
        string fpzt = "部分分配";
        if (kydzs == 0)
        {
            fpzt = "已分配";
        }
        else if (zdzs == kydzs)
        {
            fpzt = "未分配";
        }
        sql = string.Format("update t_logic_equ_ip set KYDZS={0},IPFPZT='{1}' where guid='{2}'", kydzs, fpzt, guid);
        DataFunction.ExecuteNonQuery(sql);
        if (p != "p")
        {
            KYDZS.Text = kydzs.ToString();
            IPFPZT.Text = fpzt;
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("LogicResourceIpEdit.aspx?P_GUID=" + GUID.Text);
    }
    protected void IP2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IP3.Items.Count > 0)
        {
            GetIPState3();
            InitIpListColor(IP2);
        }
    }
    protected void IP3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IP4.Items.Count > 0)
        {
            GetIPState4();
            InitIpListColor(IP2);
            InitIpListColor(IP3);
        }
    }

    protected void BtnSX_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(P_GUID.Text))
        {
            P_GUID.Text = GetP_GUID();
        }
        if (!string.IsNullOrEmpty(P_GUID.Text))
        {
            InitPaterEquIp();
        }

        if (string.IsNullOrEmpty(START_FD.Text))
        {
            START_FD.Text = "8";
        }
        BindDropIPFD();
        FillPage();
        BindDropIpList(true);
        BindGridPage(BindGrid());
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridViewLogicEquIP.PageIndex = 0;
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
        GridViewLogicEquIP.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridViewLogicEquIP.PageIndex + 1);
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
        GridViewLogicEquIP.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewLogicEquIP.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewLogicEquIP.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewLogicEquIP.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridViewLogicEquIP.PageIndex + 1);
        BindGrid();
    }
    #endregion

    #region 分页管理
    private void BindGridPage1(int DataCount)
    {
        GridView1.PageIndex = 0;
        int PageCount = DataCount / Convert.ToInt32(PageSize1.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize1.SelectedValue) > 0)
        {
            PageCount++;
        }
        GridPageList1.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
            GridPageList1.Items.Add(LI);
        }
        DataCountLab1.Text = DataCount.ToString();
        PageCountLab1.Text = PageCount.ToString();
        PageIndexLab1.Text = "1";
    }


    protected void PrevButton1_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList1.SelectedIndex;
        if (PageIndex == 0)
        {
            GridPageList1.SelectedIndex = GridPageList1.Items.Count - 1;
        }
        else
        {
            GridPageList1.SelectedIndex = PageIndex - 1;
        }
        GridView1.PageIndex = Convert.ToInt32(GridPageList1.SelectedValue);
        PageIndexLab1.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid1();
    }

    protected void NextButton1_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList1.SelectedIndex;
        if (GridPageList1.Items.Count - 1 == PageIndex)
        {
            GridPageList1.SelectedIndex = 0;
        }
        else
        {
            GridPageList1.SelectedIndex = PageIndex + 1;
        }
        GridView1.PageIndex = GridPageList1.SelectedIndex;
        PageIndexLab1.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid1();
    }

    protected void PageSize1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize1.SelectedValue);
        BindGridPage1(BindGrid1());
    }
    protected void GridPageList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList1.SelectedIndex;
        PageIndexLab1.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid1();
    }
    #endregion

    protected void DeleteButton_Click(object sender, EventArgs e)
    {

        string sql = "select * from t_logic_equ_ip where p_guid='" + GUID.Text + "'";
        if (DataFunction.HasRecord(sql))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请先删除下级IP分段！');</script>");
        }
        else
        {
            sql = "delete from t_logic_equ_ip where guid='" + GUID.Text + "'";
            DataFunction.ExecuteNonQuery(sql);
            GetIPZDS(P_GUID.Text, "p");
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
        }
    }

    protected void BtnYwdl_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(YWDL, "YWDL", DZFL.SelectedValue);
    }
    protected void BtnIpywlx_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX", YWDL.SelectedValue);
    }
    protected void BtnDzfl_Click(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(IPYWLX, "DZFL");
    }
    protected void SFQW_CheckedChanged(object sender, EventArgs e)
    {
        if (SFQW.Checked)
        {
            TD_SSJF.Style.Add("display", "none");
        }
        else
        {
            TD_SSJF.Style.Add("display", "block");
        }
    }

    protected void DZFL_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(YWDL, "YWDL", DZFL.SelectedValue);
    }
    protected void YWDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX", YWDL.SelectedValue);
    }


    //IP配置单信息
    protected void gvLogicEquIp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = gvLogicEquIp.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + guid + "')");
        }
    }

    #region 分Grid按钮事件
    //IP初始化数据
    protected void BtnIpCsh_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
        ippzd_tr.Attributes.CssStyle.Add("display", "none");
        ippzdfy_tr.Attributes.CssStyle.Add("display", "none");

        Tr1.Attributes.CssStyle.Add("display", "none");
        Tr2.Attributes.CssStyle.Add("display", "none");

        ipzypz_tr.Attributes.CssStyle.Add("display", "block");
        ipzypzfy_tr.Attributes.CssStyle.Add("display", "block");
    }

    //IP配置单数据
    protected void BtnIpPzd_Click(object sender, EventArgs e)
    {
        BindPzdGrid();
        ipzypz_tr.Attributes.CssStyle.Add("display", "none");
        ipzypzfy_tr.Attributes.CssStyle.Add("display", "none");

        ippzd_tr.Attributes.CssStyle.Add("display", "block");
        ippzdfy_tr.Attributes.CssStyle.Add("display", "block");

        Tr1.Attributes.CssStyle.Add("display", "none");
        Tr2.Attributes.CssStyle.Add("display", "none");
    }
    #endregion

    protected void Pager1_PageIndexChanged(object sender, EventArgs e)
    {
        string sql = string.Format(@"select distinct  b.ipdz,p.vlan,p.sbpzxx,p.guid,r.SUBSCRIBER_ID,p.Subscriber_Code,p.ywlx,p.wljf,
                                r.CUSTOMER_NAME,
                                r.customer_code,
                                r.CUSTOMER_LEVEL,
                                r.CUSTTYPE1,
                                r.CUSTTYPE,
                                r.region,
                                r.sub_name,
                                r.LINKMAN
                  from t_logic_equ_ip     a,
                       t_logic_equ_ip_pz  b,
                       t_con_logic_equ_ip p,
                       RMSS               r
                 where a.ipdz1 <= b.ipdz1
                   and a.ipdz2 >= b.ipdz2
                   and b.pk_guid = p.guid
                   and p.Subscriber_ID = r.SUBSCRIBER_ID
                   and a.guid = '{0}'
                   and (b.SFHS <> '1' or b.SFHS is null) ", GUID.Text);

        sql = "select t.*, row_number() over (order by ywlx) idx from (" + sql + ") t";
        sql = "select * from (" + sql + ") where idx between " + Pager1.BeginRowIndex + " and " + Pager1.EndRowIndex;

        var ds = DataFunction.FillDataSet(sql);
        gvLogicEquIp.DataSource = ds;
        gvLogicEquIp.DataBind();
    }

    #region
    //private DataTable BindKFPIP()
    //{
    //    //初始化表格
    //    DataTable dt = new DataTable();
    //    DataColumn dc1 = new DataColumn("IPDZ");
    //    DataColumn dc2 = new DataColumn("IPZT");
    //    dt.Columns.Add(dc1);
    //    dt.Columns.Add(dc2);

    //    int sfd=Convert.ToInt32(IPFD.SelectedValue);
    //    int ip1 = Convert.ToInt32(IP1.Text);
    //    int ip2 = Convert.ToInt32(IP2.SelectedValue);
    //    int ip3 = Convert.ToInt32(IP3.SelectedValue);
    //    int ip4 = Convert.ToInt32(IP4.SelectedValue);
    //    for (int i = sfd + 1; i <= 32;i++ )
    //    {
    //        if (i > 24)
    //        {
    //            InitIpList(dt, 32 - i, 32 - sfd, ip4, ip1, ip2, ip3, ip4, i);
    //        }
    //        else if (i > 16)
    //        {
    //            InitIpList(dt, 24 - i, 24 - sfd, ip3, ip1, ip2, ip3, ip4, i);
    //        }
    //        else if (i > 8)
    //        {
    //            InitIpList(dt, 16 - i, 16 - sfd, ip2, ip1, ip2, ip3, ip4, i);
    //        }
    //    }
    //    return dt;
    //}

    //private void InitIpList(DataTable dt, int sFd, int eFd, int sIp,int ip1,int ip2,int ip3,int ip4,int ipfd)
    //{
    //    if (eFd > 8)
    //    {
    //        eFd = 8;
    //    }
    //    //ListItem li = new ListItem(sIp.ToString() + "【可选】", sIp.ToString());
    //    //IpList.Items.Add(li);
    //    List<int> li = new List<int>();
    //    li.Add(sIp);
    //    SelIPZT(dt, ipfd, sIp, ip1, ip2, ip3, ip4);
    //    for (int i = sFd; i < eFd; i++)
    //    {
    //        int count = li.Count;
    //        for (int j = 0; j < count; j++)
    //        {
    //            int ipNum = li[j] + Convert.ToInt32(Math.Pow(2, i));
    //            li.Add(ipNum);
    //            SelIPZT(dt, ipfd, ipNum, ip1, ip2, ip3, ip4);
    //        }
    //    }
    //}

    //private bool SelIPZT(DataTable dt,int ipfd,int ipNum,int ip1,int ip2,int ip3,int ip4)
    //{
    //    if (ipfd > 24)
    //    {
    //        ip4 = ipNum;
    //    }
    //    else if (ipfd > 16)
    //    {
    //        ip3 = ipNum;
    //    }
    //    else if (ipfd > 8)
    //    {
    //        ip2 = ipNum;
    //    }
    //    string ipdz = ip1 + "." + ip2 + "." + ip3 + "." + ip4 + "/" + ipfd;
    //    string strSql = "select * from t_logic_equ_ip_pz t where ipdz='" + ipdz + "'";
    //    if(DataFunction.HasRecord(strSql))
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        DataRow dr = dt.NewRow();
    //        dr["IPDZ"] = ipdz;
    //        dt.Rows.Add(dr);
    //        return true;
    //    }

    //}
    #endregion

    protected void BtnIpKFP_Click(object sender, EventArgs e)
    {


        ippzd_tr.Attributes.CssStyle.Add("display", "none");
        ippzdfy_tr.Attributes.CssStyle.Add("display", "none");

        ipzypz_tr.Attributes.CssStyle.Add("display", "none");
        ipzypzfy_tr.Attributes.CssStyle.Add("display", "none");

        Tr1.Attributes.CssStyle.Add("display", "block");
        Tr2.Attributes.CssStyle.Add("display", "block");

        BindGridPage1(BindGrid1());
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{
        //    int ipfd = Convert.ToInt32(dr["IPFD"]);
        //}

    }

    private int BindGrid1()
    {
        DataTable dt = new DataTable();
        DataColumn dc1 = new DataColumn("IPDZ");
        DataColumn dc2 = new DataColumn("IPZT");
        dt.Columns.Add(dc1);
        dt.Columns.Add(dc2);
        string sql = string.Format("select max(ipfd) as ipfd from t_logic_equ_ip_pz t where t.ipdz1>={0} and t.ipdz2<={1} and (SFHS <> '1' or SFHS is null) order by t.ipfd", IPDZ1.Text, IPDZ2.Text);
        string seIpfd = DataFunction.GetStringResult(sql);
        if (string.IsNullOrEmpty(seIpfd))
        {
            DataRow ipDr = dt.NewRow();
            ipDr["IPDZ"] = IP1.Text + "." + IP2.SelectedValue + "." + IP3.SelectedValue + "." + IP4.SelectedValue + "/" + IPFD.SelectedValue;
            dt.Rows.Add(ipDr);
        }
        else
        {
            Int64 sIpfd = Convert.ToInt32(IPFD.Text);
            Int64 eIpfd = Convert.ToInt32(seIpfd);
            Int64 sip1 = Convert.ToInt32(IP1.Text);
            Int64 sip2 = Convert.ToInt32(IP2.SelectedValue);
            Int64 sip3 = Convert.ToInt32(IP3.SelectedValue);
            Int64 sip4 = Convert.ToInt32(IP4.SelectedValue);
            setIp(dt, sIpfd + 1, sip1, sip2, sip3, sip4, eIpfd);
        }
        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
        return dt.DefaultView.Count;
    }
    private void getIpd(DataRow dr)
    {
        Int64 sIpfd = Convert.ToInt32(IPFD.Text);
        Int64 eIpfd = Convert.ToInt32(dr["IPFD"]);
        Int64 sip1 = Convert.ToInt32(IP1.Text);
        Int64 sip2 = Convert.ToInt32(IP2.SelectedValue);
        Int64 sip3 = Convert.ToInt32(IP3.SelectedValue);
        Int64 sip4 = Convert.ToInt32(IP4.SelectedValue);

    }

    private void setIp(DataTable dt, Int64 ipfd, Int64 sip1, Int64 sip2, Int64 sip3, Int64 sip4, Int64 eIpfd)
    {
        if (ipfd < 16)
        {
            sfYfp(dt, ipfd, sip1, sip2, sip3, sip4, eIpfd);
            sfYfp(dt, ipfd, sip1, Convert.ToInt64(sip2 + Math.Pow(2, 16 - ipfd)), sip3, sip4, eIpfd);

        }
        else if (ipfd < 24)
        {
            sfYfp(dt, ipfd, sip1, sip2, sip3, sip4, eIpfd);
            sfYfp(dt, ipfd, sip1, sip2, Convert.ToInt64(sip3 + Math.Pow(2, 24 - ipfd)), sip4, eIpfd);
        }
        else if (ipfd < 32)
        {
            sfYfp(dt, ipfd, sip1, sip2, sip3, sip4, eIpfd);
            sfYfp(dt, ipfd, sip1, sip2, sip3, Convert.ToInt64(sip4 + Math.Pow(2, 32 - ipfd)), eIpfd);
        }
    }

    private void sfYfp(DataTable dt, Int64 ipfd, Int64 sip1, Int64 sip2, Int64 sip3, Int64 sip4, Int64 eIpfd)
    {
        if (ipfd <= eIpfd)
        {

            Int64 ipdz11 = Convert.ToInt64(IP1.Text), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
            Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
            Int64 zwip1 = 255, zwip2 = 0, zwip3 = 0, zwip4 = 0;
            if (ipfd > 16)
            {
                zwip2 = 255;
                ipdz12 = sip2;
                ipdz22 = ipdz12;
                if (ipfd > 24)
                {
                    zwip3 = 255;
                    ipdz13 = sip3;
                    ipdz23 = ipdz13;
                    getIp(ref ipdz14, ref ipdz24, ref zwip4, sip4, 32 - ipfd);
                }
                else
                {
                    getIp(ref ipdz13, ref ipdz23, ref zwip3, sip3, 24 - ipfd);
                }
            }
            else if (ipfd > 8)
            {
                getIp(ref ipdz12, ref ipdz22, ref zwip2, sip2, 16 - ipfd);
            }
            string ipdz1 = Convert.ToString(Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14);
            string ipdz2 = Convert.ToString(Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24);

            string sql = string.Format("select * from t_logic_equ_ip_pz t where t.ipdz1>={0} and t.ipdz2<={1} and (SFHS <> '1' or SFHS is null) order by t.ipfd", ipdz1, ipdz2);
            if (!DataFunction.HasRecord(sql))
            {
                DataRow ipDr = dt.NewRow();
                ipDr["IPDZ"] = sip1 + "." + sip2 + "." + sip3 + "." + sip4 + "/" + ipfd;
                dt.Rows.Add(ipDr);
            }
            else
            {
                setIp(dt, ipfd + 1, sip1, sip2, sip3, sip4, eIpfd);
            }
        }
    }



}


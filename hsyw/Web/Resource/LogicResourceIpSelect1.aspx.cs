using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceIpSelect1 : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            HOUSE_NAME_GUID.Text = Request.QueryString["HOUSE_GUID"];
            HOUSE_NAME.Text = Request.QueryString["HOUSE_NAME"];
            TextBox1.Text = Request.QueryString["HOUSE_NAME"];
            //NAME.Text = Request.QueryString["NAME"];
          //  HOUSE_GUID.Text = "71212fa4-9eac-461c-bdda-e3b0fcb38a72";
            ShareFunction.BindEnumDropList(YWDL, "YWDL");
            ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX","",true);
            ShareFunction.BindEnumDropList(DZFL, "DZFL");
            YWDL.SelectedValue = Request.QueryString["YWDL"];
            SSQY.Text = Request.QueryString["SSQY"];
            SSQY_CODE.Text = Request.QueryString["SSQY_CODE"];
            Label3.Text = Request.QueryString["SSQY"];
            PK_GUID.Text =Request.QueryString["PK_GUID"];
            LogicEquIpGrid.Attributes.Add("BorderColor", "#5B9ED1");
            LogicEquIpGrid.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindLogicEquIpGrid());
            BindGridPage0(BindLogicEquIpPzGrid());
            
        }
    }

    /// <summary>
    /// 判断ip段是否可分配
    /// </summary>
    private void PDSFKFP()
    {
        if (Convert.ToInt32(IPFD.SelectedValue)>=24){
            string ip = IP1.Text + "." + IP2.SelectedValue + "." + IP3.SelectedValue + "." + IP4.SelectedValue ;
            string strSql = "select * from t_logic_equ_ip_pz t where ipdz like '"+ip+"%' and ipfd>=24 and ipfd<='"+IPFD.SelectedValue+"'";
            if(DataFunction.HasRecord(strSql))
            {
                OKButton.Enabled = false;
            }

        }
       
    }
        #region 可选IP资源
        private int BindLogicEquIpGrid()
        {
            string sql = @"select E0.IMAGE_URL as IPZT_URL, t.*
  from t_logic_equ_ip t
  left join T_RES_SYS_ENUMDATA E0 on t.ipfpzt = E0.ENUM_NAME
                                 and E0.ENUM_SORT = 'IPFPZT'
 where 1 = 1 ";
            if (!string.IsNullOrEmpty(YWDL.SelectedValue))
            {
                sql += " and ywdl='"+YWDL.SelectedValue+"'";
            }
            if (string.IsNullOrEmpty(SSQY_CODE.Text))
            {
                if (!string.IsNullOrEmpty(SSQY.Text))
                {
                    sql += " and ssqy  like '%" + SSQY.Text + "%'";
                }
            }
            else
            {
                sql += " and ssqy_code  like '" + SSQY_CODE.Text + "%'";
            }
            if (SFQW.Checked)
            {
                sql += " and sfqw='1'";
            }
            else
            {
                if (string.IsNullOrEmpty(HOUSE_NAME_GUID.Text))
                {

                    if (!string.IsNullOrEmpty(HOUSE_NAME.Text))
                    {
                        sql += " and ssjf  like '%" + HOUSE_NAME.Text + "%'";
                    }
                }
                else
                {
                    sql += " and ssjf_guid='" + HOUSE_NAME_GUID.Text + "'";
                }
            }
            if (!string.IsNullOrEmpty(IPDZD.Text))
            {
                sql += " and ip like '"+IPDZD.Text+"%'";
            }
            if (!string.IsNullOrEmpty(YWDL.SelectedValue))
            {
                sql += " and YWDL='" + YWDL.SelectedValue + "'";
            }
            if (!string.IsNullOrEmpty(IPYWLX.SelectedValue))
            {
                sql += " and IPYWLX='" + IPYWLX.SelectedValue + "'";
            }
            if (!string.IsNullOrEmpty(DZFL.SelectedValue))
            {
                sql += " and DZFL='" + DZFL.SelectedValue + "'";
            }
            sql += "  order by ip1,ip2,ip3,ip4,ipfd";
            DataSet ds = DataFunction.FillDataSet(sql);
            LogicEquIpGrid.DataSource = ds;
            LogicEquIpGrid.DataBind();
            return ds.Tables[0].Rows.Count;
        }

        #region 分页管理
        private void BindGridPage(int DataCount)
        {
            LogicEquIpGrid.PageIndex = 0;
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
            LogicEquIpGrid.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
            PageIndexLab.Text = Convert.ToString(LogicEquIpGrid.PageIndex + 1);
            BindLogicEquIpGrid();
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
            LogicEquIpGrid.PageIndex = GridPageList.SelectedIndex;
            PageIndexLab.Text = Convert.ToString(LogicEquIpGrid.PageIndex + 1);
            BindLogicEquIpGrid();
        }

        protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicEquIpGrid.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindLogicEquIpGrid());
        }

        protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicEquIpGrid.PageIndex = GridPageList.SelectedIndex;
            PageIndexLab.Text = Convert.ToString(LogicEquIpGrid.PageIndex + 1);
            BindLogicEquIpGrid();
        }

        private void BindGridPage0(int DataCount)
        {
            LogicEquIpPzGrid.PageIndex = 0;
            int PageCount = DataCount / Convert.ToInt32(PageSize0.SelectedValue);
            if (DataCount % Convert.ToInt32(PageSize0.SelectedValue) > 0)
            {
                PageCount++;
            }
            GridPageList0.Items.Clear();
            for (int i = 1; i <= PageCount; i++)
            {
                ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
                GridPageList0.Items.Add(LI);
            }
            DataCountLab0.Text = DataCount.ToString();
            PageCountLab0.Text = PageCount.ToString();
            PageIndexLab0.Text = "1";
        }

        protected void PrevButton0_Click(object sender, System.EventArgs e)
        {
            int PageIndex = GridPageList0.SelectedIndex;
            if (PageIndex == 0)
            {
                GridPageList0.SelectedIndex = GridPageList0.Items.Count - 1;
            }
            else
            {
                GridPageList0.SelectedIndex = PageIndex - 1;
            }
            LogicEquIpPzGrid.PageIndex = Convert.ToInt32(GridPageList0.SelectedValue);
            PageIndexLab0.Text = Convert.ToString(LogicEquIpPzGrid.PageIndex + 1);
            BindLogicEquIpPzGrid();
        }

        protected void NextButton0_Click(object sender, System.EventArgs e)
        {
            int PageIndex = GridPageList0.SelectedIndex;
            if (GridPageList0.Items.Count - 1 == PageIndex)
            {
                GridPageList0.SelectedIndex = 0;
            }
            else
            {
                GridPageList0.SelectedIndex = PageIndex + 1;
            }
            LogicEquIpPzGrid.PageIndex = GridPageList0.SelectedIndex;
            PageIndexLab0.Text = Convert.ToString(LogicEquIpPzGrid.PageIndex + 1);
            BindLogicEquIpPzGrid();
        }

        protected void PageSize0_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicEquIpPzGrid.PageSize = Convert.ToInt32(PageSize0.SelectedValue);
            BindGridPage0(BindLogicEquIpPzGrid());
        }

        protected void GridPageList0_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogicEquIpPzGrid.PageIndex = GridPageList0.SelectedIndex;
            PageIndexLab0.Text = Convert.ToString(LogicEquIpPzGrid.PageIndex + 1);
            BindLogicEquIpGrid();
        }
        #endregion

    #endregion

    #region 已分配的IP资源
        private int BindLogicEquIpPzGrid()
        {
            string sql = "select * from T_LOGIC_EQU_IP_PZ";
            DataSet ds= DataFunction.FillDataSet(sql);
            LogicEquIpPzGrid.DataSource = ds;
            LogicEquIpPzGrid.DataBind();
            return LogicEquIpPzGrid.Rows.Count;
        }

   
    #endregion


        private DataRow GetT_LOGIC_EQU_IP_PZData()
        {
            string sql = "select * from T_LOGIC_EQU_IP_PZ where GUID='" + GUID.Text + "'";
            DataSet ds = DataFunction.FillDataSet(sql);
            DataRow dr =null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                dr = ds.Tables[0].Rows[0];
            }
            else
            {
                dr = ds.Tables[0].NewRow();
                GUID.Text= Guid.NewGuid().ToString();
                dr["GUID"] = GUID.Text;
                CREATEDATETIME.Text = DateTime.Now.ToString();
                ds.Tables[0].Rows.Add(dr);
            }
            return dr;
        }

  

        protected void OKButton_Click(object sender, EventArgs e)
        {
            GetIpdz();
            UPDATEDATETIME.Text = DateTime.Now.ToString();
            if (string.IsNullOrEmpty(IP1.Text))
            {
                IPDZ.Text = "";
            }
            else
            {
                IPDZ.Text = IP1.Text + "." + IP2.SelectedValue + "." + IP3.SelectedValue + "." + IP4.SelectedValue + "/" + IPFD.SelectedValue;
            }
            string ipdzStr = "select * from t_logic_equ_ip_pz t where ipdz='" + IPDZ.Text + "'";
            if (DataFunction.HasRecord(ipdzStr))
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('IP已分配或部分分配,分配不成功！');</script>");
                return;
            }
            else if (IP4.SelectedItem.Text.IndexOf("可选") < 0)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('IP已分配或部分分配,分配不成功！');</script>");
                return;
            }
            else
            {
                btnOK_Click(null,null);
            }
            PDSFKFP();
        }
        
        protected void LogicEquIpGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            string guid = LogicEquIpGrid.DataKeys[e.NewSelectedIndex].Value.ToString();
            string sql = "select * from t_logic_equ_ip t where t.guid='"+guid+"'";
            DataRow dr = DataFunction.GetSingleRow(sql);
            //GUID.Text = dr["GUID"].ToString();
            START_FD.Text = dr["IPFD"].ToString();
            IP1.Text = dr["IP1"].ToString();
            P_IP1.Text = dr["IP1"].ToString();
            P_IP2.Text = dr["IP2"].ToString();
            P_IP3.Text = dr["IP3"].ToString();
            P_IP4.Text = dr["IP4"].ToString();
            PZ_IPYWLX.Text = dr["IPYWLX"].ToString();
            BindLogicEquIpPzGrid();
            BindDropIPFD();
            BindDropIpList(true);
            OKButton.Enabled = true;
            PDSFKFP();
        }

        #region 初始化分段下拉框
        private void BindDropIPFD()
        {
            string str_Ipfd = IPFD.SelectedValue;
            IPFD.Items.Clear();
            int sFd = Convert.ToInt32(START_FD.Text);
            //for (int i = sFd + 1; i <= 32; i++)  9.6 dsh
            for (int i = sFd; i <= 32; i++)
            {
                IPFD.Items.Add(i.ToString());
            }
            if(!string.IsNullOrEmpty( str_Ipfd) && IPFD.Items.FindByValue(str_Ipfd)!=null)
            IPFD.SelectedValue = str_Ipfd;
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
                   ChangeState(IP3,IP4);
                }
            }
            else if (ipfd > 8)
            {
                InitDropIpList(IP2, 16 - ipfd, 16 - sfd, pr_ip2, isSetOld);
                GetIPState2();
                InitDropIpList(IP3, 0, 0, 0, isSetOld);
                ChangeState(IP2, IP3);
                InitDropIpList(IP4, 0, 0, 0, isSetOld);
                ChangeState(IP3, IP4);
            }
        }

        private void ChangeState(DropDownList IpList1, DropDownList IpList2)
        {
            if (IpList1.SelectedItem.Text.IndexOf("部分分配") > -1)
            {
                IpList2.SelectedItem.Text = IpList2.SelectedItem.Text.Replace("可选", "部分分配");
                IpList2.SelectedItem.Attributes.Add("style", "color:blue");
            }
            else if (IpList1.SelectedItem.Text.IndexOf("已分配") > -1)
            {
                IpList2.SelectedItem.Text = IpList2.SelectedItem.Text.Replace("可选", "已分配");
                IpList2.SelectedItem.Attributes.Add("style", "color:red");
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
            if ((IpList.Items.FindByValue(strIp) != null) && !string.IsNullOrEmpty(strIp))
            {

                IpList.SelectedValue = strIp;
            }
        }
        private void GetIPState2()
        {
            foreach (ListItem li in IP2.Items)
            {
                GetIpdzState(li, IP1.Text, li.Value, "0", "0", IPFD.SelectedValue);
            }
        }
        private void GetIPState3()
        {
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

        #region 获取IP地址
        private void GetIpdz()
        {
            Int64 ipfd = Convert.ToInt64(IPFD.SelectedValue);
            Int64 ipdz11 = Convert.ToInt64(IP1.Text), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
            Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
            if (ipfd > 16)
            {
                ipdz12 = Convert.ToInt64(IP2.SelectedValue);
                ipdz22 = ipdz12;
                if (ipfd > 24)
                {
                    ipdz13 = Convert.ToInt64(IP3.SelectedValue);
                    ipdz23 = ipdz13;
                    getIp(ref ipdz14, ref ipdz24, Convert.ToInt64(IP4.SelectedValue), 32 - ipfd);
                }
                else
                {
                    getIp(ref ipdz13, ref ipdz23, Convert.ToInt64(IP3.SelectedValue), 24 - ipfd);
                }
            }
            else if (ipfd > 8)
            {
                getIp(ref ipdz12, ref ipdz22, Convert.ToInt64(IP2.SelectedValue), 16 - ipfd);
            }
            IPDZ1.Text = Convert.ToString(Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14);
            IPDZ2.Text = Convert.ToString(Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24);
        }

        private bool GetIpdzState(ListItem li, string s_ip1, string s_ip2, string s_ip3, string s_ip4, string s_ipfd)
        {
            bool isState = false;
            Int64 ipfd = Convert.ToInt64(s_ipfd);
            Int64 ipdz11 = Convert.ToInt64(s_ip1), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
            Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
            if (ipfd > 16)
            {
                ipdz12 = Convert.ToInt64(s_ip2);
                ipdz22 = ipdz12;
                if (ipfd > 24)
                {
                    ipdz13 = Convert.ToInt64(s_ip3);
                    ipdz23 = ipdz13;
                    getIp(ref ipdz14, ref ipdz24, Convert.ToInt64(s_ip4), 32 - ipfd);
                }
                else
                {
                    getIp(ref ipdz13, ref ipdz23, Convert.ToInt64(s_ip3), 24 - ipfd);
                }
            }
            else if (ipfd > 8)
            {
                getIp(ref ipdz12, ref ipdz22, Convert.ToInt64(s_ip2), 16 - ipfd);
            }

            string s_ipdz1 = Convert.ToString(Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14);
            string s_ipdz2 = Convert.ToString(Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24);
            string sql = string.Format(@"select * from T_LOGIC_EQU_IP_PZ t where (sfhs <> '1' or sfhs is null) and   t.ipdz1<{0} and t.ipdz2>{1} and t.ipfd<={2}", s_ipdz2, s_ipdz1, IPFD.SelectedValue);
           
            if (DataFunction.HasRecord(sql))
            {
                li.Text = li.Value + "［已分配］";
                li.Attributes.Add("style", "color:red");
            }
            else
            {
                sql = string.Format(@"select * from T_LOGIC_EQU_IP_PZ t where (sfhs <> '1' or sfhs is null) and  t.ipdz1<{0} and t.ipdz2>{1}", s_ipdz2, s_ipdz1);
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

        private void getIp(ref Int64 ips1, ref Int64 ips2, Int64 ipdz, Int64 ipfd)
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

        protected void IP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IP3.Items.Count > 0)
            {
                GetIPState3();
                InitIpListColor(IP2);
                InitIpListColor(IP3);
            }
        }
        protected void IP3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IP4.Items.Count > 0)
            {
                GetIPState4();
                InitIpListColor(IP2);
                InitIpListColor(IP3);
                InitIpListColor(IP4);
            }
        }

        protected void IPFD_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropIpList(false);

        }


        //#region 获取IP数据
        //private DataRow Gett_Logic_equ_ipData()
        //{
        //    string sql = "select * from t_logic_equ_ip where GUID='" + GUID.Text + "'";
        //    DataSet ds = DataFunction.FillDataSet(sql);
        //    DataRow dr = null;
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        dr = ds.Tables[0].NewRow();
        //        dr["GUID"] = GUID.Text;
        //        if (!string.IsNullOrEmpty(P_GUID.Text))
        //        {
        //            dr["P_GUID"] = P_GUID.Text;
        //        }
        //        dr["IP1"] = P_IP1.Text;
        //        dr["IP2"] = P_IP2.Text;
        //        dr["IP3"] = P_IP3.Text;
        //        dr["IP4"] = P_IP4.Text;
        //        dr["IPFD"] = IPFD.SelectedValue;
        //        dr["CREATEDATETIME"] = DateTime.Now;
        //        ds.Tables[0].Rows.Add(dr);
        //    }
        //    else
        //    {
        //        dr = ds.Tables[0].Rows[0];
        //    }
        //    return dr;
        //}
        //#endregion

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            BindGridPage(BindLogicEquIpGrid());
        }
        protected void YWDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            IPYWLX.Items.Clear();
            ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX", YWDL.SelectedItem.Text,true);
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            DataRow dr = GetT_LOGIC_EQU_IP_PZData();
            ShareFunction.GetControlData(this.Page, dr);
            DataFunction.SaveData(dr.Table.DataSet, "T_LOGIC_EQU_IP_PZ");
//            DataRow ipdr = DataFunction.GetSingleRow(@"select case when p.cn=t.ipdz2-t.ipdz1 then '已分配'  when p.cn>0 then '部分分配' else '未分配' end as fpqk ,
//                                    p.cn, t.*,t.rowid from t_logic_equ_ip t  join
//                                    (select a.guid,sum(b.ipdz2-b.ipdz1) as cn from t_logic_equ_ip a,t_logic_equ_ip_pz b
//                                    where a.ipdz1<=b.ipdz1 and a.ipdz2>=b.ipdz2 and b.guid='" + GUID.Text + "' and (b.sfhs <> '1' or b.sfhs is null) group by a.guid) p on  t.guid=p.guid");

//            DataFunction.ExecuteNonQuery("update t_logic_equ_ip set ipfpzt='" + Convert.ToString(ipdr["fpqk"]) + "' where guid='" + Convert.ToString(ipdr["GUID"]) + "'");
            shareResource.updateIP();
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");

        }
}

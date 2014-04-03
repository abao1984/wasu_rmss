using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceIpSelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            HOUSE_NAME_GUID.Text = Request.QueryString["HOUSE_GUID"];
            HOUSE_NAME.Text = Request.QueryString["HOUSE_NAME"];
            

            //得到用户的访问区域
            string fwqy = Convert.ToString(Session["FWQY"]);
            SSQY.Text = DataFunction.GetStringResult("select branchname from t_sys_branch t where branchcode='" + fwqy + "'");
            SSQY_CODE.Text = fwqy;
            SSQY.Attributes.Add("readonly", "true");
            NAME.Text = Request.QueryString["NAME"];
            ShareFunction.BindEnumDropList(YWDL, "YWDL");
            ShareFunction.BindEnumDropList(IPYWLX, "IPYWLX");
          //  HOUSE_GUID.Text = "71212fa4-9eac-461c-bdda-e3b0fcb38a72";
            GridView1.Attributes.Add("BorderColor", "#5B9ED1");
            GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(GetT_logic_equ_ipData());   
            
        }

    }


    private int GetT_logic_equ_ipData()
    {
        string sql = "select * from t_logic_equ_ip where 1=1 ";
        if(!string.IsNullOrEmpty(YWDL.SelectedValue))
        {
            sql += " and YWDL='"+YWDL.SelectedValue+"'";
        }
        if (SFQW.Checked)
        {
            sql += " and  SFQW='1'";
        }
        else if (!string.IsNullOrEmpty(HOUSE_NAME_GUID.Text))
        {
            sql += " and ssjf_guid='" + HOUSE_NAME_GUID.Text + "'";
        }
       
        if (!string.IsNullOrEmpty(IPYWLX.SelectedValue))
        {
            sql += " and ipywlx='" + IPYWLX.SelectedValue + "'";
        }
        if (!string.IsNullOrEmpty(SSQY_CODE.Text))
        {
            sql += " and SSQY_CODE like '" + SSQY_CODE.Text + "%'";
        }
        if (!string.IsNullOrEmpty(IPDZ.Text))
        {
            sql += " and IP like '" + IPDZ.Text + "%'";
        }
        sql += " order by ip1,ip2,ip3,ip4,ipfd";        
        DataSet ds = DataFunction.FillDataSet(sql);
        DataTable tb = new DataTable("EQU_IP");
        DataColumn dc = new DataColumn("XH", System.Type.GetType("System.String"));
        tb.Columns.Add(dc);
        dc = new DataColumn("IP", System.Type.GetType("System.String"));
        tb.Columns.Add(dc);
        tb.PrimaryKey =new DataColumn[]{dc};
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
           SetEquIpData(tb, dr);
        }
        //if (!string.IsNullOrEmpty(IPDZ.Text))
        //{
        //    tb.DefaultView.RowFilter = "IP like '" + IPDZ.Text + "%'";
        //    //DataRow[] drs = tb.Select();

        //    //GridView1.DataSource = tb.DefaultView;
        //    //GridView1.DataBind();
        //    //return drs.Length;
        //}
       
            GridView1.DataSource = tb.DefaultView;
            GridView1.DataBind();

            return tb.DefaultView.Count;
       
    }

    private void SetEquIpData(DataTable tb,DataRow dr)
    {
        string s_ip1 = dr["IP1"].ToString();
        string s_ip2 = dr["IP2"].ToString();
        string s_ip3 = dr["IP3"].ToString();
        string s_ip4 = dr["IP4"].ToString();
        int ipfd = Convert.ToInt32(dr["IPFD"]);
        int ipdz11 = Convert.ToInt32(s_ip1), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
        int ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
        int zwip2 = 0, zwip3 = 0, zwip4 = 0;
        if (ipfd > 16)
        {
            zwip2 = 255;
            ipdz12 = Convert.ToInt32(s_ip2);
            ipdz22 = ipdz12;
            if (ipfd > 24)
            {
                zwip3 = 255;
                ipdz13 = Convert.ToInt32(s_ip3);
                ipdz23 = ipdz13;
                getIp(ref ipdz14, ref ipdz24, ref zwip4, Convert.ToInt32(s_ip4), 32 - ipfd);
            }
            else
            {
                getIp(ref ipdz13, ref ipdz23, ref zwip3, Convert.ToInt32(s_ip3), 24 - ipfd);
            }
        }
        else if (ipfd > 8)
        {
            getIp(ref ipdz12, ref ipdz22, ref zwip2, Convert.ToInt32(s_ip2), 16 - ipfd);
        }

        
        for (int p2 = ipdz12; p2 <= ipdz22; p2++)
        {
            if (ipdz22 > ipdz12){ ipdz23 = 255;}
            for (int p3 = ipdz13; p3 <= ipdz23; p3++)
            {
                if (ipdz23 > ipdz13) { ipdz24 = 255; }
                for (int p4 = ipdz14; p4 <= ipdz24; p4++)
                {
                    string ip= ipdz11.ToString() + "." + p2.ToString() + "." + p3.ToString()+"." + p4.ToString();
                    if (tb.Rows.Find(ip) == null)
                    {
                        DataRow drIp = tb.NewRow();
                        drIp["XH"] = tb.Rows.Count + 1;
                        drIp["IP"] = ip;
                        tb.Rows.Add(drIp);
                    }
                }
            }
        }
      
    }


    private void getIp(ref int ips1, ref int ips2, ref int zwip, int ipdz, int ipfd)
    {
        int[] ips = new int[8];
        ips[0] = ipdz % 2;
        ips[1] = ipdz / 2 % 2;
        ips[2] = ipdz / 4 % 2;
        ips[3] = ipdz / 8 % 2;
        ips[4] = ipdz / 16 % 2;
        ips[5] = ipdz / 32 % 2;
        ips[6] = ipdz / 64 % 2;
        ips[7] = ipdz / 128 % 2;
        int ip = 0;
        for (int i = 7; i >= ipfd; i--)
        {
            ip += ips[i] * Convert.ToInt32(Math.Pow(2, i));
            zwip += Convert.ToInt32(Math.Pow(2, i));
        }
        ips1 = ip;
        ips2 = ip + Convert.ToInt32(Math.Pow(2, ipfd));
    }


    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridView1.PageIndex = 0;
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
        GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        GetT_logic_equ_ipData();
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
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        GetT_logic_equ_ipData();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(GetT_logic_equ_ipData());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        GetT_logic_equ_ipData();
    }
    #endregion








//    private void BindIpList1()
//    {
//        string sql = "select distinct ip1 as IP from t_logic_equ_ip where ssjf_guid='"+HOUSE_GUID.Text+"' order by ip1";
//        DataSet ds = DataFunction.FillDataSet(sql);
//        IP1.DataSource = ds;
//        IP1.DataBind();
//        BindIpList2();
//    }

//    private void BindIpList2()
//    {
//        string sql =string.Format( @"select * from 
//(select distinct ip2 as IP,
//case when  ipfd>16 then 0
//     when  ipfd>8 then  16-ipfd
//     else 8
//end as ipfd  from t_logic_equ_ip t where ip1='{0}' and ssjf_guid='{1}' ) a order by IP,ipfd", IP1.SelectedValue, HOUSE_GUID.Text);
//        DataSet ds = DataFunction.FillDataSet(sql);
//        InitIpList(IP2, ds);
//        BindIpList3();
//    }

//    private void BindIpList3()
//    {
//        string sql = string.Format(@"
//select * from 
//(select distinct ip3 as ip,
//case when  ipfd>24 then 0
//     when  ipfd>16 then  24-ipfd
//     else 8
//end as ipfd  from t_logic_equ_ip t where ip1='{0}' and ip2='{1}') a order by ip,ipfd", IP1.SelectedValue,IP2.SelectedValue);
//        DataSet ds = DataFunction.FillDataSet(sql);
//        InitIpList(IP3, ds);
//        BindIpList4();
//    }

//    private void BindIpList4()
//    {
//        string sql = string.Format(@"
//select * from 
//(select distinct ip4 as ip,
//case  when  ipfd>24 then  32-ipfd
//     else 8
//end as ipfd  from t_logic_equ_ip t where ip1='{0}' and ip2='{1}' and ip3='{2}') a order by ip,ipfd", IP1.SelectedValue, IP2.SelectedValue,IP3.SelectedValue);
//        DataSet ds = DataFunction.FillDataSet(sql);
//        InitIpList(IP4, ds);
//    }

    private void InitIpList(DropDownList ipList,DataSet ds)
    {
        ipList.Items.Clear();
        if (ds.Tables[0].Rows.Count == 0)
        {
            InsertListItem(ipList, 0, 256);
        }
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            int ip = Convert.ToInt32(dr["IP"]);
            int ipfd = Convert.ToInt32(dr["IPFD"]);
            int con = Convert.ToInt32(Math.Pow(2, ipfd));
            InsertListItem(ipList, ip, con);
        }
    }
    private void InsertListItem(DropDownList ipList,int ip,int con)
    { 
        for (int i = 0; i < con; i++)
        {
            string ipdz = Convert.ToString(ip + i);
            if (ipList.Items.FindByValue(ipdz) == null)
            {
                ListItem li = new ListItem(ipdz,ipdz);
                ipList.Items.Add(li);
            }
        }
    }

    //protected void IP1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindIpList2();
    //}
    //protected void IP2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindIpList3();
    //}
    //protected void IP3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindIpList4();
    //}
    protected void OKButton_Click(object sender, EventArgs e)
    {
        string strIp = "";
        foreach(GridViewRow gr in GridView1.Rows )
        {
            CheckBox ch = (CheckBox)gr.FindControl("XZ");
            if (ch.Checked)
            {
                if (strIp != "")
                {
                    strIp += ",";
                }
                strIp += gr.Cells[2].Text;
            }
        }

        string strScript = string.Format(@"<script> window.close();parent.WindowClose(); parent.document.getElementById('{0}').value = '{1}';</script>"
         , NAME.Text,strIp);
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), strScript);

    }
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(GetT_logic_equ_ipData());       
    }
}

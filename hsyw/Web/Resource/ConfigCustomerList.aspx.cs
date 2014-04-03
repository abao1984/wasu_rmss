using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Web_Resource_ConfigCustomerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
        }
    }


    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        string strwhere = " 1=1 ";
        if (REGION_CODE.Text!="")
        {
            strwhere += " and R.REGION_CODE='" + REGION_CODE .Text+ "'";
        }

        if (CUSTOMER_CODE.Text != "")
        {
            strwhere += " and R.SUBSCRIBER_CODE like '%" + CUSTOMER_CODE.Text + "%'";
        }

        if (CUSTOMER_LEVEL.SelectedValue != "")
        {
            strwhere += " and R.CUSTOMER_LEVEL='" + CUSTOMER_LEVEL.SelectedValue + "'";
        }

        if (CUSTOMER_NAME.Text != "")
        {
            strwhere += " and R.CUSTOMER_NAME like '%" + CUSTOMER_NAME.Text + "%'";
        }

        if (CUSTTYPE1.Text != "")
        {
            strwhere += " and R.CUSTTYPE1 like '%" + CUSTTYPE1.Text + "%'";
        }

        if (CUSTTYPE.Text != "")
        {
            strwhere += " and R.CUSTTYPE like '%" + CUSTTYPE.Text + "%'";
        }

        if (SUB_NAME.Text != "")
        {
            strwhere += " and R.SUB_NAME like '%" + SUB_NAME.Text + "%'";
        }

        if (LINKMAN.Text != "")
        {
            strwhere += " and R.LINKMAN like '%" + LINKMAN.Text + "%'";
        }

        if (ADDRESS.Text != "")
        {
            strwhere += " and R.ADDRESS like '%" + ADDRESS.Text + "%'";
        }
        //光缆
        string sql1 = "select distinct t.*,nvl(t.VJF,nvl(t.JDJF,'')||'  '||nvl(t.YDJF,'')) as JFMC ,R.* from T_CON_LIGHT_BUSINESS t left join T_CON_LIGHT_BUSINESS_CABLE c on t.YWGUID = c.LIGHTGUID left join RMSS R on t.subscriber_id = R.SUBSCRIBER_ID　where  ";
        if (strwhere != "")
        {
            sql1 += strwhere;
        }
        gvLightList.DataSource = DataFunction.FillDataSet(sql1);
        gvLightList.DataBind();
        //传输业务
        string sql2 = @"select distinct t.*,
jd.REGION as REGION,
jd.SUBSCRIBER_CODE as JD_SUBSCRIBER_CODE,
jd.SUB_NAME as JD_SUB_NAME,
yd.SUBSCRIBER_CODE as YD_SUBSCRIBER_CODE,
yd.SUB_NAME as YD_SUB_NAME,
jd.CUSTOMER_LEVEL as JD_CUSTOMER_LEVEL,
jd.LINKMAN as JD_LINKMAN,yd.LINKMAN as YD_LINKMAN, jd.PHONE_NO, jd.MOBILE_NO,
jd.MOBILE_NO||case when jd.MOBILE_NO is not null and jd.MOBILE_NO<> ' ' and jd.PHONE_NO is not null and  jd.PHONE_NO <>' '  then '/' end||jd.PHONE_NO as JD_LINK,
yd.MOBILE_NO||case when yd.MOBILE_NO is not null and yd.MOBILE_NO<>' ' and yd.PHONE_NO is not null and yd.PHONE_NO <>' ' then '/' end||yd.PHONE_NO as YD_LINK
from T_CON_TRANSM_BUSSINESS t
left join T_CON_TRANSM_BUSSINESS_CB c on t.YWGUID = c.YWGUID 
left join rmss jd on t.jd_subscriber_id=jd.SUBSCRIBER_ID
left join rmss yd on t.yd_subscriber_id=yd.SUBSCRIBER_ID
where ";
        if (strwhere!="")
        {
            sql2 += " 1=1 and ((" + strwhere.Replace("R.", "jd.") + ") or (" + strwhere.Replace("R.", "yd.") + "))";
        }
        gvCSYWList.DataSource = DataFunction.FillDataSet(sql2);
        gvCSYWList.DataBind();
        //IP资源
        
        string sql3 = "select distinct IP.*,R.CUSTOMER_NAME,r.customer_code,r.CUSTOMER_LEVEL,r.CUSTTYPE1,r.CUSTTYPE,r.region,r.sub_name,r.LINKMAN from T_CON_LOGIC_EQU_IP IP left join RMSS R on IP.Subscriber_ID = R.SUBSCRIBER_ID left join t_con_logic_equ_vlan t on IP.GUID = t.PK_GUID where  ";
        if (strwhere != "")
        {
            sql3 += strwhere;
        }
        gvLogicEquIp.DataSource = DataFunction.FillDataSet(sql3);
        gvLogicEquIp.DataBind();
    }

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

    protected void gvLightList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string ywguid = gvLightList.DataKeys[e.Row.RowIndex]["YWGUID"].ToString();
            if (!ywguid.Equals(""))
            {
                string ywlx = e.Row.Cells[2].Text;
                if (ywlx.IndexOf("VPN") > -1)
                {
                    e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "','vpn')");
                }
                else
                {
                    e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "','')");
                }

            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }

    protected void gvCSYWList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            //添加甲、乙端联系方式

            string ywguid = gvCSYWList.DataKeys[e.Row.RowIndex]["YWGUID"].ToString();
            if (!ywguid.Equals(""))
            {
                string ywlx = e.Row.Cells[2].Text;
                e.Row.Attributes.Add("ondblclick", "windowOpen('" + ywguid + "')");
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            //e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1 + GridViewPhyResource.PageSize * GridViewPhyResource.PageIndex);
            //e.Row.Cells[e.Row.Cells.Count - 1].Text = "<a href=#  onclick=\"windowOpen('" + strGuid + "','" + strName + "','" + NAME_FILED.Text + "')\">详细</a>";
        }
    }

    
}

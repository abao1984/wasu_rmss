using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_WDGZ_DaiBanGz : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    #region 绑定数据
    private void BindGrid()
    {
        //VPNGGPZ VPN更改配置  T_LCGL_VPNGGPZ
        //IPDZCKYH IP地址出口优化申请单 T_LCGL_IPDZCKYH
        //XKVPN  新开VPN申请单   T_LCGL_XKVPN
        //WLJKSQD  网络镜像监控申请单  T_LCGL_WLJKSQD
        string[] grid = new string[] { "VPNGGPZ:subscriber_id", "IPDZCKYH:guid", "XKVPN:guid", "WLJKSQD:guid" };

        string userName = Convert.ToString(Session["USERNAME"]);

        for (int i = 0; i < grid.Length; i++)
        {
            string GridViewId = grid[i].Split(new char[] { ':' })[0];
            string id = grid[i].Split(new char[] { ':' })[1];
            string tableName = "T_LCGL_" + GridViewId;


            string sql = @"select * from (select A.LCJRZT,A.SFQF,B.USERNAME,C.* from (select lc.pcode,zb.* from t_lcgl_sys_lcjl zb
                                   left join t_lcgl_sys_lckz_cb lc on zb.lcbm=lc.lcbm and zb.lcjrzt=lc.jdbm
                                   where zb.sfqf='0' and zb.lcjrzt<>'GD') A
                             join
                                 (select p.*,ug.username from t_sys_private p 
                                   left join t_sys_r_groupprivate gp on gp.pcode = p.pcode 
                                   left join t_Sys_r_Usergroup ug on ug.groupcode = gp.groupcode) B on A.pcode=b.pcode
                              join(
                             select * from  " + tableName + @" t  
                             left join rmss r on t." + id + @"=r.SUBSCRIBER_ID
                            )  C ON A.Lc_Guid=C.guid) where username='" + userName + "' AND SFQF='0'";

            DataTable dt = DataFunction.FillDataSet(sql).Tables[0];
            ((Literal)Page.FindControl("Lit" + GridViewId)).Text = Convert.ToString(dt.Rows.Count);
            gzcl.BindGridView(((GridView)Page.FindControl(GridViewId)), dt.DataSet);

        };
    }
    #endregion

    //VPN更改配置
    protected void VPNGGPZ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string jdbm = VPNGGPZ.DataKeys[e.Row.RowIndex]["LCJRZT"].ToString();
            string guid = VPNGGPZ.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpenEdit('" + guid + "','../LCGL/VpnGengGaiPeiZhiEdit.aspx?GUID=" + guid + "&JDBM=" + jdbm + "','VPN更改配置')");
        }
    }

    //IP地址出口优化申请单
    protected void IPDZCKYH_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string jdbm =IPDZCKYH.DataKeys[e.Row.RowIndex]["LCJRZT"].ToString();
            string guid = IPDZCKYH.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpenEdit('" + guid + "','../LCGL/IpChuKouYouHuaEdit.aspx?GUID=" + guid + "&JDBM=" + jdbm + "','IP地址出口优化申请单')");
        }
    }

    //新开VPN申请单
    protected void XKVPN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string jdbm = XKVPN.DataKeys[e.Row.RowIndex]["LCJRZT"].ToString();
            string guid = XKVPN.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpenEdit('" + guid + "','../LCGL/XinKaiVpnEdit.aspx?GUID=" + guid + "&JDBM=" + jdbm + "','新开VPN申请单')");
        }
    }

    //网络镜像监控申请单
    protected void WLJKSQD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string jdbm = WLJKSQD.DataKeys[e.Row.RowIndex]["LCJRZT"].ToString();
            string guid = WLJKSQD.DataKeys[e.Row.RowIndex]["GUID"].ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpenEdit('" + guid + "','../LCGL/WangLuoJingXiangEdit.aspx?GUID=" + guid + "&JDBM=" + jdbm + "','网络镜像监控申请单')");
        }
    }
    //刷新
    protected void MenuButton_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}

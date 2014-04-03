using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Web_LCGL_VpnGengGaiPeiZhiEdit : BasePage
{
    private ShareLiuChengGuanLi shareLcgl = new ShareLiuChengGuanLi();
    private string tableName = "T_LCGL_VPNGGPZ";
    private string lcbm = "VPNGGPZ";
    private string firstJdbm = "YWSL";
    private string lastJdbm = "GD";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!this.IsPostBack)
        {
            GUID.Text=Request.QueryString["GUID"];
            JDBM.Text = Request.QueryString["JDBM"];
            GridViewList.Attributes.Add("BorderColor", "#5B9ED1");
            if (GUID.Text == "NEW")
            {
                GUID.Text = Guid.NewGuid().ToString();
            }
            FillPage();
            BindGrid();
            InitPrivate();
            HeadTitle.Text = shareLcgl.GetHeadTitle(lcbm, JDBM.Text);
        }
    }

     #region 初始化权限
    private void InitPrivate()
    {
         string pcode = shareLcgl.GetPcode(lcbm, JDBM.Text);
         if (IsHavePrivate(pcode))
         {
             Tr_Button.Style.Add("display", "block");
         }
    }
     #endregion

    private void BindGrid()
    {
        string sql = string.Format(@"select 
case when t.lcqfbj=0 then '被驳回' when t.lcqfbj=1 then  '已签发' else '待处理' end  as qfbj,
t.lcjrsj,j.jdmc as dqzt,t.lcqfsj,q.jdmc as qfhzt,case when t.lcqfsj is not null then
round((t.lcqfsj-t.lcjrsj),0)||'天'||to_char(mod(round((t.lcqfsj-t.lcjrsj)*24),24),'00')||'时'
||to_char(mod(round((t.lcqfsj-t.lcjrsj)*24*60),60),'00')||'分' end as lcclsj 
,t.lcqfr, t.lcsm from t_lcgl_sys_lcjl t  
left join t_lcgl_sys_lckz_cb j on t.lcbm=j.lcbm and t.lcjrzt=j.jdbm
left join t_lcgl_sys_lckz_cb q on t.lcbm=q.lcbm and t.lcqfzt=q.jdbm where t.lc_guid='{0}' order by t.lcjrsj desc
", GUID.Text);
        DataSet ds= DataFunction.FillDataSet(sql);
        GridViewList.DataSource = ds;
        GridViewList.DataBind();
    }

    private DataRow GetDataRow()
    {
        string sql = "select * from " + tableName + " t where t.guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = GUID.Text;
            dr["CREATEDATETIME"] = DateTime.Now;
            dr["UPDATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        return dr;
    }

    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, GetDataRow());
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        InitContrlDisplay();
        if (JDBM.Text == firstJdbm)
        {
            BackButton.Visible = false;
        }
       
        if (JDBM.Text == lastJdbm)
        {
            SendButton.Visible = false;
        }
        //else
        //{
        //    TR_QUERY.Style.Add("display", "none");
        //}
    }

    private void SaveData()
    {
        DataRow dr = GetDataRow();
        ShareFunction.GetControlData(this.Page, dr);
        DataFunction.SaveData(dr.Table.DataSet, tableName);
        shareLcgl.SaveFirstLczt(GUID.Text, lcbm, JDBM.Text);
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        SaveData();
    } 


    protected void SendButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(SQDBH.Text))
        {
            SQDBH.Text = shareLcgl.GetSqdbh(tableName);
        }
        SaveData();
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>sendPageShow('"+lcbm+"','"+JDBM.Text+"','1');</script>");
    }
    protected void BackButton_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>sendPageShow('"+lcbm+"','" + JDBM.Text + "','0');</script>");

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

    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");

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
    #endregion

    protected void JRFS_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitContrlDisplay();
    }

    private void InitContrlDisplay()
    {
        switch (JRFS.SelectedValue)
        {
            case "交换机改路由器":
                TR_PZHLDZ.Style.Add("display", "block");
                TR_PZLYWD.Style.Add("display", "block");
                TR_LYFFYQ.Style.Add("display", "block");
                TR_PZJHDZWD.Style.Add("display", "none");
                TR_HLDZSFSH.Style.Add("display", "none");
                TR_SSVPN.Style.Add("display", "none");
                TR_YJRFS.Style.Add("display", "none");
                TR_TSSM.Style.Add("display", "block");
                TR_BZ.Style.Add("display", "block");
                break;
            case "路由器改交换机接入":
                TR_PZHLDZ.Style.Add("display", "none");
                TR_PZLYWD.Style.Add("display", "none");
                TR_LYFFYQ.Style.Add("display", "none");
                TR_PZJHDZWD.Style.Add("display", "block");
                TR_HLDZSFSH.Style.Add("display", "block");
                TR_SSVPN.Style.Add("display", "none");
                TR_YJRFS.Style.Add("display", "none");
                TR_TSSM.Style.Add("display", "block");
                TR_BZ.Style.Add("display", "block");
                break;
            case "交换机接入VPN用户业务变更":
                TR_PZHLDZ.Style.Add("display", "none");
                TR_PZLYWD.Style.Add("display", "none");
                TR_LYFFYQ.Style.Add("display", "none");
                TR_PZJHDZWD.Style.Add("display", "block");
                TR_HLDZSFSH.Style.Add("display", "none");
                TR_SSVPN.Style.Add("display", "none");
                TR_YJRFS.Style.Add("display", "none");
                TR_TSSM.Style.Add("display", "block");
                TR_BZ.Style.Add("display", "block");
                break;
            case "路由器接入VPN用户业务变更":
                TR_PZHLDZ.Style.Add("display", "none");
                TR_PZLYWD.Style.Add("display", "block");
                TR_LYFFYQ.Style.Add("display", "block");
                TR_PZJHDZWD.Style.Add("display", "none");
                TR_HLDZSFSH.Style.Add("display", "none");
                TR_SSVPN.Style.Add("display", "none");
                TR_YJRFS.Style.Add("display", "none");
                TR_TSSM.Style.Add("display", "block");
                TR_BZ.Style.Add("display", "block");
                break;
            case "所属VPN（VRF）变更":
                TR_PZHLDZ.Style.Add("display", "none");
                TR_PZLYWD.Style.Add("display", "none");
                TR_LYFFYQ.Style.Add("display", "none");
                TR_PZJHDZWD.Style.Add("display", "none");
                TR_HLDZSFSH.Style.Add("display", "none");
                TR_SSVPN.Style.Add("display", "block");
                TR_YJRFS.Style.Add("display", "block");
                TR_TSSM.Style.Add("display", "block");
                TR_BZ.Style.Add("display", "block");
                break;
        }
    }
}

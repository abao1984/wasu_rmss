using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;

public partial class Web_Resource_ZyPzList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.QueryString["bh"].IsNullOrEmpty())
        {
            TxtBh.Text = Convert.ToString(Request.QueryString["bh"]);
           
        }
        if (!Request.QueryString["ybh"].IsNullOrEmpty())
        {
            TxtYBh.Text = Convert.ToString(Request.QueryString["ybh"]);
        }
        BindGrid();
    }

    #region 绑定数据
    private void BindGrid()
    {
        try
        {
            Action<GridView, string> bind = new Action<GridView, string>((gv, str) =>
            {
                DataTable dt = DataFunction.FillDataSet(str).Tables[0];
                if (dt.IsNullOrEmpty())
                {
                    dt.Rows.Add(dt.NewRow());
                    gv.DataSource = dt;
                    gv.DataBind();
                    int count1 = gv.Columns.Count;
                    gv.Rows[0].Cells.Clear();
                    gv.Rows[0].Cells.Add(new TableCell());
                    gv.Rows[0].Cells[0].Text = "没有相关的信息！";
                    gv.Rows[0].Cells[0].ColumnSpan = count1;
                    gv.Rows[0].Cells[0].Style.Add("text-align", "center");
                }
                else
                {
                    gv.DataSource = dt;
                    gv.DataBind();
                }
            });

            //绑定光缆资源配置
            string sql = @"select distinct t.*,nvl(t.VJF,nvl(t.JDJF,'')||'  '||nvl(t.YDJF,'')) as JFMC ,R.* from T_CON_LIGHT_BUSINESS t left join T_CON_LIGHT_BUSINESS_CABLE c on t.YWGUID = c.LIGHTGUID left join RMSS R on t.subscriber_id = R.SUBSCRIBER_ID
                        where subscriber_code='" + TxtBh.Text + "'";
            bind(GvGl, sql);

            //传输业务配置 
            sql = @"select distinct t.*,
                jd.REGION as REGION,
                jd.SUBSCRIBER_CODE as JD_SUBSCRIBER_CODE,
                jd.SUB_NAME as JD_SUB_NAME,
                yd.SUBSCRIBER_CODE as YD_SUBSCRIBER_CODE,
                yd.SUB_NAME as YD_SUB_NAME
                from T_CON_TRANSM_BUSSINESS t 
                left join T_CON_TRANSM_BUSSINESS_CB c on t.YWGUID = c.YWGUID 
                left join rmss jd on t.jd_subscriber_id=jd.SUBSCRIBER_ID
                left join rmss yd on t.yd_subscriber_id=yd.SUBSCRIBER_ID
                whEre JD_SUBSCRIBER_CODE='" + TxtBh.Text + "' OR YD_SUBSCRIBER_CODE='" + TxtYBh.Text + "'";
            bind(GvCs, sql);
            //IP资源配置
            sql = @"select distinct IP.*,R.* from T_CON_LOGIC_EQU_IP IP left join RMSS R on IP.Subscriber_ID = R.SUBSCRIBER_ID left join t_con_logic_equ_vlan t on IP.GUID = t.PK_GUID
                    where ip.subscriber_code='" + TxtBh.Text + "'";
            bind(GvIp, sql);
        }
        catch (Exception ex)
        {
            this.Alert(ex.ToString());
        }

    }
    #endregion


    #region 行生成事件
    protected void GvGl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridRowBound(e, "YWGUID", "ConfigLightEdit.aspx?YWGUID", GvGl, "光缆资源配置");
        }
    }
    protected void GvCs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridRowBound(e, "YWGUID", "ConfigTransmissionEdit.aspx?YWGUID", GvCs, "传输业务配置");
        }
    }
    protected void GvIp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridRowBound(e, "GUID", "ConfigResourceIpEdit.aspx?GUID", GvIp, "IP资源配置");
        }
    }
    #endregion

    private void GridRowBound(GridViewRowEventArgs e, string id, string url, GridView gv,string title)
    {
        string guid = Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values[id]);
        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
        e.Row.Attributes.Add("ondblclick", "windowOpen('" + url + "=" + guid + "','" + title + "')");

    }
}

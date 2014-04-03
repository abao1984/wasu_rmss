using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_LCGL_WangLuoJingXiangEdit : BasePage
{
    private ShareLiuChengGuanLi shareLcgl = new ShareLiuChengGuanLi();
    private string tableName = "T_LCGL_WLJKSQD";
    private string lcbm = "WLJXJK";
    private string firstJdbm = "YWSL";
    private string lastJdbm = "GD";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            GUID.Text = Request.QueryString["GUID"];
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
        DataSet ds = DataFunction.FillDataSet(sql);
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
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>sendPageShow('" + lcbm + "','" + JDBM.Text + "','1');</script>");
    }
    protected void BackButton_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>sendPageShow('" + lcbm + "','" + JDBM.Text + "','0');</script>");

    }

}

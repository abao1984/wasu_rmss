using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_PuChongEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindDrop();
            InitialControl();
        }
    }

    private void InitialControl()
    {
        //caoguangyao 2014-04-01
        for (int i = 0; i < 4; i++)
        { 
            string v = (i+1)+ @"级";
            GZDJ.Items.Add(new ListItem(v,v));
        }

        string zbguid=Request.QueryString["ZBGUID"];
        string sql = "select * from T_FAU_ZB where ZBGUID='"+zbguid+"'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        dr["GZPCR"] = Session["UserName"];
        dr["GZPCSJ"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        ShareFunction.FillControlData(Page, dr);
        //YWLB.SelectedValue = dr["YWLB"].ToString();
        //GZCC.SelectedValue = dr["GZCC"].ToString();
        //GZLX.SelectedValue = dr["GZLX"].ToString();
        //GZYY.SelectedValue = dr["GZYY"].ToString();
        BindGZCC();
        BindGZYY();
        GZPCR.Text = Session["UserRealName"].ToString();
    }

    private void BindDrop()
    {
        string sql = "select * from T_FAU_LXSZ where LB='ywzt' and SFQY=1";
        DataSet ds = DataFunction.FillDataSet(sql);
        YWZT.DataSource = ds;
        YWZT.DataTextField = "CODENAME";
        YWZT.DataValueField = "CODENAME";
        YWZT.DataBind();
        BindYWLB();
        BindGZLX();
       
    }

    private void BindGZLX()
    {
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='gzlx' and SFQY=1 and t.PARENT_NAME='{0}'", YWZT.SelectedValue);
        GZLX.Items.Clear();
        DataSet dts = DataFunction.FillDataSet(sql);
        GZLX.DataSource = dts;
        GZLX.DataTextField = "codename";
        GZLX.DataValueField = "codename";
        GZLX.DataBind();
    }
    private void BindYWLB()
    {
        YWLB.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywlx' and SFQY=1 and t.PARENT_NAME='{0}'", YWZT.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        YWLB.DataSource = ds;
        YWLB.DataTextField = "codename";
        YWLB.DataValueField = "codename";
        YWLB.DataBind();
    }

    private void BindGZCC()
    {
        string sql = "select * from T_FAU_LXSZ where LB='cc' and SFQY=1 and PARENT_NAME='" + YWLB.SelectedValue + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        GZCC.DataSource = ds;
        GZCC.DataTextField = "CODENAME";
        GZCC.DataValueField = "CODENAME";
        GZCC.DataBind();
    }

    private void BindGZYY()
    {
        string sql = "select * from T_FAU_LXSZ where LB='yy' and SFQY=1 and PARENT_NAME='" + GZLX.SelectedValue + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        GZYY.DataSource = ds;
        GZYY.DataTextField = "CODENAME";
        GZYY.DataValueField = "CODENAME";
        GZYY.DataBind();
    }

    protected void BtnCL_Click(object sender, EventArgs e)
    {
        if (YWZT.SelectedValue == "" || YWLB.SelectedValue == "" || GZCC.SelectedValue == "" || GZLX.SelectedValue == "" || GZYY.Text == "" )
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), @"<script>alert('业务主体、业务类型、故障层次、故障类型、故障原因不能为空，请检查！');</script>");
            return;
        }
        string zbguid = Request.QueryString["ZBGUID"];
        string sql = "select * from T_FAU_ZB where ZBGUID='" + zbguid + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        ShareFunction.GetControlData(Page,dr);
        DataFunction.SaveData(dr.Table.DataSet, "T_FAU_ZB");

        string guid = Guid.NewGuid().ToString();
        sql = "select t.* from t_fau_cllc t where t.guid='" + guid + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dataRow = ds.Tables[0].NewRow();
        dataRow["GUID"] = guid;
        dataRow["ZBGUID"] = ZBGUID.Text;
        dataRow["CLSJ"] = GZPCSJ.Text;
        dataRow["CLBM"] = Session["BranchName"].ToString();
        dataRow["CLRY"] = Session["UserRealName"].ToString();
        dataRow["CLRYID"] = Session["UserID"].ToString();
        dataRow["GZZT"] = "结单";
        dataRow["LCCZ"] = "故障补充";
        dataRow["CLSM"] = BCYJ.Text;
        ds.Tables[0].Rows.Add(dataRow);
        DataFunction.SaveData(ds, "t_fau_cllc");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true'; window.close();</script>");
    }
    protected void BtnQX_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();</script>");
    }
    protected void YWZT_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindYWLB();
        BindGZLX();
        ListItem item = new ListItem("---请选择---", "");
        YWLB.Items.Add(item);
        YWLB.SelectedValue = "";
        GZLX.Items.Add(item);
        GZLX.SelectedValue = "";
    }
    protected void YWLB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGZCC();
        ListItem item = new ListItem("---请选择---", "");
        GZCC.Items.Add(item);
        GZCC.SelectedValue = "";
    }
    protected void GZLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGZYY();
        ListItem item = new ListItem("---请选择---", "");
        GZYY.Items.Add(item);
        GZYY.SelectedValue = "";
    }
}

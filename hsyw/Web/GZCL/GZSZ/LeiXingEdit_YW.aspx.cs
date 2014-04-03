using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_LeiXingEdit_YW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(ShareGZCL));
        if (!Page.IsPostBack)
        {
            LB.Text = Request.QueryString["LB"];
            BindDrop();
            BindDropPARENT_NAME();
            InitialControl();
            
           
        }
    }

    private void BindDrop()
    {
        string sql = string.Format("select CODENAME,GUID from T_FAU_LXSZ where lb='ywzt' and SFQY=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        PARENT_SPECIALTY.DataSource = ds;
        PARENT_SPECIALTY.DataTextField = "CODENAME";
        PARENT_SPECIALTY.DataValueField = "GUID";
        PARENT_SPECIALTY.DataBind();
        ListItem item = new ListItem("---请选择---", "");
        PARENT_SPECIALTY.Items.Add(item);
        //PARENT_SPECIALTY.SelectedValue = "";
    }

    private void BindDropPARENT_NAME()
    {
        string lbs = "ywlx";
        string sql = string.Format("select CODENAME,GUID from T_FAU_LXSZ where lb='{0}' and SFQY=1",lbs);
        if (PARENT_SPECIALTY.SelectedValue != "")
        {
            sql += " and PARENT_ID='" + PARENT_SPECIALTY.SelectedValue + "'";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        PARENT_NAME.Items.Clear();
        PARENT_NAME.DataSource = ds;
        PARENT_NAME.DataTextField = "CODENAME";
        PARENT_NAME.DataValueField = "GUID";
        PARENT_NAME.DataBind();
        ListItem item = new ListItem("---请选择---", "");
        PARENT_NAME.Items.Add(item);
        //PARENT_NAME.SelectedValue = "";
    }
    private void BindDropCengci()
    {
        string lbs = "cc";
        string sql = string.Format("select CODENAME,GUID from T_FAU_LXSZ where lb='{0}' and SFQY=1", lbs);
        if (PARENT_SPECIALTY.SelectedValue != "")
        {
            sql += " and PARENT_ID='" + PARENT_NAME.SelectedValue + "'";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        CengCi.Items.Clear();
        CengCi.DataSource = ds;
        CengCi.DataTextField = "CODENAME";
        CengCi.DataValueField = "GUID";
        CengCi.DataBind();
        ListItem item = new ListItem("---请选择---", "");
        CengCi.Items.Add(item);
        //PARENT_NAME.SelectedValue = "";
    }
    private void InitialControl()
    {
        GUID.Text = Request.QueryString["GUID"];
        LB.Text = Request.QueryString["LB"];
        string sql = "select * from T_FAU_LXSZ where GUID='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid().ToString();
            dr["SFQY"] = 1;
            dr["LB"] = LB.Text;
            ds.Tables[0].Rows.Add(dr);
        }
        ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
        //sql = "select t.codeid from t_fau_lxsz t where t.codename in select t.codename from t_fau_lxsz h where h.parent_name='" + PARENT_NAME.Text + "'";
        //CODENAME.Attributes.Add("onchange", "CheckAndGetCode(this);");
        if (LB.Text == "gzlx_yw")
        {
            this.Title = "故障类型";
            Label1.Text = "故障类型描述";
            Label2.Text = "原因描述";
            Label3.Text = "业务类型";
            Label4.Text = "故障层次";
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (PARENT_SPECIALTY.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择业务主体！')</script>");
            return;
        }
        if (PARENT_NAME.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择故障类型/业务类型！')</script>");
            return;
        }
        if (CengCi.SelectedValue == "")
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择故障层次！')</script>");
            return;
        }


        string sql = "select * from T_FAU_LXSZ where GUID='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = GUID.Text;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        ShareFunction.GetControlData(Page,dr);
        dr["PARENT_NAME"] = PARENT_NAME.SelectedItem.Text + "/" + CengCi.SelectedItem.Text;
        dr["PARENT_ID"] = PARENT_NAME.SelectedValue;
        dr["PARENT_SPECIALTY"] = PARENT_SPECIALTY.SelectedItem.Text;
        
        DataFunction.SaveData(ds, "T_FAU_LXSZ");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('保存成功！');window.returnValue='true'; window.close();</script>");
    }
    protected void BtnCZ_Click(object sender, EventArgs e)
    {
        InitialControl();
    }
    protected void BtnGB_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();</script>");
    }

    protected void PARENT_SPECIALTY_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropPARENT_NAME();
    }
    protected void PARENT_NAME_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropCengci();
    }
}

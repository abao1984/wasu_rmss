using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_CengCiEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(ShareGZCL));
        if (!Page.IsPostBack)
        {
            LB.Text = Request.QueryString["LB"];
            BindDrop();
            InitialControl();
        }
    }

    private void BindDrop()
    {

        string sql = string.Format(@"select CODENAME,GUID from T_FAU_LXSZ where lb='ywzt' and SFQY=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        PARENT_NAME.DataSource = ds;
        PARENT_NAME.DataTextField = "CODENAME";
        PARENT_NAME.DataValueField = "GUID";
        PARENT_NAME.DataBind();
        ListItem item = new ListItem("", "");
        PARENT_NAME.Items.Add(item);
        PARENT_NAME.SelectedValue = "";
    }

    private void InitialControl()
    {
        GUID.Text = Request.QueryString["GUID"];
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
        if (LB.Text == "ywlx")
        {
            this.Title = "业务类型";
            Label1.Text = "业务类型";
            Label2.Text = "描述";
            Label3.Text = "业务主体";
        }
        else if (LB.Text == "gzlx")
        {
            this.Title = "故障类型";
            Label1.Text = "故障类型";
            Label2.Text = "描述";
            Label3.Text = "业务主体";
        }
        //CODENAME.Attributes.Add("onchange", "CheckAndGetCode(this);");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
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
        dr["PARENT_NAME"] = PARENT_NAME.SelectedItem.Text;
        dr["PARENT_ID"] = PARENT_NAME.SelectedValue;
        if (PARENT_NAME.SelectedValue=="")
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择业务类别！')</script>");
            return;
        }
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
}

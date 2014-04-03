using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_JinChengGengZhong : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindDropZT();
            InitialControl();
        }
    }

    private void InitialControl()
    {
        string guid = Guid.NewGuid().ToString();
        string sql = "select t.* from t_fau_cllc t where t.guid='" + guid + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        ShareFunction.FillControlData(Page, dr);
        GUID.Text = guid;
        ZBGUID.Text = Request.QueryString["id"];
        CLBM.Text = Session["BranchName"].ToString();
        CLRY.Text = Session["UserRealName"].ToString();
        
    }


    private void BindDropZT()
    {
        ZT.Items.Clear();
        string sql = "select t.* from t_fau_enumdata t where t.enum_sort='GZZT'";
        ZT.DataSource = DataFunction.FillDataSet(sql);
        ZT.DataTextField = "enum_name";
        ZT.DataValueField = "enum_name";
        ZT.DataBind();
    }

    protected void BtnQX_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }

    protected void BtnCL_Click(object sender, EventArgs e)
    {
       
        //操作记录
        string sql = "select t.* from t_fau_cllc t where t.guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = ds.Tables[0].NewRow();
        ShareFunction.GetControlData(Page, dr);
        dr["clsj"] = DateTime.Now;
        dr["LCCZ"] = "进程跟踪";
        dr["LCZT"] = Request.QueryString["lczt"];
        dr["GZZT"] = ZT.SelectedValue;
        ds.Tables[0].Rows.Add(dr);
        DataFunction.SaveData(dr.Table.DataSet, "t_fau_cllc");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('处理成功！');parent.WindowClose();</script>");
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        BindDropZT();
    }
}

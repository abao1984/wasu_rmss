using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Web_INFO_IpDiZhiBeiAnEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            GUID.Text = Convert.ToString(Request.QueryString["id"]);
            TxtLx.Text = Convert.ToString(Request.QueryString["lx"]);
            BinDropDown();
            BindGrid();
        }
    }

    #region 绑定DropDownList
    private void BinDropDown()
    {
        Action<DropDownList, string> bind = new Action<DropDownList, string>((ddl, lx) =>
        {
            string sql = "select enum_name from T_RES_SYS_ENUMDATA where ENUM_SORT='" + lx + "'";
            DataTable dt = DataFunction.FillDataSet(sql).Tables[0];
            DataRow dr = dt.NewRow();
            dr["ENUM_NAME"] = "";
            dt.Rows.InsertAt(dr, 0);
            ddl.DataSource = dt;
            ddl.DataTextField = "ENUM_NAME";
            ddl.DataValueField = "ENUM_NAME";
            ddl.DataBind();
        });
        bind(SYFF, "IPDZBZSYFF");
       
    }
    #endregion

    #region 绑定数据
    private void BindGrid()
    {
        if (!string.IsNullOrEmpty(GUID.Text))
        {
            string sql = "select guid, qsipdz, zzipdz, syff, syrq, dwmc, dwfl, dwhyfl, dwszs, dwszshi, dwszx, dwdz, lxrmc, lxrdh, lxrdzyj, wgipdz, drrq, wgszdz from t_info_ipdzba where guid='" + GUID.Text + "'";
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            }
        }
    }
    #endregion

    #region 保存方法
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = "select guid, qsipdz, zzipdz, syff, syrq, dwmc, dwfl, dwhyfl, dwszs, dwszshi, dwszx, dwdz, lxrmc, lxrdh, lxrdzyj, wgipdz, drrq, wgszdz from t_info_ipdzba where guid='" + GUID.Text + "'";
            DataSet ds = DataFunction.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                GUID.Text = System.Guid.NewGuid().ToString().ToUpper();
                DataRow dr = ds.Tables[0].NewRow();
                ds.Tables[0].Rows.Add(dr);
            }
            ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
            DataFunction.SaveData(ds, "T_INFO_IPDZBA");
         
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('保存成功！')</script>");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('保存失败！')</script>");
        }
    }
    #endregion

    #region 刷新方法
    protected void Btn_Click(object sender, EventArgs e)
    {
        DropDownList ddl = Page.FindControl(DDLID.Text) as DropDownList;
        string sv = ddl.SelectedValue;
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_RES_SYS_ENUMDATA where enum_sort = '{0}' order by sequence", DDLLX.Text));
        if (ds.Tables[0].Select("ENUM_NAME = '" + sv + "'").Length == 0)
        {
            sv = "";
        }
        ddl.DataSource = ds;
        ddl.DataTextField = "ENUM_NAME";
        ddl.DataValueField = "ENUM_NAME";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("", ""));
        ddl.SelectedValue = sv;
    }
    #endregion

   
}

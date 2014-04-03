using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_JianKongSbAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Request.QueryString["guid"].IsNullOrEmpty())
            {
                GUID.Text = Convert.ToString(Request.QueryString["guid"]);
            }
            if (!Request.QueryString["lsid"].IsNullOrEmpty())
            {
                LSID.Text = Convert.ToString(Request.QueryString["lsid"]);
            }
            if (!Request.QueryString["khbh"].IsNullOrEmpty())
            {
                KHBH.Text = Convert.ToString(Request.QueryString["khbh"]);
            }
            BinDropDown();
            BindGrid();

            //自动动生成通道号 罗耀斌
            if (TDH.Text.IsNullOrEmpty())
            {
                //如果通道号有多个，用"-"连接起来
                //LSIDj是t_con_jkpzb表的GUID
                string sql = "select TDBH from T_RES_CHILD_JKTD where ssjksb_guid in (select JKSBID from t_con_jkpzb t where t.guid='" + LSID.Text + "')";
                DataTable dt = DataFunction.FillDataSet(sql).Tables[0];
                string tdh = dt.Rows.Cast<DataRow>().JoinString("-",dr=>Convert.ToString(dr["TDBH"]));
                TDH.Text = tdh;
            }

        }
    }

    #region 绑定数据方法
    private void BindGrid()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_CON_JKPZB_CB where guid='"+GUID.Text+"' ");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
        }
    }
    #endregion

    #region  绑定DropDownList方法
    private void BinDropDown()
    {
        Action<DropDownList, string> bind = new Action<DropDownList, string>((ddl,lx) => {
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
        bind(SXJLX, "JKPZDSXJLX");
        bind(SXJXH, "JKPZDSXJXH");
        bind(SCCS, "JKPZDSCCS");
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

    #region 保存方法
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_CON_JKPZB_CB where guid='" + GUID.Text + "' ");
        if (ds.Tables[0].Rows.Count == 0)
        {
            GUID.Text = Guid.NewGuid().ToString().ToUpper();
            DataRow dr = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(dr);
        }
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "T_CON_JKPZB_CB");

    }
    #endregion
}

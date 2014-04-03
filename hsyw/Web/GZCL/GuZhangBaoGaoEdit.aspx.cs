using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangBaoGaoEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            GZBH.Text = Request.QueryString["GZBH"];
            BindDrop();
            FillPage();
        }
    }

    private void BindDrop()
    {
        string sql = "select codename from T_FAU_LXSZ t where t.lb='GZLY' and SFQY=1";
        GZLY.DataSource = DataFunction.FillDataSet(sql);
        GZLY.DataTextField = "codename";
        GZLY.DataValueField = "codename";
        GZLY.DataBind();
        ListItem items = new ListItem("----请选择----","");
        GZLY.Items.Add(items);
        GZLY.SelectedValue = "";
        sql = "select codename from T_FAU_LXSZ t where t.lb='ywzt' and SFQY=1";
        GZZY.DataSource = DataFunction.FillDataSet(sql);
        GZZY.DataTextField = "codename";
        GZZY.DataValueField = "codename";
        GZZY.DataBind();
    }

    private void FillPage()
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_FAU_GZBG where GZBH = '{0}'",GZBH.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            if (!KSSJ.Text.Equals(""))
            {
                KSSJ.Text = Convert.ToDateTime(KSSJ.Text).ToString("yyyy-MM-dd HH:mm");
            }
            if (!JSSJ.Text.Equals(""))
            {
                JSSJ.Text = Convert.ToDateTime(JSSJ.Text).ToString("yyyy-MM-dd HH:mm");
            }
        }
        else
        {
            DataSet ds1 = DataFunction.FillDataSet(string.Format("select t.* as  from T_FAU_ZB t  where GZBH = '{0}'", GZBH.Text));
            DataRow dr1 = ds1.Tables[0].Rows[0];
            DataRow dr = ds.Tables[0].NewRow();
            GZBGGUID.Text = Guid.NewGuid().ToString();
            SLR.Text = Session["UserName"].ToString();
            GZLY.Text = (dr1["GZLY"] == DBNull.Value?"":dr1["GZLY"].ToString());
            GZZY.Text = (dr1["YWZT"] == DBNull.Value ? "" : dr1["YWZT"].ToString());
            CLR.Text = (dr1["GZYYR"] == DBNull.Value ? "" : dr1["GZYYR"].ToString());
            BM.Text = (dr1["YYRBM"] == DBNull.Value ? "" : dr1["YYRBM"].ToString());
            if (dr1["TSSJ"] != DBNull.Value)
            {
                KSSJ.Text = Convert.ToDateTime(dr1["TSSJ"]).ToString("yyyy-MM-dd HH:mm");
            }
            if(dr1["JDSJ"] != DBNull.Value)
            {
                JSSJ.Text = Convert.ToDateTime(dr1["JDSJ"]).ToString("yyyy-MM-dd HH:mm");
            }
            GZMS.Text = dr1["GZMS"].ToString();
            ZT.Text = "保留";
        }
        
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_FAU_GZBG where GZBH = '{0}'", GZBH.Text));
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["CJSJ"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        ShareFunction.GetControlData(Page, dr);
        DataFunction.SaveData(ds, "T_FAU_GZBG");
    }
 
}

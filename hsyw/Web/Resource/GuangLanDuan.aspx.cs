using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_GuangLanDuan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            JFMC_GUID.Text = Request.QueryString["HOUSE_GUID"];
            JFMC.Text = Request.QueryString["HOUSE_NAME"];
            GUID.Text = Request.QueryString["GUID"];
            LIGHTGUID.Text = Request.QueryString["LIGHTGUID"];
            YWBM.Text = Request.QueryString["YWBM"];
            YHMC.Text = Request.QueryString["yhmc"];
            GLDMC_CODE.Attributes.Add("readonly", "true");
            GLDMC.Attributes.Add("readonly", "true");
            GXH.Attributes.Add("readonly", "true");
            FillPage();
        }
    }
    private void FillPage()
    {
        if (string.IsNullOrEmpty(GUID.Text))
        {
            GUID.Text = Guid.NewGuid().ToString();
            LB.Text = Request.QueryString["LB"];
           
            string strSql = string.Format("select count(*) from T_CON_LIGHT_BUSINESS_CABLE where LIGHTGUID = '{0}' and LB = '{1}'  ", LIGHTGUID.Text, LB.Text);
            GLDXH.Text = (DataFunction.GetIntResult(strSql)+1).ToString();
        }
        else 
        {
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS_CABLE where GUID = '{0}'", GUID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
                OLD_GXH_GUID.Text = GXH_GUID.Text;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('您打开的页面没有数据！');</script>");
                BtnSave.Enabled = false;
            }
        }
        
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS_CABLE where GUID = '{0}'", GUID.Text));
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        if (OLD_GXH_GUID.Text != GXH_GUID.Text && !string.IsNullOrEmpty(OLD_GXH_GUID.Text))
        {
            SetResChildCoreZt(OLD_GXH_GUID.Text, "未使用");
        }
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "T_CON_LIGHT_BUSINESS_CABLE");
        //FillPage();
        //光芯 保存业务编码 T_RES_CHILD_CORE
        SetResChildCoreZt(GXH_GUID.Text, "已使用");

        DataFunction.ExecuteNonQuery(string.Format(@"update T_RES_EQU_LIGHT_CABLE t set t.ywbm='{0}' where t.GLDBH='{1}'", YWBM.Text + "/" + GXH.Text, GLDMC_CODE.Text));
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.close();parent.WindowClose();</script>");
    }

    private void SetResChildCoreZt(string gxhGuid,string gxzt)
    {
        string sql = string.Format("update t_res_child_core t set t.gxzt='{1}',ywbm='{2}',yhmc='{3}' where t.guid in ('{0}') ", gxhGuid.Replace(",", "','"), gxzt, YWBM.Text,YHMC.Text);
        DataFunction.ExecuteNonQuery(sql);
    }

}

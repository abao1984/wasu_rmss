using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigTransmissionEdit1 : System.Web.UI.Page
{
    private ShareResource shareResource = new ShareResource();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetContrlReadonly();
            ZYHS_BJ.Text = Request.QueryString["ZYHS_BJ"];
            JRSB_UNIT_ID.Text = "9e2393f1-931d-4b14-b44f-0ba9ff846853,64602091-d4fe-4c89-ac6a-52f6acdd836d,41d1081d-7925-485b-996c-72f4519c7898";
            GUID.Text = Request.QueryString["GUID"];
            FillPage();
        }
    }

    private void SetContrlReadonly()
    {
        JDJRJF.Attributes.Add("readonly", "true");
        JDJRSB.Attributes.Add("readonly", "true");
        JDSBDK.Attributes.Add("readonly", "true");

        YDJRJF.Attributes.Add("readonly", "true");
        YDJRSB.Attributes.Add("readonly", "true");
        YDSBDK.Attributes.Add("readonly", "true");
    }

    private void FillPage()
    {
        if (GUID.Text == "NEW")
        {
            GUID.Text = Guid.NewGuid().ToString();
            YWGUID.Text = Request.QueryString["YWGUID"];
            BH.Text = GetBH().ToString();
            
        }
        else
        {
            DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_TRANSM_BUSSINESS_CB where GUID = '{0}'", GUID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('您打开的页面没有数据！');</script>");
                BtnSave.Enabled = false;
            }
            //BtnSave.Enabled = false;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_TRANSM_BUSSINESS_CB where GUID = '{0}'", GUID.Text));
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        DataRow DR = ds.Tables[0].Rows[0];
        string strPortGuid = DR["JDSBDK_GUID"].ToString() + "," + DR["YDSBDK_GUID"].ToString() + "," + JDSBDK_GUID.Text + "," + YDSBDK_GUID.Text;
        
        string strSql = string.Format(@"select case when t.dkzt='启用' and t.sfkfy=0 then DKBM end as DKBM from t_res_child_port t where guid in ('{0}','{1}') ", JDSBDK_GUID.Text, YDSBDK_GUID.Text);
        string dkmc = "";
        DataSet dts = DataFunction.FillDataSet(strSql);
        foreach (DataRow dtr in dts.Tables[0].Rows)
        {
            if (!dtr["DKBM"].ToString().IsNullOrEmpty() && dtr["DKBM"].ToString() != DR["JDSBDK"].ToString() && dtr["DKBM"].ToString() != DR["YDSBDK"].ToString())
            {
                dkmc += dtr["DKBM"].ToString() + ",";
            }
        }
        if (dkmc!="")
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('端口" + dkmc .Substring(0,dkmc.Length-1)+ "已被暂用，请重新选择!');</script>");
            return;
        }
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "T_CON_TRANSM_BUSSINESS_CB");
        shareResource.SetResourcePort(strPortGuid);
        FillPage();
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }
    private int GetBH()
    {
        int bh = DataFunction.GetIntResult("select BH from T_CON_TRANSM_BUSSINESS_CB order by BH desc");
        if (bh == -1)
        {
            bh = 1;
        }
        else
        {
            ++bh;
        }
        return bh;
    }

    private void SetResChildPortZt(string dkGuid, string dkzt)
    {
        string sql = string.Format("update t_res_child_port t set t.dkzt='{1}' where t.guid in ('{0}') ",
            dkGuid.Replace(",", "','"), dkzt);
        DataFunction.ExecuteNonQuery(sql);
    }
}

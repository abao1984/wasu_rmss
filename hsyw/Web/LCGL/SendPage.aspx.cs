using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_LCGL_SendPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LC_GUID.Text = Request.QueryString["LC_GUID"];
            LCZT.Text = Request.QueryString["LCZT"];
            LCBM.Text = Request.QueryString["LCBM"];
            LCQFBJ.Text = Request.QueryString["LCQFBJ"];
            if (LCQFBJ.Text == "1")
            {
                Title.Text = "签发意见";
            }
            else
            {
                Title.Text = "驳回意见";
            }
            BindSendRadio();
        }
    }

    protected void CloseButton_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(),Guid.NewGuid().ToString(),
            "<script>window.close();</script>");
    }

    protected void SendButton_Click(object sender, EventArgs e)
    {
        Send();
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(),
           "<script>window.close();window.returnValue='OK';</script>");
    }

    private void Send()
    {
        string sql = string.Format("select t.* from t_lcgl_sys_lcjl t where t.lc_guid='{0}' and  lcjrzt='{1}' and sfqf=0",
            LC_GUID.Text,LCZT.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow drt = null;
        if (ds.Tables[0].Rows.Count > 0)
        {
            drt = ds.Tables[0].Rows[0];
        }
        else
        {
            drt = ds.Tables[0].NewRow();
            drt["GUID"] = Guid.NewGuid().ToString();
            drt["LC_GUID"] = LC_GUID.Text;
            drt["LCJRSJ"] = DateTime.Now;
            ds.Tables[0].Rows.Add(drt);           
        }
        drt["LCQFSJ"] = DateTime.Now;
        drt["LCBM"] = LCBM.Text;
        drt["LCQFZT"] = XYJDBM.Text;
        drt["LCQFR"] = GetUserName();
        drt["LCQFBJ"] = LCQFBJ.Text;
        drt["LCSM"] = GetObjectValue(LCSM.Text);
        drt["SFQF"] = 1;
        drt["IPDZ"] = GetClientIP();
        foreach (ListItem li in SendRadio.Items)
        {
            if (li.Selected || QFLX.Text == "BXQF")
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["GUID"] = Guid.NewGuid().ToString();
                dr["LC_GUID"] = LC_GUID.Text;
                dr["LCJRSJ"] = DateTime.Now;
                dr["LCBM"] =LCBM.Text;
                dr["LCJRZT"] = li.Value;
                dr["SFQF"] = 0;
                ds.Tables[0].Rows.Add(dr);
            }
        }
        DataFunction.SaveData(ds, "T_LCGL_SYS_LCJL");
    }

    private object GetObjectValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Convert.DBNull;
        }
        else
        {
            return value;
        }
    }

  


    private void BindSendRadio()
    {
        SendRadio.Items.Clear();
        string sql = string.Format("select t.* from t_lcgl_sys_lckz_cb t where t.lcbm='{0}' and t.jdbm='{1}'",
            LCBM.Text, LCZT.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            if (LCQFBJ.Text=="1")
            {
                XYJDBM.Text = dr["XYJDBM"].ToString();
            }
            else
            {
                XYJDBM.Text = dr["BHJDBM"].ToString();
            }
            QFLX.Text = dr["QFLX"].ToString();
            switch (QFLX.Text)
            {
                case "DXQF":
                    ListItem li = new ListItem(GetJdmc(XYJDBM.Text), XYJDBM.Text);
                    SendRadio.Items.Add(li);
                    li.Selected = true;
                    break;
                case "FZQF":
                case "BXQF":
                    string[] xylcbms = XYJDBM.Text.Split(',');
                    foreach (string s in xylcbms)
                    {
                        ListItem lis = new ListItem(GetJdmc(s),s );
                        SendRadio.Items.Add(lis);
                    }
                    break;
            }
        }
    }

    private string GetJdmc(string jdbm)
    {
        string sql =string.Format("select jdmc from t_lcgl_sys_lckz_cb t where t.lcbm='{0}' and t.jdbm='{1}'",
            LCBM.Text,jdbm);
        return DataFunction.GetStringResult(sql);
    }
}

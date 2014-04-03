using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_LogicResourceVpnEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GUID.Text=Request.QueryString["GUID"];
            InitialControl();
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_logic_equ_vpn t where t.guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["GUID"] = GUID.Text;
            dr["CREATEDATETIME"] = CREATEDATETIME.Text;
            ds.Tables[0].Rows.Add(dr);
        }
        ShareFunction.GetControlData(Page,ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "t_logic_equ_vpn");
    }
    protected void DelButton_Click(object sender, EventArgs e)
    {
        string sql = "delete t_logic_equ_vpn where guid='"+GUID.Text+"'";
        DataFunction.ExecuteNonQuery(sql);
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script> window.close();parent.WindowClose();</script>");
    }

    private void InitialControl()
    {
        string sql = "select * from t_logic_equ_vpn t where t.guid='"+GUID.Text+"'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if(ds.Tables[0].Rows.Count==0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid().ToString();
            dr["CREATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
    }


}

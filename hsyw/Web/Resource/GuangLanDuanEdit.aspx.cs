using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_GuangLanDuanEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GUID.Text = Convert.ToString(Request.QueryString["GUID"]);
            LSID.Text = Convert.ToString(Request.QueryString["LSID"]);
            FillPage();
        }
    }
    private void FillPage()
    {
        if (!string.IsNullOrEmpty(GUID.Text))
        {

            DataSet ds = DataFunction.FillDataSet(string.Format("select * from t_res_gld where GUID = '{0}'", GUID.Text));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
            }
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from t_res_gld where GUID = '{0}'", GUID.Text));
        if (ds.Tables[0].Rows.Count == 0)
        {
            GUID.Text = System.Guid.NewGuid().ToString().ToUpper();
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
       
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "t_res_gld");
        FillPage();

    }
}

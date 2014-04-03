using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_WDGZ_GeRenSz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControl();
        }
    }

    void BindControl()
    {
        DataTable dt = DataFunction.FillDataSet("select * from t_sys_user t where username='" + Convert.ToString(Session["USERNAME"]) + "'").Tables[0];
        if (dt.Rows.Count > 0)
        {
            ShareFunction.FillControlData(Page, dt.Rows[0]);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = DataFunction.FillDataSet("select * from t_sys_user t where username='" + Convert.ToString(Session["USERNAME"]) + "'").Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            ShareFunction.GetControlData(Page, dt.Rows[0]);
            DataFunction.SaveData(dt.DataSet, "T_SYS_USER");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

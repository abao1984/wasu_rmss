using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GZSZ_ZhuanYeEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(ShareGZCL));
        if(!Page.IsPostBack)
        {
            InitialControl();
        }
    }

    private void InitialControl()
    {
        GUID.Text = Request.QueryString["GUID"];
        LB.Text = Request.QueryString["LB"];
        string sql = "select * from T_FAU_LXSZ where GUID='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid().ToString();
            dr["SFQY"] = 1;
            dr["LB"] = LB.Text;
            ds.Tables[0].Rows.Add(dr);
        }
        ShareFunction.FillControlData(Page, ds.Tables[0].Rows[0]);
       // CODENAME.Attributes.Add("onchange", "CheckAndGetCode(this);");
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string sql = "select * from T_FAU_LXSZ where GUID='" + GUID.Text + "'";
         DataSet ds = DataFunction.FillDataSet(sql);
         if (ds.Tables[0].Rows.Count == 0)
         {
             DataRow dr = ds.Tables[0].NewRow();
             dr["GUID"] = GUID.Text;
             ds.Tables[0].Rows.Add(dr);
         }
        ShareFunction.GetControlData(Page,ds.Tables[0].Rows[0]);
        DataFunction.SaveData(ds, "T_FAU_LXSZ");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true'; window.close();</script>");
    }
    protected void BtnCZ_Click(object sender, EventArgs e)
    {
        InitialControl();
    }
    protected void BtnGB_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();</script>");
    }



    //protected void CopyBtn_Click(object sender, EventArgs e)
    //{
        
    //    string codename = CODENAME.Text;
    //    string Codeid = ShareFunction.hz2py(codename);
    //    string oldCodename = DataFunction.GetStringResult("select CODENAME from t_fau_lxsz t where GUID='" + GUID.Text + "'");
    //    if (oldCodename != codename)
    //    {

    //        string[] sql = new string[3];
    //        sql[0] = "insert into  T_FAU_LXSZ select sys_guid(),CODEID,codename,MS,SFQY,LB,parent_name,'" + codename + "' parent_specialty  from t_fau_lxsz t where parent_name in(select CODENAME from t_fau_lxsz t where parent_name='" + oldCodename + "')";
    //        sql[1] = "insert into  T_FAU_LXSZ select sys_guid(),CODEID,codename,MS,SFQY,LB,'" + codename + "' parent_name,parent_specialty from T_FAU_LXSZ where parent_name ='" + oldCodename + "'";
    //        sql[2] = "insert into  T_FAU_LXSZ select sys_guid(),CODEID,'" + codename + "',MS,SFQY,LB,parent_name, parent_specialty from T_FAU_LXSZ where CODENAME ='" + oldCodename + "'";

    //        DataFunction.ExecuteTransaction(sql);
    //    }
    //    else
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('该代码名己经存在！');</script>");
    //    }
    //}
}

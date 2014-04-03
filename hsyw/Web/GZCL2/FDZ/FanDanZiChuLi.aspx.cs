using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_FDZ_FanDanZiChuLi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            InitialControl();
        }
    }

    private void InitialControl()
    {
        tr_ldlx.Visible = false;
        ZBGUID.Text = Request.QueryString["ZBGUID"];
        CLBM.Text = Session["BranchName"].ToString();
        CLR.Text = Session["UserRealName"].ToString();
        //处理类型
        string CLtype = Request.QueryString["Type"];
        switch(CLtype)
        {
            case "BL":
                this.Title = "留单处理";
                BtnCL.Text = "留单";
                tr_ldlx.Visible = true;
                break;
            case "YL":
                this.Title = "遗留单处理";
                BtnCL.Text="遗留";
                break;
            case "SFD":
                this.Title = "送调度发单";
                BtnCL.Text = "送调度发单";
                break;
            case "FHD":
                this.Title = "返单处理";
                BtnCL.Text = "返单";
                break;
        }
    }

    protected void BtnCL_Click(object sender, EventArgs e)
    {
        //处理类型
        string CLtype = Request.QueryString["Type"];
        string UserId = Session["UserID"].ToString();
        GUID.Text=Guid.NewGuid().ToString();
        string sql = "select * from t_fau_zb2 where zbguid='"+ZBGUID.Text+"'";
        DataRow dataRow = DataFunction.GetSingleRow(sql);
        dataRow["LDSJ"] = DBNull.Value;
        sql = "select t.* from t_fau_cllc2 t where t.guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = ds.Tables[0].NewRow();
        dr["GUID"] = GUID.Text;
        dr["ZBGUID"] = ZBGUID.Text;
        dr["CLSJ"] = DateTime.Now;
        dr["CLBM"] = CLBM.Text; ;
        dr["CLRY"] = CLR.Text;
        dr["CLRYID"] = UserId;
        dr["CLSM"] = CLSM.Text;
        
        
        dataRow["SFSD"] = "1";
        dataRow["SDRY"] = "";
        switch (CLtype)
        {
            case "BL":
                dataRow["LDLX"] = LDLX.SelectedValue;
                dataRow["LDSJ"] = DateTime.Now;
                dataRow["fdzzt"] = "电话处理";
                dr["LCCZ"] = LDLX.SelectedValue;
                dr["GZZT"] = "留单";
                dr["LCZT"] = Request.QueryString["lczt"];
                break;
            case "YL":
                dataRow["fdzzt"] = "遗单";
                dataRow["LDSJ"] = DateTime.Now;
                dataRow["FDYLSM"] = CLSM.Text;
                dr["LCCZ"] = "遗单";
                dr["GZZT"] = "遗单";
                dr["LCZT"] = Request.QueryString["lczt"];
                break;
            case "SFD":
                dataRow["fdzzt"] = "调度发单";
                dataRow["DHSLSJ"] = DateTime.Now;
                dr["LCCZ"] = "送调度发单";
                dr["GZZT"] = "调度发单";
                dr["LCZT"] = Request.QueryString["lczt"];
                break;
            case "FHD":
                dataRow["fdzzt"] = "调度发单";
                dr["GZZT"] = "调度发单";
                dr["LCCZ"] = "返调度发单";
                //这个是以前人做的，SQL语句写的有问题，看了下代码只是把实现处理人置上而已  罗耀斌
//                dr["SJCLRY"] = DataFunction.GetStringResult(string.Format(@"select t.SJCLRY
//  from t_fau_cllc2 t
// where t.zbguid = '{0}' and t.
//   and t.LCCZ = '调度发单'
//       (select max(clsj)
//          from t_fau_cllc2
//         where zbguid = '{0}')", ZBGUID.Text));
                dr["SJCLRY"] = DataFunction.GetSingleRow("select t.sjclry from t_fau_cllc2 t where t.zbguid='"+ZBGUID.Text+"' order by clsj desc")["SJCLRY"];
                break;
        }
        DataFunction.SaveData(dataRow.Table.DataSet, "t_fau_zb2");
        ds.Tables[0].Rows.Add(dr);
        DataFunction.SaveData(ds, "t_fau_cllc2");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true'; window.close();</script>");
    }
    protected void BtnQX_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.close();</script>");
    }
}

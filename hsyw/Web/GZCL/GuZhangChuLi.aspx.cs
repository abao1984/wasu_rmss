using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangChuLi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            //BindGrop();
            InitialControl();
        }
    }

    private void InitialControl()
    {
        //处理类型
        CLtype.Text = Request.QueryString["Type"];
        CLBM.Text= Session["BranchName"].ToString();
        CLR.Text=Session["UserRealName"].ToString();
        ZBGUID.Text=Request.QueryString["ZBGUID"];
        tr_gzzt.Style.Add("display", "none");
        tr_gzzt.Style.Add("display", "none");
        if(CLtype.Text=="BL")
        {
            tr_gzzt.Style.Add("display", "block");
            BindDropZT();
            BtnCL.Text = "故障保留";
            this.Title = "故障保留";
        }
        else if (CLtype.Text == "YJ")
        {
            tr_gzzt.Style.Add("display", "none");
            //t_yjbm.Style.Add("display", "block");
            t_yjr.Style.Add("display", "block");
            t_gzyj.Style.Add("display", "block");
            //tr_csbm.Style.Add("display", "block");
            tr_csry.Style.Add("display", "block");
            BtnCL.Text = "故障移交";
            this.Title = "故障移交";
            //bool bl = true;
            //foreach (string bm in YJBM.Text.Split(','))
            //{
            //    if (bm == "网管中心")
            //    {
            //        bl = false;
            //        break;
            //    }
            //}
            //if (bl)  //不是移交到网管中心
            //{

            //}
            //else //移交到客户维护部
            //{
            //    string sql = "select t.* from t_fau_cllc t where t.guid='" + GUID.Text + "'";
            //    DataSet ds = DataFunction.FillDataSet(sql);
            //    sql = "select t.* from t_fau_zb t where zbguid='" + ZBGUID.Text + "'";
            //    DataRow dataRow = DataFunction.GetSingleRow(sql);
            //}
                
            //else
            //{
            //    //sql = "select t.branchname from t_sys_branch t where t.isuse=1";
            //    //DataSet ds = DataFunction.FillDataSet(sql);
            //    //YJBM.DataSource = ds;
            //    //YJBM.DataTextField = "branchname";
            //    //YJBM.DataValueField = "branchname";
            //    //YJBM.DataBind();
            //}
        }
        
        
        ////部门、人员
        //string branch = Session["BranchName"].ToString();
        //string 
        //CLBM.Text = branch;

    }
    private void BindDropZT()
    {
        ZT.Items.Clear();
        string sql = "select t.* from t_fau_enumdata t where t.enum_sort='GZZT'";
        ZT.DataSource = DataFunction.FillDataSet(sql);
        ZT.DataTextField = "enum_name";
        ZT.DataValueField = "enum_name";
        ZT.DataBind();
    }
    
    protected void BtnQX_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script> window.close();</script>");
    }
    
    protected void BtnCL_Click(object sender, EventArgs e)
    {
        //*by hangyt@3.2
        if (BtnCL.Text == "故障移交")
        {
            if (string.IsNullOrEmpty(YJR.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('主送人员不能为空！')</script>");
                return;
            }
        }
        //*by hangyt@3.2
        string BM = Session["BranchName"].ToString();
        string UserId = Session["UserID"].ToString();
        string UserName = Session["UserRealName"].ToString();
        DateTime nowTime = DateTime.Now;
        //操作记录
        string sql = "select t.* from t_fau_cllc t where t.guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        //主表
        sql = "select t.* from t_fau_zb t where zbguid='" + ZBGUID.Text + "'";
        DataRow dataRow = DataFunction.GetSingleRow(sql);
        DataRow dr = ds.Tables[0].NewRow();
        
        dr["GUID"] = Guid.NewGuid().ToString();
        dr["ZBGUID"] = ZBGUID.Text;
        dr["CLSJ"] = nowTime;
        dr["CLBM"] = BM;
        dr["CLRY"] = UserName;
        dr["CLRYID"] = UserId;
        dr["CLSM"] = CLSM.Text;
        dataRow["yjbmcode"] = YJBMCODE.Text;
        dataRow["csbmcode"] = CSBMCODE.Text;
        switch(BtnCL.Text)
        {
            case "故障保留":
                dr["GZZT"] = ZT.SelectedValue;
                dr["LCCZ"] = "故障保留";
                //dataRow["ldsj"] = nowTime;
                dataRow["SFSD"] = 1;
                dataRow["gzclzt"] = ZT.SelectedValue;
                break;
            case "故障移交":
                dr["GZZT"] = "移交";
                dr["LCCZ"] = "故障移交";

                dataRow["YJR"] = YJR.Text;
                dataRow["YJRCODE"] = YJRID.Text;
                dataRow["CSRID"] = CSRID.Text;
                dataRow["CSRNAME"]=CSRNAME.Text;
                dataRow["SFSD"] = 1;
                dataRow["SFYD"] = 1;
                dataRow["SDRY"] = null;
                dataRow["GZYYR"] = null;
                dataRow["GZYYRID"] = null;
                dataRow["GZYDRID"] = null;//故障阅读人
                //选择部门所有人的部门名称
                dataRow["CSBM"] = CSBM.Text;
                dataRow["YJBM"] = YJBM.Text;
                //移交到客户维护部
                if (YJR.Text == "客户维护部")
                {
                    dataRow["fdzzt"] = "电话处理";
                    dataRow["YJR"] = null;
                    dataRow["YJRCODE"] = null;
                    dataRow["SFFDZ"] = 1;//是否返单子
                }
                break;
        }
        DataFunction.SaveData(dataRow.Table.DataSet, "t_fau_zb");
        ds.Tables[0].Rows.Add(dr);
        DataFunction.SaveData(dr.Table.DataSet, "t_fau_cllc");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('处理成功！')</script>");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true'; window.close();</script>");
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        BindDropZT();
    }
}

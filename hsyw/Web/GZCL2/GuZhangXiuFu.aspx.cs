using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangXiuFu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindDrop();
            BindDropZT();
            InitialControl();
        }

    }

    private void BindDrop()
    {
        YWZT.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywzt' and sfqy=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        YWZT.DataSource = ds;
        YWZT.DataTextField = "codename";
        YWZT.DataValueField = "codename";
        YWZT.DataBind();
        ListItem itmes = new ListItem("---请选择---","");
        YWZT.Items.Add(itmes);
        YWZT.SelectedValue = "";
    }

    private void InitialControl()
    {
        
        GZYY.Attributes.Add("ReadOnly","flase");
        ZBGUID.Text = Request.QueryString["ZBGUID"];
        string sql = "select * from t_fau_zb2 where zbguid='"+ZBGUID.Text+"'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        dr["JDSJ"] = DateTime.Now;
        ShareFunction.FillControlData(Page, dr);
        XFBM.Text = Session["BranchName"].ToString();
        //XFBMCODE.Text = Session["BranchCode"].ToString();
        XFRY.Text=dr["DDFDR"].ToString();
        if(XFRY.Text=="")
        {
            XFRY.Text = Session["UserRealName"].ToString();
        }
        XFRYCODE.Text=Session["UserID"].ToString();
        BindYWLB();
        //YWZT.SelectedValue = dr["YWZT"].ToString();
        YWLB.SelectedValue = dr["YWLB"].ToString();
        BindGZLX();
        BindGZCC();
        ListItem item = new ListItem("---请选择---", "");
        GZCC.Items.Add(item);
        GZCC.SelectedValue = "";
        GZLX.Items.Add(item);
        GZLX.SelectedValue = "";
        
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        //*by hangyt@2012.3.1
        //if (YWZT.SelectedValue == "" || YWLB.SelectedValue == "" || GZCC.SelectedValue == "" || GZLX.SelectedValue == "" || GZCLFF.SelectedValue == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), @"<script>alert('业务主体、业务类型、故障层次、故障类型、处理方法不能为空，请检查！');</script>");
        //    return;
        //}
        string strMessge = "";
        if (YWZT.SelectedValue == "")
        {
            strMessge += "业务主体、";
        }
        if (YWLB.SelectedValue == "")
        {
            strMessge += "业务类型、";
        }
        if (GZCC.SelectedValue == "")
        {
            strMessge += "故障层次、";
        }
        if (GZLX.SelectedValue == "")
        {
            strMessge += "故障类型、";
        }
        if (GZCLFF.SelectedValue == "")
        {
            strMessge += "处理方法不能为空、";
        }
        if (GZYY.Text == "")
        {
            strMessge += "故障原因、";
        }
        if (XFRY.Text == "")
        {
            strMessge += "修复人员、";
        }
        if (strMessge != "")
        {
            strMessge = strMessge.Substring(0, strMessge.Length - 1);
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('" + strMessge + "不能为空！')</script>");
            return;
        }
        //*by hangyt@2012.3.1

        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        ShareFunction.GetControlData(Page, dr);
        if (dr["FDZZT"] != DBNull.Value)
        {
            dr["FDZZT"] = "结单";
        }
        dr["GZZT"] = "结单";
        dr["SFSD"] = 1;
        if (GZCLFF.SelectedItem != null)
        {
            dr["GZCLFF"] = GZCLFF.SelectedItem.Text;
        }
        
        dr["GZFFMS"] = GZFFMS.Text;
        setValue(dr, YWLB, "YWLX");
        setValue(dr, YWZT, "YWZT");
        setValue(dr, GZCC, "GZCC");
        setValue(dr, GZLX, "GZLX");
        //setValue(dr, GZYY, "GZYY");
        DataFunction.SaveData(dr.Table.DataSet, "t_fau_zb2");
        string guid = Guid.NewGuid().ToString();
        sql = "select t.* from t_fau_cllc t where t.guid='" + guid + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dataRow = ds.Tables[0].NewRow();
        dataRow["GUID"] = guid;
        dataRow["ZBGUID"] = ZBGUID.Text;
        dataRow["CLSJ"] = JDSJ.Text;
        dataRow["CLBM"] = XFBM.Text;
        dataRow["CLRY"] = Session["UserRealName"].ToString();
        dataRow["CLRYID"] = Session["UserID"].ToString();
        dataRow["GZZT"] = "修复";
        dataRow["LCCZ"] = "故障修复";
        dataRow["SJCLRY"] = XFRY.Text;
        dataRow["LCZT"] = Request.QueryString["lczt"];
        ds.Tables[0].Rows.Add(dataRow);
        DataFunction.SaveData(ds, "t_fau_cllc");

        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>window.returnValue='true'; window.close();</script>");
    }

    private void setValue(DataRow dataRow,DropDownList drop,string cellName)
    {
        if (drop.SelectedValue != "")
        {
            dataRow[cellName] = drop.SelectedItem.Text;
        }
    }

    protected void BtnGB_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script> window.close();</script>");
    }
    protected void BtnCZ_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        dr["JDSJ"] = DateTime.Now;
        ShareFunction.FillControlData(Page, dr);
        BindDrop();
        //Random ra = new Random();
        
    }
    protected void GZZY_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGZCC();
        
    }
    private void BindGZCC()
    {
        string sql = "select * from T_FAU_LXSZ where LB='cc' and SFQY=1 and PARENT_NAME='" + YWLB.SelectedValue + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        GZCC.DataSource = ds;
        GZCC.DataTextField = "CODENAME";
        GZCC.DataValueField = "CODENAME";
        GZCC.DataBind();
    }
   
    //protected void GZLX_SelectedIndexChanged(object sender, EventArgs e)
    //{
        //GZYY.Items.Clear();
        //string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='zy' and SFQY=1 and t.PARENT_ID='{0}'", GZLX.SelectedValue);
        //DataSet ds = DataFunction.FillDataSet(sql);
        //GZYY.DataSource = ds;
        //GZYY.DataTextField = "codename";
        //GZYY.DataValueField = "guid";
        //GZYY.DataBind();
    //}

    protected void YWLB_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindYWLB();
        BindGZCC();
        BindGZLX();
    }
    private void BindGZLX()
    {
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='gzlx' and SFQY=1 and t.PARENT_NAME='{0}'", YWZT.SelectedValue);
        GZLX.Items.Clear();
        DataSet dts = DataFunction.FillDataSet(sql);
        GZLX.DataSource = dts;
        GZLX.DataTextField = "codename";
        GZLX.DataValueField = "guid";
        GZLX.DataBind();
        //if(YWZT.SelectedValue=="")
        //{
        //    ListItem items = new ListItem("---请选择---", "");
        //    GZLX.Items.Add(items);
        //    GZLX.SelectedValue = "";
        //}
    }
    private void BindYWLB()
    {
        YWLB.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywlx' and SFQY=1 and t.PARENT_NAME='{0}'", YWZT.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        YWLB.DataSource = ds;
        YWLB.DataTextField = "codename";
        YWLB.DataValueField = "codename";
        YWLB.DataBind();
        //if(YWZT.SelectedValue=="")
        //{
        //    ListItem item = new ListItem("---请选择---", "");
        //    YWLB.Items.Add(item);
        //    YWLB.SelectedValue = "";
        //}
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        BindDropZT();
    }
    private void BindDropZT()
    {
        GZCLFF.Items.Clear();
        string sql = "select t.* from t_fau_enumdata t where t.enum_sort='CLFF'";
        GZCLFF.DataSource = DataFunction.FillDataSet(sql);
        GZCLFF.DataTextField = "enum_name";
        GZCLFF.DataValueField = "enum_name";
        GZCLFF.DataBind();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_FDZ_FanDanZiEdit : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDrop();
            ZBGUID.Text = Request.QueryString["ZBGUID"];
            per.Text = Request.QueryString["per"];
            InitialControl();
            BindFileGrid();
            BindGrid();
        }
    }
    private void BindGrid()
    {
        string sql = "select t.* from t_fau_cllc2 t where zbguid='" + ZBGUID.Text + "'  order by clsj";
        DataSet ds = DataFunction.FillDataSet(sql);
        gzcl.BindGridView(GridView_CLCC, ds);
    }

    private void InitialControl()
    {
        BtnBL.Attributes.Add("onclick", "OpenCL('BL')");
        BtnYL.Attributes.Add("onclick", "OpenCL('YL')");
        BtnSDDFD.Attributes.Add("onclick", "OpenCL('SFD')");
        BtnFHD.Attributes.Add("onclick", "OpenCL('FHD')");
        BtnXF.Attributes.Add("onclick", "OpenXF()");
        BtnFD.Attributes.Add("onclick", "OpenDDFD('DDFD')");
        BtnFHWG.Attributes.Add("onclick", "OpenDDFD('FHWG')");

        t_cllc.Style.Add("display", "bolck");
        t_xfjg.Style.Add("display", "bolck");
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        ShareFunction.FillControlData(Page, dr);
        XFYWZT.Text = dr["YWZT"].ToString();
        XFYWLB.Text = dr["YWLB"].ToString();
        if (dr["SFSD"] != DBNull.Value && Convert.ToInt32(dr["SFSD"]) == 1)//未锁定
        {
            BtnSD.Text = "锁定";
            BtnBL.Enabled = false;
            BtnXF.Enabled = false;
            BtnYL.Enabled = false;
            BtnSDDFD.Enabled = false;
            BtnFD.Enabled = false;
            BtnFHD.Enabled = false;
            SaveButton.Enabled = false;
            BtnJCGZ.Enabled = false;
            BtnFHWG.Enabled = false;
        }
        else
        {
            BtnSD.Text = "解锁";
            //SDRY.Text = DataFunction.GetStringResult("select t.userrealname from t_sys_user t where id='" + dr["GZYYRID"].ToString() + "'");
            if (dr["GZYYRID"].ToString() != Session["UserID"].ToString())
            {
                //超级用户
                if (Convert.ToString(Session["Username"]) == "admin")
                {
                    BtnBL.Enabled = true;
                    BtnXF.Enabled = true;
                    BtnYL.Enabled = true;
                    BtnSDDFD.Enabled = true;
                    BtnFD.Enabled = true;
                    BtnFHD.Enabled = true;
                    BtnJCGZ.Enabled = true;
                    SaveButton.Enabled = true;
                    BtnFHWG.Enabled = true;
                }
                else
                {
                    BtnSD.Enabled = false;
                    BtnBL.Enabled = false;
                    BtnXF.Enabled = false;
                    BtnYL.Enabled = false;
                    BtnSDDFD.Enabled = false;
                    BtnFD.Enabled = false;
                    BtnFHD.Enabled = false;
                    BtnJCGZ.Enabled = false;
                    SaveButton.Enabled = false;
                    BtnFHWG.Enabled = false;
                }
            }
            else
            {
                BtnBL.Enabled = true;
                BtnXF.Enabled = true;
                BtnYL.Enabled = true;
                BtnSDDFD.Enabled = true;
                BtnFD.Enabled = true;
                BtnFHD.Enabled = true;
                BtnJCGZ.Enabled = true;
                SaveButton.Enabled = true;
                BtnFHWG.Enabled = true;
            }
        }

        switch (per.Text)
        {
            case "dhsl"://电话受理
                //SaveButton.Style.Add("display", "block");
                BtnFD.Style.Add("display", "none");
                BtnFHD.Style.Add("display", "none");
                BtnFHWG.Style.Add("display", "none");
                break;
            case "ddfd":
                //SaveButton.Style.Add("display", "block");
                BtnBL.Style.Add("display", "none");
                BtnYL.Style.Add("display", "none");
                BtnSDDFD.Style.Add("display", "none");
                // BtnFHWG.Style.Add("display", "block");
                // BtnXF.Text = "结单";
                //BtnXF.Style.Add("display", "none");
                BtnFHD.Style.Add("display", "none");
                break;
            case "wxfd":
                // SaveButton.Style.Add("display", "block");
                BtnSDDFD.Style.Add("display", "none");
                BtnFD.Style.Add("display", "none");
                BtnFHWG.Style.Add("display", "none");
                break;
            case "yld":
                // SaveButton.Style.Add("display", "block");
                BtnBL.Style.Add("display", "none");
                BtnYL.Style.Add("display", "none");
                BtnSDDFD.Style.Add("display", "none");
                BtnFHWG.Style.Add("display", "none");
                BtnFD.Style.Add("display", "none");
                BtnFHD.Style.Add("display", "none");
                break;
            default:
                SaveButton.Style.Add("display", "none");
                BtnBL.Style.Add("display", "none");
                BtnYL.Style.Add("display", "none");
                BtnSDDFD.Style.Add("display", "none");
                BtnFD.Style.Add("display", "none");
                BtnFHD.Style.Add("display", "none");
                BtnXF.Style.Add("display", "none");
                BtnSD.Style.Add("display", "none");
                BtnFHWG.Style.Add("display", "none");
                Label1.Style.Add("display", "none");
                BtnJCGZ.Style.Add("display", "none");
                break;
        }
    }
    private void BindFileGrid()
    {
        DataSet ds = WebFileUtil2.GetFileList(ZBGUID.Text, "ALL");
        FileGrid.DataSource = ds;
        FileGrid.DataBind();
    }
    protected void FileGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            DataRowView dv = e.Row.DataItem as DataRowView;
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            (e.Row.Cells[1].FindControl("HyperLink1") as HyperLink).NavigateUrl = Request.ApplicationPath + "/" + dv["FILEURL"].ToString();

            //下面行代码本来不是注释的，不注释的话就会报错，因为电话受理时是不让删除附件的，GridView里也没有删除按钮   罗耀斌 2011-6-8 15：18
            //(e.Row.FindControl("BtnDel") as Button).CommandArgument = e.Row.RowIndex.ToString();
        }
    }

    protected void BtnSD_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        //bool bl = false;
        if (BtnSD.Text == "锁定")
        {
            dr["SFSD"] = 0;
            dr["GZYYRID"] = Session["UserID"];
            dr["GZYYR"] = Session["UserRealName"];
            dr["GZSDSJ"] = DateTime.Now;
            dr["SDRY"] = Session["UserRealName"];
            SDRY.Text = Session["UserRealName"].ToString();
            //bl = true;
            BtnSD.Text = "解锁";
        }
        else
        {
            dr["SFSD"] = 1;
            dr["GZYYRID"] = null;
            dr["GZYYR"] = null;
            dr["SDRY"] = "";
            SDRY.Text = "";
            BtnSD.Text = "锁定";
        }
        DataFunction.SaveData(dr.Table.DataSet, "t_fau_zb2");
        InitialControl();
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr;
        if (ds.Tables[0].Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('故障单保存失败！')</script>");
            return;
        }
        dr = ds.Tables[0].Rows[0];
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        dr["GZZT"] = "处理中";
        dr["gzyyrid"] = Session["UserID"].ToString();
        sql = "select t.sjsc,t.cssc from t_fau_gzdj t where sfqy=1 and MC='" + GZDJ.Text + "'";//故障等级
        DataRow dtr = DataFunction.GetSingleRow(sql);
        dr["SJSJ"] = Convert.ToDateTime(TSSJ.Text).AddHours(Convert.ToInt32(dtr["sjsc"]));
        dr["CSSJ"] = Convert.ToDateTime(TSSJ.Text).AddHours(Convert.ToInt32(dtr["cssc"]));
        DataFunction.SaveData(ds, "t_fau_zb2");
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('故障单保存成功！')</script>");
        //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('故障单创建成功！故障编号为:" + GZBH.Text + "')</script>");
        //sql = string.Format("update t_sys_data set datadm='{0}' where datamc='故障编号'", (Convert.ToInt32(GZBH.Text) + 1));
        //DataFunction.ExecuteNonQuery(sql);
        //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }
    protected void TQ_Click(object sender, ImageClickEventArgs e)
    {
        if (YWBH.Text != "")
        {

            string sql = "select * from rmss t where 1=1";

            sql += " and SUBSCRIBER_CODE like '%" + YWBH.Text.Trim() + "%'";
            DataSet dt = DataFunction.FillDataSet(sql);
            if (dt.Tables[0].Rows.Count == 1)
            {
                SUBSCRIBER_ID.Text = dt.Tables[0].Rows[0]["SUBSCRIBER_ID"].ToString();
                FillRmssPage(SUBSCRIBER_ID.Text, "");
            }
            else if (dt.Tables[0].Rows.Count > 1)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpenRmssTQ()</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('没有查到相应的用户信息！');</script>");
            }

        }
    }
    protected void YWZT_SelectedIndexChanged(object sender, EventArgs e)
    {
        YWLB.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywlx' and SFQY=1 and t.PARENT_NAME='{0}'", YWZT.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        YWLB.DataSource = ds;
        YWLB.DataTextField = "codename";
        YWLB.DataValueField = "codename";
        YWLB.DataBind();
        ListItem item = new ListItem("---请选择---", "");
        YWLB.Items.Add(item);
        YWLB.SelectedValue = "";
    }

    private void BindDrop()
    {
        YWZT.Items.Clear();
        string sql = "select CODENAME,guid from T_FAU_LXSZ where LB='ywzt' and SFQY=1";
        YWZT.DataSource = DataFunction.FillDataSet(sql);
        YWZT.DataTextField = "CODENAME";
        YWZT.DataValueField = "CODENAME";
        YWZT.DataBind();

        YWLB.Items.Clear();
        sql = "select CODENAME from T_FAU_LXSZ where LB='ywlb' and SFQY=1";
        YWLB.DataSource = DataFunction.FillDataSet(sql);
        YWLB.DataTextField = "CODENAME";
        YWLB.DataValueField = "CODENAME";
        YWLB.DataBind();

        sql = "select CUSTTYPE1 from rmss_boss group by CUSTTYPE1";
        HYFL.DataSource = DataFunction.FillDataSet(sql);
        HYFL.DataTextField = "CUSTTYPE1";
        HYFL.DataValueField = "CUSTTYPE1";
        HYFL.DataBind();

        GZLY.Items.Clear();
        sql = "select codename from t_fau_lxsz t where t.lb='GZLY' and SFQY=1";
        GZLY.DataSource = DataFunction.FillDataSet(sql);
        GZLY.DataTextField = "codename";
        GZLY.DataValueField = "codename";
        GZLY.DataBind();

        sql = "select MC from t_fau_gzdj t where sfqy=1";
        GZDJ.DataSource = DataFunction.FillDataSet(sql);
        GZDJ.DataTextField = "MC";
        GZDJ.DataValueField = "MC";
        GZDJ.DataBind();
    }

    #region 业务单管理
    /// <summary>
    /// 填充业务编码内容
    /// </summary>
    /// <param name="SUBSCRIBER_ID">业务ID</param>
    /// <param name="JYD">甲乙端</param>
    private void FillRmssPage(string str_SUBSCRIBER_ID, string JYD)
    {
        string sql = string.Format("select * from rmss t where t.SUBSCRIBER_ID='{0}'", str_SUBSCRIBER_ID);
        DataRow dataRow = DataFunction.GetSingleRow(sql);
        YWBH.Text = dataRow["subscriber_code"].ToString();
        LXRNAME.Text = dataRow["linkman"].ToString();
        GZMC.Text = dataRow["sub_name"].ToString();
        LXDH.Text = dataRow["MOBILE_NO"].ToString();
        KHDZ.Text = dataRow["ADDRESS"].ToString();
        KHQY.Text = dataRow["REGION"].ToString();
        KHBH.Text = dataRow["CUSTOMER_CODE"].ToString();
        KHQYID.Text = dataRow["REGION_CODE"].ToString();
        HYFL.SelectedValue = dataRow["custtype1"].ToString();
        CUSTOMER_LEVEL.SelectedValue = dataRow["CUSTOMER_LEVEL"].ToString();
    }
    #endregion

    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        //by hangyt@2012.2.29
        if (!string.IsNullOrEmpty(SUBSCRIBER_ID.Text))
        {
            FillRmssPage(SUBSCRIBER_ID.Text, "");
        }
    }

    protected void btnSX_Click(object sender, EventArgs e)
    {
        InitialControl();
        BindGrid();
    }
    protected void GridView_CLCC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string bm = e.Row.Cells[1].Text;
            string str = string.Format(@"select * from t_sys_branch h where h.branchname ='{0}' and h.branchcode like '10010103%'", bm);
            if (DataFunction.HasRecord(str))
            {
                e.Row.Cells[1].Text = "客户维护部";
            }
            //string lccz=e.Row.Cells[4].Text;
            //if (lccz == "故障修复")
            //{
            //    e.Row.Cells[3].Text = DataFunction.GetStringResult("select t.xfry from t_fau_zb2 t where t.zbguid='" + ZBGUID.Text + "'");
            //}
        }
    }
}

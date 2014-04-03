using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
public partial class Web_GZCL_GuZhangEdit : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SBLX.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            BindDrop();
            InitialControl();
            BindGrid();
            BtnBL.Attributes.Add("onclick", "OpenCL('BL')");
            BtnYJ.Attributes.Add("onclick", "OpenCL('YJ')");
            BtnXF.Attributes.Add("onclick", "OpenXF()");
            KHQY.Attributes.Add("ReadOnly", "true");
            BindFileGrid();
            BindGridViewYxyh();
        }
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
        ListItem item = new ListItem("", "");
        YWLB.Items.Add(item);
        YWLB.SelectedValue = "";

        //*by hangyt@2012.3.1
        //sql = "select CUSTTYPE1 from rmss_boss group by CUSTTYPE1";
        sql = "select CUSTTYPE1,1 as xh from rmss_boss group by CUSTTYPE1 union select to_char(datamc),2 as xh from t_sys_data2 where datacode='CUSTTYPE1' order by xh";
        //*by hangyt@2012.3.1
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

    private void InitialControl()
    {
        //string sg = Request.QueryString["SG"];
        string userID = Session["UserID"].ToString();
        ZBGUID.Text = Request.QueryString["zbguid"];

        //切割分析时用的数据
        JF.Text = Request.QueryString["JF"];
        JF_CODE.Text = Request.QueryString["JF_CODE"];
        SB.Text = Request.QueryString["SB"];
        SB_GUID.Text = Request.QueryString["SB_GUID"];
        //####################

        if (string.IsNullOrEmpty(ZBGUID.Text))
        {
            ZBGUID.Text = Guid.NewGuid().ToString();
        }
        //if(sg=="1")
        //{
        //    td_addUser.Style.Add("display","block");

        //}
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            SaveButton.Text = "保存故障单";
            dr = ds.Tables[0].NewRow();
            //ZBGUID.Text = Guid.NewGuid().ToString();
            string userName = Session["UserRealName"].ToString();
            DateTime time = DateTime.Now;
            dr["ZBGUID"] = ZBGUID.Text;
            dr["GZCJRNAME"] = userName;
            dr["TSSJ"] = DateTime.Now;
            dr["gzzt"] = "待处理";
            dr["SFSD"] = 1;
            dr["gzyyr"] = userName;
            dr["GZDJ"] = "4级";
            dr["GZYDRID"] = userID;
            //dr["GZBH"] = DataFunction.GetStringResult("select t.datadm from t_sys_data2 t where t.datamc='故障编号'");
            ds.Tables[0].Rows.Add(dr);
            BtnBL.Style.Add("display", "none");
            BtnXF.Style.Add("display", "none");
            BtnSD.Style.Add("display", "none");
            BtnPY.Style.Add("display", "none");
            BtnBC.Style.Add("display", "none");
            BtnYJ.Style.Add("display", "none");
            Label1.Style.Add("display", "none");
            //BtnBL.Enabled = true;
            SUBSCRIBER_ID.Text = Request.QueryString["YWBM"];
            sql = "select * from rmss t where t.SUBSCRIBER_ID='" + SUBSCRIBER_ID.Text + "'";
            DataSet dts = DataFunction.FillDataSet(sql);
            if (dts.Tables[0].Rows.Count != 0)
            {
                DataRow dataRow = dts.Tables[0].Rows[0];
                dr["YWBH"] = dataRow["subscriber_code"];
                dr["LXRNAME"] = dataRow["linkman"];
                dr["GZMC"] = dataRow["sub_name"];
                dr["LXDH"] = dataRow["MOBILE_NO"];
                dr["KHDZ"] = dataRow["ADDRESS"];
                dr["KHQY"] = dataRow["REGION"];
                dr["YWBHID"] = SUBSCRIBER_ID.Text;
                dr["KHBH"] = dataRow["CUSTOMER_CODE"];
                //dr["YWLB"] = dataRow["custtype1"];
                //区域编号
                dr["KHQYID"] = dataRow["REGION_CODE"]; //DataFunction.GetStringResult("select t.branchcode from t_sys_branch t where t.branchname ='" + dataRow["REGION"].ToString() + "'");
                // HYFL.SelectedItem.Text = dataRow["custtype1"].ToString();
                dr["HYFL"] = dataRow["custtype1"];
                //CUSTOMER_LEVEL.SelectedValue = dataRow["CUSTOMER_LEVEL"].ToString();
                dr["CUSTOMER_LEVEL"] = dataRow["CUSTOMER_LEVEL"];
            }
            else
            {
                ShareFunction.FillControlData(Page, dr);
                //客户rmss数据不全，暂时全部默认
                getUserSSQY();
                ShareFunction.GetControlData(Page, dr);
            }

        }
        else
        {
            //SaveButton.Style.Add("display","none");
            dr = ds.Tables[0].Rows[0];
            ControlShow();
            if (dr["SFSD"] != DBNull.Value && Convert.ToInt32(dr["SFSD"]) == 1)//未锁定
            {
                BtnSD.Text = "锁定";
                BtnBL.Enabled = false;
                BtnXF.Enabled = false;
                BtnYJ.Enabled = false;
                SaveButton.Enabled = false;
                //BtnSD.Enabled = true;
            }
            else
            {
                BtnSD.Text = "解锁";
                //SDRY.Text = DataFunction.GetStringResult("select t.userrealname from t_sys_user t where id='" + dr["GZYYRID"].ToString() + "'");
                if (dr["GZYYRID"].ToString() != Session["UserID"].ToString())
                {
                    BtnSD.Enabled = false;
                    BtnBL.Enabled = false;
                    BtnXF.Enabled = false;
                    BtnYJ.Enabled = false;
                    SaveButton.Enabled = false;
                    //BtnJCGZ.Enabled = false;
                }
                else
                {
                    SaveButton.Enabled = true;
                    BtnBL.Enabled = true;
                    BtnYJ.Enabled = true;
                    BtnXF.Enabled = true;
                    //BtnJCGZ.Enabled = true;
                }
            }
            //保存故障阅读人
            string ydrSql = string.Format(@"select * from t_fau_zb2 where GZYDRID like '%{0}%' and zbguid='{1}'", userID, ZBGUID.Text);
            if (!DataFunction.HasRecord(ydrSql))
            {
                dr["GZYDRID"] = dr["GZYDRID"].ToString() + "," + userID;
            }
            DataFunction.SaveData(ds, "t_fau_zb2");

            //主送/抄送
            txtZS.Text = getZSYRBM(dr["yjr"].ToString(), dr["yjbm"].ToString());
            txtCS.Text = getZSYRBM(dr["CSRNAME"].ToString(), dr["csbm"].ToString());
        }
        ShareFunction.FillControlData(Page, dr);
        Per.Text = Request.QueryString["per"];
        if (Per.Text == "")
        {
            string str = string.Format("select case when t.gzyyrid='{0}' or t.yjrcode like '%{0}%'  then 1 else 0 end as per from t_fau_zb2 t where  zbguid='{1}' ", userID, ZBGUID.Text);
            Per.Text = DataFunction.GetStringResult(str);
        }
        switch (Per.Text)
        {
            case "0":
                SaveButton.Style.Add("display", "none");
                BtnBL.Style.Add("display", "none");
                BtnSD.Style.Add("display", "none");
                BtnXF.Style.Add("display", "none");
                BtnPY.Style.Add("display", "none");
                BtnBC.Style.Add("display", "none");
                BtnYJ.Style.Add("display", "none");
                Label1.Style.Add("display", "none");
                //BtnJCGZ.Style.Add("display", "none");
                break;
            case "1"://待办、未复故障,操作权限
                //SaveButton.Style.Add("display", "block");
                BtnPY.Style.Add("display", "none");
                BtnBC.Style.Add("display", "none");
                break;
            case "py"://评议
                SaveButton.Style.Add("display", "none");
                BtnBL.Style.Add("display", "none");
                BtnSD.Style.Add("display", "none");
                BtnXF.Style.Add("display", "none");
                BtnBC.Style.Add("display", "none");
                BtnYJ.Style.Add("display", "none");
                tr_cllc.Style.Add("display", "none");
                //tr_gzztxx.Style.Add("display", "none");
                tr_xfjg.Style.Add("display", "none");
                //Imageztxx.ImageUrl = "../Images/add_up.gif";
                Imageclc.ImageUrl = "../Images/add_up.gif";
                Imagexfjg.ImageUrl = "../Images/add_up.gif";
                Label1.Style.Add("display", "none");
                if (dr["PYSJ"] == DBNull.Value)
                {
                    PYRY.Text = Session["UserRealName"].ToString();
                    PYSJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                }
                break;
            case "pc"://补充
                SaveButton.Style.Add("display", "none");
                BtnPY.Style.Add("display", "none");
                BtnBL.Style.Add("display", "none");
                BtnSD.Style.Add("display", "none");
                BtnXF.Style.Add("display", "none");
                BtnYJ.Style.Add("display", "none");
                Label1.Style.Add("display", "none");
                break;
            case "query":
                BtnPY.Style.Add("display", "none");
                BtnBC.Style.Add("display", "none");
                break;
            default:
                BtnPY.Style.Add("display", "none");
                BtnBC.Style.Add("display", "none");
                break;
        }
        XFYWZT.Text = dr["YWZT"].ToString();
        XFYWLB.Text = dr["YWLB"].ToString();
        //XFYYR.Text = dr["GZYYR"].ToString();


    }

    #region 获取主送/抄送

    private string getZSYRBM(string ZSRY, string ZSBM)
    {
        string yrbmStr = "";
        string strSql = "";
        foreach (string ry in ZSRY.Split(','))
        {
            foreach (string bm in ZSBM.Split(','))
            {
                strSql = string.Format(@"select t.*
  from t_sys_user t, t_sys_branch b
 where t.branchcode = b.branchcode
   and t.userrealname = '{0}'
   and b.branchname = '{1}'", ry, bm);
                if (!DataFunction.HasRecord(strSql))
                {
                    yrbmStr += ry + ",";
                }
            }
        }
        if (yrbmStr != "")
        {
            yrbmStr = yrbmStr.Substring(0, yrbmStr.Length - 1);
            yrbmStr += "/" + ZSBM;
        }
        return yrbmStr;
    }

    #endregion


    private void BindGrid()
    {
        string sql = "select t.* from t_fau_cllc2 t where zbguid='" + ZBGUID.Text + "' order by clsj";
        DataSet ds = DataFunction.FillDataSet(sql);
        gzcl.BindGridView(GridView_CLCC, ds);
        //sql = "select t.* from t_fau_cllc2 t where zbguid='" + ZBGUID.Text + "' order by clsj";
        //ds = DataFunction.FillDataSet(sql);
        //gzcl.BindGridView(GridView_GZBG, ds);
    }

    private void BindGridViewYxyh()
    {
        string sql = "select * from t_fau_yxyh2 t where t.zbguid='" + ZBGUID.Text + "' order by t.llmc";
        DataSet ds = DataFunction.FillDataSet(sql);
        GridViewYxyh.DataSource = ds;
        GridViewYxyh.DataBind();

    }

    //保存方法 
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string strMessge = "";
        if (GZLY.SelectedValue == "")
        {
            strMessge += "故障来源、";
        }
        if (YWZT.SelectedValue == "")
        {
            strMessge += "业务主体、";
        }
        if (HYFL.SelectedValue == "")
        {
            strMessge += "行业、";
        }
        if (YWLB.SelectedValue == "")
        {
            strMessge += "业务类别、";
        }
        if (LXDH.Text == "")
        {
            strMessge += "联系电话、";
        }
        if (strMessge != "")
        {
            strMessge = strMessge.Substring(0, strMessge.Length - 1);
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('" + strMessge + "不能为空！')</script>");
            return;
        }

        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["ZBGUID"] = ZBGUID.Text;
            dr["SFSD"] = 1;
            dr["SFYD"] = 0;//是否阅读
            dr["YJBMCODE"] = Session["BranchCode"].ToString();
            dr["yjrcode"] = Session["UserID"].ToString();
            ds.Tables[0].Rows.Add(dr);
            //保存故障单时生产故障编号
            //GZBH.Text = DataFunction.GetStringResult("select t.datadm from t_sys_data2 t where t.datamc='故障编号'");
            GZBH.Text = DataFunction.GetStringResult("select max(to_number(gzbh))+1 from t_fau_zb2");
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('故障单创建成功！故障编号为:" + GZBH.Text + "')</script>");
            //if (str != GZBH.Text)
            //{
            // ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('故障单创建成功！故障编号为:" + GZBH.Text +  "')</script>");
            //GZBH.Text = str;
        }
        dr = ds.Tables[0].Rows[0];
        ShareFunction.GetControlData(Page, ds.Tables[0].Rows[0]);
        dr["GZZT"] = "处理中";
        dr["gzyyrid"] = Session["UserID"].ToString();
        sql = "select t.sjsc,t.cssc from t_fau_gzdj t where sfqy=1 and MC='" + GZDJ.SelectedItem.Text + "'";//故障等级
        DataRow dtr = DataFunction.GetSingleRow(sql);
        if (dtr.Table.Rows.Count == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择故障等级！')</script>");
            return;
        }
        else
        {
            dr["SJSJ"] = Convert.ToDateTime(TSSJ.Text).AddHours(Convert.ToInt32(dtr["sjsc"]));
            dr["CSSJ"] = Convert.ToDateTime(TSSJ.Text).AddHours(Convert.ToInt32(dtr["cssc"]));
        }
        DataFunction.SaveData(ds, "t_fau_zb2");

        //sql = string.Format("update t_sys_data2 set datadm='{0}' where datamc='故障编号'", (Convert.ToInt32(GZBH.Text) + 1));
        //DataFunction.ExecuteNonQuery(sql);
        sql = "select t.* from t_fau_cllc2 t where zbguid='" + ZBGUID.Text + "'";
        //状态变更记录
        if (!DataFunction.HasRecord(sql))
        {
            string clguid = Guid.NewGuid().ToString();
            sql = "select t.* from t_fau_cllc2 t where t.guid='" + clguid + "'";
            DataSet dataSet = DataFunction.FillDataSet(sql);
            string BM = Session["BranchName"].ToString();
            DataRow dataRow = dataSet.Tables[0].NewRow();
            dataRow["GUID"] = clguid;
            dataRow["ZBGUID"] = ZBGUID.Text;
            dataRow["CLSJ"] = DateTime.Now;
            dataRow["CLBM"] = BM;
            dataRow["CLRY"] = Session["UserRealName"];
            dataRow["CLRYID"] = Session["UserID"];
            dataRow["GZZT"] = "处理中";
            dataRow["LCCZ"] = "故障申告";
            dataSet.Tables[0].Rows.Add(dataRow);
            DataFunction.SaveData(dataRow.Table.DataSet, "t_fau_cllc2");
            BindGrid();
        }
        //InitialControl();
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>parent.WindowClose();</script>");
    }

    private void ControlShow()
    {
        t_cllc.Style.Add("display", "block");
        t_gzpy.Style.Add("display", "block");
        t_gztsd.Style.Add("display", "block");
        //t_gzztxx.Style.Add("display", "block");
        t_xfjg.Style.Add("display", "block");
        tr_cllc.Style.Add("display", "block");
        tr_gzpy.Style.Add("display", "block");
        //tr_gzztxx.Style.Add("display", "block");
        tr_xfjg.Style.Add("display", "block");

    }

    //锁定按钮
    protected void BtnSD_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        bool bl = false;
        if (BtnSD.Text == "锁定")
        {
            dr["SFSD"] = 0;
            dr["GZYYRID"] = Session["UserID"];
            dr["GZYYR"] = Session["UserRealName"];
            bl = true;
            dr["SDRY"] = Session["UserRealName"];
            SDRY.Text = Session["UserRealName"].ToString();
            BtnSD.Text = "解锁";
        }
        else
        {
            dr["SFSD"] = 1;
            dr["GZYYRID"] = null;
            dr["GZYYR"] = null;
            bl = false;
            dr["SDRY"] = "";
            SDRY.Text = "";
            BtnSD.Text = "锁定";
        }
        DataFunction.SaveData(dr.Table.DataSet, "t_fau_zb2");

        //BtnSD.Enabled = false;
        BtnBL.Enabled = bl;
        BtnYJ.Enabled = bl;
        BtnXF.Enabled = bl;
        //BtnJCGZ.Enabled = bl;
        SaveButton.Enabled = bl;
    }

    //目前没用
    protected void btnSX_Click(object sender, EventArgs e)
    {
        InitialControl();
        BindGrid();
    }

    //故障评议按钮
    protected void BtnPY_Click(object sender, EventArgs e)
    {
        string sql = "select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'";
        DataRow dr = DataFunction.GetSingleRow(sql);
        dr["PYRY"] = PYRY.Text;
        dr["PYSJ"] = PYSJ.Text;
        dr["GZPY"] = GZPY.Text;
        dr["GZPF"] = GZPF.Text;
        DataFunction.SaveData(dr.Table.DataSet, "t_fau_zb2");
    }

    /// <summary>
    /// 创建故障单，默认为用户所属区域
    /// </summary>
    private void getUserSSQY()
    {
        string ssbm = Session["BranchCode"].ToString();
        ssqyrec(ssbm);
    }

    /// <summary>
    /// 所属区域
    /// </summary>
    /// <param name="ssbm"></param>

    private void ssqyrec(string ssbm)
    {
        string sql = "select t.pbranchcode,t.isqy,t.path from t_sys_branch t where t.branchcode='" + ssbm + "'";
        DataRow dr = DataFunction.FillDataSet(sql).Tables[0].Rows[0];
        if (dr["isqy"] != DBNull.Value && dr["isqy"].ToString() == "1")
        {
            KHQY.Text = dr["path"].ToString();
            KHQYID.Text = ssbm;
        }
        else
        {
            ssqyrec(dr["pbranchcode"].ToString());
        }

    }
    protected void BtnUpLoad_Click(object sender, EventArgs e)
    {
        if (File1.Value == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择上传文件！');</script>");
            return;
        }
        if (WebFileUtil2.FileUpLoad(File1, ZBGUID.Text, Session["UserRealName"].ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('上传成功！');</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('上传失败！可能是存储空间不足，请联系管理员！');</script>");
        }
        BindFileGrid();
    }
    private void BindFileGrid()
    {
        DataSet ds = WebFileUtil2.GetFileList(ZBGUID.Text, "ALL");
        FileGrid.DataSource = ds;
        FileGrid.DataBind();
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.Button BtnDel = sender as System.Web.UI.WebControls.Button;
        int index = Convert.ToInt32(BtnDel.CommandArgument);
        string fileguid = FileGrid.DataKeys[index]["FILEGUID"].ToString();
        string fileurl = FileGrid.DataKeys[index]["FILEURL"].ToString();
        WebFileUtil2.DelFileByID(fileguid);
        WebFileUtil2.DeleteUrl("~/" + fileurl);
        BindFileGrid();
    }
    protected void FileGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            DataRowView dv = e.Row.DataItem as DataRowView;
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            (e.Row.Cells[1].FindControl("HyperLink1") as HyperLink).NavigateUrl = Request.ApplicationPath + "/" + dv["FILEURL"].ToString();
            (e.Row.FindControl("BtnDel") as System.Web.UI.WebControls.Button).CommandArgument = e.Row.RowIndex.ToString();
        }
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
        FillRmssPage(SUBSCRIBER_ID.Text, "");
    }
    protected void BtnEXCEL_Click(object sender, EventArgs e)
    {
        try
        {
            WorkbookDesigner designer1 = new WorkbookDesigner();
            DataSet ds = DataFunction.FillDataSet("select * from t_fau_zb2 where zbguid='" + ZBGUID.Text + "'");
            ds.Tables[0].TableName = "T1";

            object filePath = Server.MapPath("GZGL.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                //Cells cells = workbook.Worksheets[0].Cells; cells.Merge(0, 1, 2, 1);
                //designer1.Workbook.Worksheets[0].Cells["B1"].PutValue("");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    designer1.SetDataSource(ds);
                }
                else
                {
                    return;
                }
                designer1.Process();
                designer1.Save(ReturnUrlEncode("故障单.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
                Response.End();
            }
            else
                return;
        }
        catch (Exception ex)
        {
            string aa = ex.Message;
        }
    }

    /// <summary>
    /// 将中文转换为浏览器可识别的编码
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public static string ReturnUrlEncode(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return "";
        return System.Web.HttpUtility.UrlEncode(fileName);
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
        }
    }
    protected void gjfxButton_Click(object sender, EventArgs e)
    {
        DataSet ds = CreateCutBuniss();
        DataFunction.SaveData(ds, "t_fau_yxyh2");
        GridViewYxyh.DataSource = ds;
        GridViewYxyh.DataBind();
    }



    #region 割接分析
    private DataSet CreateCutBuniss()
    {
        DataSet dst = GetYxyhDataSet();
        DataTable tb = dst.Tables[0];
        DataSet ds = GetBusinessData(SB_GUID.Text);
        string strSbid = SB_GUID.Text;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (strSbid.IndexOf(dr["SBID"].ToString()) == -1 || dr["YWBM"].ToString() != "")
            {
                // strSbid += dr["PSBID"].ToString();
                DataRow drt = tb.NewRow();
                drt["YH_GUID"] = Guid.NewGuid().ToString();
                drt["ZBGUID"] = ZBGUID.Text;
                drt["LLMC"] = dr["SBMC"].ToString();
                drt["YWBM"] = dr["YWBM"].ToString();
                drt["YWMC"] = dr["YWMC"].ToString();
                tb.Rows.Add(drt);
                ChildCutBuniss(strSbid, tb, drt, dr["SBID"].ToString());
            }
        }
        return dst;
    }

    private void ChildCutBuniss(string strSbid, DataTable tb, DataRow drt, string sbid)
    {
        DataSet ds = GetBusinessData(sbid);
        strSbid += "," + sbid;
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (strSbid.IndexOf(dr["SBID"].ToString()) == -1 || dr["YWBM"].ToString() != "")
            {
                // strSbid += dr["PSBID"].ToString();
                DataRow cDr = null;
                if (i == 0)
                {
                    cDr = drt;
                }
                else
                {
                    cDr = tb.NewRow();
                    cDr["YH_GUID"] = Guid.NewGuid().ToString();
                    cDr["ZBGUID"] = ZBGUID.Text;
                    tb.Rows.Add(cDr);
                }
                if (drt["LLMC"].ToString().IndexOf(dr["SBMC"].ToString()) == -1)
                {
                    if (dr["LLFX"].ToString() == "0")
                    {
                        cDr["LLMC"] = drt["LLMC"].ToString() + "～" + dr["SBMC"].ToString();
                    }
                    else
                    {
                        cDr["LLMC"] = drt["LLMC"].ToString() + "→" + dr["SBMC"].ToString();
                    }
                }
                else
                {
                    cDr["LLMC"] = drt["LLMC"].ToString();
                }
                cDr["YWID"] = dr["YWID"];
                cDr["YWBM"] = dr["YWBM"];
                cDr["YWMC"] = dr["YWMC"];
                i++;
                ChildCutBuniss(strSbid, tb, cDr, dr["SBID"].ToString());
            }
        }
    }


    private DataSet GetBusinessData(string sbid)
    {
        string sql = string.Format(@"select t.jdsb_guid as psbid, t.ydsb_guid as sbid,t.jdjrjf||'【'||t.jdsb||'】' as sbmc,t.ywbm,r.SUB_NAME as ywmc,t.llfx,t.ywid from v_business t 
left join rmss r on t.ywid=r.SUBSCRIBER_ID where t.jdsb_guid='{0}' and nvl(t.llfx,0) in ('1','0')
union
select t.ydsb_guid as psbid,t.jdsb_guid as sbid,t.ydjrjf||'【'||t.ydsb||'】' as sbmc,t.ywbm,r.SUB_NAME as ywmc,t.llfx,t.ywid from v_business t 
left join rmss r on t.ywid=r.SUBSCRIBER_ID  where t.ydsb_guid='{0}' and nvl(t.llfx,0) in ('-1','0')", sbid);
        return DataFunction.FillDataSet(sql);
    }

    private DataSet GetYxyhDataSet()
    {
        string sql = "select * from t_fau_yxyh2 t where t.zbguid='" + ZBGUID.Text + "'";
        return DataFunction.FillDataSet(sql);
    }
    #endregion


    protected void GridViewYxyh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
    protected void ExpButton_Click(object sender, EventArgs e)
    {
        WorkbookDesigner designer1 = new WorkbookDesigner();

        object filePath = Server.MapPath("YXYH.xls");
        if (System.IO.File.Exists(Convert.ToString(filePath)))
        {
            designer1.Open(Convert.ToString(filePath));
            string[] str = SELECT_CH.Text.Split(':');
            int j = 0;
            string strColum = str[0];
            string[] strName = str[1].Split(',');

            foreach (string name in strName)
            {
                designer1.Workbook.Worksheets[0].Cells[0, j].PutValue(name);
                designer1.Workbook.Worksheets[0].Cells[0, j].Style.Font.IsBold = true;
                j++;
            }
            if (!string.IsNullOrEmpty(strColum))
            {
                string sql = string.Format("select {0} from t_fau_yxyh2 a left join rmss b on a.ywid=b.SUBSCRIBER_ID where a.ZBGUID='{1}'", strColum, ZBGUID.Text);
                DataSet ds = DataFunction.FillDataSet(sql);

                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int k = 0;
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        designer1.Workbook.Worksheets[0].Cells[i, k].PutValue(dr[dc.ColumnName]);
                        designer1.Workbook.Worksheets[0].AutoFitRow(i);

                        k++;
                    }
                    i++;
                }
            }
            designer1.Process();
            designer1.Save(Guid.NewGuid().ToString() + ".xls", Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
            // Response.End();
        }
        else
            return;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_LCGL_BackBoneProcEdit : System.Web.UI.Page
{
    private ShareLiuChengGuanLi shareLcgl = new ShareLiuChengGuanLi();
    private ShareResource shareResource = new ShareResource();
    private string tableName = "T_LCGL_BACKBONE";
    private string lcbm = "GGYWLC";
    private string firstJdbm = "YWSQ";
    private string last1Jdbm = "YWQR";
    private string lastJdbm = "GD";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            GUID.Text = Request.QueryString["GUID"];
            JDBM.Text = Request.QueryString["JDBM"];
            lcbm = Request.QueryString["lcbm"];
            firstJdbm = Request.QueryString["firstJdbm"];
            last1Jdbm = Request.QueryString["last1Jdbm"];
            lastJdbm = Request.QueryString["lastJdbm"];
            if (GUID.Text == "NEW")
            {
                GUID.Text = Guid.NewGuid().ToString();
            }
            BindDrp();
            FillPage();
            BindGrid();
            InitPrivate();
            HeadTitle.Text = shareLcgl.GetHeadTitle(lcbm, JDBM.Text);
        }
    }

    #region 初始化权限
    private void InitPrivate()
    {
        string pcode = shareLcgl.GetPcode(lcbm, JDBM.Text);
        if (IsHavePrivate(pcode))
        {
            Tr_Button.Style.Add("display", "block");
        }
    }

    public bool IsHavePrivate(string pcode)
    {
        if (Session["ISSUPER"].ToString() == "1")
        {
            return true;
        }
        else
        {
            string username = Session["UserName"].ToString();
            return DataFunction.HasRecord(string.Format(@"select p.* from t_sys_private p left join t_sys_r_groupprivate gp on gp.pcode = p.pcode left join t_Sys_r_Usergroup ug on ug.groupcode = gp.groupcode where ug.username = '{0}' and p.pcode = '{1}'", username, pcode));
        }
    }
    #endregion
    private void BindGrid()
    {
        string sql = string.Format(@"select 
case when t.lcqfbj=0 then '被驳回' when t.lcqfbj=1 then  '已签发' else '待处理' end  as qfbj,
t.lcjrsj,j.jdmc as dqzt,t.lcqfsj,q.jdmc as qfhzt,case when t.lcqfsj is not null then
round((t.lcqfsj-t.lcjrsj),0)||'天'||to_char(mod(round((t.lcqfsj-t.lcjrsj)*24),24),'00')||'时'
||to_char(mod(round((t.lcqfsj-t.lcjrsj)*24*60),60),'00')||'分' end as lcclsj 
,t.lcqfr, t.lcsm from t_lcgl_sys_lcjl t  
left join t_lcgl_sys_lckz_cb j on t.lcbm=j.lcbm and t.lcjrzt=j.jdbm
left join t_lcgl_sys_lckz_cb q on t.lcbm=q.lcbm and t.lcqfzt=q.jdbm where t.lc_guid='{0}' order by t.lcjrsj desc
", GUID.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        GridViewList.DataSource = ds;
        GridViewList.DataBind();
    }
    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, GetDataRow());
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        if (JDBM.Text == firstJdbm)
        {
            BackButton.Visible = false;
        }
        if (JDBM.Text == lastJdbm)
        {
            SendButton.Visible = false;
        }
        if (lcbm == "GGYWLC")
        {
            ImageButton1.Visible = false;
        }
        BindGV("1");
        BindGV("2");
    }

    private void BindDrp()
    {
        DataSet ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'YWLX' order by sequence");
        YWLX.DataSource = ds;
        YWLX.DataTextField = "ENUM_NAME";
        YWLX.DataValueField = "ENUM_NAME";
        YWLX.DataBind();
        YWLX.Items.Insert(0, new ListItem("", ""));
        YWLX.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'WXZL' order by sequence");
        JDWXZL.DataSource = ds;
        JDWXZL.DataTextField = "ENUM_NAME";
        JDWXZL.DataValueField = "ENUM_NAME";
        JDWXZL.DataBind();
        JDWXZL.Items.Insert(0, new ListItem("", ""));
        JDWXZL.SelectedIndex = 0;

        ds = DataFunction.FillDataSet("select * from T_RES_SYS_ENUMDATA where enum_sort = 'WXZL' order by sequence");
        YDWXZL.DataSource = ds;
        YDWXZL.DataTextField = "ENUM_NAME";
        YDWXZL.DataValueField = "ENUM_NAME";
        YDWXZL.DataBind();
        YDWXZL.Items.Insert(0, new ListItem("", ""));
        YDWXZL.SelectedIndex = 0;
        //YWLX.SelectedIndex = 0;
    }

    private DataRow GetDataRow()
    {
        string sql = "select * from " + tableName + " t where t.guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow dr = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = GUID.Text;
            dr["CREATEDATETIME"] = DateTime.Now;
            dr["UPDATEDATETIME"] = DateTime.Now;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        return dr;
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
        DataRow DR = DataFunction.GetSingleRow(sql);
        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName = JYD + DC.ColumnName;
            string strValue = DR[DC.ColumnName].ToString(); ;
            System.Web.UI.Control webControl = this.Page.FindControl(ControlName);
            if (webControl != null)
            {
                string controlType = webControl.GetType().FullName;
                switch (controlType)
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((System.Web.UI.WebControls.TextBox)webControl).Text = strValue;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        ShareFunction.SetDropListSelectedValue(((System.Web.UI.WebControls.DropDownList)webControl), strValue);
                        break;
                    case "System.Web.UI.WebControls.Label":
                        ((System.Web.UI.WebControls.Label)webControl).Text = strValue;
                        break;
                }
            }
        }
    }
    #endregion

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        SaveData();
    }

    private void SaveData()
    {
        //保存基本信息
        string message = "";
        if (SQDBH.Text == "")
        {
            message += "申请单编号不能为空！";
        }
        if (SUBSCRIBER_CODE.Text == "")
        {
            message += ",业务编号不能为空！";
        }
        if(message!="")
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('" + message + "');</script>");
            return;
        }
        string strSql = "select *  from " + tableName + " where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(strSql);
        DataRow dr;
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            YWGUID.Text = Guid.NewGuid().ToString();
            dr["guid"] = GUID.Text;
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
        }
        ShareFunction.GetControlData(Page, dr);
        DataFunction.SaveData(ds, tableName);
        shareLcgl.SaveFirstLczt(GUID.Text, lcbm, JDBM.Text);
    }

    protected void SendButton_Click(object sender, EventArgs e)
    {
        
       
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>sendPageShow('" + lcbm + "','" + JDBM.Text + "','1');</script>");
        if (string.IsNullOrEmpty(SQDBH.Text))
        {
            SQDBH.Text = shareLcgl.GetSqdbh(tableName);
        }
        if (JDBM.Text == last1Jdbm)
        {
            //更新光缆、骨干业务数据 t_con_bone_business、T_CON_LIGHT_BUSINESS
            getDateBone();
            getDateLight();
        }
        SaveData();
    }

    private void getDateBone()
    {
        string strSql = "select * from t_con_bone_business where ywguid='"+YWGUID.Text+"'";
        DataSet ds = DataFunction.FillDataSet(strSql);
        if(ds.Tables[0].Rows.Count==0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        DataRow DR = ds.Tables[0].Rows[0];
        string strPortGuid = DR["JDSBDK_GUID"].ToString() + "," + DR["YDSBDK_GUID"].ToString() + "," + JDSBDK_GUID.Text + "," + YDSBDK_GUID.Text;
        GetYWBM();
        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        string strComment = ShareFunction.GetControlData(Page, DR, "T_CON_BONE_BUSINESS");
        DataFunction.SaveData(ds, "T_CON_BONE_BUSINESS");
        shareResource.SetResourcePort(strPortGuid);
    }

    private void getDateLight()
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS where YWGUID = '{0}'", YWGUID.Text));
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            CREATEDATETIME.Text = DateTime.Now.ToString();
            ZYHS_BJ.Text = "1";
        }
        DataRow DR = ds.Tables[0].Rows[0];
        UPDATEDATETIME.Text = DateTime.Now.ToString();
        UPDATEUSERNAME.Text = "【" + Session["BranchName"].ToString().Trim() + "】" + Session["UserRealName"].ToString().Trim();
        string strComment = ShareFunction.GetControlData(Page, DR, "T_CON_LIGHT_BUSINESS");
        DR["YWLX"] = "骨干业务";
        DataFunction.SaveData(ds, "T_CON_LIGHT_BUSINESS");
        //更新T_CON_LIGHT_BUSINESS_CABLE 关联光缆段
        string strSql = "delete T_CON_LIGHT_BUSINESS_CABLE t where t.lightguid='"+YWGUID.Text+"'";
        DataFunction.ExecuteNonQuery(strSql);
        strSql = "update T_CON_LIGHT_BUSINESS_CABLE set lightguid='" + YWGUID.Text + "' where lightguid='"+GUID.Text+"'";
        DataFunction.ExecuteNonQuery(strSql);
    }

    private void GetYWBM()
    {
        if (string.IsNullOrEmpty(YWBM.Text))
        {
            string bm = "G_" + DateTime.Now.ToString("yyyyMM");
            string sql = "select nvl(max(to_number(substr(ywbm,9,4))),0)+1 as sxh  from t_con_bone_business t where t.ywbm like '" + bm + "%'";
            int sxh = DataFunction.GetIntResult(sql);
            YWBM.Text = bm + sxh.ToString("0000");
        }
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>sendPageShow('" + lcbm + "','" + JDBM.Text + "','0');</script>");
    }
    protected void TQ_Click(object sender, ImageClickEventArgs e)
    {
        if (SUBSCRIBER_CODE.Text != "")
        {

            string sql = "select * from rmss t where 1=1";

            sql += " and SUBSCRIBER_CODE  like '%" + SUBSCRIBER_CODE.Text + "%'";
            DataSet dt = DataFunction.FillDataSet(sql);
            if (dt.Tables[0].Rows.Count == 1)
            {
                SUBSCRIBER_ID.Text = dt.Tables[0].Rows[0]["SUBSCRIBER_ID"].ToString();
                FillRmssPage(SUBSCRIBER_ID.Text, "");
                //FileLightBone();
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
    protected void SelectBOSS_Click(object sender, ImageClickEventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        //FileLightBone();
    }

    protected void BtnDelJDGLD_Click(object sender, EventArgs e)
    {

    }

    protected void BtnDelYDGLD_Click(object sender, EventArgs e)
    {

    }
    protected void BtnRmss_Click(object sender, EventArgs e)
    {
        FillRmssPage(SUBSCRIBER_ID.Text, "");
        //FileLightBone();
    }
    protected void gvJDGLD_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvYDGLD_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGV("1");
        BindGV("2");
    }

    private void BindGV(string lb)
    {
        string str=GUID.Text;
        if (JDBM.Text == lastJdbm)
        {
            str=YWGUID.Text;
        }
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_LIGHT_BUSINESS_CABLE where LIGHTGUID = '{0}' and LB = '{1}' order by GLDXH", str, lb));
        GridView gv = gvJDGLD;
        if (lb.Equals("2"))
        {
            gv = gvYDGLD;
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

            gv.DataSource = ds;
            gv.DataBind();
        }
        else
        {
            gv.DataSource = ds;
            gv.DataBind();
        }
    }

    private void FileLightBone()
    {
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from T_CON_BONE_BUSINESS b where b.ywguid in (select ywguid from T_CON_LIGHT_BUSINESS where subscriber_code = '{0}')", SUBSCRIBER_CODE.Text));
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count > 0)
        {
            DR = ds.Tables[0].Rows[0];
        }
        else
        {
            DR = ds.Tables[0].NewRow();
            DR["YWGUID"] = YWGUID.Text;
            DR["ZYHS_BJ"] = "1";
            ds.Tables[0].Rows.Add(DR);
        }

        ShareFunction.FillControlData(Page, DR);
    }
    protected void SUBSCRIBER_GDLY_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeSUBSCRIBER_CODE();
        SUBSCRIBER_ID.Text = "";
        FillRmssPage(SUBSCRIBER_ID.Text, "");
    }

    private void ChangeSUBSCRIBER_CODE()
    {
        if (SUBSCRIBER_GDLY.SelectedValue == "BOSS")
        {
            TQ.Visible = true;
            SelectBOSS.Visible = true;
        }
        else
        {
            TQ.Visible = false;
            SelectBOSS.Visible = false;
        }
    }

    protected void BtnBone_Click(object sender, EventArgs e)
    {
        FillBonePage(YWBM.Text, "");
    }

    private void FillBonePage(string ywbm, string JYD)
    {
        string sql = string.Format("select * from t_con_bone_business t where t.ywbm='{0}'", ywbm);
        DataRow DR = DataFunction.GetSingleRow(sql);
        foreach (DataColumn DC in DR.Table.Columns)
        {
            string ControlName = JYD + DC.ColumnName.Replace("JR", "");
            string strValue = DR[DC.ColumnName].ToString();
            System.Web.UI.Control webControl = this.Page.FindControl(ControlName);
            if (webControl != null)
            {
                string controlType = webControl.GetType().FullName;
                switch (controlType)
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((System.Web.UI.WebControls.TextBox)webControl).Text = strValue;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        ShareFunction.SetDropListSelectedValue(((System.Web.UI.WebControls.DropDownList)webControl), strValue);
                        break;
                }
            }
        }
    }
}

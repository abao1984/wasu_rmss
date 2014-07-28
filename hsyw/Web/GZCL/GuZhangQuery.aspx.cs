using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;

public partial class Web_GZCL_GuZhangQuery : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {

            TSSJ1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TSSJ2.Text = TSSJ1.Text;
            TYPE.Text = Request.QueryString["TYPE"].ToString();
            //txtYRBM.Attributes.Add("ReadOnly", "true");
            //txtYRR.Attributes.Add("ReadOnly", "true");
            //txtCJRY.Attributes.Add("ReadOnly", "true");
            //txtCJBM.Attributes.Add("ReadOnly", "true");
            BindDrop();
            BindGridPage(BindGrid());
        }
    }
    private void BindDrop()
    {
        YWZT.Items.Clear();
        string sql = "select CODENAME from T_FAU_LXSZ where LB='ywzt' and SFQY=1";
        YWZT.DataSource = DataFunction.FillDataSet(sql);
        YWZT.DataTextField = "CODENAME";
        YWZT.DataValueField = "CODENAME";
        YWZT.DataBind();
        ListItem items = new ListItem("----请选择----","");
        YWZT.Items.Add(items);
        YWZT.SelectedValue = "";
        GZLY.Items.Clear();
        sql = "select codename from t_fau_lxsz t where t.lb='GZLY' and SFQY=1";
        GZLY.DataSource = DataFunction.FillDataSet(sql);
        GZLY.DataTextField = "codename";
        GZLY.DataValueField = "codename";
        GZLY.DataBind();
        GZLY.Items.Add(items);
        GZLY.SelectedValue = "";
        sql = "select MC from t_fau_gzdj t where sfqy=1";
        GZDJ.DataSource = DataFunction.FillDataSet(sql);
        GZDJ.DataTextField = "MC";
        GZDJ.DataValueField = "MC";
        GZDJ.DataBind();
        GZDJ.Items.Add(items);
        GZDJ.SelectedValue = "";
    }
    private int BindGrid()
    {
        DataSet ds = GetData();
        return gzcl.BindGridView(GridView1, ds) ;
    }

    private DataSet GetData()
    {
        string userID = Session["UserID"].ToString();
        //string sql = string.Format("select t.*,case when t.gzyyrid='{0}' or t.yjrcode like '%{0}%'  then 1 else 0 end as CZQX from t_fau_zb t where (t.gzyyrid='{0}' or t.yjrcode like '%{0}%' or t.csrid like '%{0}%') and  t.fdzzt is null ", userID);
        string sql = "select * from t_fau_zb t where 1=1 ";
        if (GZBH.Text != "")
        {
            sql += " and GZBH  like '%" + GZBH.Text + "%'";
        }
        if (TSSJ1.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd') >= '" + TSSJ1.Text + "'";
        }

        if (TSSJ2.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd') <= '" + TSSJ2.Text + "'";
        }

        if (dropGZZT.SelectedValue != "")
        {
            sql += " and gzzt= '" + dropGZZT.SelectedItem.Text + "'";
        }
        if (GZMC.Text != "")
        {
            sql += " and GZMC like '%" + GZMC.Text + "%'";
        }
        if (YWBH.Text != "")
        {
            sql += " and YWBH like '%" + YWBH.Text + "%'";
        }
        if (LXRNAME.Text != "")
        {
            sql += " and LXRNAME like '%" + LXRNAME.Text + "%'";
        }
        if (LXDH.Text != "")
        {
            sql += " and LXDH like '%" + LXDH.Text + "%'";
        }
        if (KHDZ.Text != "")
        {
            sql += " and KHDZ like '%" + KHDZ.Text + "%'";
        }
        if (GZDJ.SelectedValue != "")
        {
            sql += " and GZDJ like '%" + GZDJ.SelectedValue + "%'";
        }
        if (YWZT.SelectedValue != "")
        {
            sql += " and ywzt like '%" + YWZT.SelectedValue + "%'";
        }
        else
        {
            //默认显示华数传媒
            sql += "and ywzt like '%华数传媒%'";
        }
        if(YWLB.SelectedValue!="")
        {
            sql += " and ywlb  like '%" + YWLB.SelectedValue + "%'";
        }
        if (GZLY.SelectedValue != "")
        {
            sql += " and GZLY like '%" + GZLY.SelectedValue + "%'";
        }
        if (dropGZZT.SelectedValue != "")
        {
            sql += " and GZZT like '%" + dropGZZT.SelectedValue + "%'";
        }

        if (txtCJRY.Text != "")
        {
            sql += " and GZCJRNAME like '%" + txtCJRY.Text + "%'";
        }
        else
        {
            if(txtCJBMCODE.Text!="")
            {
                sql += " and GZCJRNAME in (select t.userrealname from t_sys_user t where t.branchcode='" + txtCJBMCODE.Text+ "') ";
            }
        }

        if (txtYRRID.Text != "")
        {
            sql += " and (GZYYRID like '%" + txtYRRID.Text + "%' or yjrcode like '%" + txtYRRID.Text + "%')";
        }
        else
        {
            if(txtYRBMCODE.Text!="")
            {
                sql += " and ( YJBMCODE like '%" + txtYRBMCODE.Text + "%' or CSBMCODE like '%"+txtYRBMCODE.Text+"%')";
            }
        }

        if (KHQY.Text != "")
        {
            sql += " and KHQY like '%" + KHQY.Text + "%'";
        }

        if (Session["ISSUPER"].ToString() != "1")
        {
            if (Session["FWQY"] == null)
            {
                sql += " and 1<>1 ";
            }
            else
            {
                string[] fwqy = Session["FWQY"].ToString().Split(',');
                string strQy = "";
                foreach (string qy in fwqy)
                {
                    if (strQy != "")
                    {
                        strQy += " or ";
                    }
                    strQy += " t.KHQYID like '" + qy + "%' ";
                }
                if (!string.IsNullOrEmpty(strQy))
                {
                    sql += " and (" + strQy + ") ";
                }
            }
        }
        switch (TYPE.Text)
        {
            case "CSZT":
                sql += " and CSZT ='1'";
                break;
            case "SJZT":
                sql += " and SJZT='1'";
                break;
        }
        sql += " order by tssj desc";
        DataSet ds = DataFunction.FillDataSet(sql);
        return ds;
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridView1.PageIndex = 0;
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        GridPageList.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
            GridPageList.Items.Add(LI);
        }
        DataCountLab.Text = DataCount.ToString();
        PageCountLab.Text = PageCount.ToString();
        PageIndexLab.Text = "1";
    }


    protected void PrevButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (PageIndex == 0)
        {
            GridPageList.SelectedIndex = GridPageList.Items.Count - 1;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex - 1;
        }
        GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }

    protected void NextButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (GridPageList.Items.Count - 1 == PageIndex)
        {
            GridPageList.SelectedIndex = 0;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex + 1;
        }
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }
    #endregion

    protected void BtnJS_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    protected void BtnEXCEL_Click(object sender, EventArgs e)
    {       
        try
        {
            string[] names = Session["dateName"].ToString().Split(',');
            string[] codes = Session["dateID"].ToString().Split(',');

            License lic = new License();
            lic.SetLicense(Server.MapPath("../../Aspose.Total.lic"));
            WorkbookDesigner designer1 = new WorkbookDesigner();
            object filePath = Server.MapPath("GZBB/gzbb.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                DataSet ds = GetData();
                string yyr = "";
                designer1.Open(Convert.ToString(filePath));
                Workbook workbook = designer1.Workbook;
                Cells cells = workbook.Worksheets[0].Cells;
                //头
                for (int i = 0; i < names.Length; i++ )
                {
                    cells[0, i].PutValue(names[i]);
                   // cells[0, i].Style.HorizontalAlignment = TextAlignmentType.Center;
                }
                
                //行
                for (int j = 1; j <= ds.Tables[0].Rows.Count; j++ )
                {
                    DataRow dr = ds.Tables[0].Rows[j-1];
                    for (int i = 0; i < codes.Length; i++)
                    {
                        if (codes[i] != "GZSC" && codes[i] != "YYR")
                        {
                            cells[j, i].PutValue(dr[codes[i]].ToString());
                        }
                        else
                        {
                            if (codes[i] == "GZSC")
                            {
                                //故障时长
                                if (dr["tssj"] == DBNull.Value)
                                {
                                    continue;
                                }
                                if (dr["jdsj"] == DBNull.Value)
                                {
                                    DateTime dt = DateTime.Now;
                                    TimeSpan ts = dt - Convert.ToDateTime(dr["tssj"]);
                                    cells[j, i].PutValue(ts.TotalMinutes.ToString("f0"));
                                }
                                else
                                { 
                                    TimeSpan ts = Convert.ToDateTime(dr["jdsj"] )- Convert.ToDateTime(dr["tssj"]) ;
                                    cells[j, i].PutValue(ts.TotalMinutes.ToString("f0"));
                                }
                            }
                            else if (codes[i] == "YYR")
                            {
                                //故障拥有人
                                if (dr["gzyyrid"].ToString().Length == 36)
                                {

                                    yyr = DataFunction.GetStringResult("select h.branchname||'/'||t.userrealname from t_sys_user t left join t_sys_branch h on t.branchcode=h.branchcode  where t.id='" + dr["gzyyrid"].ToString() + "'");
                                    cells[j, i].PutValue(yyr);
                                }
                            }
                        }
                        //cells[j, i].Style.HorizontalAlignment = TextAlignmentType.Center;
                    }
                }
                Session.Remove("dateName");
                Session.Remove("dateID");
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
    public static string ReturnUrlEncode(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return "";
        return System.Web.HttpUtility.UrlEncode(fileName);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string userId = Session["UserID"].ToString();
        if (e.Row.RowIndex > -1)
        {
            string zbguid = GridView1.DataKeys[e.Row.RowIndex][0].ToString();
            string strSql = string.Format(@"select t.gzyyrid,t.sfsd,
       case when t.csrid like '%{0}%' then 0 else 1 end as cs,
       case when t.yjrcode like '%{0}%' then 0 else 1 end as zs, 
       case when GZYDRID like '%{0}%' then 0 else 1 end as yd,
       t.sjzt,t.cszt,JDSJ,tssj
       from t_fau_zb t where zbguid='{1}'", userId, zbguid);
            DataRow dr = DataFunction.GetSingleRow(strSql);
            //故障时长
            if (dr["tssj"] == DBNull.Value)
            {
                return;
            }
            if (dr["jdsj"] == DBNull.Value)
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts = dt - Convert.ToDateTime(dr["tssj"]);
                e.Row.Cells[10].Text = ts.TotalMinutes.ToString("f0");
            }
            else
            {
                TimeSpan ts = Convert.ToDateTime(dr["jdsj"]) - Convert.ToDateTime(dr["tssj"]);
                e.Row.Cells[10].Text = ts.TotalMinutes.ToString("f0");
            }

            //故障拥有人
            if (dr["gzyyrid"].ToString().Length == 36)
            {

                e.Row.Cells[9].Text = DataFunction.GetStringResult("select h.branchname||'/'||t.userrealname from t_sys_user t left join t_sys_branch h on t.branchcode=h.branchcode  where t.id='" + dr["gzyyrid"].ToString() + "'");
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
}

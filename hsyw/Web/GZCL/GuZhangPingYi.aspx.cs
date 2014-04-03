using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangPingYi : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            TSSJ1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TSSJ2.Text = TSSJ1.Text;
            BindDrop();
            BindGridPage(BindGrid());
        }
    }

    private int BindGrid()
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

        //if (dropGZZT.SelectedValue != "")
        //{
        //    sql += " and gzzt= '" + dropGZZT.SelectedItem.Text + "'";
        //}
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
        if (GZLY.SelectedValue != "")
        {
            sql += " and GZLY like '%" + GZLY.SelectedValue + "%'";
        }
        //caoguangyao 2014-04-01
        if (ywlxDropDownList.SelectedValue.Length > 0)
        {
            string v = ywlxDropDownList.SelectedValue;
            sql += " and (ywlx = '" + v +"' or ywlb = '" + v + "')";
        }
       

        //if (dropGZZT.SelectedValue != "")
        //{
        //    sql += " and GZZT like '%" + dropGZZT.SelectedValue + "%'";
        //}
        //if (GZCJRNAME.Text != "")
        //{
        //    sql += " and GZCJRNAME like '%" + GZCJRNAME.Text + "%'";
        //}
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
        //sql += " order by tssj desc";
        //DataSet ds = DataFunction.FillDataSet(sql);
        //return ds;

        if (Session["ISSUPER"].ToString() != "1")
        {
            if (Session["FWQY"] != null)
            {
                string sql1 = "";
                string[] fwqy = Session["FWQY"].ToString().Split(',');
                foreach (string fwqy1 in fwqy)
                {

                    if (sql1 != "")
                    {
                        sql1 += " or t.KHQYID like '" + fwqy1 + "%'";
                    }
                    else
                    {
                        sql1 += "t.KHQYID like '" + fwqy1 + "%'";
                    }
                }
                sql += " and (" + sql1 + ")";
            }
            else
            {
                sql += " and 1<>1";
            }
        }
        sql += " order by tssj desc";
        DataSet ds = DataFunction.FillDataSet(sql);
       // gzcl.BindGridView(GridView1, ds);
        //int count = ds.Tables[0].Rows.Count ;
        //int num=0;
        //if (count > 0)
        //{
        //    Random ra = new Random(DateTime.Now.Millisecond);
        //    string guids = "''";
        //    //ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>windowOpen('" + gzbh + "');</script>");
        //    int n = 0;
        //    for (int i = 0; i < Convert.ToInt32(GS.Text); i++)
        //    {
        //        guids += ",'" + ds.Tables[0].Rows[ra.Next(count-1)]["ZBGUID"].ToString() + "'";
        //    }
        //    sql = "select * from T_FAU_ZB where zbguid in (" + guids + ")";
        //    DataSet dataSet = DataFunction.FillDataSet(sql);
        //    num=dataSet.Tables[0].Rows.Count;
        //    gzcl.BindGridView(GridView1, dataSet);
        //}

        //if (GridView1.Rows[0].Cells[0].Text == "没有相关的信息！")
        //{
        //    int count1 = GridView1.Columns.Count;
        //    GridView1.Rows[0].Cells.Clear();
        //    GridView1.Rows[0].Cells.Add(new TableCell());
        //    GridView1.Rows[0].Cells[0].Text = "没有相关的信息！";
        //    GridView1.Rows[0].Cells[0].ColumnSpan = count1;
        //    GridView1.Rows[0].Cells[0].Style.Add("text-align", "center");
        //}
        return gzcl.BindGridView(GridView1, ds);
    }

    private void BindDrop()
    {
        string sql = "select * from T_FAU_LXSZ where LB='ywzt' and SFQY=1";
        DataSet ds = DataFunction.FillDataSet(sql);
        YWZT.DataSource = ds;
        YWZT.DataTextField = "CODENAME";
        YWZT.DataValueField = "CODENAME";
        YWZT.DataBind();
        ListItem item = new ListItem("", "");
        YWZT.Items.Add(item);
        YWZT.SelectedValue = "";

        string query = @"select codename from t_fau_lxsz where lb = 'ywlx'";
        DataSet dataSet = DataFunction.FillDataSet(query);

        ywlxDropDownList.DataSource = dataSet;
        ywlxDropDownList.DataTextField = @"codename";
        ywlxDropDownList.DataValueField = @"codename";
        ywlxDropDownList.DataBind();

        ywlxDropDownList.Items.Add(item);
        ywlxDropDownList.SelectedValue = @"";
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
    //随机新增
    protected void BtnSJ_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    //protected void BtnQuery_Click(object sender, EventArgs e)
    //{
    //    BindGridPage(BindGrid());
    //}
    //protected void GZZY_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   string sql = "select * from T_FAU_LXSZ where LB='cc' and SFQY=1 and PARENT_NAME='" + YWZT.SelectedValue + "'";
    //    DataSet ds = DataFunction.FillDataSet(sql);
    //    GZCC.DataSource = ds;
    //    GZCC.DataTextField = "CODENAME";
    //    GZCC.DataValueField = "CODENAME";
    //    GZCC.DataBind();
    //    ListItem item = new ListItem("", "");
    //    GZCC.Items.Add(item);
    //    GZCC.SelectedValue = "";
    //}
    //protected void GZCC_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string sql = "select * from T_FAU_LXSZ where LB='gzlx' and SFQY=1 and PARENT_NAME='" + GZCC.SelectedValue + "'";
    //    DataSet ds = DataFunction.FillDataSet(sql);
    //    GZLX.DataSource = ds;
    //    GZLX.DataTextField = "CODENAME";
    //    GZLX.DataValueField = "CODENAME";
    //    GZLX.DataBind();
    //    ListItem item = new ListItem("", "");
    //    GZLX.Items.Add(item);
    //    GZLX.SelectedValue = "";
    //}
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
}

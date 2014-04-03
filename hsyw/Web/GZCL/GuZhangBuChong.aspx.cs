using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangBuChong : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateTime time = DateTime.Now;
            TSSJ1.Text = time.AddDays(-10).ToString("yyyy-MM-dd");
            TSSJ2.Text = time.ToString("yyyy-MM-dd");
            BindDrop();
            BindGridPage(BindGrid());
        }
    }

    private int BindGrid()
    {
        string sql = "select * from T_FAU_ZB t where t.GZZT = '结单' ";
        if (!string.IsNullOrEmpty(GZMS.Text))
        {
            sql += " and t.GZMC like '%" + GZMS.Text + "%'";
        }
        if (!string.IsNullOrEmpty(YWZT.SelectedValue))
        {
            sql += " and t.YWZT like '%" + YWZT.SelectedValue + "%'";
        }
        //if (!string.IsNullOrEmpty(GZZY.SelectedValue))
        //{
        //    sql += " and t.GZZY like '%" + GZZY.SelectedValue + "%'";
        //}
        //if (!string.IsNullOrEmpty(GZLX.SelectedValue))
        //{
        //    sql += " and t.GZLX like '%" + GZLX.SelectedValue + "%'";
        //}
        if (TSSJ1.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd') >= '" + TSSJ1.Text + "'";
        }

        if (TSSJ2.Text != "")
        {
            sql += " and to_char(t.tssj,'yyyy-mm-dd') <= '" + TSSJ2.Text + "'";
        }
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

        DataSet ds = DataFunction.FillDataSet(sql);
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
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    //protected void GZZY_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string sql = "select * from T_FAU_LXSZ where LB='cc' and SFQY=1 and PARENT_NAME='" + GZZY.SelectedValue + "'";
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
    //    string sql = "select * from T_FAU_LXSZ where LB='lx' and SFQY=1 and PARENT_NAME='" + GZCC.SelectedValue + "'";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_WeiFuGuZhangList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BM.ReadOnly = true;
            txtBM.Text = Session["BranchName"].ToString();
            txtBMCODE.Text = Session["BranchCode"].ToString();
            GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGV());
        }
    }
    private int BindGV()
    {
        int count = 0;
        //string sql = @"select t.*,round((JDSJ-TSSJ)*24*60,0) as GZSC,u.USERREALNAME as GZYYRNAME from T_FAU_ZB t left join T_SYS_USER u on t.GZYYR = u.USERNAME where t.GZZT != '结单' and FDZZT is null";
        string sql = string.Format(@"select t.*,
       case
         when t.gzyyrid = '{0}' or t.yjrcode like '%{0}%' then
          1
         else
          0
       end as CZQX
  from t_fau_zb t
 where  t.GZZT != '结单' and FDZZT is null", Session["UserID"].ToString());

            sql += " and (( yjbmcode like '%" + txtBMCODE.Text + "%' or csbmcode like '%" + txtBMCODE.Text + "%') ";

            sql += @" or  ( t.zbguid in
       (select distinct zbguid
          from t_fau_cllc h
         where h.clryid in
               (select id
                  from t_sys_user t
                 where t.branchcode =
                       (select branchcode from t_sys_user where id = '" +Session["UserID"].ToString()+"'))))) ";

        if (GZBH.Text != "")
        {
            sql += " and GZBH  like '%" + GZBH.Text + "%'";
        }

        if (KHMC.Text != "")
        {
            sql += " and GZMC like '%" + KHMC.Text + "%'";
        }
        if (!string.IsNullOrEmpty(KHQY.Text))
        {
            sql += " and t.KHQY = '" + KHQY.Text + "'";
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
        sql += " order by tssj desc";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GridView1.DataSource = ds;
            GridView1.DataBind();
            int count1 = GridView1.Columns.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].Text = "没有相关的信息！";
            GridView1.Rows[0].Cells[0].ColumnSpan = count1;
            GridView1.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
        else
        {
            count = ds.Tables[0].Rows.Count;
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        return count;
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
        BindGV();
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
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGV();
    }
    #endregion
    //检索
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }
    //导出EXCEL
    protected void BtnExpExcel_Click(object sender, EventArgs e)
    {

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
            //if (dr["gzyyrid"].ToString() != userId && dr["sfsd"].ToString() == "0")  //故障拥有人为空,锁定
            //{
            //    e.Row.FindControl("img_SD").Visible = true;
            //}

            ////主送，抄送
            //if (dr["zs"].ToString() == "0")
            //{
            //    e.Row.FindControl("img_zs").Visible = true;
            //}
            //else if (dr["cs"].ToString() == "0")
            //{
            //    e.Row.FindControl("img_cs").Visible = true;
            //}

            ////升级
            //if (dr["sjzt"].ToString() == "0")
            //{
            //    e.Row.FindControl("img_sj").Visible = true;
            //}
            ////超时
            //if (dr["cszt"].ToString() == "0")
            //{
            //    e.Row.FindControl("img_yq").Visible = true;
            //}
            ////string strYDR = string.Format(@"select * from t_fau_zb where GZYDRID like '%{0}%'",userId);
            ////是否阅读
            //if (dr["yd"].ToString() == "0")//已读
            //{
            //    e.Row.FindControl("img_yd").Visible = true;
            //}
            //else
            //{
            //    e.Row.FindControl("img_wd").Visible = true;
            //}

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
        }
    }
}

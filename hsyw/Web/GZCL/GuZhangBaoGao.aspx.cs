using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_GZCL_GuZhangBaoGao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            gvGZBG.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGV());
        }
    }

    private void BindDrop()
    {
        
        string sql = "select codename from T_FAU_LXSZ t where t.lb='ywzt' and SFQY=1";
        YWZT.DataSource = DataFunction.FillDataSet(sql);
        YWZT.DataTextField = "codename";
        YWZT.DataValueField = "codename";
        YWZT.DataBind();
    }

    private DataSet GetData()
    {
        string sql = "select t.*,b.BRANCHNAME as SSBM from T_FAU_GZBG t left join T_SYS_BRANCH b on t.SLRBCODE = b.BRANCHCODE left join T_FAU_ZB z on t.GZBH = z.GZBH  where z.GZZT = '结单'";
        if (!string.IsNullOrEmpty(GZMS.Text))
        {
            sql += " and t.GZMC like '%" + GZMS.Text + "%'";
        }
        if (!string.IsNullOrEmpty(YWZT.SelectedValue))
        {
            sql += " and z.YWZT ='" + YWZT.SelectedValue + "'";
        }
        //if (!string.IsNullOrEmpty(GZZY.SelectedValue))
        //{
        //    sql += " and t.GZZY like '%" + GZZY.SelectedValue + "%'";
        //}
        //if (!string.IsNullOrEmpty(GZLX.SelectedValue))
        //{
        //    sql += " and z.GZLX like '%" + GZLX.SelectedValue + "%'";
        //}
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
                        sql1 += " or z.KHQYID like '" + fwqy1 + "%'";
                    }
                    else
                    {
                        sql1 += "z.KHQYID like '" + fwqy1 + "%'";
                    }
                }
                sql += " and (" + sql1 + ")";
            }
            else
            { 
               sql += " and 1<>1";
            }
        }
        sql += " order by t.CJSJ desc";
        return DataFunction.FillDataSet(sql);
    }
    private int BindGV()
    {
        int count = 0;
        DataSet ds = GetData();
        count = ds.Tables[0].Rows.Count;
        if (count > 0)
        {
            gvGZBG.DataSource = ds;
            gvGZBG.DataBind();
        }
        else
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvGZBG.DataSource = ds;
            gvGZBG.DataBind();
            int count1 = gvGZBG.Columns.Count;
            gvGZBG.Rows[0].Cells.Clear();
            gvGZBG.Rows[0].Cells.Add(new TableCell());
            gvGZBG.Rows[0].Cells[0].Text = "没有相关的信息！";
            gvGZBG.Rows[0].Cells[0].ColumnSpan = count1;
            gvGZBG.Rows[0].Cells[0].Style.Add("text-align", "center");
          
        }
        return count;
    }
    //查询
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }
    //随机新增
    protected void BtnSJ_Click(object sender, EventArgs e)
    {
        string sql = "select t.GZBH from T_FAU_ZB t where t.GZZT = '结单' and FDZZT is null";
        if (!string.IsNullOrEmpty(GZMS.Text))
        {
            sql += " and t.GZMC like '%" + GZMS.Text + "%'";
        }
        if (!string.IsNullOrEmpty(YWZT.SelectedValue))
        {
            sql += " and t.YWZT ='" + YWZT.SelectedValue + "'";
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
        DataSet ds =DataFunction.FillDataSet(sql);
        int count = ds.Tables[0].Rows.Count;
        if (count > 0)
        {
            Random ra = new Random(DateTime.Now.Millisecond);
            int num = ra.Next(count);
            string gzbh = ds.Tables[0].Rows[num]["GZBH"].ToString();
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>windowOpen('"+gzbh+"');</script>");
        }
        else
        {
            
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('没有数据，请重新填写查询条件！');</script>");
        }
        if (gvGZBG.Rows[0].Cells[0].Text == "没有相关的信息！")
        {
            int count1 = gvGZBG.Columns.Count;
            gvGZBG.Rows[0].Cells.Clear();
            gvGZBG.Rows[0].Cells.Add(new TableCell());
            gvGZBG.Rows[0].Cells[0].Text = "没有相关的信息！";
            gvGZBG.Rows[0].Cells[0].ColumnSpan = count1;
            gvGZBG.Rows[0].Cells[0].Style.Add("text-align", "center");
        }
    }
    //删除
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        string gzbhs = "''";
        foreach (GridViewRow row in gvGZBG.Rows)
        {
            if (row.RowIndex > -1 && !row.Cells[0].Text.Equals("没有相关的信息！") && (row.FindControl("CheckBox1") as CheckBox).Checked)
            {
                gzbhs += ",'" + gvGZBG.DataKeys[row.RowIndex]["GZBH"].ToString() + "'";
            }
        }
        if (!gzbhs.Equals("''"))
        {
            string[] sql = new string[2];
            sql[0] = string.Format("delete from T_FAU_ZB where GZBH in ({0})", gzbhs);
            sql[1] = string.Format("delete from T_FAU_GZBG where GZBH in ({0})", gzbhs);
            DataFunction.ExecuteTransaction(sql);
            BindGV();
        }
        else
        {
            if (gvGZBG.Rows[0].Cells[0].Text == "没有相关的信息！")
            {
                int count1 = gvGZBG.Columns.Count;
                gvGZBG.Rows[0].Cells.Clear();
                gvGZBG.Rows[0].Cells.Add(new TableCell());
                gvGZBG.Rows[0].Cells[0].Text = "没有相关的信息！";
                gvGZBG.Rows[0].Cells[0].ColumnSpan = count1;
                gvGZBG.Rows[0].Cells[0].Style.Add("text-align", "center");
            }
        }
    }
    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvGZBG.PageIndex = 0;
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
        gvGZBG.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvGZBG.PageIndex + 1);
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
        gvGZBG.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvGZBG.PageIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvGZBG.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGV());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvGZBG.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvGZBG.PageIndex + 1);
        BindGV();
    }
    #endregion

    protected void Btn_Click(object sender, EventArgs e)
    {
        int DataCount = BindGV();
        DataCountLab.Text = DataCount.ToString();
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        PageCountLab.Text = PageCount.ToString();
    }
}

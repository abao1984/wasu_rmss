using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_JianKongUserQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GridView1.Attributes.Add("BorderColor", "#5B9ED1");
            if (!Request.QueryString["KHBM"].IsNullOrEmpty())
            {
                KHBM.Text = Convert.ToString(Request.QueryString["KHBM"]);
            }

            if (!Request.QueryString["YHBH"].IsNullOrEmpty())
            {
                TextYHBH.Text = Convert.ToString(Request.QueryString["YHBH"]);
            }

            if (!Request.QueryString["YHMC"].IsNullOrEmpty())
            {
                TextYHMC.Text = Convert.ToString(Request.QueryString["YHMC"]);
            }
            GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
            BindGridPage(BindGrid());
        }
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
        // GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
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
        // GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        // GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridPageList.SelectedIndex + 1);
        BindGrid();
    }
    #endregion

    #region 绑定数据
    private int BindGrid()
    {
        string sql = "select * from rmss t where 1=1";
        if (KHBM.Text != "")
        {
            sql += " and CUSTOMER_CODE like '%" + KHBM.Text.Trim() + "%'";
        }
        if (DataFunction.GetIntResult("select count(*) from ("+sql+")") == 0)
        {
            sql = "select * from rmss t where 1=1";
        }

        if (!YHBH.Text.IsNullOrEmpty())
        {
            sql += " and SUBSCRIBER_CODE like '%"+YHBH.Text+"%'";
        }
        if (!YHMC.Text.IsNullOrEmpty())
        {
            sql += " and SUB_NAME like '%" + YHMC.Text + "%'";
        }
     

        int num = DataFunction.GetIntResult(string.Format("select count(*) from ({0})", sql));
        int n = 0;
        if (GridPageList.SelectedIndex > -1)
        {
            n = GridPageList.SelectedIndex;
        }
        int s = n * Convert.ToInt32(PageSize.SelectedValue);
        int e = s + Convert.ToInt32(PageSize.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(string.Format("select * from  (select rownum as rn,a.* from ({0}) a ) where rn>{1} and rn<={2}", sql, s, e));



        //  int num = ds.Tables[0].Rows.Count;
        if (num == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if (num == 0)
        {
            GridView1.Rows[0].Cells.Clear();
            return 0;
        }
        return num;
    }
    #endregion

    #region 查询
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
    #endregion

    #region 确认
    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string yhbh = "";
        string yhmc = "";
        int idx = 0;
        GridView1.Rows.Cast<GridViewRow>().ForEach(dr => {
            bool bh = ((CheckBox)dr.Cells[0].FindControl("CheckBox1")).Checked;
            if (bh == true)
            {
                if (!yhbh.IsNullOrEmpty())
                {
                    yhbh += ",";
                }
                yhbh += Convert.ToString(GridView1.DataKeys[idx].Values["SUBSCRIBER_CODE"]);
                 if (!yhmc.IsNullOrEmpty())
                {
                    yhmc += ",";
                }
                yhmc+=Convert.ToString(GridView1.DataKeys[idx].Values["SUB_NAME"]);
            }
            ++idx;
        });
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script> parent.document.getElementById('" + TextYHMC.Text + "').value='" + yhmc + "';parent.document.getElementById('" + TextYHBH.Text + "').value='" + yhbh + "';parent.WindowClose();</script>");
    }
    #endregion
}

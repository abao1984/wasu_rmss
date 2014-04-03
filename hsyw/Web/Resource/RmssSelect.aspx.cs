using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_RmssSelect : System.Web.UI.Page
{
   
     protected void Page_Load(object sender, EventArgs e)
    {

        if(!Page.IsPostBack)
        {
            GridView1.Attributes.Add("BorderColor", "#5B9ED1");
            YWBM_NAME.Text = Request.QueryString["YWBM_NAME"];
            YWBMID.Text = Request.QueryString["YWBM_ID"];
            YWBM.Text = Request.QueryString["SUBSCRIBER_CODE"];
            GridView1.PageSize =Convert.ToInt32( PageSize.SelectedValue);
            BindGridPage( BindGrid());
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
    protected void BtnCJ_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>windowOpen(1)</script>");
    }

    private int BindGrid()
    {
        string sql = "select * from rmss t where 1=1";
        if (YWBM.Text!="")
        {
            sql += " and SUBSCRIBER_CODE like '%" + YWBM.Text.Trim() + "%'";
        }
        if (KHMC.Text!="")
        {
            sql += " and CUSTOMER_NAME like '%" + KHMC.Text.Trim() + "%'";
        }
        //if (SSQY.SelectedValue!="")
        //{
            
        //}
        //DataSet ds = DataFunction.FillDataSet(sql);

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
        if(num == 0 )
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        GridView1.DataSource = ds;
        GridView1.DataBind();
        if(num==0)
        {
            GridView1.Rows[0].Cells.Clear();
            return 0;
        }
        return num;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowIndex > -1)
        //{
        //    string ywbm = e.Row.Cells[1].Text;
        //    e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        //    e.Row.Cells[0].Text = "<a href=# onclick=\"windowOpen('','" + ywbm + "')\">创建</a>";
        //}
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        string ywbm = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script> parent.document.getElementById('" + YWBM_NAME.Text + "').value='" + ywbm + "';parent.WindowClose();</script>");
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script> parent.document.getElementById('" + YWBM_NAME.Text + "').value='';parent.WindowClose();</script>");

    }
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }
}

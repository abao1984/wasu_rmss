using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ConfigLightQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            gvLightList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        }
    }
    protected void BtnQuery_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGV());
    }

    private int BindGV()
    {
        string sql = @"select * from glzyjc";
        DataSet ds = DataFunction.FillDataSet(sql);
        gvLightList.DataSource = ds;
        gvLightList.DataBind();
        return ds.Tables[0].Rows.Count;
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        gvLightList.PageIndex = 0;
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
        gvLightList.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(gvLightList.PageIndex + 1);
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
        gvLightList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvLightList.PageIndex + 1);
        BindGV();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLightList.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGV();
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLightList.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(gvLightList.PageIndex + 1);
        BindGV();
    }
    #endregion
}

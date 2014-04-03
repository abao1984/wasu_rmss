using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Web_LCGL_XinKaiVpnList : BasePage
{
    private ShareLiuChengGuanLi shareLcgl = new ShareLiuChengGuanLi();
    private string tableName = "T_LCGL_XKVPN";
    private string lcbm = "XKVPN";
    private string firstJdbm = "YWSL";
    private string lastJdbm = "GD";
    protected void Page_Load(object sender, EventArgs e)
    {

        CreateMenuTable();
        GridViewList.Attributes.Add("BorderColor", "#5B9ED1");
        if (!this.IsPostBack)
        {
            KSRQ.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            JSRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            AspNetPager1.PageSize = 50;
            BindGrid();
        }
        InitPrivate();
    }

    #region 初始化权限
    private void InitPrivate()
    {
        string pcode = shareLcgl.GetPcode(lcbm, JDBM.Text);
        if (IsHavePrivate(pcode))
        {
            if (JDBM.Text == firstJdbm)
            {
                TR_ADD.Style.Add("display", "block");
            }
            else
            {
                TR_ADD.Style.Add("display", "none");
            }
        }
        else
        {
            TR_ADD.Style.Add("display", "none");
        }
    }
    #endregion

    #region 创建列表

    private void CreateMenuTable()
    {
        DataSet ds = GetMenuData();
        int i = 0;
        MenuTr.Cells.Clear();
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            CreateMenuCell(DR["JDBM"].ToString(), DR["JDMC"].ToString(), i);
            i++;
        }
      
        if (JDBM.Text == lastJdbm)
        {
            TR_QUERY.Style.Add("display", "block");
        }
        else
        {
            TR_QUERY.Style.Add("display", "none");
        }
    }
    private void CreateMenuCell(string jdbm, string jdmc, int i)
    {
        HtmlTableCell TD = new HtmlTableCell();
        TD.InnerHtml = "<a href=# onclick='changeMenu(\"" + jdbm + "\")'>" + jdmc + "</a>";
        if (JDBM.Text == "" && i == 0)
        {
            TD.Attributes.Add("class", "nav01");
            JDBM.Text = jdbm;
        }
        else if (JDBM.Text == jdbm)
        {
            TD.Attributes.Add("class", "nav01");
        }
        else
        {
            TD.Attributes.Add("class", "nav02");
        }
        MenuTr.Cells.Add(TD);
    }
    private DataSet GetMenuData()
    {
        string sql = string.Format(@" select a.jdbm,a.jdmc||'【'||nvl(b.cn,0)||'】' as jdmc from 
t_lcgl_sys_lckz_cb a left join 
(select t.lcjrzt,count(*) as cn from t_lcgl_sys_lcjl t where t.lcbm='{0}' and t.sfqf=0 group by t.lcjrzt) b
on a.jdbm=b.lcjrzt where a.lcbm='{0}' order by a.sxh", lcbm);
        return DataFunction.FillDataSet(sql);
    }
    #endregion


    private void BindGrid()
    {
        string sql = string.Format(@"select t.* from  t_lcgl_sys_lcjl j left join {2} t on t.guid=j.lc_guid  
 where j.LCBM='{0}' and  j.LCJRZT='{1}' and j.sfqf=0", lcbm, JDBM.Text, tableName);
        if (JDBM.Text == lastJdbm)
        {
            if (!string.IsNullOrEmpty(KSRQ.Text))
            {
                sql += " and t.CREATEDATETIME>=to_date('" + KSRQ.Text + "','yyyy-mm-dd')";
            }
            if (!string.IsNullOrEmpty(JSRQ.Text))
            {
                sql += " and t.CREATEDATETIME<to_date('" + JSRQ.Text + "','yyyy-mm-dd')+1";
            }
            if (!string.IsNullOrEmpty(SUBSCRIBER_CODE.Text))
            {
                sql += " and r.SUBSCRIBER_CODE  like '%" + SUBSCRIBER_CODE.Text + "%'";
            }
            if (!string.IsNullOrEmpty(SUB_NAME.Text))
            {
                sql += " and r.SUB_NAME  like '%" + SUB_NAME.Text + "%'";
            }
        }
        sql += " order by t.CREATEDATETIME";
        DataSet ds = DataFunction.FillDataSet(sql);
        ShareFunction.BindGridView(GridViewList, ds, AspNetPager1);
        //GridViewList.DataSource = ds;
        //GridViewList.DataBind();

    }

    protected void MenuButton_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GridViewList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string guid = GridViewList.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpenEdit('" + guid + "')");
        }
    }
    protected void QueryButton_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
}
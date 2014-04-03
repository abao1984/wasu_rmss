using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Web_GZCL_GZBB_GuZhangBaoBiao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            bindDrop();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string bblx=dropBBLX.SelectedItem.Text;
        switch (bblx)
        {
           case "---请选择---":
                return;
           case "今日统计":
//                strSql = string.Format(@"select count(case when h.lccz='留单' then h.guid end) as ld,count(case when h.lccz='遗单' then h.guid end) as yd,
//count(case when h.lccz='发单' then h.guid end) as fd from t_fau_cllc h where h.zbguid in (
//select zbguid from t_fau_zb t where  t.sffdz='1') and to_char(h.clsj,'yyyy-mm-dd')='2011-01-01' ");
                strSql = "select zbguid from t_fau_zb t where  t.sffdz='1' ";
                break;
           case "处理方法报表":
                strSql = "";
                break;
           case "故障类型报表":
                break;
           case "历史数据统计":
                break;
           case "历史记录查询":
                break;
           case "客户类型报表":
                break;
           case "故障数量统计":
                break;
           case "行业分类报表":
                break;
        }

        if (bblx != "今日统计")
        {
            if(TSSJ1.Text!="")
            {
                strSql += " and to_char(t.tssj,'yyyy-mm-dd')>='" + TSSJ1.Text + "'";
            }

            if (TSSJ2.Text != "")
            {
                strSql += " and to_char(t.tssj,'yyyy-mm-dd')<='" + TSSJ2.Text + "'";
            }
        }

        if (dropYWLB.SelectedValue!="")
        {
            strSql += " and YWLB='"+ dropYWLB.SelectedItem.Text +"'";
        }

        if (dropGZZY.SelectedValue != "")
        {
            strSql += " and GZZY='" + dropGZZY.SelectedItem.Text + "'";
        }
        if (dropGZCC.SelectedValue != "")
        {
            strSql += " and GZCC='" + dropGZCC.SelectedItem.Text + "'";
        }
        if (dropGZLX.SelectedValue != "")
        {
            strSql += " and GZLX='" + dropGZLX.SelectedItem.Text + "'";
        }
        if (dropGZYY.SelectedValue != "")
        {
            strSql += " and GZYY='" + dropGZYY.SelectedItem.Text + "'";
        }

        string sql = "";
        switch (bblx)
        {
            case "---请选择---":
                return;
            case "今日统计":
                sql = string.Format(@"select * from (
                select '留单' as ld, '遗单' as yd, '发单' as fd from dual
                union
                select to_char(count(case when h.lccz='留单' then h.guid end)) as ld,to_char(count(case when h.lccz='遗单' then h.guid end)) as yd,
                to_char(count(case when h.lccz='发单' then h.guid end)) as fd from t_fau_cllc h where h.zbguid in ({0}) and to_char(h.clsj,'yyyy-mm-dd')='{1}') b order by ld desc ", strSql, TSSJ1.Text);
                break;
            case "处理方法报表":
                strSql = "";
                break;
            case "故障类型报表":
                sql = string.Format(@"");
                break;
            case "历史数据统计":
                break;
            case "历史记录查询":
                break;
            case "客户类型报表":
                break;
            case "故障数量统计":
                break;
            case "行业分类报表":
                break;
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        initTable(ds,"50%");
    }

    private void initTable(DataSet ds,string tableWidth)
    {
        int i = 0;
        //tbbb.Rows
        tbbb.Width = tableWidth;
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            
            HtmlTableRow htmlRow = new HtmlTableRow();
            if(i==0)
            {
                htmlRow.Attributes.Add("class", "tdBak");
            }
            foreach(object obj in dr.ItemArray )
            {
                HtmlTableCell cell = new HtmlTableCell();
                cell.Align = "center";
                cell.InnerText = obj.ToString();
                htmlRow.Cells.Add(cell);
            }
            tbbb.Rows.Add(htmlRow);
            i++;
        }
    }

    #region 下拉列表
    private void bindDrop()
    {
        dropYWLB.Items.Clear();
        ListItem itmes = new ListItem("---请选择---");
        dropYWLB.Items.Add(itmes);
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywlb'");
        DataSet ds = DataFunction.FillDataSet(sql);
        dropYWLB.DataSource = ds;
        dropYWLB.DataTextField = "codename";
        dropYWLB.DataValueField = "guid";
        dropYWLB.DataBind();
    }

    protected void dropYWLB_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropGZZY.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='zy' and SFQY=1 and t.PARENT_ID='{0}'",dropYWLB.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        dropGZZY.DataSource = ds;
        dropGZZY.DataTextField = "codename";
        dropGZZY.DataValueField = "guid";
        dropGZZY.DataBind();
        sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='lx' and SFQY=1 and t.PARENT_ID='{0}'", dropYWLB.SelectedValue);
        dropGZLX.Items.Clear();
        DataSet dts = DataFunction.FillDataSet(sql);
        dropGZLX.DataSource = dts;
        dropGZLX.DataTextField = "codename";
        dropGZLX.DataValueField = "guid";
        dropGZLX.DataBind();
    }
    protected void dropGZZY_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropGZCC.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='zy' and SFQY=1 and t.PARENT_ID='{0}'",dropGZZY.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        dropGZCC.DataSource = ds;
        dropGZCC.DataTextField = "codename";
        dropGZCC.DataValueField = "guid";
        dropGZCC.DataBind();
    }
    protected void dropGZLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropGZYY.Items.Clear();
        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='zy' and SFQY=1 and t.PARENT_ID='{0}'", dropGZLX.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        dropGZYY.DataSource = ds;
        dropGZYY.DataTextField = "codename";
        dropGZYY.DataValueField = "guid";
        dropGZYY.DataBind();
    }

    protected void dropBBLX_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropBBLX.SelectedValue == "今日统计")
        {
            TSSJ2.Enabled = false;
        }
    }
    #endregion
}

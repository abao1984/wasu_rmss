using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Text;

public partial class Web_GZCL_GZBB_LiuDanYeWuTongJi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            bindDrop();
            dropYWZT_SelectedIndexChanged(null, null);
            TSSJ1.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 00";
            TSSJ2.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00";
        }
        string des = TSSJ1.Text;
    }

    private void bindDrop()
    {
        dropYWZT.Items.Clear();

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywzt' and sfqy=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        dropYWZT.DataSource = ds;
        dropYWZT.DataTextField = "codename";
        dropYWZT.DataValueField = "guid";
        dropYWZT.DataBind();
        ListItem itmes = new ListItem("全部", "");
        dropYWZT.Items.Add(itmes);
        dropYWZT.SelectedValue = "";
    }

    protected void dropYWZT_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckBoxList1.Items.Clear();
        string sql = @"select codename from t_fau_lxsz t where   t.lb='ywlx'";
        if (dropYWZT.SelectedValue != "")
        {
            sql += " and t.parent_id='" + dropYWZT.SelectedValue + "'";
        }
        DataSet ds = DataFunction.FillDataSet(sql);
        CheckBoxList1.DataSource = ds;
        CheckBoxList1.DataTextField = "codename";
        CheckBoxList1.DataBind();
        foreach (ListItem box in CheckBoxList1.Items)
        {
            box.Selected = true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        LoadTable();
    }

    private void LoadTable()
    {
        tb_dhgdl.Rows.Clear();
        //distinct z.ywzt,z.ywlb
        string strSql = @"select distinct z.ywzt,z.ywlb from t_fau_cllc t,t_fau_zb z 
where t.zbguid=z.zbguid  and t.lccz like '%留单%' ";
        if (TSSJ1.Text != "")
        {
            strSql += " and  t.clsj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
        }

        if (TSSJ2.Text != "")
        {
            strSql += "  and  t.clsj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
        }

        string ywlx = "";
        foreach (ListItem box in CheckBoxList1.Items)
        {
            if (box.Selected)
            {
                ywlx += "'" + box.Text + "',";
            }
        }
        if (ywlx != "")
        {
            ywlx = ywlx.Substring(0, ywlx.Length - 1);
            strSql += " and z.ywlb  in (" + ywlx + ")";
        }
        strSql += " order by ywzt,ywlb";
        DataSet ds = DataFunction.FillDataSet(strSql);
        strSql = @"select count(case when t.lccz='被动留单' then t.zbguid end) as bdcs,count(distinct case when t.lccz='被动留单' then t.zbguid end) as bdzs ,
count(case when t.lccz='主动留单' then t.zbguid end) as zhdcs,count(distinct case when t.lccz='主动留单' then t.zbguid end) as zhdzs ,
count(case when t.lccz='自动留单' then t.zbguid end) as zdcs,count(distinct case when t.lccz='自动留单' then t.zbguid end) as zdzs ,
count(t.zbguid) as cs,count(distinct t.zbguid) as zs
from t_fau_cllc t,t_fau_zb z where z.zbguid=t.zbguid and t.lccz in ('被动留单','主动留单','自动留单')";
        if (TSSJ1.Text != "")
        {
            strSql += " and  t.clsj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
        }

        if (TSSJ2.Text != "")
        {
            strSql += "  and  t.clsj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
        }
        tb_dhgdl.Width = Convert.ToString(180 + ds.Tables[0].Rows.Count * 80);
        HtmlTableRow htr1 = new HtmlTableRow();
        htr1.Align = "center";
        HtmlTableCell cell1 = new HtmlTableCell();
        cell1.InnerText = "业务主体";
        cell1.Width = "100";
        htr1.Cells.Add(cell1);

        HtmlTableRow htr2 = new HtmlTableRow();
        HtmlTableCell cell2 = new HtmlTableCell();
        htr2.Align = "center";
        cell2.InnerText = "业务类别";
        cell2.Width = "100";
        htr2.Cells.Add(cell2);

        HtmlTableRow htr3 = new HtmlTableRow();
        htr3.Align = "center";
        HtmlTableCell cell3 = new HtmlTableCell();
        cell3.InnerText = "被动留单";
        cell3.Width = "100";
        htr3.Cells.Add(cell3);

        HtmlTableRow htr4 = new HtmlTableRow();
        htr4.Align = "center";
        HtmlTableCell cell4 = new HtmlTableCell();
        cell4.InnerText = "主动留单";
        cell4.Width = "100";
        htr4.Cells.Add(cell4);

        HtmlTableRow htr5 = new HtmlTableRow();
        htr5.Align = "center";
        HtmlTableCell cell5 = new HtmlTableCell();
        cell5.InnerText = "自动留单";
        cell5.Width = "100";
        htr5.Cells.Add(cell5);

        HtmlTableRow htr6 = new HtmlTableRow();
        htr6.Align = "center";
        HtmlTableCell cell6 = new HtmlTableCell();
        cell6.InnerText = "合计";
        cell6.Width = "100";
        htr6.Cells.Add(cell6);

        int cellNum = 1;
        string sql = "";
        string ywzt = "";
        int bdcs = 0;
        int bdzs = 0;
        int zhdcs = 0;
        int zhdzs = 0;
        int zdcs = 0;
        int zdzs = 0;
        
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sql = string.Format(strSql+" and z.ywlb='" + dr["ywlb"].ToString() + "'");
            DataRow dataRow = DataFunction.GetSingleRow(sql);
            if (ywzt == "")
            {
                cell1 = new HtmlTableCell();
                ywzt = dr["ywzt"].ToString();
            }
            else if (ywzt != dr["ywzt"].ToString())
            {
                cell1.ColSpan = cellNum;
                cell1.InnerText = ywzt;
                cell1.Width = Convert.ToString(cellNum * 80);
                htr1.Cells.Add(cell1);
                cell1 = new HtmlTableCell();
                ywzt = dr["ywzt"].ToString();
                cellNum = 1;
            }
            else if (ywzt == dr["ywzt"].ToString())
            {
                cellNum++;
            }

            cell2 = new HtmlTableCell();
            cell2.InnerText = dr["ywlb"].ToString();
            cell2.Width = "80";
            htr2.Cells.Add(cell2);

            bdcs = Convert.ToInt32(dataRow["bdcs"]);
            bdzs = Convert.ToInt32(dataRow["bdzs"]);
            zhdcs = Convert.ToInt32(dataRow["zhdcs"]);
            zhdzs = Convert.ToInt32(dataRow["zhdzs"]);
            zdcs = Convert.ToInt32(dataRow["zdcs"]);
            zdzs = Convert.ToInt32(dataRow["zdzs"]);
            int cs = bdcs+zhdcs+zdcs;
            int zs = bdzs+zhdzs+zdzs;

            cell3 = new HtmlTableCell();
            if (CheckBoxList2.Items[0].Selected)
            {
                cell3.InnerText = cell3.InnerText+bdcs+",";
            }
            if (CheckBoxList2.Items[1].Selected)
            {
                cell3.InnerText = cell3.InnerText + bdzs + ",";
            }
            cell3.InnerText = cell3.InnerText.Substring(0, cell3.InnerText.Length - 1);
            cell3.Width = "80";
            htr3.Cells.Add(cell3);

            cell4 = new HtmlTableCell();
            if (CheckBoxList2.Items[0].Selected)
            {
                cell4.InnerText = cell4.InnerText + zhdcs + ",";
            }
            if (CheckBoxList2.Items[1].Selected)
            {
                cell4.InnerText = cell4.InnerText + zhdzs + ",";
            }
            cell4.InnerText = cell4.InnerText.Substring(0, cell4.InnerText.Length - 1);
            cell4.Width = "80";
            htr4.Cells.Add(cell4);

            cell5 = new HtmlTableCell();
            if (CheckBoxList2.Items[0].Selected)
            {
                cell5.InnerText = cell5.InnerText + zdcs + ",";
            }
            if (CheckBoxList2.Items[1].Selected)
            {
                cell5.InnerText = cell5.InnerText + zdzs + ",";
            }
            cell5.InnerText = cell5.InnerText.Substring(0, cell5.InnerText.Length - 1);
            cell5.Width = "80";
            cell5.Width = "80";
            htr5.Cells.Add(cell5);

            cell6 = new HtmlTableCell();
            if (CheckBoxList2.Items[0].Selected)
            {
                cell6.InnerText = cell6.InnerText + cs + ",";
            }
            if (CheckBoxList2.Items[1].Selected)
            {
                cell6.InnerText = cell6.InnerText + zs + ",";
            }
            cell6.InnerText = cell6.InnerText.Substring(0, cell6.InnerText.Length - 1);
            cell6.Width = "80";
            cell6.Width = "80";
            htr6.Cells.Add(cell6);
        }
        cell1.ColSpan = cellNum;
        cell1.InnerText = ywzt;
        cell1.Width = Convert.ToString(cellNum * 80);
        cell1.Align = "center";
        htr1.Cells.Add(cell1);

        //合计
        cell1 = new HtmlTableCell();
        cell1.RowSpan = 2;
        cell1.Width = "80";
        cell1.InnerText = "总计";
        htr1.Cells.Add(cell1);

        DataRow dataRow1 = DataFunction.GetSingleRow(strSql);
        bdcs = Convert.ToInt32(dataRow1["bdcs"]);
        bdzs = Convert.ToInt32(dataRow1["bdzs"]);
        zhdcs = Convert.ToInt32(dataRow1["zhdcs"]);
        zhdzs = Convert.ToInt32(dataRow1["zhdzs"]);
        zdcs = Convert.ToInt32(dataRow1["zdcs"]);
        zdzs = Convert.ToInt32(dataRow1["zdzs"]);
        int cs1 = bdcs + zhdcs + zdcs;
        int zs1 = bdzs + zhdzs + zdzs;

        cell2 = new HtmlTableCell();
        if (CheckBoxList2.Items[0].Selected)
        {
            cell2.InnerText = cell2.InnerText + bdcs + ",";
        }
        if (CheckBoxList2.Items[1].Selected)
        {
            cell2.InnerText = cell2.InnerText + bdzs + ",";
        }
        cell2.InnerText = cell2.InnerText.Substring(0, cell2.InnerText.Length - 1);
        cell2.Width = "80";
        htr3.Cells.Add(cell2);

        cell3 = new HtmlTableCell();
        if (CheckBoxList2.Items[0].Selected)
        {
            cell3.InnerText = cell3.InnerText + zhdcs + ",";
        }
        if (CheckBoxList2.Items[1].Selected)
        {
            cell3.InnerText = cell3.InnerText + zhdzs + ",";
        }
        cell3.InnerText = cell3.InnerText.Substring(0, cell3.InnerText.Length - 1);
        cell3.Width = "80";
        htr4.Cells.Add(cell3);

        cell4 = new HtmlTableCell();
        if (CheckBoxList2.Items[0].Selected)
        {
            cell4.InnerText = cell4.InnerText + zdcs + ",";
        }
        if (CheckBoxList2.Items[1].Selected)
        {
            cell4.InnerText = cell4.InnerText + zdzs + ",";
        }
        cell4.InnerText = cell4.InnerText.Substring(0, cell4.InnerText.Length - 1);
        cell4.Width = "80";
        cell4.Width = "80";
        htr5.Cells.Add(cell4);

        cell5 = new HtmlTableCell();
        if (CheckBoxList2.Items[0].Selected)
        {
            cell5.InnerText = cell5.InnerText + cs1 + ",";
        }
        if (CheckBoxList2.Items[1].Selected)
        {
            cell5.InnerText = cell5.InnerText + zs1 + ",";
        }
        cell5.InnerText = cell5.InnerText.Substring(0, cell5.InnerText.Length - 1);
        cell5.Width = "80";
        cell5.Width = "80";
        htr6.Cells.Add(cell5);

        tb_dhgdl.Rows.Add(htr1);
        tb_dhgdl.Rows.Add(htr2);
        tb_dhgdl.Rows.Add(htr3);
        tb_dhgdl.Rows.Add(htr4);
        tb_dhgdl.Rows.Add(htr5);
        tb_dhgdl.Rows.Add(htr6);
        //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>test2()</script>");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        CreateExcel(TextBox1.Text);
    }

    public void CreateExcel(string str)
    {

        Response.AddHeader("Content-Disposition", "attachment;filename=ldywtj.xls");
        Response.ContentType = "application/ms-excel";
       // Response.ContentEncoding = Encoding.GetEncoding("GB2312");
        Response.Write(str);
        Response.End();
    }

    //[Ajax.AjaxMethod()]
    //public string GetTJRY(string sj1, string sj2)
    //{
    //    string tjry = "";
    //    string strSql = string.Format(@"select distinct clry from t_fau_cllc t where t.clsj>=to_date('{0}','yyyy-mm-dd hh24') and  t.clsj<=to_date('{1}','yyyy-mm-dd hh24') and t.lczt='电话处理'", sj1, sj2);
    //    DataSet ds = DataFunction.FillDataSet(strSql);
    //    foreach (DataRow dr in ds.Tables[0].Rows)
    //    {
    //        tjry += dr["clry"].ToString() + ",";
    //    }
    //    if (tjry != "")
    //    {
    //        tjry = tjry.Substring(0, tjry.Length - 1);
    //    }
    //    return tjry;
    //}
}

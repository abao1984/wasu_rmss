using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using Aspose.Cells;

public partial class Web_GZCL_GZBB_GuZhangLeiXing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            bindDrop();
            InitPage();
            InitTable();
            BindTable();
        }
    }
    private void bindDrop()
    {
        dropYWLB.Items.Clear();

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywzt' and sfqy=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        dropYWLB.DataSource = ds;
        dropYWLB.DataTextField = "codename";
        dropYWLB.DataValueField = "codename";
        dropYWLB.DataBind();
        ListItem itmes = new ListItem("---请选择---", "");
        dropYWLB.Items.Add(itmes);
        dropYWLB.SelectedValue = "";
    }

    private void InitPage()
    {
        DateTime nowTime = DateTime.Now;
        TSSJ1.Text = nowTime.AddDays(-7).ToString("yyyy-MM-dd");
        TSSJ2.Text = nowTime.AddDays(-1).ToString("yyyy-MM-dd");
    }

    private void InitTable()
    {
        int idx = 0;
        tbbb.Rows.Clear();
        //表头
        string ywlb = "";
        int colspan = 1;
        string sql = string.Format(@"select ywzt,gzlx from t_fau_zb t 
where t.gzzt = '结单' and to_char(t.jdsj,'yyyy-mm-dd')>='{0}' and to_char(t.jdsj,'yyyy-mm-dd')<='{1}'
", TSSJ1.Text,TSSJ2.Text);
        if (dropYWLB.SelectedValue != "")
        {
            sql += " and t.ywzt='" + dropYWLB.SelectedValue + "'";
        }
        string ywlx = GetYWLX();
        if(ywlx != "''")
        {
            sql += " and t.ywlx in (" + ywlx + ")";
        }
        sql += " group by gzlx,ywzt order by ywzt desc ";
        DataSet ds = DataFunction.FillDataSet(sql);
        HtmlTableRow htmlRow1 = new HtmlTableRow();
        HtmlTableRow htmlRow2 = new HtmlTableRow();
        htmlRow1.Attributes.Add("class", "GridViewHead");
        htmlRow2.Attributes.Add("class", "GridViewHead");
        HtmlTableCell cell = new HtmlTableCell();
        cell.Align = "center";
        //cell.Width = "5%";
        cell.InnerText = "时间";
        cell.RowSpan = 2;
        //cell.Attributes.Add("class","time");
        htmlRow1.Cells.Add(cell);
        HtmlTableCell cell1 = new HtmlTableCell();
       
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dr = ds.Tables[0].Rows[i];
            if (ywlb == "")
            {
                ywlb = dr["ywzt"].ToString();
                cell1 = new HtmlTableCell();
                cell1.Align = "center";
                cell1.InnerText = dr["ywzt"].ToString();
            }
            else if (ywlb != dr["ywzt"].ToString())
            {
                cell1.ColSpan = colspan;
                htmlRow1.Cells.Add(cell1);
                ywlb = dr["ywzt"].ToString();
                colspan = 1;
                cell1 = new HtmlTableCell();
                cell1.Align = "center";
                cell1.InnerText = dr["ywzt"].ToString();
            }
            else if (ywlb == dr["ywzt"].ToString() && dr["gzlx"] != DBNull.Value)
            {
                colspan++;
            }

            if (i == ds.Tables[0].Rows.Count - 1)
            {
                cell1.ColSpan = colspan;
                htmlRow1.Cells.Add(cell1);
            }
            if (dr["gzlx"] != DBNull.Value)
            {
                HtmlTableCell cell2 = new HtmlTableCell();
                cell2.Align = "center";
                //cell2.Attributes.Add("class", "th");
                cell2.InnerText = dr["gzlx"].ToString();
                cell2.ID = ywlb;
                htmlRow2.Cells.Add(cell2);
                //用于得到有几列，算出表的总宽度 罗耀斌
                ++idx;
            }
        }
        tbbb.Rows.Add(htmlRow1);
        tbbb.Rows.Add(htmlRow2);
        //tbbb.Width = idx * 80 + "px";
        
    }

    private void BindTable()
    {
        //时间行
        DateTime time1 = Convert.ToDateTime(TSSJ1.Text);
        DateTime time2 = Convert.ToDateTime(TSSJ2.Text);

        HtmlTableRow htmlRow1 = tbbb.Rows[1]; 
        if (htmlRow1.Cells.Count > 1)
        {
            while (time1 <= time2)
            {
                int i = 0;
                HtmlTableRow htmlRow = new HtmlTableRow();
                HtmlTableCell cell2 = new HtmlTableCell();
                //第一列时间
                cell2.Align = "center";
                cell2.InnerText = time1.ToString("yyyy-MM-dd");
                htmlRow.Cells.Add(cell2);

                foreach (HtmlTableCell cell in htmlRow1.Cells)
                {
                    HtmlTableCell cell1 = new HtmlTableCell();
                    cell1.Align = "center";
                    cell1.InnerText = getNum(cell.InnerText, time1.ToString("yyyy-MM-dd"),cell.ID);
                    htmlRow.Cells.Add(cell1);
                    i++;
                }
                tbbb.Rows.Add(htmlRow);
                time1 = time1.AddDays(1);
            }
        }
        else
        {
            tbbb.Rows.Clear();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitTable();
        BindTable();
    }

    private string getNum(string lxName,string time,string ztName)
    {
        string sql = string.Format(@"select count(*) from t_fau_zb t where  to_char(t.jdsj,'yyyy-mm-dd')='{1}' and t.gzlx = '{0}' and t.ywzt='{2}'", lxName, time, ztName);
        if (dropYWLB.SelectedValue != "")
        {
            sql += " and t.ywzt='" + dropYWLB.SelectedValue + "'";
        }
        string ywlx = GetYWLX();
        if (ywlx != "''")
        {
            sql += " and t.ywlx in ("+ywlx+")";
        }
        return DataFunction.GetStringResult(sql);

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            WorkbookDesigner designer1 = new WorkbookDesigner();
            object filePath = Server.MapPath("GZLXBB.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Workbook workbook=designer1.Workbook;
                InitTable();
                BindTable();
                int length = tbbb.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    int length1 = tbbb.Rows[i].Cells.Count;
                    int colspan = 0;
                    for (int j = 0; j < length1; j++)
                    {
                        HtmlTableCell cell = tbbb.Rows[i].Cells[j];
                        if (i == 0)
                        {
                            if (j == 0)
                            {
                                workbook.Worksheets[0].Cells[i, j].PutValue(cell.InnerText);
                                workbook.Worksheets[0].Cells.Merge(0, 0, 2, 1);
                            }
                            else
                            {
                                workbook.Worksheets[0].Cells[i, 1+colspan].PutValue(cell.InnerText);
                                workbook.Worksheets[0].Cells.Merge(0,1+colspan,1, cell.ColSpan);
                                if (cell.ColSpan > 1)
                                {
                                    colspan += cell.ColSpan;
                                }
                            }
                            
                        }
                        else if (i == 1)
                        {
                            workbook.Worksheets[0].Cells[i, j+1].PutValue(cell.InnerText);
                        }
                        else
                        {
                            workbook.Worksheets[0].Cells[i, j].PutValue(cell.InnerText);
                        }
                    }
                }
               
                designer1.Save(System.Web.HttpUtility.UrlEncode("故障类型报表.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
                Response.End();
            }
            else
                return;
        }
        catch (Exception ex)
        {
            string aa = ex.Message;
        }
    }
    protected void dropYWLB_SelectedIndexChanged(object sender, EventArgs e)
    {
        YWLX.Items.Clear();

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywlx' and sfqy=1 and parent_name = '{0}'",dropYWLB.SelectedValue);
        DataSet ds = DataFunction.FillDataSet(sql);
        YWLX.DataSource = ds;
        YWLX.DataTextField = "codename";
        YWLX.DataValueField = "codename";
        YWLX.DataBind();
    }
    private string GetYWLX()
    {
        string ywlx = "''";
        foreach (ListItem item in YWLX.Items)
        {
            if (item.Selected)
            {
                ywlx += ",'"+item.Value+"'";
            }
        }
        return ywlx;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using Aspose.Cells;

public partial class Web_GZCL_GZBB_JinRiTongJi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {

            bindDrop();
            InitPage();
            InitTable();
            BindList();
        }
    }

    private void bindDrop()
    {
        DropDownList1.Items.Clear();

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywzt' and sfqy=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "codename";
        DropDownList1.DataValueField = "guid";
        DropDownList1.DataBind();
        ListItem item = new ListItem("---请选择---", "");
        DropDownList1.Items.Add(item);
        DropDownList1.SelectedValue = "";
    }

    private void InitPage()
    {
        DateTime nowTime = DateTime.Now;
        TSSJ1.Text = nowTime.ToString("yyyy-MM-dd");
        //TSSJ2.Text = nowTime.AddDays(-1).ToString("yyyy-MM-dd");
    }

    private void InitTable()
    {
        //表头
        string sql = string.Format(@"select *
  from (select case
                 when t.gzyyr is not null and t.xfry is null and to_char(t.gzsdsj,'yyyy-mm-dd')='{0}' then
                  t.gzyyr
                  when to_char(t.jdsj,'yyyy-mm-dd')='{0}' 
                  then t.xfry 
               end as name
          from t_fau_zb t
         where t.SFFDZ = 1 or t.gzzt='结单' ", TSSJ1.Text);
        if (DropDownList1.SelectedValue != "")
        {
            sql += " and t.ywlb='" + DropDownList1.SelectedValue + "'";
        }
        sql += "  ) group by name ";
        DataSet ds = DataFunction.FillDataSet(sql);
        HtmlTableRow htmlRow1 = new HtmlTableRow();
        HtmlTableRow htmlRow2 = new HtmlTableRow();
        HtmlTableRow htmlRow3 = new HtmlTableRow();
        HtmlTableRow htmlRow4 = new HtmlTableRow();
        htmlRow1.Attributes.Add("class", "GridViewHead");
        HtmlTableCell cell1 = new HtmlTableCell();
        cell1.Width = "5%";
        cell1.Align = "center";
        cell1.InnerText = "组";
        htmlRow1.Cells.Add(cell1);
        HtmlTableCell cell2 = new HtmlTableCell();
        cell2.Align = "center";
        cell2.InnerText = "已完成";
        cell2.Attributes.Add("class", "GridViewHead");
        htmlRow2.Cells.Add(cell2);
        HtmlTableCell cell3 = new HtmlTableCell();
        cell3.Align = "center";
        cell3.InnerText = "处理中";
        cell3.Attributes.Add("class", "GridViewHead");
        htmlRow3.Cells.Add(cell3);
        HtmlTableCell cell4 = new HtmlTableCell();
        cell4.Align = "center";
        cell4.InnerText = "超时单";
        cell4.Attributes.Add("class", "GridViewHead");
        htmlRow4.Cells.Add(cell4);
        int num1 = 0;
        int num2 = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (dr["name"] == DBNull.Value)
            {
                continue;
            }
            int number1 = GetYWC(dr["name"].ToString());
            int number2 = GetCLZ(dr["name"].ToString());
            //组员
            HtmlTableCell cells1 = new HtmlTableCell();
            cells1.Align = "center";
            cells1.InnerText = dr["name"].ToString() ;
            htmlRow1.Cells.Add(cells1);
            //已完成
            HtmlTableCell cells2 = new HtmlTableCell();
            cells2.Align = "center";
            cells2.InnerText = number1.ToString() ;
            htmlRow2.Cells.Add(cells2);
            num1 += number1;
            //处理中
            HtmlTableCell cells3 = new HtmlTableCell();
            cells3.Align = "center";
            cells3.InnerText = number2.ToString();
            num2 += number2;
            htmlRow3.Cells.Add(cells3);
            //超时单
            HtmlTableCell cells4 = new HtmlTableCell();
            cells4.Align = "center";
            cells4.InnerText = GetCSD(dr["name"].ToString());
            htmlRow4.Cells.Add(cells4);
        }
        cell1 = new HtmlTableCell();
        cell1.Width = "5%";
        cell1.Align = "center";
        cell1.InnerText = "总计";
        htmlRow1.Cells.Add(cell1);
        cell2 = new HtmlTableCell();
        cell2.Align = "center";
        cell2.InnerText = num1.ToString();
        htmlRow2.Cells.Add(cell2);
        cell3 = new HtmlTableCell();
        cell3.Align = "center";
        cell3.InnerText = num2.ToString();
        htmlRow3.Cells.Add(cell3);
        cell4 = new HtmlTableCell();
        cell4.Align = "center";
        cell4.InnerText = GetCSD("");
        htmlRow4.Cells.Add(cell4);
        tbbb.Rows.Add(htmlRow1);
        tbbb.Rows.Add(htmlRow2);
        tbbb.Rows.Add(htmlRow3);
        tbbb.Rows.Add(htmlRow4);
    }

    private int GetYWC(string name)
    {
        //@dsh 2012-03-05
        //string sql = string.Format(@"select count(*) from t_fau_zb t where (t.xfry='{0}' or and to_char(t.jdsj,'yyyy-mm-dd')='{1}'",name,TSSJ1.Text);
        string sql = string.Format(@"select count(*) from t_fau_zb t where  (t.xfry='{0}' or(t.xfry is null and  t.gzyyr='{0}')) and  t.gzzt='结单' and to_char(t.jdsj,'yyyy-mm-dd')='{1}'", name, TSSJ1.Text);
        return DataFunction.GetIntResult(sql) ;
    }


    private int GetCLZ(string name)
    {
        string sql = string.Format(@"select count(*) from t_fau_zb t where t.gzyyr='{0}' and to_char(t.gzsdsj,'yyyy-mm-dd')='{1}'", name, TSSJ1.Text);
        return DataFunction.GetIntResult(sql);
    }

    private string GetCSD(string name)
    {
        string sql = string.Format(@"select sum(to_number(t.ms))
  from t_fau_lxsz t
 where t.lb = 'fdjg'
   and t.sfqy = 0
   and t.parent_name='{0}'
   and t.codename in ('调度发单','维修返单')",DropDownList1.SelectedItem.Text);
        int fdfdsj = DataFunction.GetIntResult(sql);
        string strSql = string.Format(@"select count(case
               when (t.jdsj - t.ddfdsj) * 24 * 60 > {0} then
                zbguid
             end) || '/' || count(case
                                    when t.cszt = 1 then
                                     zbguid
                                  end) as car
  from t_fau_zb t
 where (t.sffdz = 1 or t.gzzt='结单') and t.jdsj is not null
    ", fdfdsj);
        if(name!="")
        {
            strSql += " and t.xfry ='"+ name +"'";
        }
        return DataFunction.GetStringResult(strSql);
    }

    #region 绑定故障明细时得到表结构、数据和设置表的行数，列数等
    private void setGzTable()
    {
        
        string sql = string.Format(@"select GZLX,count(*) as num from t_fau_zb t where  to_char(t.jdsj,'yyyy-mm-dd')='{0}' group by GZLX", TSSJ1.Text);
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            return;
        }
        //得到表格最大行数，
        int row = getMaxRow(ds);
        //得到表头
        HtmlTableRow header = new HtmlTableRow();
        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
        {
            HtmlTableCell gzlx = new HtmlTableCell();
            gzlx.InnerText=Convert.ToString(ds.Tables[0].Rows[i]["GZLX"]);
            gzlx.Attributes.Add("class", "GridViewHead");

            HtmlTableCell num = new HtmlTableCell();
            num.InnerText=Convert.ToString(ds.Tables[0].Rows[i]["NUM"]);
            num.Attributes.Add("class", "GridViewHead");
            header.Cells.Add(gzlx);
            header.Cells.Add(num);
        }
        GzlxGrid.Rows.Add(header);

        //得到表结构
        for (int i = 0; i < row; i++)
        {
            HtmlTableRow dr = new HtmlTableRow();
            for (int j = 0; j < ds.Tables[0].Rows.Count*2; j++)
            {
                HtmlTableCell cell = new HtmlTableCell();
               
                cell.Align = "center";
                dr.Cells.Add(cell);
            }
           GzlxGrid.Rows.Add(dr);
        }
      
        //创建数据
        int idx=0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            idx = i * 2;
            string gzlx = Convert.ToString(ds.Tables[0].Rows[i]["GZLX"]);
            DataTable dt = CreateTableData(gzlx);
            for (int c = 0; c < dt.Rows.Count; c++)
            {
                GzlxGrid.Rows[c + 1].Cells[idx].InnerText = Convert.ToString(dt.Rows[c]["GZYY"]);
                GzlxGrid.Rows[c + 1].Cells[idx + 1].InnerText = Convert.ToString(dt.Rows[c]["NUM"]);
            }
        }
    }

    #region 得到表数据
    private DataTable CreateTableData(string gzlx)
    {
        string sql = @"select GZYY,count(*) as num from t_fau_zb t 
                            where to_char(t.jdsj,'yyyy-mm-dd')='" + TSSJ1.Text + "' and t.gzlx='" + gzlx + "' group by t.GZYY";

        return DataFunction.FillDataSet(sql).Tables[0];
       
    }
    #endregion

    #region 得到表的行数
    private int getMaxRow(DataSet ds)
    {
        int row = 0;
        string sql = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                sql += " union all ";
            }
            sql += "select count(*) r from (select gzyy,count(*) num from t_fau_zb t where to_char(t.jdsj,'yyyy-MM-dd')='" + TSSJ1.Text + "' and t.gzlx='" + Convert.ToString(ds.Tables[0].Rows[i]["GZLX"]) + "' group by t.gzyy )";
        }
        sql = "select max(r) from (" + sql + ")";
        row = DataFunction.GetIntResult(sql);
        return row;
    }
    #endregion


    #endregion


    private void BindList()
    {
        //string sql = string.Format(@"select GZLX,count(*) as num from t_fau_zb t where  to_char(t.jdsj,'yyyy-mm-dd')='{0}' group by GZLX", TSSJ1.Text);
        //DataSet ds = DataFunction.FillDataSet(sql);
        //DataList1.DataSource = ds;
        //DataList1.DataBind();
        setGzTable();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitTable();
        BindList();
        
    }

    //private string getNum(string lxName, string time)
    //{
    //    string sql = string.Format(@"select count(*) from t_fau_zb t where t.fdzzt='结单' and to_char(t.jdsj,'yyyy-mm-dd')='{1}' and t.gzlx = '{0}'", lxName, time);
    //    return DataFunction.GetStringResult(sql);

    //}
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if(e.Item.ItemIndex > -1)
        {
            string ywlb = ((System.Web.UI.WebControls.Label)e.Item.FindControl("Label1")).Text;
            DataList dl = (DataList)e.Item.FindControl("DataList2");
            string sql = string.Format(@"select GZYY,count(*) as num from t_fau_zb t 
where  to_char(t.jdsj,'yyyy-mm-dd')='{0}' and t.gzlx='{1}' group by t.GZYY", TSSJ1.Text, ywlb);
            DataSet ds = DataFunction.FillDataSet(sql);
            dl.DataSource = ds;
            dl.DataBind();
        }
    }
    //导出Excel
    protected void Button2_Click(object sender, EventArgs e)
    {
        Button1_Click(null, null);
        License lic = new License();
        lic.SetLicense(Server.MapPath("../../../Aspose.Total.lic"));
        WorkbookDesigner designer1 = new WorkbookDesigner();
        object filePath = Server.MapPath("GZLXBB.xls");
        if (System.IO.File.Exists(Convert.ToString(filePath)))
        {
            designer1.Open(Convert.ToString(filePath));
            Workbook workbook = designer1.Workbook;
            int length = tbbb.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                HtmlTableRow row = tbbb.Rows[i];
                int length1 = row.Cells.Count;
                for (int j = 0; j < length1; j++)
                {
                    workbook.Worksheets[0].Cells[i, j].PutValue(row.Cells[j].InnerText);
                    workbook.Worksheets[0].Cells[i, j].Style.Font.Size = 9;
                    if (i == 0 || j == 0)
                    {
                           workbook.Worksheets[0].Cells[i, j].Style.Font.IsBold = true;
                        workbook.Worksheets[0].Cells[i, j].Style.BackgroundColor = System.Drawing.Color.BlueViolet;
                        workbook.Worksheets[0].Cells[i, j].Style.Borders.SetStyle(CellBorderType.Thin);
                        workbook.Worksheets[0].Cells[i, j].Style.Borders.DiagonalStyle = CellBorderType.None;
                    }
                }
            }
            int idx = 0;
            for (int i = 0; i < GzlxGrid.Rows.Count; i++)
            {
                idx = i + 5;
                HtmlTableRow row = GzlxGrid.Rows[i];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    workbook.Worksheets[0].Cells[idx, j].PutValue(row.Cells[j].InnerText);
                    workbook.Worksheets[0].Cells[idx, j].Style.Font.Size = 9;
                    if (i == 0)
                    {
                        workbook.Worksheets[0].Cells[idx, j].Style.Font.IsBold = true;
                        workbook.Worksheets[0].Cells[idx, j].Style.BackgroundColor = System.Drawing.Color.BlueViolet;
                        workbook.Worksheets[0].Cells[idx, j].Style.Borders.SetStyle(CellBorderType.Thin);
                        workbook.Worksheets[0].Cells[idx, j].Style.Borders.DiagonalStyle = CellBorderType.None;
                    }
                }
            }

                //length = DataList1.Items.Count;
                //for (int i = 0; i < length; i++)
                //{
                //    string gzlx = (DataList1.Items[i].FindControl("Label1") as System.Web.UI.WebControls.Label).Text;
                //    string num = (DataList1.Items[i].FindControl("Label2") as System.Web.UI.WebControls.Label).Text;
                //    workbook.Worksheets[0].Cells[4, i * 2].PutValue(gzlx);
                //    workbook.Worksheets[0].Cells[4, i * 2].Style.BackgroundColor = System.Drawing.Color.Blue;
                //    workbook.Worksheets[0].Cells[4, i * 2].Style.Font.IsBold = true;
                //    workbook.Worksheets[0].Cells[4, i * 2 + 1].PutValue(num);
                //    workbook.Worksheets[0].Cells[4, i * 2 + 1].Style.BackgroundColor = System.Drawing.Color.Blue;
                //    workbook.Worksheets[0].Cells[4, i * 2 + 1].Style.Font.IsBold = true;
                //    DataList datalist = DataList1.Items[i].FindControl("DataList2") as DataList;
                //    int length1 = datalist.Items.Count;
                //    for (int j = 0; j < length1; j++)
                //    {
                //        string gzyy = (datalist.Items[j].FindControl("Label1") as System.Web.UI.WebControls.Label).Text;
                //        string num1 = (datalist.Items[j].FindControl("Label2") as System.Web.UI.WebControls.Label).Text;
                //        workbook.Worksheets[0].Cells[j + 5, i * 2].PutValue(gzyy);
                //        workbook.Worksheets[0].Cells[j + 5, i * 2 + 1].PutValue(num1);
                //    }
                //}
                designer1.Save(System.Web.HttpUtility.UrlEncode("今日统计报表.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
            Response.End();
        }
       
    }
}

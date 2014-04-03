using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;
using System.Drawing;

public partial class Web_GZCL_GZBB_HangYeFenLei : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitPage();
            bindDrop();
            InitGridView();
            BindGrid();
        }
    }

    private void bindDrop()
    {
        dropYWLB.Items.Clear();

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywzt' and sfqy=1");
        DataSet ds = DataFunction.FillDataSet(sql);
        dropYWLB.DataSource = ds;
        dropYWLB.DataTextField = "codename";
        dropYWLB.DataValueField = "guid";
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
    private void InitGridView()
    {
        string sql = "select CUSTTYPE1,1 as xh from rmss_boss group by CUSTTYPE1 union select to_char(datamc),2 as xh from t_sys_data where datacode='CUSTTYPE1' order by xh";
        DataSet ds = DataFunction.FillDataSet(sql);
        GridViewBind(GridView1, ds);
    }
    public static void GridViewBind(GridView gdv, DataSet dataSet)
    {
        gdv.Columns.Clear();
        gdv.AutoGenerateColumns = false;
        //gdv.DataSource = dtblDataSource;
        //gdv.DataKeyNames = new string[]{ strDataKey };
        BoundField bfColumn1 = new BoundField();
        bfColumn1.HeaderText = "日期";
        bfColumn1.DataField = "sj";
        bfColumn1.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bfColumn1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //bfColumn1.ItemStyle.Width = Unit.Percentage(15);
        gdv.Columns.Add(bfColumn1);
        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)   //绑定普通数据列
        {
            BoundField bfColumn = new BoundField();
            bfColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            bfColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            DataRow dr = dataSet.Tables[0].Rows[i];
            //bfColumn.DataField = dtblDataSource.Columns[i].ColumnName;
            bfColumn.HeaderText = dr["CUSTTYPE1"].ToString();
            gdv.Columns.Add(bfColumn);
        }
        BoundField bfColumn2 = new BoundField();
        bfColumn2.HeaderText = "合计";
        bfColumn2.DataField = "sj";
        bfColumn2.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        bfColumn2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        //bfColumn2.ItemStyle.Width = Unit.Percentage(3);
        gdv.Columns.Add(bfColumn2);
        //CommandField cfModify = new CommandField();  //绑定命令列
        //cfModify.ButtonType = ButtonType.Button;
        //cfModify.SelectText = "修改";
        //cfModify.ShowSelectButton = true;
        //gdv.Columns.Add(cfModify);
        //gdv.DataBind();
    }

    private void BindGrid()
    {
        DataTable dt = new DataTable();
        DataColumn column = new DataColumn("sj");
        dt.Columns.Add(column);
        DateTime startTime = Convert.ToDateTime(TSSJ1.Text);
        DateTime endTime = Convert.ToDateTime(TSSJ2.Text);
        while (startTime <= endTime)
        {
            DataRow dr = dt.NewRow();
            dr["sj"] = startTime.ToString("yyyy-MM-dd");
            dt.Rows.Add(dr);
            startTime = startTime.AddDays(1);
        }
        DataRow dtr = dt.NewRow();
        dtr["sj"] = "合计";
        dt.Rows.Add(dtr);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string jdsj = e.Row.Cells[0].Text;
            //if(jdsj=="合计")
            //{
            //    return;
            //}
            string clff = "";
            string sql = "";
            int num = 0;
            for (int i = 1; i < GridView1.Columns.Count - 1; i++)
            {
                clff = GridView1.HeaderRow.Cells[i].Text; //e.Row.Cells[i].ToString();

                sql = string.Format("select count(*) as tjcs from t_fau_zb t where t.gzzt = '结单' and t.HYFL='{1}' and to_char(t.jdsj,'yyyy-mm-dd')='{0}'", jdsj, clff);
                if (jdsj == "合计")
                {
                    sql = string.Format("select count(*) as tjcs from t_fau_zb t where t.gzzt = '结单' and t.HYFL='{2}'  and to_char(t.jdsj,'yyyy-mm-dd')<='{0}' and to_char(t.jdsj,'yyyy-mm-dd')>='{1}' ", TSSJ2.Text, TSSJ1.Text, clff);
                }
                if (dropYWLB.SelectedValue != "")
                {
                    sql += " and t.ywzt='" + dropYWLB.SelectedItem.Text + "'";
                }
                e.Row.Cells[i].Text = DataFunction.GetStringResult(sql);
                num += Convert.ToInt32(e.Row.Cells[i].Text);
            }
            e.Row.Cells[GridView1.Columns.Count - 1].Text = num.ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            License lic = new License();
            lic.SetLicense(Server.MapPath("../../../Aspose.Total.lic"));
            WorkbookDesigner designer1 = new WorkbookDesigner();
            object filePath = Server.MapPath("gzbb.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Workbook workbook = designer1.Workbook;
                Cells cells = workbook.Worksheets[0].Cells;
                //合并单元格
                cells.Merge(0, 0, 1, GridView1.HeaderRow.Cells.Count);
                //居中显示
                cells[0, 0].Style.HorizontalAlignment = TextAlignmentType.Center;
                //cells[0, 0].Style.BackgroundColor = Color.GreenYellow;
                cells[0, 0].PutValue("行业分类报表" + TSSJ1.Text + "至" + TSSJ2.Text);
                for(int i=0; i < GridView1.HeaderRow.Cells.Count ; i++)
                {
                    TableCell tc = GridView1.HeaderRow.Cells[i];
                    cells[1, i].PutValue(tc.Text);
                }
                int cellsY = 2;
                foreach (GridViewRow gvr in GridView1.Rows)
                {
                    for (int i = 0; i < gvr.Cells.Count; i++)
                    {
                        TableCell tc = gvr.Cells[i];
                        cells[cellsY, i].PutValue(tc.Text);
                        
                    }
                    cellsY++;
                }
                designer1.Process();
                designer1.Save(ReturnUrlEncode("故障单.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
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

    public static string ReturnUrlEncode(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return "";
        return System.Web.HttpUtility.UrlEncode(fileName);
    }
}

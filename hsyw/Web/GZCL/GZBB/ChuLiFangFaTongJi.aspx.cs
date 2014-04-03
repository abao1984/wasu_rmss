using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;

public partial class Web_GZCL_GZBB_ChuLiFangFaTongJi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
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
        dropYWLB.DataValueField = "codename";
        dropYWLB.DataBind();
        ListItem itmes = new ListItem("---请选择---", "");
        dropYWLB.Items.Add(itmes);
        dropYWLB.SelectedValue = "";
    }

    private void InitPage()
    {
        DateTime nowTime=DateTime.Now;
        TSSJ1.Text = nowTime.AddDays(-7).ToString("yyyy-MM-dd") ;
        TSSJ2.Text = nowTime.AddDays(-1).ToString("yyyy-MM-dd");
    }
    private void InitGridView()
    {
        string sql = string.Format(@"select gzclff as enum_name
  from t_fau_zb t
 where jdsj is not null
   and to_char(t.jdsj, 'yyyy-mm-dd') >= '{0}'
   and to_char(t.jdsj, 'yyyy-mm-dd') <= '{1}'
 ",TSSJ1.Text,TSSJ2.Text);
        if (dropYWLB.SelectedValue != "")
        {
            sql += " and t.ywzt='" + dropYWLB.SelectedItem.Text + "'";
        }

        sql += " group by gzclff";
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
            bfColumn.HeaderText = dr["enum_name"].ToString();
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
        DateTime startTime=Convert.ToDateTime(TSSJ1.Text);
        DateTime endTime=Convert.ToDateTime(TSSJ2.Text);
        while(startTime<=endTime)
        {
            DataRow dr = dt.NewRow();
            dr["sj"] = startTime.ToString("yyyy-MM-dd");
            dt.Rows.Add(dr);
            startTime=startTime.AddDays(1);
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
            string jdsj = e.Row.Cells[0].Text ;
            //if(jdsj=="合计")
            //{
            //    return;
            //}
            string clff="";
            string sql = "";
            int num = 0;
            for (int i = 1; i < GridView1.Columns.Count - 1; i++ )
            {
                clff = GridView1.HeaderRow.Cells[i].Text ; //e.Row.Cells[i].ToString();
                
                sql = string.Format("select count(*) as tjcs from t_fau_zb t where t.jdsj is not null and t.gzclff='{1}' and to_char(t.jdsj,'yyyy-mm-dd')='{0}'",jdsj , clff);
                if (jdsj == "合计")
                {
                    sql = string.Format("select count(*) as tjcs from t_fau_zb t where t.jdsj is not null and t.gzclff='{2}'  and to_char(t.jdsj,'yyyy-mm-dd')<='{0}' and to_char(t.jdsj,'yyyy-mm-dd')>='{1}' ", TSSJ2.Text, TSSJ1.Text, clff);
                }
                if (dropYWLB.SelectedValue != "")
                {
                    sql += " and t.ywzt='" + dropYWLB.SelectedItem.Text + "'";
                }
                e.Row.Cells[i].Text = DataFunction.GetStringResult(sql);
                num += Convert.ToInt32( e.Row.Cells[i].Text );
            }
            e.Row.Cells[GridView1.Columns.Count - 1].Text = num.ToString() ;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitGridView();
        BindGrid();
    }
    protected void BtnExpExcel_Click(object sender, EventArgs e)
    {
        Aspose.Cells.WorkbookDesigner designer = new Aspose.Cells.WorkbookDesigner();
        designer.Open(Server.MapPath("CLFFBB.xls"));
        int length = GridView1.Rows.Count;
        int length1 = GridView1.Columns.Count;
        string[] zm = { "A", "B", "C", "D", "E", "F", "G", "H" };
        Aspose.Cells.Style style1= designer.Workbook.Worksheets["Sheet1"].Cells["A1"].Style;
        Aspose.Cells.Style style2= designer.Workbook.Worksheets["Sheet1"].Cells["A2"].Style;
        for (int i = 0; i < length; i++)
        {
            int index = i+2;
            int countIndex=GridView1.Columns.Count;
            for (int j = 0; j < countIndex ; j++ )
            {
                designer.Workbook.Worksheets["Sheet1"].Cells[zm[j]+1].PutValue(GridView1.HeaderRow.Cells[j].Text);
                designer.Workbook.Worksheets["Sheet1"].Cells[zm[j] + index].PutValue(GridView1.Rows[i].Cells[j].Text);
                designer.Workbook.Worksheets["Sheet1"].Cells[zm[j] + 1].Style = style1;
                designer.Workbook.Worksheets["Sheet1"].Cells[zm[j] + index].Style = style2;
            }
			
            //designer.Workbook.Worksheets["Sheet1"].Cells["B" + index].PutValue(GridView1.Rows[i].Cells[1].Text);
            //designer.Workbook.Worksheets["Sheet1"].Cells["C" + index].PutValue(GridView1.Rows[i].Cells[2].Text);
            //designer.Workbook.Worksheets["Sheet1"].Cells["D" + index].PutValue(GridView1.Rows[i].Cells[3].Text);
            //designer.Workbook.Worksheets["Sheet1"].Cells["E" + index].PutValue(GridView1.Rows[i].Cells[4].Text);
        }
        designer.Save(Guid.NewGuid().ToString() + ".xls", Aspose.Cells.SaveType.OpenInExcel, Aspose.Cells.FileFormatType.Default, this.Response);
        Response.End();
    }
}

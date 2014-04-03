using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Aspose.Cells;

public partial class Web_GZCL_GZBB_GuZhangShuLiang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            bindDrop();
            InitPage();
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

    private void BindGrid()
    {
        string sql = "select * from t_fau_zb t where 1=2";
        DataSet ds = DataFunction.FillDataSet(sql);
        DateTime dt1 = Convert.ToDateTime(TSSJ1.Text);
        DateTime dt2 = Convert.ToDateTime(TSSJ2.Text);

        for (DateTime dt = dt1; dt <= dt2; dt=dt.AddDays(1))
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["TSSJ"] = dt.ToShortDateString();
            ds.Tables[0].Rows.Add(dr);
        }
        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        GridView1.DataSource = ds;
        GridView1.DataBind();
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
                cells[0, 0].PutValue("故障数量统计" + TSSJ1.Text + "至" + TSSJ2.Text);
                for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
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
    int tslhj = 0;
    int cllhj = 0;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[0].Text != "&nbsp;")
            {
                string sql = "select count(*) from t_fau_zb where to_char(TSSJ,'yyyy-MM-dd') = '{0}' and GZLY in ('客户投诉','内部投诉')";
                if (dropYWLB.SelectedValue != "")
                {
                    sql += " and ywzt = '" + dropYWLB.SelectedItem.Text + "'";
                }
                e.Row.Cells[2].Text = DataFunction.GetStringResult(string.Format(sql, e.Row.Cells[0].Text));
                tslhj = tslhj + Convert.ToInt32(e.Row.Cells[2].Text);

                sql = "select count(*) from t_fau_zb where gzzt = '结单' and to_char(JDSJ,'yyyy-MM-dd') = '{0}'";
                if (dropYWLB.SelectedValue != "")
                {
                    sql += " and ywzt = '" + dropYWLB.SelectedItem.Text + "'";
                }
                e.Row.Cells[1].Text = DataFunction.GetStringResult(string.Format(sql, e.Row.Cells[0].Text));
                cllhj = cllhj + Convert.ToInt32(e.Row.Cells[1].Text);
            }
            else
            {
                e.Row.Cells[0].Text = "合计";
                e.Row.Cells[1].Text = cllhj.ToString();
                e.Row.Cells[2].Text = tslhj.ToString();
            }
        }
    }
}

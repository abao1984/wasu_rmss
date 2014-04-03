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

public partial class Web_GZCL_GZBB_LiuDanAnRiQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bindDrop();
            InitPage();
            //InitTable();
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
        if (string.IsNullOrEmpty(TSSJ1.Text.Trim()) || string.IsNullOrEmpty(TSSJ2.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('时间不能为空！')</script>");
            return;
        }

        DateTime t1 = DateTime.Parse(TSSJ1.Text);
        DateTime t2 = DateTime.Parse(TSSJ2.Text);
        System.TimeSpan ts = t2 - t1;
        int days = ts.Days;

        if (days > 31)
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>alert('时间跨度不能超过一个月！')</script>");
            return;
        }

        string sql = string.Empty;
        if (dropYWLB.SelectedValue != "")
        {
            sql += " and t.ywzt='" + dropYWLB.SelectedValue + "'";
        }
        string ywlx = GetYWLX();
        if (ywlx != "''")
        {
            sql += " and t.ywlx in (" + ywlx + ")";
        }

        string strSql = string.Format(@"select sj,
       sum(zdld) as zdld,
       sum(bdld) as bdld,
       sum(zd) as zd,
       sum(zdld) + sum(bdld) + sum(zd) as ldzs,
       sum(zdldkh) + sum(zdld) as zdldkh
  from (select to_char(t.tssj, 'yyyy-MM-dd') as sj,
               (select count(*)
                  from t_fau_cllc
                 where lccz = '主动留单'
                   and zbguid = t.zbguid) as zdld,
               (select count(*)
                  from t_fau_cllc
                 where lccz = '被动留单'
                   and zbguid = t.zbguid) as bdld,
               (select count(*)
                  from t_fau_zb
                 where ((to_char(tssj, 'yyyy-MM-dd') <
                       to_char(jdsj, 'yyyy-MM-dd')) or
                       (to_char(tssj, 'yyyy-MM-dd') <
                       to_char(sysdate, 'yyyy-MM-dd') and jdsj is null))
                   and zbguid not in
                       (select zbguid from t_fau_cllc where gzzt = '留单')
                   and zbguid = t.zbguid) as zd,
               (select count(*)
                  from t_fau_zb
                 where ((jdsj >
                       to_date(to_char(tssj + 1, 'yyyy-MM-dd') || ' 08:00',
                                 'yyyy-MM-dd hh24:mi') and jdsj is not null) or
                       (sysdate >
                       to_date(to_char(tssj + 1, 'yyyy-MM-dd') || ' 08:00',
                                 'yyyy-MM-dd hh24:mi') and jdsj is null))
                   and zbguid not in
                       (select zbguid from t_fau_cllc where gzzt = '留单')
                   and zbguid = t.zbguid) as zdldkh
          from t_fau_zb t
         where to_char(t.tssj, 'yyyy-mm-dd') >='{0}'
           and to_char(t.tssj, 'yyyy-mm-dd') <='{1}' {2})  group by sj order by sj", TSSJ1.Text, TSSJ2.Text, sql);


        DataSet ds = DataFunction.FillDataSet(strSql);

        GridView1.DataSource = ds;
        GridView1.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        InitTable();
    }

    private string getNum(string lxName, string time, string ztName)
    {
        string sql = string.Format(@"select count(*) from t_fau_zb t where  to_char(t.jdsj,'yyyy-mm-dd')='{1}' and t.gzlx = '{0}' and t.ywzt='{2}'", lxName, time, ztName);
        if (dropYWLB.SelectedValue != "")
        {
            sql += " and t.ywzt='" + dropYWLB.SelectedValue + "'";
        }
        string ywlx = GetYWLX();
        if (ywlx != "''")
        {
            sql += " and t.ywlx in (" + ywlx + ")";
        }
        return DataFunction.GetStringResult(sql);

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            WorkbookDesigner designer1 = new WorkbookDesigner();
            object filePath = Server.MapPath("LDARTJ.xls");
            if (System.IO.File.Exists(Convert.ToString(filePath)))
            {
                designer1.Open(Convert.ToString(filePath));
                Workbook workbook = designer1.Workbook;
                InitTable();
                int length = GridView1.Rows.Count;
                for (int i = 0; i < length; i++)
                {
                    int length1 = GridView1.Rows[i].Cells.Count;

                    for (int j = 0; j < length1; j++)
                    {
                        workbook.Worksheets[0].Cells[i + 3, j].PutValue(GridView1.Rows[i].Cells[j].Text);
                    }
                }

                designer1.Save(System.Web.HttpUtility.UrlEncode("留单按日统计报表.xls"), Aspose.Cells.SaveType.OpenInExcel, FileFormatType.Default, Response);
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

        string sql = string.Format(@"select t.codename,t.guid from t_fau_lxsz t where t.lb='ywlx' and sfqy=1 and parent_name = '{0}'", dropYWLB.SelectedValue);
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
                ywlx += ",'" + item.Value + "'";
            }
        }
        return ywlx;
    }
}

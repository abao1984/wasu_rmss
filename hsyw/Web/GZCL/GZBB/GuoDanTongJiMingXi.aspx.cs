using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Web_GZCL_GZBB_GuoDanTongJiMingXi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(Web_GZCL_GZBB_GuoDanTongJiMingXi));
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
        if(dropYWZT.SelectedValue!="")
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
//        string strSql = @"select {0} from t_fau_cllc t,t_fau_zb z 
//where t.zbguid=z.zbguid  ";
//        if(TSSJ1.Text!="")
//        {
//            strSql += " and  t.clsj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
//        }
//        if(TSSJ2.Text!="")
//        {
//            strSql += "  and  t.clsj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
//        }
//        string ry = txtBMRY.Text;
//        if (ry != "")
//        {
//            strSql += " and t.clry='" + ry + "'";
//        }
        string strSql = @"select {0} from t_fau_zb t where 1=1   ";
        if (TSSJ1.Text != "")
        {
            strSql += " and  t.jdsj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
        }
        if (TSSJ2.Text != "")
        {
            strSql += "  and  t.jdsj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
        }

        string ry = txtBMRY.Text;
        if (ry != "")
        {
            strSql += " and t.xfry='" + ry + "'";
        }
        strSql += "{1} order by ywzt,ywlb";

        string ywSql = @"select {0} from t_fau_cllc t,t_fau_zb z 
        where t.zbguid=z.zbguid  ";
        if (TSSJ1.Text != "")
        {
            ywSql += " and  t.clsj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
        }
        if (TSSJ2.Text != "")
        {
            ywSql += "  and  t.clsj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
        }
        //string ry = txtBMRY.Text;
        if (ry != "")
        {
            ywSql += " and t.clry='" + ry + "'";
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
            ywSql += " and z.ywlb  in (" + ywlx + ")";
        }

        DataSet ds = DataFunction.FillDataSet(string.Format(ywSql, "distinct z.ywzt,z.ywlb", ""));
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
        cell3.InnerText = "电话完成总量";
        cell3.Width = "100";
        htr3.Cells.Add(cell3);

        HtmlTableRow htr4 = new HtmlTableRow();
        htr4.Align = "center";
        HtmlTableCell cell4 = new HtmlTableCell();
        cell4.InnerText = "电话过单总量";
        cell4.Width = "100";
        htr4.Cells.Add(cell4);

        HtmlTableRow htr5 = new HtmlTableRow();
        htr5.Align = "center";
        HtmlTableCell cell5 = new HtmlTableCell();
        cell5.InnerText = "过单成功率";
        cell5.Width = "100";
        htr5.Cells.Add(cell5);

        HtmlTableRow htr6 = new HtmlTableRow();
        htr6.Align = "center";
        HtmlTableCell cell6 = new HtmlTableCell();
        cell6.InnerText = "平均成功率";
        cell6.Width = "100";
        htr6.Cells.Add(cell6);

        int cellNum = 1;
        int zlzh = 0;
        int wczh = 0;
        double pjzh = 0;
        string sql = "";
        string ywzt = "";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //sql = string.Format(strSql, "count(*)", " and z.ywlb='" + dr["ywlb"].ToString() + "'");
            // zl = DataFunction.GetIntResult(sql);
            int zl = GetGdZl(dr["ywlb"].ToString());
            sql = string.Format(strSql, "count(*)", " and t.ywlb='" + dr["ywlb"].ToString() + "'");
            int wc = DataFunction.GetIntResult(sql);
            double pj=(Convert.ToDouble(wc) / Convert.ToDouble(zl)*100);
            
            if (ywzt == "")
            {
                pjzh += pj;
                cell1 = new HtmlTableCell();
                cell6 = new HtmlTableCell();
                ywzt = dr["ywzt"].ToString();
            }
            else if (ywzt != dr["ywzt"].ToString())
            {
                cell1.ColSpan = cellNum;
                cell1.InnerText = ywzt;
                cell1.Width = Convert.ToString(cellNum*80);
                htr1.Cells.Add(cell1);
                cell1 = new HtmlTableCell();
                ywzt = dr["ywzt"].ToString();
               

                cell6.ColSpan = cellNum;
                cell6.InnerText = (pjzh/cellNum).ToString("0.00")+"%";
                cell6.Width = Convert.ToString(cellNum * 80);
                htr6.Cells.Add(cell6);
                cell6 = new HtmlTableCell();
                pjzh = pj;
                cellNum = 1;
                //cellNum = 1;
            }
            else if (ywzt == dr["ywzt"].ToString())
            {
                cellNum++;
                pjzh += pj;
            }
            
            cell2 = new HtmlTableCell();
            cell2.InnerText = dr["ywlb"].ToString();
            cell2.Width = "80";
            htr2.Cells.Add(cell2);

            cell3 = new HtmlTableCell();
            cell3.InnerText = wc.ToString();
            cell3.Width = "80";
            htr3.Cells.Add(cell3);

            cell4 = new HtmlTableCell();
            cell4.InnerText = zl.ToString();
            cell4.Width = "80";
            htr4.Cells.Add(cell4);

            cell5 = new HtmlTableCell();
            cell5.InnerText = pj.ToString("0.00") + "%";
            cell5.Width = "80";
            htr5.Cells.Add(cell5);

            zlzh += zl;
            wczh += wc;
        }
        string pjhj=(Convert.ToDouble(wczh) / Convert.ToDouble(zlzh) * 100).ToString("0.00") + "%";
        cell1.ColSpan = cellNum;
        cell1.InnerText = ywzt;
        cell1.Width = Convert.ToString(cellNum * 80);
        cell1.Align = "center";
        htr1.Cells.Add(cell1);

        cell6.ColSpan = cellNum;
        cell6.InnerText = (pjzh / cellNum).ToString("0.00") + "%";
        cell6.Width = Convert.ToString(cellNum * 80);
        htr6.Cells.Add(cell6);

        //合计
        cell1 = new HtmlTableCell();
        cell1.RowSpan = 2;
        cell1.Width = "80";
        cell1.InnerText = "合计";
        htr1.Cells.Add(cell1);

        cell2 = new HtmlTableCell();
        cell2.Width = "80";
        cell2.InnerText = wczh.ToString();
        htr3.Cells.Add(cell2);

        cell3 = new HtmlTableCell();
        cell3.Width = "80";
        cell3.InnerText = zlzh.ToString();
        htr4.Cells.Add(cell3);

        cell5 = new HtmlTableCell();
        cell5.InnerText = pjhj;
        cell5.Width = "80";
        htr5.Cells.Add(cell5);

        cell6 = new HtmlTableCell();
        cell6.InnerText = pjhj;
        cell6.Width = "80";
        htr6.Cells.Add(cell6);

        tb_dhgdl.Rows.Add(htr1);
        tb_dhgdl.Rows.Add(htr2);
        tb_dhgdl.Rows.Add(htr3);
        tb_dhgdl.Rows.Add(htr4);
        tb_dhgdl.Rows.Add(htr5);
        tb_dhgdl.Rows.Add(htr6);
        //ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(), "<script>test2()</script>");
    }

    private int GetGdZl(string ywlx)
    {
        int num = 0;
        string strSql = @"select distinct t.zbguid from t_fau_cllc t left join t_fau_zb z on z.zbguid=t.zbguid where 1=1";
        if (TSSJ1.Text != "")
        {
            strSql += " and  t.clsj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
            //strSql += " and  z.tssj>=to_date('" + TSSJ1.Text + "','yyyy-mm-dd hh24')";
        }
        if (TSSJ2.Text != "")
        {
            strSql += "  and  t.clsj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
            //strSql += "  and  z.tssj<=to_date('" + TSSJ2.Text + "','yyyy-mm-dd hh24') ";
        }
        string ry = txtBMRY.Text;
        if (ry != "")
        {
            strSql += " and t.clry='" + ry + "'";
        }
        if(ywlx!="")
        {
            strSql += " and z.ywlx='" + ywlx + "'";
        }
        DataSet guidDs = DataFunction.FillDataSet(strSql);
        strSql = @"select  y.* from (
select c.*,z.ywlx,z.xfry,z.GZCLFF from t_fau_cllc c left join t_sys_branch b on b.branchname=c.clbm 
left join   t_fau_zb z on z.zbguid=c.zbguid
where zbguid ='{0}'  and b.branchcode like '10010103%'   order by zbguid,clsj ) y 
";
        foreach(DataRow guidDr in guidDs.Tables[0].Rows)
        {
            string zbguid = guidDr["zbguid"].ToString();
            DataSet ds = DataFunction.FillDataSet(string.Format(strSql,zbguid));
            DataRow datarow = ds.Tables[0].Rows[0];
            string gzclff = datarow["GZCLFF"].ToString();
            if (gzclff.IndexOf("电话") > -1)
            {
                DataRow dataRowEnd = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];
                string endxfry = dataRowEnd["xfry"].ToString();

                if (endxfry == ry)
                {
                    num++;
                }
                else if (datarow["clry"].ToString() == ry)
                {
                    num++;
                }
            }
            else
            {
                if (datarow["clry"].ToString() == ry)
                {
                    num++;
                }
            }
        }
        return num;
    }

    
    protected void Button2_Click(object sender, EventArgs e)
    {
        CreateExcel(TextBox1.Text);
    }

    public void CreateExcel(string str)
    {
        Response.AddHeader("Content-Disposition", "attachment;filename=gdlztj.xls");
        Response.ContentType = "application/ms-excel";
        //Response.ContentEncoding = Encoding.GetEncoding("GB2312");
        Response.Write(str);
        Response.End();
    }
    protected void drpTJRY_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        
    }

    [Ajax.AjaxMethod()]
    public string GetTJRY(string sj1,string sj2)
    {
        string tjry = "";
        string strSql = string.Format(@"select distinct clry from t_fau_cllc t where t.clsj>=to_date('{0}','yyyy-mm-dd hh24') and  t.clsj<=to_date('{1}','yyyy-mm-dd hh24') and t.lczt='电话处理'", sj1, sj2);
        DataSet ds=DataFunction.FillDataSet(strSql);
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            tjry += dr["clry"].ToString() +",";
        }
        if(tjry!="")
        {
            tjry = tjry.Substring(0, tjry.Length - 1);
        }
        return tjry;
    }
   
}

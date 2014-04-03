using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Web_Resource_ComputerHouseEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            GUID.Text = Request.QueryString["GUID"];
           P_GUID.Text = Request.QueryString["P_GUID"];
            GetTableName();
           
        } CreateTable();
    }

    #region 创建属性表单
    private DataSet GetT_RES_PROPERTYData()
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.unit_id='" + UNIT_ID.Text + "' order by t.sequence";
        return DataFunction.FillDataSet(sql);
    }

    private void GetTableName()
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + UNIT_ID.Text + "'";
        string tableName = DataFunction.GetStringResult(sql);
        if (string.IsNullOrEmpty(tableName))
        {
            tableName = "T_RES_COMPUTER_HOUSE";
        }
        TABLE_NAME.Text = tableName;
    }

    private DataRow GetUnitDataRow()
    {

        string sql = "select * from " + TABLE_NAME.Text + " where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            DR["GUID"] = GUID.Text;
            DR["P_GUID"] = P_GUID.Text;
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            DR = ds.Tables[0].Rows[0];
        }
        return DR;
    }

    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, GetUnitDataRow());
    }

    private void CreateTable()
    {
        DataSet ds = GetT_RES_PROPERTYData();
        if (ds.Tables[0].Rows.Count > 0)
        {
            HtmlTable tb = new HtmlTable();
            tb.Border = 1;
            tb.BorderColor = "#5b9ed1";
            tb.Width = "100%";
            tb.CellPadding = 3;
            tb.CellSpacing = 1;
            HtmlTableRow tr = null;
            int i = 0;
            foreach (DataRow DR in ds.Tables[0].Rows)
            {
                if (i % 2 == 0)
                {
                    tr = new HtmlTableRow();
                    tb.Rows.Add(tr);
                }
                i++;
                HtmlTableCell td1 = new HtmlTableCell();
                td1.InnerText = DR["PROPERY_NAME"].ToString();
                td1.Attributes.Add("class", "tdBak");
                td1.Width = "20%";
                tr.Cells.Add(td1);
                HtmlTableCell td2 = new HtmlTableCell();
                td2.Width = "30%";
                TextBox tex = new TextBox();
                tex.ID = DR["FILED_NAME"].ToString();
                td2.Controls.Add(tex);
                tr.Cells.Add(td2);
            }

            TD_PROPERTY.Controls.Add(tb);
        }
        FillPage();
    }
    #endregion

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataRow DR = GetUnitDataRow();
        ShareFunction.GetControlData(this.Page, DR);
        DataFunction.SaveData(DR.Table.DataSet, TABLE_NAME.Text);
    }
}

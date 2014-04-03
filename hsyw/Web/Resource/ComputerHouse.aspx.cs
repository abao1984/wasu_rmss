﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class Web_Resource_ComputerHouse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            CreateGridView_Unit();
           
            GetTableName();
            SUB_UNIT_ID.Text = "d86fbb8d-87c4-44f8-abfd-8ca14744299d";
          
        }
        CreateMenuTable();
        CreateTable();
        if (!this.IsPostBack)
        { 
            CreateGridView_SubUnit();
        }
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
        //string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + UNIT_ID.Text + "'";
        //string tableName = DataFunction.GetStringResult(sql);
        //if (string.IsNullOrEmpty(tableName))
        //{
        //    tableName = "T_RES_COMPUTER_HOUSE";
        //}
        string sql = "select * from " + TABLE_NAME.Text + " where guid='" + GUID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            DR["GUID"] = Guid.NewGuid().ToString();
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
           DR= ds.Tables[0].Rows[0];
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

    #region 创建菜单标签
    private DataSet GetMenuData()
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.parent_unit_id='" + UNIT_ID.Text + "' order by  t.sequence";
        return DataFunction.FillDataSet(sql);
    }
    private void CreateMenuTable()
    {
        DataSet ds = GetMenuData();
        int i = 0;
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            HtmlTableCell TD = new HtmlTableCell();
            TD.InnerText = DR["UNIT_NAME"].ToString();
            if (i == 0)
            {
                TD.Attributes.Add("class", "nav01");
            }
            else
            {
                TD.Attributes.Add("class", "nav02");
            }
            MenuTr.Cells.Add(TD);
            i++;
        }
    }
    #endregion

    #region 创建子单元属性列表
    private DataSet GetPROPERTYData()
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.unit_id='" + UNIT_ID.Text + "' and ISGRIDSHOW='1' order by t.sequence";
        return DataFunction.FillDataSet(sql);
    }
    private void CreateGridView_Unit()
    {
        DataSet ds = GetPROPERTYData();
        GridView_Unit.Columns.Clear();
        CommandField cf = new CommandField();
        cf.HeaderText = "选择";
        cf.EditText = "选择";
        cf.ShowSelectButton = true;
        cf.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
        cf.ControlStyle.BorderColor = Color.FromName("#5B9ED1");
        cf.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
        GridView_Unit.Columns.Add(cf);
        foreach (DataRow DR in ds.Tables[0].Rows)
        {

            ButtonField bf = new ButtonField();
            bf.CommandName = "Select";
            bf.DataTextField = DR["FILED_NAME"].ToString();
            bf.HeaderText = DR["PROPERY_NAME"].ToString();
            bf.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
            bf.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
            bf.ControlStyle.BorderColor = Color.FromName("#5B9ED1");
            GridView_Unit.Columns.Add(bf);
        }
        GridView_Unit.DataSource = GetUnitData();
        GridView_Unit.DataBind();
    }

    private DataSet GetUnitData()
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + UNIT_ID.Text + "'";
        string tableName = DataFunction.GetStringResult(sql);
        if (string.IsNullOrEmpty(tableName))
        {
            tableName = "T_RES_COMPUTER_HOUSE";
        }
        sql = "select * from " + tableName;
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow DR = ds.Tables[0].NewRow();
            DR["GUID"] = Guid.NewGuid().ToString();
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            GUID.Text = ds.Tables[0].Rows[0]["GUID"].ToString();
        }
        return ds;
    }
    #endregion

    #region 创建子单元属性列表
    private DataSet GetSubPROPERTYData()
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.unit_id='" + SUB_UNIT_ID.Text + "' and ISGRIDSHOW='1' order by t.sequence";
        return DataFunction.FillDataSet(sql);
    }
    private void CreateGridView_SubUnit()
    {
        DataSet ds = GetSubPROPERTYData();
        GridView_SubUnit.Columns.Clear();

        BoundField bfHead = new BoundField();

        bfHead.HeaderText ="编辑";
        bfHead.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
        bfHead.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
        GridView_SubUnit.Columns.Add(bfHead);    

        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            BoundField bf = new BoundField();
            bf.DataField = DR["FILED_NAME"].ToString();
            bf.HeaderText = DR["PROPERY_NAME"].ToString();
            bf.ItemStyle.BorderColor = Color.FromName("#5B9ED1");
            bf.HeaderStyle.BorderColor = Color.FromName("#5B9ED1");
            GridView_SubUnit.Columns.Add(bf);    
        }
        GridView_SubUnit.DataSource = GetSubUnitData();
        GridView_SubUnit.DataBind();
    }

    private DataSet GetSubUnitData()
    {
        string sql = "select t.table_name from T_RES_SYS_UNIT t where t.unit_id='" + SUB_UNIT_ID.Text + "'";
        string tableName = DataFunction.GetStringResult(sql);
        if (string.IsNullOrEmpty(tableName))
        {
            tableName = "T_RES_COMPUTER_HOUSE";
        }
        sql = "select * from " + tableName + " where P_GUID='"+GUID.Text+"'";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow DR = ds.Tables[0].NewRow();
            DR["GUID"] = Guid.NewGuid().ToString();
            ds.Tables[0].Rows.Add(DR);
        }
        return ds;
    }
    #endregion

    protected void GridView_SubUnit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = "<a href=# onclick=windowOpen('" + GridView_SubUnit.DataKeys[e.Row.RowIndex].Value + "')>编辑</a>";
        }
    }



    protected void GridView_Unit_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GUID.Text = GridView_Unit.DataKeys[e.NewSelectedIndex].Value.ToString();
        FillPage();
        GridView_SubUnit.DataSource = GetSubUnitData();
        GridView_SubUnit.DataBind();
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataRow DR = GetUnitDataRow();
        ShareFunction.GetControlData(this.Page, DR);
        DataFunction.SaveData(DR.Table.DataSet, TABLE_NAME.Text);
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        GridView_SubUnit.DataSource = GetSubUnitData();
        GridView_SubUnit.DataBind();
    }
}

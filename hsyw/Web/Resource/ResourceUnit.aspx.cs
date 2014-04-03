using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ResourceUnit : BasePage
{
    private ShareResource shareResource = new ShareResource();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        GridView_PROPERY.Attributes.Add("BorderColor", "#5B9ED1");
        if (!this.IsPostBack)
        {
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];           
            BindCH_CHILD_UNIT();
            InitPage();
        }
    }


    private DataRow GetUnitData()
    {
        string sql = "select * from T_RES_SYS_UNIT t where t.unit_id='" + UNIT_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            DR["UNIT_ID"] = UNIT_ID.Text;            
            ds.Tables[0].Rows.Add(DR);
        }
        else
        {
            DR = ds.Tables[0].Rows[0];
        }
        return DR;
    }

    private void InitPage()
    {
        FillPage();
    }

    
    private void FillPage()
    {
        ShareFunction.FillControlData(this.Page, GetUnitData());
        BindGridView_PROPERY();
        SetCH_CHILD_UNIT();
    }

   
    protected void AddButton_Click(object sender, EventArgs e)
    {
        string parent_id = PARENT_UNIT_ID.Text;
        UNIT_ID.Text = Guid.NewGuid().ToString();
        FillPage();
        PARENT_UNIT_ID.Text = parent_id;
    }
    protected void AddSubButton_Click(object sender, EventArgs e)
    {
        string parent_id = UNIT_ID.Text;
        UNIT_ID.Text = Guid.NewGuid().ToString();
        FillPage();
        PARENT_UNIT_ID.Text = parent_id;
    }

    private DataSet GetProperyData(bool isSave)
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.unit_id='"+UNIT_ID.Text+"' order by SEQUENCE";
        DataSet ds= DataFunction.FillDataSet(sql);
        if (!isSave)
        {
            DataRow DR = ds.Tables[0].NewRow();
            DR["PROPERY_ID"] = Guid.NewGuid().ToString();
            ds.Tables[0].Rows.Add(DR);
        }
        return ds;
    }

    private void BindGridView_PROPERY()
    {
        GridView_PROPERY.DataSource = GetProperyData(false);
        GridView_PROPERY.DataBind();
    }

    protected void GridView_PROPERY_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;
            if (dv["INHERIT_FATHER"].ToString() == "1")
            {
                e.Row.ForeColor = System.Drawing.Color.Blue;
            }
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#E3F8B7';this.style.cursor='hand'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor;this.style.cursor='default'");
            e.Row.Attributes.Add("ondblclick", "windowOpen('" + GridView_PROPERY.DataKeys[e.Row.RowIndex].Value + "')");
            e.Row.Cells[e.Row.Cells.Count-1].Text = "<a href=# onclick=\"windowOpen('" + GridView_PROPERY.DataKeys[e.Row.RowIndex].Value + "')\">详细</a>";
        }
    }
    protected void Btn_Click(object sender, EventArgs e)
    {
        BindGridView_PROPERY();
    }


    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataRow DR = GetUnitData();
        bool isRef = false;
        if (DR["UNIT_NAME"].ToString() != UNIT_NAME.Text)
        {
            isRef = true;
        }
        ShareFunction.GetControlData(this.Page, DR);
        DataFunction.SaveData(DR.Table.DataSet, "T_RES_SYS_UNIT");
        SaveCH_CHILD_UNIT();
        CreateTableName(TABLE_NAME.Text);
        if (isRef)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>parent.document.getElementById('UNIT_ID').value='" + UNIT_ID.Text + "';parent.document.getElementById('Btn').click();</script>");
        }
    }

    #region 创建数据库表急字段
    public void CreateTableName(string tableName)
    {
       
        if (!HasTableName(tableName) && IS_CREATE_TABLE.Checked)
        {
            string[] sqls = new string[7];
            sqls[0] = string.Format("create table {0} (GUID CHAR(36) not null,CREATEDATETIME DATE,UPDATEDATETIME DATE,UPDATEUSERNAME VARCHAR2(100))", tableName);
            sqls[1] = string.Format("alter table {0}  add constraint PK_{0} primary key (GUID)", tableName);
            sqls[2] = string.Format("comment on column {0}.GUID  is '设备主键'", tableName);
            sqls[3] = string.Format("comment on column {0}.CREATEDATETIME  is '创建时间'", tableName);
            sqls[4] = string.Format("comment on column {0}.UPDATEDATETIME  is '修改时间'", tableName);
            sqls[5] = string.Format("comment on column {0}.UPDATEUSERNAME  is '修改人姓名'", tableName);
            DataFunction.ExecuteTransaction(sqls);
            tableName = tableName + "_LS";
            sqls[0] = string.Format("create table {0} (PK_GUID CHAR(36) not null,GUID CHAR(36),CREATEDATETIME DATE,UPDATEDATETIME DATE,UPDATEUSERNAME VARCHAR2(100))", tableName);
            sqls[1] = string.Format("alter table {0}  add constraint PK_{0} primary key (PK_GUID)", tableName);
            sqls[2] = string.Format("comment on column {0}.GUID  is '设备主键'", tableName);
            sqls[3] = string.Format("comment on column {0}.CREATEDATETIME  is '创建时间'", tableName);
            sqls[4] = string.Format("comment on column {0}.UPDATEDATETIME  is '修改时间'", tableName );
            sqls[5] = string.Format("comment on column {0}.PK_GUID  is '主键'", tableName);
            sqls[6] = string.Format("comment on column {0}.UPDATEUSERNAME  is '修改人姓名'", tableName);
            DataFunction.ExecuteTransaction(sqls);          
        }
    }

    private bool HasTableName(string tableName)
    {
        string sql = "select * from user_tables t where t.table_name='"+tableName.ToUpper()+"'";
        return DataFunction.HasRecord(sql);
    }

    #endregion


    #region 绑定下属资源
    private void BindCH_CHILD_UNIT()
    {
        CH_CHILD_UNIT.Items.Clear();

        CH_CHILD_UNIT.DataSource =shareResource.GetListUnitData();
        CH_CHILD_UNIT.DataBind();
    }
    private void SetCH_CHILD_UNIT()
    {
        string sql = "select * from T_RES_SYS_UNIT_RELATION t where t.father_unit_id='"+UNIT_ID.Text+"'";
        DataSet ds = DataFunction.FillDataSet(sql);
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["CHILD_UNIT_ID"] };
        foreach (ListItem li in CH_CHILD_UNIT.Items)
        {
            if (ds.Tables[0].Rows.Find(li.Value) != null)
            {
                li.Selected = true;
            }
        }
    }
    private void SaveCH_CHILD_UNIT()
    {
        string sql = "delete from  T_RES_SYS_UNIT_RELATION t where t.father_unit_id='" + UNIT_ID.Text + "'";
        DataFunction.ExecuteNonQuery(sql);
         sql = "select * from T_RES_SYS_UNIT_RELATION t where t.father_unit_id='" + UNIT_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
       // ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["CHILD_UNIT_ID"] };
        foreach (ListItem li in CH_CHILD_UNIT.Items)
        {
            if (li.Selected)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["GUID"] = Guid.NewGuid().ToString();
                dr["FATHER_UNIT_ID"] = UNIT_ID.Text;
                dr["CHILD_UNIT_ID"] = li.Value;
                ds.Tables[0].Rows.Add(dr);
            }
        }
        DataFunction.SaveData(ds, "T_RES_SYS_UNIT_RELATION");
    }
    #endregion
}

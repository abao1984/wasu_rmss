using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_Resource_ResourceProperty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Expires = 0;
        //Response.CacheControl = "no-cache";
        if (!this.IsPostBack)
        {
            PROPERY_ID.Text = Request.QueryString["PROPERY_ID"];
            UNIT_ID.Text = Request.QueryString["UNIT_ID"];
            TABLE_NAME.Text = Request.QueryString["TABLE_NAME"];
            BindCH_CHILD_UNIT();
            InitPage();

        }
    }


    private void InitPage()
    {
        FillPage();
    }

    private DataRow GetPropertyData()
    {
        string sql = "select * from T_RES_SYS_PROPERTY t where t.propery_id='" + PROPERY_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        DataRow DR = null;
        if (ds.Tables[0].Rows.Count == 0)
        {
            DR = ds.Tables[0].NewRow();
            DR["PROPERY_ID"] = PROPERY_ID.Text;
            DR["UNIT_ID"] = UNIT_ID.Text;
            DR["ISEDIT"] = 1;
            DR["ISEMPTY"] = 1;
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
        ShareFunction.FillControlData(this.Page, GetPropertyData());
        SetCH_CHILD_UNIT();
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataRow DR = GetPropertyData();       
        ShareFunction.GetControlData(this.Page, DR);
        DataFunction.SaveData(DR.Table.DataSet, "T_RES_SYS_PROPERTY");
        CreateColName();
        SaveCH_CHILD_UNIT();
    }

    public void CreateColName()
    {        
        switch (DATA_TYPE.Text)
        {
            case "数字":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, "NUMBER");
                break;
            case "长文本":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " VARCHAR2(4000)");
                break;
            case "中文本":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " VARCHAR2(500)");
                break;
            case "短文本":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " VARCHAR2(200)");
                break;
            case "枚举":
                if (ISENUMSHORT.Checked)
                {
                    CreateFiledName(FILED_NAME.Text+"_SHORT", PROPERY_NAME.Text+"简称", " VARCHAR2(200)");
                }
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " VARCHAR2(200)");
                break;
            case "复选":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " CHAR(1)");
                break;
            case "组织机构":
                CreateFiledName(FILED_NAME.Text + "_CODE", PROPERY_NAME.Text+"编码", " VARCHAR2(200)");
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " VARCHAR2(200)");
                break;
            case "VLAN资源":
            case "IP资源":
            case "资源选择":
                if (ISMULTISELECT.Checked)
                {
                    CreateFiledName(FILED_NAME.Text + "_GUID", PROPERY_NAME.Text + "_GUID", " VARCHAR2(4000)");
                }
                else
                {
                    CreateFiledName(FILED_NAME.Text + "_GUID", PROPERY_NAME.Text + "_GUID", " char(36)");
                }
                if (ISEQUCODE.Checked)
                {
                    CreateFiledName(FILED_NAME.Text+"_CODE", PROPERY_NAME.Text+"编码", " VARCHAR2(200)");
                }
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, " VARCHAR2(200)");
                break;
            case "日期":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, "DATE");
                break;
            case "日期时间":
                CreateFiledName(FILED_NAME.Text, PROPERY_NAME.Text, "DATE");
                break;
        }
        if (!string.IsNullOrEmpty(FORMULA.Text) || !string.IsNullOrEmpty(TSGS.Text))
        {
            CreateFiledName(FILED_NAME.Text+"_CHECK", PROPERY_NAME.Text, " CHAR(1)");
        }
    }

    private void CreateFiledName(string FiledName,string ProperyName,string DataType)
    {
        string str = " add ";
        if (hasColName(FiledName))
        {
            str = " modify ";
        }
        string[] sqls = new String[2];
        sqls[0] = "alter table " + TABLE_NAME.Text + " " + str + " " + FiledName + " " + DataType;
        sqls[1] = string.Format("comment on column {0}.{1}  is '{2}'", TABLE_NAME.Text, FiledName, ProperyName);
        DataFunction.ExecuteTransaction(sqls);

       
        sqls[0] = "alter table " + TABLE_NAME.Text + "_LS " + str + " " + FiledName + " " + DataType;
        sqls[1] = string.Format("comment on column {0}.{1}  is '{2}'", TABLE_NAME.Text+"_LS", FiledName, ProperyName);
        DataFunction.ExecuteTransaction(sqls);
    }

    private bool hasColName(string filed_name)
    {
      string sql=string.Format("  select * from user_tab_columns t where t.TABLE_NAME='{0}' and t.COLUMN_NAME='{1}'",
          TABLE_NAME.Text.ToUpper(), filed_name.ToUpper());
      return DataFunction.HasRecord(sql);
    }
    private void BindCH_CHILD_UNIT()
    {
//        string sql = string.Format(@"select * from (select distinct u.unit_id,u.unit_name,u.sequence from T_RES_SYS_UNIT_RELATION r left join T_RES_SYS_UNIT u on r.father_unit_id=u.unit_id 
//start with r.child_unit_id='{0}' connect by prior r.father_unit_id = r.child_unit_id) order by sequence", UNIT_ID.Text);
        string sql = "select * from t_res_sys_unit t where t.is_create_table=1 order by t.sequence";
        DataSet ds = DataFunction.FillDataSet(sql);
        CH_CHILD_UNIT.DataSource = ds;
        CH_CHILD_UNIT.DataBind();
    }

    private void SetCH_CHILD_UNIT()
    {
        string sql = "select * from T_RES_SYS_PROPERTY_UNIT t where t.PROPERY_ID='" + PROPERY_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["UNIT_ID"] };
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
        string sql = "delete from  T_RES_SYS_PROPERTY_UNIT t where t.PROPERY_ID='" + PROPERY_ID.Text + "'";
        DataFunction.ExecuteNonQuery(sql);
        sql = "select * from T_RES_SYS_PROPERTY_UNIT t where t.PROPERY_ID='" + PROPERY_ID.Text + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        int i = 0;
        foreach (ListItem li in CH_CHILD_UNIT.Items)
        {
            if (li.Selected)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["GUID"] = Guid.NewGuid().ToString();
                dr["PROPERY_ID"] = PROPERY_ID.Text;
                dr["UNIT_ID"] = li.Value;
                ds.Tables[0].Rows.Add(dr);
                i++;
            }
        }
        DataFunction.SaveData(ds, "T_RES_SYS_PROPERTY_UNIT");
        if (i == 0 && DATA_TYPE.SelectedValue == "资源选择")
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>alert('请选择可选资源！');</script>");
        }
    }
    protected void DeletButton_Click(object sender, EventArgs e)
    {
        string[] sqls=new string[4];
        sqls[0] = "delete from  T_RES_SYS_PROPERTY t where t.PROPERY_ID='" + PROPERY_ID.Text + "'";
        sqls[1] = "alter table "+TABLE_NAME.Text+" drop column "+FILED_NAME.Text;
        sqls[2] = "delete from  T_RES_SYS_PROPERTY_UNIT t where t.PROPERY_ID='" + PROPERY_ID.Text + "'";
        sqls[3] = "alter table " + TABLE_NAME.Text + "_LS drop column " + FILED_NAME.Text;
        DataFunction.ExecuteTransaction(sqls);

    }


}

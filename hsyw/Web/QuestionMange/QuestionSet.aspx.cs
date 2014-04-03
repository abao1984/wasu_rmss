using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_QuestionMange_QuestionSet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ENUM_SORT.Text = Request.QueryString["ENUM_SORT"];
            //switch (ENUM_SORT.Text)
            //{ 
            //    case"WTLY":
            //        LblTitle.Text = "问题来源";
            //        break;
            //    case"ZXTLB":
            //        LblTitle.Text = "子系统类别";
            //        break;
            //    case "WTYXJ":
            //        LblTitle.Text = "问题优先级";
            //        break;
            //    case "WTJG":
            //        LblTitle.Text = "问题结果";
            //        break;
            //    case "WTZT":
            //        LblTitle.Text = "问题状态";
            //        break;
            //}
            P_ENUM_NAME.Text = Request.QueryString["P_ENUM_NAME"];
            BindGridViewEnum();
        }
    }

    #region 绑定列表
    private DataSet GetEnumData(bool isSave)
    {
        string sql = "select * from T_RES_SYS_ENUMDATA where ENUM_SORT='" + ENUM_SORT.Text + "' ";
        if (!string.IsNullOrEmpty(P_ENUM_NAME.Text))
        {
            sql += " and P_ENUM_NAME='" + P_ENUM_NAME.Text + "'";
        }
        sql += " order by SEQUENCE";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (!isSave)
        {
            DataRow DR = ds.Tables[0].NewRow();
            DR["ENUM_GUID"] = Guid.NewGuid().ToString();
            DR["ENUM_SORT"] = ENUM_SORT.Text;
            if (!string.IsNullOrEmpty(P_ENUM_NAME.Text))
            {
                DR["P_ENUM_NAME"] = P_ENUM_NAME.Text;
            }
            DR["SEQUENCE"] = ds.Tables[0].Rows.Count + 1;
            ds.Tables[0].Rows.Add(DR);
        }
        return ds;
    }
    private void BindGridViewEnum()
    {
        this.GridViewEnum.DataSource = GetEnumData(false);
        this.GridViewEnum.DataBind();
    }
    #endregion

    #region 保存数据
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        DataSet ds = GetEnumData(true);
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ENUM_GUID"] };
        foreach (GridViewRow gr in GridViewEnum.Rows)
        {
            string Enum_Guid = GridViewEnum.DataKeys[gr.RowIndex].Value.ToString();
            DataRow dr = ds.Tables[0].Rows.Find(Enum_Guid);
            object Enum_Name = GetDataByGridText(gr, "ENUM_NAME");
            if (!String.IsNullOrEmpty(Enum_Name.ToString()))
            {
                if (dr == null)
                {
                    dr = ds.Tables[0].NewRow();
                    dr["ENUM_GUID"] = Enum_Guid;
                    ds.Tables[0].Rows.Add(dr);

                }
                dr["ENUM_SORT"] = ENUM_SORT.Text;
                if (!string.IsNullOrEmpty(P_ENUM_NAME.Text))
                {
                    dr["P_ENUM_NAME"] = P_ENUM_NAME.Text;
                }
                dr["SEQUENCE"] = GetDataByGridText(gr, "SEQUENCE");
                dr["ENUM_NAME"] = GetDataByGridText(gr, "ENUM_NAME");
                dr["IMAGE_URL"] = GetDataByGridText(gr, "IMAGE_URL");
                dr["ENUM_SHORT"] = GetDataByGridText(gr, "ENUM_SHORT");
            }
            else if (dr != null)
            {
                dr.Delete();
            }
        }
        DataFunction.SaveData(ds, "T_RES_SYS_ENUMDATA");
        BindGridViewEnum();
    }

    private object GetDataByGridText(GridViewRow gr, string ColumName)
    {
        TextBox tx = (TextBox)gr.FindControl(ColumName);
        if (string.IsNullOrEmpty(tx.Text))
        {
            return Convert.DBNull;
        }
        else
        {
            return tx.Text;
        }
    }
    #endregion
    protected void GridViewEnum_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            TextBox tx = (TextBox)e.Row.FindControl("SEQUENCE");
            tx.Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_QuestionMange_QuestionSolvePlan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.JHJJSJ.Attributes.Add("readonly", "true");
            BindGridViewEnum();
        }
    }

    #region 绑定列表
    private void BindGridViewEnum()
    {
        string quesid = Convert.ToString(Request.QueryString["QUESID"]);
        if (string.IsNullOrEmpty(quesid))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script>window.close();alert('请先保存主记录')</script>");
            SaveButton.Visible = false;
            return;
        }
        string sql = "select * from T_QUES_JJJH where QUESID='" + quesid + "' order by SJ";
        DataSet ds = DataFunction.FillDataSet(sql);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string UserName = Convert.ToString(Session["USERNAME"]);
            if (Convert.ToString(ds.Tables[0].Rows[0]["JLR"]) != UserName)
            {
                SaveButton.Visible = false;
            }
        }
        DataRow DR = ds.Tables[0].NewRow();
        DR["ID"] = Guid.NewGuid().ToString();
        DR["QUESID"] = Request.QueryString["QUESID"];
        ds.Tables[0].Rows.Add(DR);
    
        this.GridViewEnum.DataSource =ds;
        this.GridViewEnum.DataBind();
        sql = string.Format("select JHJJSJ from T_QUES_INFO where ID = '{0}'", Request.QueryString["QUESID"]);
        string jhjjsj = DataFunction.GetStringResult(sql);
        if(jhjjsj != "")
        {
            JHJJSJ.Text = Convert.ToDateTime(jhjjsj).ToString("yyyy-MM-dd HH:mm");
        }
    }
    #endregion

    #region 保存数据
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string sql = "select * from T_QUES_JJJH where QUESID='" + Request.QueryString["QUESID"] + "' order by SJ";
        DataSet ds = DataFunction.FillDataSet(sql);
        
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ID"] };
        foreach (GridViewRow gr in GridViewEnum.Rows)
        {
            string id = GridViewEnum.DataKeys[gr.RowIndex]["ID"].ToString();
            string quesid = GridViewEnum.DataKeys[gr.RowIndex]["QUESID"].ToString();
            DataRow dr = ds.Tables[0].Rows.Find(id);
            string sj = GetDataByGridText(gr, "SJ");
            string wcqk = GetDataByGridText(gr, "WCQK");
            if (sj != "" && wcqk != "")
            {
                if (dr == null)
                {
                    dr = ds.Tables[0].NewRow();
                    dr["ID"] = id;
                    ds.Tables[0].Rows.Add(dr);

                }
                dr["QUESID"] = quesid;
                dr["SJ"] = sj;
                dr["WCQK"] = wcqk;
                dr["JLR"] = Convert.ToString(Session["USERNAME"]);
            }
            else if (dr != null)
            {
                dr.Delete();
            }
        }
        DataFunction.SaveData(ds, "T_QUES_JJJH");
        DataFunction.ExecuteNonQuery(string.Format("update T_QUES_INFO set JHJJSJ = to_date('{0}','YYYY-MM-DD HH24:MI') where ID = '{1}'", JHJJSJ.Text, Request.QueryString["QUESID"]));
        BindGridViewEnum();
        
       
    }

    private string GetDataByGridText(GridViewRow gr, string ColumName)
    {
        TextBox tx = (TextBox)gr.FindControl(ColumName);
        return tx.Text;
    }
    #endregion
    protected void GridViewEnum_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            TextBox tx = (TextBox)e.Row.FindControl("SJ");
            if (tx.Text != "")
            {
                tx.Text = Convert.ToDateTime(tx.Text).ToString("yyyy-MM-dd HH:mm");
            }
            tx.Attributes.Add("readonly", "true");
        }
    }
}

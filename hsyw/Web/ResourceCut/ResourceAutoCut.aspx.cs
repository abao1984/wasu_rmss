using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Web_ResourceCut_ResourceAutoCut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SBLX.Text = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            BusinessGridView.Attributes.Add("BorderColor", "#5B9ED1");
        }
    }




    protected void okCutButton_Click(object sender, EventArgs e)
    {

        BusinessGridView.DataSource = CreateCutBuniss();
        BusinessGridView.DataBind();
    }

    private DataSet CreateCutBuniss()
    { 
        DataSet dst = GetYxyhDataSet();
        DataTable tb = dst.Tables[0];
        DataSet ds = GetBusinessData(SB_GUID.Text);
        string strSbid = SB_GUID.Text;
        
        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            if (strSbid.IndexOf(dr["SBID"].ToString()) == -1 || dr["YWBM"].ToString()!="")
            {
               // strSbid += dr["PSBID"].ToString();
                DataRow drt = tb.NewRow();
                drt["YH_GUID"] = Guid.NewGuid().ToString();
                drt["ZBGUID"] = ZBGUID.Text;
                drt["LLMC"] = dr["SBMC"].ToString();
                drt["YWBM"] = dr["YWBM"].ToString();
                drt["YWMC"] = dr["YWMC"].ToString();
                tb.Rows.Add(drt);
                ChildCutBuniss(strSbid,tb, drt, dr["SBID"].ToString());
            }
        }
        return dst;
    }

    private void ChildCutBuniss(string strSbid, DataTable tb, DataRow drt,string sbid)
    {
        DataSet ds = GetBusinessData(sbid);
        strSbid +=","+ sbid;
        int i = 0;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (strSbid.IndexOf(dr["SBID"].ToString()) == -1 || dr["YWBM"].ToString() != "")
            {
               // strSbid += dr["PSBID"].ToString();
                DataRow cDr = null;
                if (i == 0)
                {
                    cDr = drt;
                }
                else
                {
                    cDr = tb.NewRow();
                    cDr["YH_GUID"] = Guid.NewGuid().ToString();
                    cDr["ZBGUID"] = ZBGUID.Text;
                    tb.Rows.Add(cDr);
                }
                if (drt["LLMC"].ToString().IndexOf(dr["SBMC"].ToString()) == -1)
                {
                    if (dr["LLFX"].ToString() == "0")
                    {
                        cDr["LLMC"] = drt["LLMC"].ToString() + "～" + dr["SBMC"].ToString();
                    }
                    else
                    {
                        cDr["LLMC"] = drt["LLMC"].ToString() + "→" + dr["SBMC"].ToString();
                    }
                }
                else
                {
                    cDr["LLMC"] = drt["LLMC"].ToString();
                }
                cDr["YWBM"] = dr["YWBM"].ToString();
                cDr["YWMC"] = dr["YWMC"].ToString();
                i++;
                ChildCutBuniss(strSbid, tb, cDr, dr["SBID"].ToString());
            }
        }
    }


    private DataSet GetBusinessData(string sbid)
    {
        string sql = string.Format(@"select t.jdsb_guid as psbid, t.ydsb_guid as sbid,t.jdjrjf||'【'||t.ydsb||'】' as sbmc,t.ywbm,r.SUB_NAME as ywmc,t.llfx from {1} t 
left join rmss r on t.ywid=r.SUBSCRIBER_ID where t.jdsb_guid='{0}' and nvl(t.llfx,0) in ('1','0')
union
select t.ydsb_guid as psbid,t.jdsb_guid as sbid,t.ydjrjf||'【'||t.jdsb||'】' as sbmc,t.ywbm,r.SUB_NAME as ywmc,t.llfx from {1} t 
left join rmss r on t.ywid=r.SUBSCRIBER_ID  where t.ydsb_guid='{0}' and nvl(t.llfx,0) in ('-1','0')", sbid,GJFL.SelectedValue);
        return DataFunction.FillDataSet(sql);
    }

    private DataSet GetYxyhDataSet()
    {
        ZBGUID.Text = Guid.NewGuid().ToString();
        string sql = "select * from t_fau_yxyh t where t.zbguid='"+ZBGUID.Text+"'";
        return DataFunction.FillDataSet(sql);
    }
    protected void BusinessGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
    protected void CreateButton_Click(object sender, EventArgs e)
    {
        DataSet ds = CreateCutBuniss();
        DataFunction.SaveData(ds, "t_fau_yxyh");
        this.Response.Redirect("../GZCL/GuZhangEdit.aspx?ZBGUID=" + ZBGUID.Text+"&SB_GUID="+SB_GUID.Text+"&SB="+SB.Text+"&JF="+JF.Text+"&JF_CODE="+JF_CODE.Text);
    }
}

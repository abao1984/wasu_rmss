using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Web_GZCL_GuZhangList : System.Web.UI.Page
{
    private ShareGZCL gzcl = new ShareGZCL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            BindGridPage(BindGrid());
        }
    }

    private int BindGrid()
    {
        string userID = Session["UserID"].ToString();
        string type = Request.QueryString["Type"];
        string str = string.Format("select t.*,case when t.gzyyrid='{0}' or t.yjrcode like '%{0}%'  then 1 else 0 end as CZQX,case when t.gzclzt is not null then t.gzclzt else t.gzzt end as lczt from t_fau_zb2 t where (t.gzyyrid='{0}' or t.yjrcode like '%{0}%' or t.csrid like '%{0}%') and t.gzzt<>'结单' and t.fdzzt is null ", userID);
        DateTime datetime = DateTime.Now;
        switch (type)
        {
            case "db":
                RadioButtonList1.Style.Add("display", "bolck");
                if (RadioButtonList1.SelectedItem.Text != "全部")
                {
                    str += " and SFYD='" + RadioButtonList1.SelectedValue + "'";
                }
               
                //per.Text = "1";
                break;
            case "yb":
                str = string.Format("select t.*,case when t.gzclzt is not null then t.gzclzt else t.gzzt end as lczt from t_fau_zb2 t where zbguid in (select ZBGUID from t_fau_cllc2 where CLRYID='{0}')", userID);
                per.Text = "0";
                break;
            case "jr":
               // str += " and to_char(t.tssj,'yyyy-mm-dd')='" + datetime.ToString("yyyy-MM-dd")+"'";
                str = string.Format(@"select t.*,case when t.gzyyrid='{0}' or t.yjrcode like '%{0}%'  then 1 else 0 end as CZQX,case when t.gzclzt is not null then t.gzclzt else t.gzzt end as lczt
        from t_fau_zb2 t where t.zbguid in (
        select distinct zbguid from t_fau_cllc2 h where to_char(h.clsj,'yyyy-mm-dd')='{1}' and h.clryid in (
        select id from t_sys_user t where t.branchcode = (select branchcode from t_sys_user where id='{0}')))", userID, datetime.ToString("yyyy-MM-dd"));
                per.Text = "0";
                break;
            case "zr":
                //str += " and to_char(t.tssj,'yyyy-mm-dd')='" + datetime.AddDays(-1).ToString("yyyy-MM-dd")+"'";
                str = string.Format(@"select t.*,case when t.gzyyrid='{0}' or t.yjrcode like '%{0}%'  then 1 else 0 end as CZQX,case when t.gzclzt is not null then t.gzclzt else t.gzzt end as lczt
        from t_fau_zb2 t where t.zbguid in (
        select distinct zbguid from t_fau_cllc2 h where to_char(h.clsj,'yyyy-mm-dd')='{1}' and h.clryid in (
        select id from t_sys_user t where t.branchcode = (select branchcode from t_sys_user where id='{0}')))", userID, datetime.AddDays(-1).ToString("yyyy-MM-dd"));
                per.Text = "0";
                break;
        }

        if (GZBH.Text != "")
        {
            str += " and GZBH  like '%" + GZBH.Text + "%'";
        }

        if (KHMC.Text != "")
        {
            str += " and GZMC like '%" + KHMC.Text + "%'";
        }

        if (KHQY.Text != "")
        {
            str += " and KHQY='" + KHQY.Text + "'";
        }

        //可访问区域
        if (Session["ISSUPER"].ToString() != "1")
        {
            if (Session["FWQY"] == null)
            {
                str += " and 1<>1 ";
            }
            else
            {
                string[] fwqy = Session["FWQY"].ToString().Split(',');
                string strQy = "";
                foreach (string qy in fwqy)
                {
                    if (strQy != "")
                    {
                        strQy += " or ";
                    }
                    strQy += " t.KHQYID like '" + qy + "%' ";
                }
                if (!string.IsNullOrEmpty(strQy))
                {
                    str += " and (" + strQy + ") ";
                }
            }
        }
        //str += " and (t.ldsj is null or (trunc(sysdate)-(INTERVAL '1' DAY))>=t.ldsj) ";//留单
        str += "  order by tssj desc";
        DataSet dts = DataFunction.FillDataSet(str);
        return gzcl.BindGridView(GridView1, dts);
    }

    #region 分页管理
    private void BindGridPage(int DataCount)
    {
        GridView1.PageIndex = 0;
        int PageCount = DataCount / Convert.ToInt32(PageSize.SelectedValue);
        if (DataCount % Convert.ToInt32(PageSize.SelectedValue) > 0)
        {
            PageCount++;
        }
        GridPageList.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            ListItem LI = new ListItem(i.ToString() + "/" + PageCount.ToString(), Convert.ToString(i - 1));
            GridPageList.Items.Add(LI);
        }
        DataCountLab.Text = DataCount.ToString();
        PageCountLab.Text = PageCount.ToString();
        PageIndexLab.Text = "1";
    }


    protected void PrevButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (PageIndex == 0)
        {
            GridPageList.SelectedIndex = GridPageList.Items.Count - 1;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex - 1;
        }
        GridView1.PageIndex = Convert.ToInt32(GridPageList.SelectedValue);
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }

    protected void NextButton_Click(object sender, System.EventArgs e)
    {
        int PageIndex = GridPageList.SelectedIndex;
        if (GridPageList.Items.Count - 1 == PageIndex)
        {
            GridPageList.SelectedIndex = 0;
        }
        else
        {
            GridPageList.SelectedIndex = PageIndex + 1;
        }
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }

    protected void PageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(PageSize.SelectedValue);
        BindGridPage(BindGrid());
    }
    protected void GridPageList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridPageList.SelectedIndex;
        PageIndexLab.Text = Convert.ToString(GridView1.PageIndex + 1);
        BindGrid();
    }
    #endregion

    protected void BtnJS_Click(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    
    private DataSet DBstr()
    {
        string userID = Session["UserID"].ToString();
        string str = string.Format("select t.*,case when t.gzyyrid='{0}' or t.yjrcode like '%%'  then 1 else 0 end as per from t_fau_zb2 t where t.gzyyrid='{0}' or t.yjrcode like '%{0}%' or t.csrid like '%{0}%'", userID);
        DataSet ds = DataFunction.FillDataSet(str);
        return ds;
    }

    /// <summary>
    /// 判断本部门下的用户是否存在于
    /// </summary>
    /// <param name="bmCode"></param>
    /// <param name="zbguid"></param>
    /// <returns></returns>
    private bool SFCZBMRY(string bmCode,string zbguid,string ryid)
    {
        string[] ryList = ryid.Split(',');
        string sql = "select * from t_sys_user t where t.branchcode='" + bmCode + "'";
        DataSet ds = DataFunction.FillDataSet(sql);
        ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["ID"] };
        foreach(string id in ryList)
        {
            DataRow dr = ds.Tables[0].Rows.Find(id);
            if(dr!=null)//存在
            {
                return true;
            }
        }
        return false;
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridPage(BindGrid());
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string userId=Session["UserID"].ToString();
        if(e.Row.RowIndex > -1)
        {
            string zbguid=GridView1.DataKeys[e.Row.RowIndex][0].ToString();
            string strSql = string.Format(@"select t.gzyyrid,t.sfsd,
       case when t.csrid like '%{0}%' then 0 else 1 end as cs,
       case when t.yjrcode like '%{0}%' then 0 else 1 end as zs, 
       case when GZYDRID like '%{0}%' then 0 else 1 end as yd,
       t.*
       from t_fau_zb2 t where zbguid='{1}'", userId,zbguid);
            DataRow dr = DataFunction.GetSingleRow(strSql);
            if ( dr["sfsd"].ToString() == "0")  //锁定
            {
                e.Row.FindControl("img_SD").Visible = true;
                e.Row.BackColor = Color.Wheat;
            }

            //主送，抄送
            if (dr["zs"].ToString() == "0")
            {
                e.Row.FindControl("img_zs").Visible = true;
            }
            else if (dr["cs"].ToString() == "0")
            {
                e.Row.FindControl("img_cs").Visible = true;
            }

            //升级
            if (dr["sjzt"].ToString() == "0")
            {
                e.Row.FindControl("img_sj").Visible = true;
            }
            //超时
            if (dr["cszt"].ToString() == "0")
            {
                e.Row.FindControl("img_yq").Visible = true;
            }
            //string strYDR = string.Format(@"select * from t_fau_zb2 where GZYDRID like '%{0}%'",userId);
            //是否阅读
            if (dr["yd"].ToString() == "0")//已读
            {
                e.Row.FindControl("img_yd").Visible = true;
            }
            else
            {
                e.Row.FindControl("img_wd").Visible = true;
            }
            //故障时长
            if (dr["tssj"]==DBNull.Value)
            {
                return;
            }
            if (dr["jdsj"] == DBNull.Value)
            {
                DateTime dt = DateTime.Now;
                TimeSpan ts =dt - Convert.ToDateTime(dr["tssj"]);
                e.Row.Cells[12].Text = ts.TotalMinutes.ToString("f0");
            }
            else
            {
                TimeSpan ts = Convert.ToDateTime(dr["jdsj"]) - Convert.ToDateTime(dr["tssj"]);
                e.Row.Cells[12].Text = ts.TotalMinutes.ToString("f0");
            }
            //故障拥有人
            if (dr["gzyyrid"].ToString().Length==36)
            {

                e.Row.Cells[9].Text = DataFunction.GetStringResult("select h.branchname||'/'||t.userrealname from t_sys_user t left join t_sys_branch h on t.branchcode=h.branchcode  where t.id='" + dr["gzyyrid"].ToString() + "'");
            }

            //处理人
            e.Row.Cells[10].Text=DataFunction.GetStringResult(string.Format(@"select clry,t.clsj
  from t_fau_cllc2 t
 where t.zbguid = '{0}'
   and t.clsj =
       (select max(clsj)
          from t_fau_cllc2
         where zbguid = '{0}')",zbguid));

            //主送
            e.Row.Cells[11].Text = getZSYRBM(dr["yjr"].ToString(), dr["yjbm"].ToString());
        }
    }

    private string getZSYRBM(string ZSRY,string ZSBM)
    {
        string yrbmStr = "";
        string strSql = "";
        foreach(string ry in ZSRY.Split(','))
        {
            foreach(string bm in ZSBM.Split(','))
            {
                strSql = string.Format(@"select t.*
  from t_sys_user t, t_sys_branch b
 where t.branchcode = b.branchcode
   and t.userrealname = '{0}'
   and b.branchname = '{1}'",ry,bm);
                if(!DataFunction.HasRecord(strSql))
                {
                    yrbmStr += ry + ",";
                }
            }
        }
        if(yrbmStr!="")
        {
            yrbmStr = yrbmStr.Substring(0,yrbmStr.Length-1);
            yrbmStr += "/" + ZSBM;
        }
        return yrbmStr;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Web_LCGL_DaiBanGongZuo : System.Web.UI.Page
{
    private string lcbm;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        }
    }
    #region 创建列表

    private void CreateMenuTable()
    {
        DataSet ds = GetMenuData();
        int i = 0;
        MenuTr.Cells.Clear();
        foreach (DataRow DR in ds.Tables[0].Rows)
        {
            CreateMenuCell(DR["JDBM"].ToString(), DR["JDMC"].ToString(), i);
            i++;
        }
       
    }
    private void CreateMenuCell(string jdbm, string jdmc, int i)
    {
        HtmlTableCell TD = new HtmlTableCell();
        TD.InnerHtml = "<a href=# onclick='changeMenu(\"" + jdbm + "\")'>" + jdmc + "</a>";
        //if (JDBM.Text == "" && i == 0)
        //{
        //    TD.Attributes.Add("class", "nav01");
        //    JDBM.Text = jdbm;
        //}
        //else if (JDBM.Text == jdbm)
        //{
        //    TD.Attributes.Add("class", "nav01");
        //}
        //else
        //{
        //    TD.Attributes.Add("class", "nav02");
        //}
        MenuTr.Cells.Add(TD);
    }
    private DataSet GetMenuData()
    {
        string sql = string.Format(@" select a.jdbm,a.jdmc||'【'||nvl(b.cn,0)||'】' as jdmc from 
t_lcgl_sys_lckz_cb a left join 
(select t.lcjrzt,count(*) as cn from t_lcgl_sys_lcjl t where t.lcbm='{0}' and t.sfqf=0 group by t.lcjrzt) b
on a.jdbm=b.lcjrzt where a.lcbm='{0}' order by a.sxh", lcbm);
        return DataFunction.FillDataSet(sql);
    }
    #endregion


}

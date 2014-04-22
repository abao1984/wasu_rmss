using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class Announcement_list : System.Web.UI.Page
{
    public DataSet ds;
    public ArrayList data_list = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        int min_value = 0;
        if (Request.QueryString.Get("start")!=null)
        {
            min_value = int.Parse(Request.QueryString["start"]);
        }
        
        int max_value = min_value + 100;

        string sql = String.Format(@"select * from 
(select a.*,rownum rnum from (select * from announcements order by post_time desc) a where rownum <= {1})where rnum>={0}",min_value,max_value);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow row in ds.Tables[0].Rows)
        { 
            Dictionary<string,string> dict = new Dictionary<string,string>();
            dict.Add("id", row["id"].ToString());
            dict.Add("time", row["post_time"].ToString());
            dict.Add("title", row["post_title"].ToString());
            data_list.Add(dict);
        }
    }
}

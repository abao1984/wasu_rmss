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

public partial class Announcement_edit : System.Web.UI.Page
{
    public int id;
    public string title;
    public string owner;
    public string content;
    public int create_message;
    public string owner_list;
    protected void  Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.Equals("GET"))
        {
            id = int.Parse(Request.QueryString["id"]);
            string sql = String.Format("select * from ANNOUNCEMENTS where id = {0}", id);
            DataRow row = DataFunction.GetSingleRow(sql);
            title = row["post_title"].ToString();
            owner = row["post_owner"].ToString();
            content = row["post_content"].ToString();

            List<string> owners = owner.Split(',').ToList<string>();
            List<string> tmp = new List<string>();
            foreach (string s in owners)
            {
                string str_id = String.Format("'{0}'",s);
                tmp.Add(str_id);
            }

            string owner_ids = string.Join(",", tmp.ToArray());

            sql = String.Format("select * from t_sys_user where id in ({0})", owner_ids);
            DataSet ds = DataFunction.FillDataSet(sql);
            List<string> names = new List<string>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            { 
                names.Add(dr["userrealname"].ToString());
            }
            owner_list = string.Join(",",names.ToArray());
        }
        else {
            string post_title = Request.Form["post_title"];
            string post_owner = Request.Form["post_owner_ids"];
            string post_content = Request.Form["post_content"];
            id = int.Parse(Request.Form["post_id"]);

            string sql = String.Format("update ANNOUNCEMENTS set post_title='{0}',post_owner='{1}',post_content='{2}' where id={3}", post_title, post_owner, post_content, id);
            int result = DataFunction.ExecuteNonQuery(sql);
            Response.Redirect("Announcement_list.aspx");
        }
    }

    public string showErrorMessage(string field)
    {
        return String.Format("'{0}'是必填的。",field);
    }

}

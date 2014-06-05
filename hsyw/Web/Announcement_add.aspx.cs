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
using HSYWContext;

public partial class Announcement_add : System.Web.UI.Page
{
    public string title = "";
    public string owner = "";
    public string content = "";
    public int create_message;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql = String.Format("select count(*) from all_tables where table_name = 'ANNOUNCEMENTS'");
        DataRow dr = DataFunction.GetSingleRow(sql);
        var count = dr.ItemArray.First();
        if (count.ToString().Equals("0"))
        {
            sql = String.Format(@"create table ANNOUNCEMENTS
(id number(10) constraint pk_id primary key,
post_time date,
post_owner clob not null,
post_title varchar2(200) not null,
post_content clob not null,
post_comment varchar2(200) default ''
)");
            int result = DataFunction.ExecuteNonQuery(sql);

        }


    }
    protected void post_to_list_Click(object sender, EventArgs e)
    {

        post_to_new_Click(sender, e);
        Response.Redirect("Announcement_list.aspx");
    }

    public string showErrorMessage(string field)
    { 
        string message = String.Format("‘{0}’ 是必填的。",field);
        return message;


    }
    protected void post_to_new_Click(object sender, EventArgs e)
    {
        title = Request.Form["post_title"];
        owner = Request.Form["post_owner_ids"];
        content = Request.Form["post_content"];
        if (title.Length == 0 || owner.Length == 0 || content.Length == 0)
        {
            return;
        }

        //string time_now = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");

        //string sql = String.Format(@"insert into ANNOUNCEMENTS (post_time,post_owner,post_title,post_content,post_comment) values(to_date('{0}','YYYY-MM-DD HH24:MI:SS'),'{1}','{2}','{3}','{4}')", time_now, owner, title, content, "");
        //create_message = DataFunction.ExecuteNonQuery(sql);

        HSYWDataContext ctx = new HSYWDataContext();
        var user_id_list = owner.Split(',');//string.Join(",",owner);
        
        var users = (from c in ctx.TSYSUSERs
                    where user_id_list.Contains(c.ID)
                    select c).ToList();

        var announce = new ANNOUNCEMENT
        {
            POSTTIME = DateTime.Now,
            POSTOWNER = owner,
            POSTTITLE = title,
            POSTCONTENT = content,
            POSTCOMMENT = "",
        };
        ctx.ANNOUNCEMENTs.InsertOnSubmit(announce);
        //ctx.SubmitChanges();
        
        foreach(TSYSUSER user in users)
        {
            var record  =new ANNOUNCEMENTRECORD{
                READFLAG = false,
                ANNOUNCEMENT = announce,
                TSYSUSER = user,
            };
            ctx.ANNOUNCEMENTRECORDs.InsertOnSubmit(record);
            //ctx.SubmitChanges();
        }
        ctx.SubmitChanges();
    }
}

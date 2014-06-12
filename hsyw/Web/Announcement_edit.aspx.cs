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
using HSYWContext;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
public partial class Announcement_edit : System.Web.UI.Page
{
    public int id;
    public string title;
    public string owner;
    public string content;
    public int create_message;
    public string owner_list;
    private HSYWDataContext ctx = new HSYWDataContext();

    protected void  Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod.Equals("GET"))
        {
            id = int.Parse(Request.QueryString["id"]);
            var announce = ctx.ANNOUNCEMENTs.Where(c => c.ID == id).FirstOrDefault();
            title = announce.POSTTITLE;
            owner = announce.POSTOWNER;
            content = announce.POSTCONTENT;

            List<string> owners = owner.Split(',').ToList<string>();
            List<string> tmp = new List<string>();
            foreach (string s in owners)
            {
                string str_id = String.Format("{0}",s);
                tmp.Add(str_id);
            }
            
            string baseURL = Request.Url.Host;
            string announceId = id.ToString();
            string requestURL = string.Format("ws.asmx/getUserNameByAnnounceId", baseURL,Request.Url.Port);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("announceId", announceId);
            HttpWebRequest request = WebRequest.Create(requestURL) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (string key in parameters.Keys)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, parameters[key]);
                }
                i++;
            }

            byte[] data = Encoding.GetEncoding("utf-8").GetBytes(buffer.ToString());
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string html = reader.ReadToEnd();

            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string
            >>(html);
            owner_list = dict["result"];
            
            string owner_ids = string.Join(",", tmp.ToArray());
            /*
            var users = ctx.TSYSUSERs.AsQueryable<TSYSUSER>().Where(c=>owners.Contains(c.ID));
            
            List<string> names = new List<string>();

            foreach(var user in users)
            { 
                names.Add(user.USERREALNAME);
            }
            owner_list = string.Join(",",names.ToArray());
             */
        }
        else {
            string post_title = Request.Form["post_title"];
            string post_owner = Request.Form["post_owner_ids"];
            string post_content = Request.Form["post_content"];
            id = int.Parse(Request.Form["post_id"]);

            var announce = ctx.ANNOUNCEMENTs.Where(c => c.ID == id).SingleOrDefault();
            announce.POSTTITLE = post_title;
            announce.POSTOWNER = post_owner;
            announce.POSTCONTENT = post_content;
            ctx.SubmitChanges();

            //string sql = String.Format("update ANNOUNCEMENTS set post_title='{0}',post_owner='{1}',post_content='{2}' where id={3}", post_title, post_owner, post_content, id);
            //int result = DataFunction.ExecuteNonQuery(sql);
            Response.Redirect("Announcement_list.aspx");
        }
    }

    public string showErrorMessage(string field)
    {
        return String.Format("'{0}'是必填的。",field);
    }

}

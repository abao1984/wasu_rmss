using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///WSAnnouncement 的摘要说明
/// </summary>
public class WSAnnouncement
{
	public WSAnnouncement()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public int id { get; set; }
    public string time { get; set; }
    public string owner { get; set; }
    public string title {get;set;}
    public string content { get; set; }
    public string comment { get; set; }

}

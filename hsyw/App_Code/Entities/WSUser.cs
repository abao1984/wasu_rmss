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
///WSUser 的摘要说明
/// </summary>
public class WSUser
{
	public WSUser()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    //select username,userrealname,branchcode,isuse,isvisible from t_sys_user
    public string name { get;set; }
    public string realName { get;set; }
    public string code { get; set; }
    public string isUse { get; set; }
    public string isVisible { get; set; }

}

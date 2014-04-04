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
///Area 的摘要说明
/// </summary>
public class Area
{
    //public string id   { get; set; }
    public string code { get; set; }
    public string name { get; set; }
    public string parentCode { get; set; }
    public string type { get; set; }
    public string order { get; set; }
    //public string isUse { get; set; }
    //public string isVisible { get; set; }
    public string isArea { get; set; }
    public string path { get; set; }
}

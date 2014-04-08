using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;

/// <summary>
///ws 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ws : System.Web.Services.WebService {

    public ws () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void HelloWorld() {
        string hello = "hello";
        string world = "world";
        ArrayList list = new ArrayList();
        list.Add(hello);
        list.Add(world);

        string jsonData = JsonConvert.SerializeObject(list);

        Context.Response.Write(jsonData);
        Context.Response.End();
        
    }

    private void writeJSONResponse(Object o)
    {
        string jsonData = JsonConvert.SerializeObject(o, Formatting.Indented);
        Context.Response.Write(jsonData);
        Context.Response.End();
    }

    [WebMethod]
    public void get_user()
    {
        ArrayList areaList = getArea("");
        Dictionary<string,string> areaDictionary = new Dictionary<string,string>();

        foreach(Area a in areaList)
        {
            areaDictionary.Add(a.code, a.path);
        }


        ArrayList userList = getUserInDB();

        ArrayList pathList = new ArrayList();
        foreach (WSUser u in userList)
        {
            string key = u.code;
            string path = string.Format(@"{0}/{1}", areaDictionary[key], u.realName);
            Dictionary<string,string> dict = new Dictionary<string,string>();
            dict.Add("label",path);
            dict.Add("value",path);
            dict.Add("id", u.id);
            pathList.Add(dict);
           
        }

        writeJSONResponse(pathList);
    }

    private ArrayList getUserInDB()
    {
        string sql = "select id,username,userrealname,branchcode,isuse,isvisible from t_sys_user";
        DataSet ds = DataFunction.FillDataSet(sql);
        ArrayList list = new ArrayList();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            WSUser user = new WSUser
            {
                id = dr["id"].ToString(),
                name = dr["username"].ToString(),
                realName = dr["userrealname"].ToString(),
                code = dr["branchcode"].ToString(),
                isUse = dr["isuse"].ToString(),
                isVisible = dr["isvisible"].ToString()
            };
            list.Add(user);
        }

        return list;
    }

    private ArrayList getArea(string isArea)
    {
        string sql;
        if (isArea.Length > 0)
        {
            sql = "select * from t_sys_branch b where b.ISUSE='1' and ISQY='1' order by DISPLAYORDER";
        }
        else
        {
            sql = "select * from t_sys_branch b where b.ISUSE='1'  order by DISPLAYORDER";
        }

        DataSet ds = DataFunction.FillDataSet(sql);
        ArrayList list = new ArrayList();
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            Area a = new Area{name = dr["branchname"].ToString(),
                            code = dr["branchcode"].ToString(),
                            parentCode = dr["pbranchcode"].ToString(),
                            type = dr["jglx_datadm"].ToString(),
                            order = dr["displayorder"].ToString(),
                            //isUse = dr["isuse"].ToString(),
                            //isVisible = dr["isvisible"].ToString(),
                            isArea = dr["isqy"].ToString(),
                            path = dr["path"].ToString()
                            };
            list.Add(a);
        }

        ArrayList tmpList = new ArrayList();
        foreach (Area a in list)
        {
            if (a.path.Length == 0)
            {
                a.path = string.Format(@"{0}/{1}", getParentPath(a.parentCode, list), a.name);
            }

            tmpList.Add(a);
        }

        list = tmpList;

        return list;
    }

    [WebMethod]
    public void get_area(string isArea)
    {
        ArrayList list = getArea(isArea);
        ArrayList pathList = new ArrayList();

        foreach (Area a in list)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("label", a.path);
            dict.Add("value", a.path);
            dict.Add("code", a.code);
            pathList.Add(dict);
        }

        writeJSONResponse(pathList);    
    }

    private string getParentPath(string parentCode,ArrayList list)
    {
        foreach (Area area in list)
        {
            string path = area.path;
            string code = area.code;
            if (code == parentCode) {
                return path;
            }
        }
        return null;
    }
    
}


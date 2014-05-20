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
using System.Text;
using System.IO;
using LinqToExcel;
using System.Web.Script.Services;
/// <summary>
///ws 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ws : System.Web.Services.WebService {
    private static Random random = new Random((int)DateTime.Now.Ticks);

    public ws () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    private string RandomString(int size)
    {
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }

        return builder.ToString();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
    public void pppoe_excel_upload()
    {
        HttpContext post = HttpContext.Current;
        HttpFileCollection files = post.Request.Files;
        if (files.Count == 0)
        {
            return;
        }
        HttpPostedFile targetFile = files[0];
        string fileName = targetFile.FileName;
        if (targetFile.ContentLength == 0)
        {
            return;
        }
      

        byte[] binaryArray = new byte[targetFile.InputStream.Length];
        targetFile.InputStream.Read(binaryArray, 0, (int)targetFile.InputStream.Length);
            
        var appData = Server.MapPath("~/upload_temp");
        var file = Path.Combine(appData, Path.GetFileName(fileName));
        
        FileStream fs = new FileStream(file,FileMode.Create,FileAccess.ReadWrite);
        fs.Write(binaryArray,0,binaryArray.Length);
        fs.Close();


        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("message", "");
        try
        {
            string result = selectExcel(file);
            dict["message"] = "ok";
            if (result.Length > 0)
            {
                dict["message"] = result;
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            dict["message"] = e.Message;
        }

        writeJSONResponse(dict);
        
    }

    private string selectExcel(string filename) 
    {
        string result = "";
        var excel = new ExcelQueryFactory(filename);
        var sheet = excel.Worksheet(0);
        var query = from d in sheet
                    select d;

        DataClassesDataContext dc = new DataClassesDataContext();

        var PPPOEBussiness = from a in dc.IP_Bussiness
                                where a.Bussiness_code.StartsWith("Z_P_") orderby a.Bussiness_code descending
                                select a;

        long count = long.Parse(PPPOEBussiness.First().Bussiness_code.Replace("Z_P_", ""));
        foreach (var item in query)
        {   
            var bussinessType = (from a in dc.Jrlx_List
                                 where a.nodename == item["业务类型"]//ywlx
                                 select a).FirstOrDefault();
            var type = bussinessType.nodeid;

            //业务编码生成
            count += 1;

            string bussinessCode = String.Format("Z_P_{0:D9}",count);

            var q = (from b in dc.TS_ZD_Info
                       where b.mc == item["带宽"]
                       select b).FirstOrDefault();
            string bandWidthCode = "";
            if (q!=null)
            {

                bandWidthCode = q.lsh;
            }

            var machineRoomId = item["机房编号"].ToString().Replace("'","");


            string configDate = "";
            if (item["配置时间"].ToString().Length > 0)
            {
                string cellDate = item["配置时间"].ToString();
                configDate = cellDate.Split(' ')[0];
            
            }

            string deviceInfo = item["设备配置信息"];
            string[] deviceInfoArray = deviceInfo.Split('.');

            string devicePort = "";

            for (int i = 0; i < 3; i++)
            {
                if (i != 2)
                {
                    devicePort += String.Format("{0}.",deviceInfoArray[i]);
                }
                else{
                    devicePort += deviceInfoArray[i];
                }
            }

            var port = from p in dc.IP_Bussiness
                       where p.sbpzxx.StartsWith(devicePort)
                       select p;

            if (port.Count() > 0)
            {
                result += port.FirstOrDefault().ID+",";
                continue;
            }
             
            var bussiness = new IP_Bussiness
            {   
                ywlx           = type,
                macid          = machineRoomId,
                sbpzxx         = item["设备配置信息"],
                Bussiness_code = bussinessCode,//item["业务编码"],
                Line_code      = item["线路编码"],
                xqjrwg         = item["小区接入网关"],
                brandwidth     = bandWidthCode,    
                sbbh           = item["设备名称"],
                sfqxh          = item["收发器型号"],
                gsfq           = item["光收发器"],
                gcjx           = item["光交接箱"],
                gqtx           = item["光纤跳线"],
                maconu         = item["MAC地址ONU"],
                fgqdk          = item["分光器端口号"],
                gsfqdk         = item["光纤收发器端口"],
                khmc           = item["接入单位"],
                khlxr          = item["用户负责人"],
                lxdh           = item["用户电话"],
                //xxx          = item["用户手机"],
                khdz           = item["用户地址"],
                remark         = item["备注"],
                pzr            = item["配置人"],
                pzsj           = configDate,
                VLAN           = item["VLAN"],
                by1            = "import by PPPOE excel file",
                
            };
            dc.IP_Bussiness.InsertOnSubmit(bussiness);        
        }

        dc.SubmitChanges();

        if (result.Length > 0)
        {
            result = String.Format(@"导入excel中的数据与数据库中ID编号为‘{0}’的资料端口冲突", result); 
                
        }

        return result;
    }

    [WebMethod(CacheDuration=60*60)]
    public void get_everyday_total_report()
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();

        string sql = "select BRANCHCODE,BRANCHNAME from t_sys_branch where PBRANCHCODE='10010103'";
        DataSet ds1 = DataFunction.FillDataSet(sql);
        Dictionary<string, string> branch_dict = new Dictionary<string, string>();
         foreach (DataRow dr in ds1.Tables[0].Rows) {
            branch_dict.Add(dr["branchcode"].ToString(),dr["branchname"].ToString());
        }

        List<string> user_list = new List<string>();

        List<Dictionary<string, string>> department_list = new List<Dictionary<string, string>>();

        //create tmp table to store current data
        string random_str = RandomString(10);
        string temp_table_name = String.Format("t_fau_zb_tmp_{0}",random_str);
        
        Console.WriteLine(temp_table_name);

        sql = String.Format("create global temporary table {0} on commit preserve rows as select * from t_fau_zb where trunc(gzsdsj)=trunc(sysdate)",temp_table_name);
        int result = DataFunction.ExecuteNonQuery(sql);
        
        foreach (string key in branch_dict.Keys)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            sql = String.Format("select userrealname from t_sys_user where branchcode like '{0}%'",key);
            DataSet ds = DataFunction.FillDataSet(sql);
            
            List<string> name_list = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                name_list.Add(String.Format("'{0}'",row[0].ToString()));
                user_list.Add(String.Format("'{0}'", row[0].ToString()));
            }

            if (name_list.Count == 0)
            {
                dic.Add("key", branch_dict[key]);
                dic.Add("value", "0");
                department_list.Add(dic);
                Console.WriteLine("name list length equal 0 continue...");
                continue;
            }

            string names = string.Join(",",name_list.ToArray());

            string condition = String.Format("ddfdr in ({0}) or SUBSTR(ddfdr, 0, INSTR(ddfdr, ',')-1) in ({0})",names);
            string str = String.Format("select count(zbguid) from {1} where trunc(gzsdsj)=trunc(sysdate) and fdzzt='维修返单' and ({0})", condition,temp_table_name);
            DataRow dr = DataFunction.GetSingleRow(str);
            
            dic.Add("key",branch_dict[key]);
            dic.Add("value", dr[0].ToString());

            department_list.Add(dic);
            
            
        }
        dict.Add("list", department_list);
        string users = string.Join(",", user_list.ToArray());
        string substring = String.Format("SUBSTR(ddfdr, 0, INSTR(ddfdr, ',')-1) in ({0}) or ddfdr in ({0})", users);
        sql = String.Format(@"select count(zbguid)  from {1} where trunc(gzsdsj)=trunc(sysdate) and ({0})
UNION ALL
select count(zbguid) from {1} where trunc(gzsdsj)=trunc(sysdate) and fdzzt='结单' and ({0})
UNION ALL
select count(zbguid) from {1} where trunc(gzsdsj)=trunc(sysdate) and fdzzt='调度发单' and ({0})
UNION ALL
select count(zbguid) from {1} where trunc(gzsdsj)=trunc(sysdate) and fdzzt='电话处理' and ({0})
UNION all
select count(zbguid) from {1} where trunc(gzsdsj)=trunc(sysdate) and fdzzt='维修返单' and ({0})
UNION ALL
select count(zbguid) from {1} where trunc(gzsdsj)=trunc(sysdate) and fdzzt='遗单' and ({0})", substring,temp_table_name);
        
        DataSet data_set = DataFunction.FillDataSet(sql);

        dict.Add("今日工单", data_set.Tables[0].Rows[0][0].ToString());
        dict.Add("结单", data_set.Tables[0].Rows[1][0].ToString());
        dict.Add("调度发单", data_set.Tables[0].Rows[2][0].ToString());
        dict.Add("电话处理", data_set.Tables[0].Rows[3][0].ToString());
        dict.Add("维修返单", data_set.Tables[0].Rows[4][0].ToString());
        dict.Add("遗单", data_set.Tables[0].Rows[5][0].ToString());
        
        //drop the tmp table
        sql = String.Format("truncate table {0}", temp_table_name);
        result = DataFunction.ExecuteNonQuery(sql);
        sql = String.Format("drop table {0}", temp_table_name);
        result = DataFunction.ExecuteNonQuery(sql);
       
        writeJSONResponse(dict);
    }

    [WebMethod]
    public void get_announcement(string user_id,string page)
    {
        if (page.Length == 0)
        {
            page = "1";
        }
        
        string sql = String.Format("select * from (select a.*,rownum rn from (select * from announcements where post_owner like '%{0}%' order by post_time desc) a where rownum <= (({1}*100)+1) ) where rn>=((({1}-1)*100)+1)",user_id,page);
        DataSet ds = DataFunction.FillDataSet(sql);
        ArrayList announce_list = new ArrayList();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            WSAnnouncement a = new WSAnnouncement
            {
                id = int.Parse(row["id"].ToString()),
                title = row["post_title"].ToString(),
                content = row["post_content"].ToString(),
                time = row["post_time"].ToString(),
                owner = row["post_owner"].ToString(),
                comment = row["post_comment"].ToString()
            };
            announce_list.Add(a);
        }

        writeJSONResponse(announce_list);
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
        Encoding encode = System.Text.Encoding.GetEncoding("Unicode"); 
        string jsonData = JsonConvert.SerializeObject(o, Formatting.Indented);
        Context.Response.AddHeader("Content-type", "text/html;charset=UTF-8");
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

    private ArrayList getIdByCode(string code)
    {
        ArrayList list = new ArrayList();
        string sql = String.Format("select id from t_sys_user where branchcode like '{0}%'",code);
        DataSet ds = DataFunction.FillDataSet(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            list.Add(dr[0].ToString());
        }
        return list;
    }

    private string listToString(ArrayList list)
    {
        string s = string.Join(",", (string[])list.ToArray(typeof(string)));
        return s;
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
            string ids = listToString(getIdByCode(a.code));
            dict.Add("id",ids);
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


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
using System.Globalization;
using System.Text.RegularExpressions;
//using HSYWContext;





/// <summary>
///ws 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class ws : System.Web.Services.WebService {
    private static Random random = new Random((int)DateTime.Now.Ticks);
    //private HSYWDataContext ctx = new HSYWDataContext();
    DataClassesDataContext dc = new DataClassesDataContext();

 

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
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ip_excel_upload()
    {
        HttpContext post = HttpContext.Current;
        HttpFileCollection files = post.Request.Files;
        if (files.Count == 0)
        {
            return;
        }
        HttpPostedFile target = files[0];
        string fileName = target.FileName;
        if (target.ContentLength == 0)
        {
            return;
        }

        byte[] binaryArray = new byte[target.InputStream.Length];
        target.InputStream.Read(binaryArray, 0, (int)target.InputStream.Length);
        var appData = Server.MapPath("~/upload_temp");
        var file = Path.Combine(appData, Path.GetFileName(fileName));
        FileStream fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite);
        fs.Write(binaryArray, 0, binaryArray.Length);
        fs.Close();
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("message", "");
        try
        {
            //string result = "";
            string result = queryExcel(file);
            dict["message"] = "ok";
            if (result.Length>0)
            {
                dict["message"] = result;
            }

        }
        catch (Exception e)
        {
            dict["message"] = e.ToString();
        }

        writeJSONResponse(dict);

    }

    private string queryExcel(string excelFile)
    {
        string result = "";
        var excel = new ExcelQueryFactory(excelFile);
        var sheet = excel.Worksheet(0);
        var rows = from c in sheet select c;

        var areas = dc.TS_ZD_Info.Where(c => c.type == "区域");
        var bussiness_types = dc.TS_ZD_Info.Where(c => c.type == "IP业务类型");

        Dictionary<int, int> dict = new Dictionary<int, int>();
        dict.Add(23, 512);
        dict.Add(24, 256);
        dict.Add(25, 128);
        dict.Add(26, 64);
        dict.Add(27, 32);
        dict.Add(28, 16);
        dict.Add(29, 8);
        dict.Add(30, 4);
        dict.Add(31, 2);
        dict.Add(32, 1);

        foreach (var row in rows)
        {
            string ip_info = row["IP地址"];

            var match = dc.vw_ip_source_master.Where(c => c.IP_FullAddress == ip_info);

            if (match.Count() > 0)
            {
                result += "<br/>" + ip_info + ","; 
                continue;
            }

            string[] ip_address = ip_info.Split('/');
            string ip_num = ip_address[1];
            string ip_start_no = ip_address[0].Split('.')[3];
            string ip_end_no = dict[int.Parse(ip_num)].ToString();
            string[] ip_head_array = ip_address[0].Split('.');
            string ip_head = "";
            for (int i = 0; i < 3; i++)
            {
                ip_head = ip_head+ip_head_array[i] + ".";
            }

            string machine_room_id = row["所属机房"];
            var room = dc.MachineRoom.Where(c => c.mac_id == machine_room_id).SingleOrDefault();

            string area_name = row["所属区域"];

            var area = areas.Where(c => c.mc == area_name).SingleOrDefault();
            string area_lsh = area.lsh;

            var type = bussiness_types.Where(c => c.mc == row["IP业务类型"]).SingleOrDefault();
            string type_lsh = type.lsh;

            var ip = new IP_Source_Master
            {
                IP_Head = ip_head,
                IP_Start_No = int.Parse(ip_start_no),
                IP_End_No = int.Parse(ip_end_no),
                IP_Num = int.Parse(ip_num),
                createDate = DateTime.Now.ToShortDateString(),
                State = 0,
                MachineID = machine_room_id,
                Used = type_lsh,
                Ssqy = area_lsh,
                bz = "import by ip excel files",
            };
            dc.IP_Source_Master.InsertOnSubmit(ip);
            dc.SubmitChanges();

            int ip_numbers = (int)ip.IP_End_No - (int)ip.IP_Start_No + 1;

            int loop = ip_numbers / 256;

            for (int i = 0; i < loop; i++)
            {
                var free = new IP_Free_Info
                {
                    IPMID = ip.IPMID,
                };
                string head =String.Format("{0}.{1}.{2}.",ip_head_array[0],ip_head_array[1],int.Parse(ip_head_array[2])+i);
                free.IP_Head = head;
                int start_no = (i>0) ? 0 : int.Parse(ip_start_no);
                free.IP_Start_No = start_no;
                
                int end_num = Math.Min(start_no+(ip_numbers-256*i),255);
                                
                int free_num = end_num-start_no+1;

                free.IP_End_No = end_num;
                free.IP_FreeNum = free_num;

                dc.IP_Free_Info.InsertOnSubmit(free);
            }
            dc.SubmitChanges();    
        }

        if (result.Length > 0)
        {
            result = String.Format(@"导入excel中的数据‘{0}’已存在", result); 
        }

        return result;

    }

    [WebMethod]
    public void get_sql_area()
    {
        var area_list = dc.TS_ZD_Info.Where(c => c.type == "区域").OrderBy(c=>c.xh);
        writeJSONResponse(area_list);
    }

    [WebMethod]
    public void get_sql_resource_list()
    {
        var resource_list = dc.IP_Bussiness.OrderBy(c => c.Bussiness_code).Take(100);
        writeJSONResponse(resource_list);
    }

    [WebMethod]
    public void get_cmts_list(string device_code, string belong_to)
    {
        var list = dc.CMTS.OrderBy(c=>c.id);
        if (device_code.Trim().Length > 0)
        {
            list = list.Where(c => c.device_code.Contains(device_code)).OrderBy(c => c.id);
        }

        if (belong_to.Trim().Length > 0)
        {
            list = list.Where(c => c.belong_to.Contains(belong_to)
                ).OrderBy(c => c.id);
        }
        writeJSONResponse(list);
    }

    [WebMethod]
    public void get_cmts_detail(string id)
    {
        var entity = dc.CMTS.Where(c => c.id == Convert.ToInt32(id)).FirstOrDefault();
        if (entity != null)
        {
            writeJSONResponse(entity);
        }
        else
        {
            writeJSONResponse(null);
        }

    }

    [WebMethod]
    public void edit_cmts(string id,string device_code, string belong_to, string room_id)
    {
        var e = dc.CMTS.Where(c => c.id == Convert.ToInt32(id)).FirstOrDefault();
        if (e != null)
        {
            e.device_code = device_code;
            e.belong_to = belong_to;
            e.room_id = Convert.ToInt32(room_id);
            dc.SubmitChanges();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("result", "0");
            writeJSONResponse(dict);
        }
        else
        { 
            Dictionary<string, string> dict = new Dictionary<string,string>();
            dict.Add("result","-1");
            writeJSONResponse(dict);
        }
        
    }

    [WebMethod]
    public void delete_cmts_list(string id_list)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        try {
            List<string> cmts_id_list = id_list.Split(',').ToList();

            foreach (string id in cmts_id_list)
            {
                int pk = Convert.ToInt32(id);
                var e = dc.CMTS.Where(c => c.id == pk).FirstOrDefault();
                dc.CMTS.DeleteOnSubmit(e);
            }
            dc.SubmitChanges();

            dict.Add("result", "0");

        }
        catch (Exception ex)
        {
            dict.Add("result", "-1");
            dict.Add("msg", ex.Message);
        }

        writeJSONResponse(dict);
    }

    [WebMethod]
    public void delete_cmts(string id)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        try {
            var e = dc.CMTS.Where(c => c.id == Convert.ToInt32(id)).FirstOrDefault();
            dc.CMTS.DeleteOnSubmit(e);
            dc.SubmitChanges();
            dict.Add("result", "0");

        }
        catch (Exception ex)
        {
            dict.Add("result", "-1");
            dict.Add("msg", ex.Message);
        }
        writeJSONResponse(dict);
    }

    [WebMethod]
    public void get_config_list()
    { 
        var list =  dc.IP_Bussiness.Where(c=>c.Bussiness_code.StartsWith("Z_P"));
        writeJSONResponse(list.Take(100));
    }

    [WebMethod]
    public void get_config_detail(string id)
    {
        var e = dc.IP_Bussiness.Where(c => c.ID == Convert.ToInt32( id)).FirstOrDefault();
        writeJSONResponse(e);

    }

    [WebMethod]
    public void get_cmts_by_config_id(string config_id)
    {
        var entities = dc.CMTS.Where(c => c.bussiness_id == Convert.ToInt32(config_id)).OrderBy(c => c.id);
        writeJSONResponse(entities);
    }

    [WebMethod]
    public void add_cmts(string device_code, string belong_to, string room_id)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        try
        {
            var cmts = new CMTS
            {
                device_code = device_code,
                belong_to = belong_to,
                room_id = Convert.ToInt32(room_id)
            };
            dc.CMTS.InsertOnSubmit(cmts);
            dc.SubmitChanges();
            dict.Add("result", "0");
        }
        catch (Exception ex)
        {
            dict.Add("result", "-1");
            dict.Add("msg", ex.Message);

        }

        writeJSONResponse(dict);
        



    }

    [WebMethod]
    public void get_sql_client_lv()
    {

        var client_lv_list = dc.TS_ZD_Info.Where(c => c.type == "客户级别").OrderBy(c => c.xh);
        writeJSONResponse(client_lv_list);
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

    private string makeTimeString() {
        return DateTime.Now.ToString("yyyyMMddHHmmssffff");
    }

    private string selectExcel(string filename) 
    {
        string result = "";
        var excel = new ExcelQueryFactory(filename);
        var sheet = excel.Worksheet(0);
        var query = from d in sheet
                    select d;

        //DataClassesDataContext dc = new DataClassesDataContext();

        var PPPOEBussiness = from a in dc.IP_Bussiness
                                where a.Bussiness_code.StartsWith("Z_P_") orderby a.Bussiness_code descending
                                select a;
        long count = 0;
        if (PPPOEBussiness.Count() != 0)
        {
            count = long.Parse(PPPOEBussiness.First().Bussiness_code.Replace("Z_P_", ""));
        
        }
        foreach (var item in query)
        {
            if (item["设备配置信息"].ToString().Length == 0)
            {
                break;
            }
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

                DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "yyyy/MM/dd";
                DateTime dt = Convert.ToDateTime(configDate, dtFormat);
                configDate = String.Format("{0:D4}-{1:D2}-{2:D2}",dt.Year,dt.Month,dt.Day);
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

            string deviceCode = deviceInfoArray[0];

            var masterID = (from c in dc.AssemblyPortSource_Master
                     where c.sbbh == deviceCode
                     select c).FirstOrDefault().ID;

            string accessPort = (deviceInfoArray.Length>2) ? deviceInfoArray[2] :"";
            string accessPortID = "";

            if (accessPort.Length > 0)
            {
                var port_query = (from c in dc.AssemblyPortSource_Details
                                  where c.port == accessPort && c.MID == masterID
                                  select c).FirstOrDefault();

                accessPortID = port_query.ID.ToString();

               

            }
            

            var port = from p in dc.IP_Bussiness
                       where p.sbpzxx.StartsWith(devicePort)
                       select p;

            if (port.Count() > 0)
            {
                result += "<br/>"+port.FirstOrDefault().sbpzxx+",";
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
                jrdk           = accessPort,
                Sbdkhid        = accessPortID,
                
            };
            dc.IP_Bussiness.InsertOnSubmit(bussiness);

            var client = new ts_kh
            {
                ywbm = bussiness.Bussiness_code,
                khmc = bussiness.khmc,
                khdz = bussiness.khdz,
                khlxr = item["用户负责人"],
                lxdh = item["用户电话"],
                ydsj = item["用户手机"],
            };
            dc.ts_kh.InsertOnSubmit(client);

            dc.SubmitChanges();

            var ports = deviceInfoArray.Where((value, index) => index > 2).ToArray();
            //update onu_dk
            foreach(string p in ports)
            //for (int i=0;i<4;i++)
            {
                var onu_port_query = (from c in dc.ONU_Dk
                                      where c.Gldkid.ToString() == accessPortID && c.Dkh == p
                                      select c).FirstOrDefault();
                
                //set port state used
                onu_port_query.Dkzt = 1;
                //insert new record in ywonu_gbl
                var instance = new YwONU_glb
                {
                    Lsh = makeTimeString(),
                    Ywid = bussiness.ID,
                    ONULsh = onu_port_query.Lsh,
                    type = 0,
                };
                dc.YwONU_glb.InsertOnSubmit(instance);
                //dc.SubmitChanges();
            }

            var vlan = new Vlan_Distribute
            {
                VlanID = item["VLAN"],
                Ywid = bussiness.ID,
                RelField = "bdjfvlan",
                jfID = machineRoomId
            };
            dc.Vlan_Distribute.InsertOnSubmit(vlan);
            dc.SubmitChanges();

            
        }

        var prefix = "Z_P_";
        var pppoeQuery = dc.IP_Bussiness.Where(c => c.Bussiness_code.StartsWith(prefix)).OrderByDescending(c=>c.Bussiness_code).First();
        var totalCount = pppoeQuery.Bussiness_code.Replace(prefix,"");
        var z = dc.zcbh_lsh.Where(c => c.Prefix == prefix).SingleOrDefault();
        z.dqgzbh = totalCount;
        dc.SubmitChanges();

        if (result.Length > 0)
        {
            result = String.Format(@"导入excel中的数据与数据库中设备配置信息为‘{0}’的资料端口冲突", result); 
                
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
    /*
    [WebMethod]
    public void get_announcement_v2(string uid, string page)
    {
        if (page.Length == 0)
        {
            page = "1";
        }
        //HSYWDataContext ctx = new HSYWDataContext();
        var query = from c in ctx.ANNOUNCEMENTs
                    where c.POSTOWNER.Contains(uid)
                    select c;
        query = query.Skip((int.Parse(page) - 1) * 100).Take(100);

        //var q = ctx.Announcements.Where(c => c.AnnouncementRecord = );

        //q = q.Skip((int.Parse(page) - 1) * 100).Take(100);

        //writeJSONResponse(q);
    }
    */
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
        var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
        string jsonData = JsonConvert.SerializeObject(o, Formatting.Indented,serializerSettings);
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
    /*
    [WebMethod]
    public void getUserNameByAnnounceId(int announceId)
    {
        
        var a = ctx.ANNOUNCEMENTs.Where(c => c.ID == announceId).FirstOrDefault();
        var strUserIds = a.POSTOWNER;
        var userIdList = strUserIds.Split(',');
        var branchQuery = ctx.TSYSBRANCHES.ToList();
        List<string> pathList = new List<string>();
        foreach (var id in userIdList)
        {
            var user = ctx.TSYSUSERs.Where(c => c.ID == id).FirstOrDefault();
            var departmentQuery = ctx.TSYSBRANCHES.Where(c => c.BRANCHCODE == user.BRANCHCODE).FirstOrDefault();
            string strDepartmentName = "";
            getDepartmentPath(ref strDepartmentName, departmentQuery.BRANCHCODE, branchQuery);
            
            string path = String.Format("{0}/{1}", strDepartmentName, user.USERREALNAME);
            pathList.Add(path);
            
        }

        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("result", string.Join(",",pathList.ToArray()));
        writeJSONResponse(dict);
    }
    
    private void getDepartmentPath(ref string path,string code,List<TSYSBRANCH> list)
    {
        var q = list.Where(c => c.BRANCHCODE == code).FirstOrDefault();
        if (q.PATH == null)
        {
            if (path.Length == 0)
            {
                path = q.BRANCHNAME;
            }
            else
            {
                path = String.Format("{0}/{1}", q.BRANCHNAME, path);
            }
            getDepartmentPath(ref path, q.PBRANCHCODE,list);
        }
        else
        {
            if (path.Length == 0)
            {
                path = q.PATH;
            }
            else
            {
                path = String.Format("{0}/{1}", q.PATH, path);
            }
            
        }
    }
    */
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


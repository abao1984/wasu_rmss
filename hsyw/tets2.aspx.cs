using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class tets2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strSql = "select t.* from odbc_import1 t where t.ip is not null";
        DataSet ds = DataFunction.FillDataSet(strSql);
        strSql = "select t.* from t_logic_equ_ip_pz t";
        DataSet pzds = DataFunction.FillDataSet(strSql);
        
        string zwym = "", ipdz1 = "", ipdz2 = "";
        
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            string [] ipList = dr["ip"].ToString().Split(',');
            foreach(string ip in ipList)
            {
                string sql = "select * from t_logic_equ_ip_pz where ipdz='" + ip + "'";
                if (ip==""||DataFunction.HasRecord(sql))
                {
                    continue;
                }
                
                string ipfd = ip.Split('/')[1];
                string[] ips = ip.Split('/')[0].Split('.');
                string ip1 = ips[0], ip2 = ips[1], ip3 = ips[2], ip4 = ips[3];
                GetIpdz(ip1, ip2, ip3, ip4, ipfd, ref zwym, ref ipdz1, ref ipdz2);
               
                
                DataRow dataRow = pzds.Tables[0].NewRow();
                dataRow["guid"] = "1220"+Guid.NewGuid().ToString("N");
                dataRow["PK_GUID"] = dr["guid"];
                dataRow["ip1"] = ip1;
                dataRow["ip2"] = ip2;
                dataRow["ip3"] = ip3;
                dataRow["ip4"] = ip4;
                dataRow["ipfd"] = ipfd;
                dataRow["ipdz"] = ip;
                dataRow["ipdz1"] = ipdz1;
                dataRow["ipdz2"] = ipdz2;
                dataRow["PZ_IPYWLX"] = dr["IP业务类型"];
                dataRow["SFHS"] = 0;
                pzds.Tables[0].Rows.Add(dataRow);
                //ipguids += dataRow["guid"].ToString() + ",";
            }
        }
        DataFunction.SaveData(pzds, "t_logic_equ_ip_pz");
        //foreach (DataRow dataRow in pzds.Tables[0].Rows)
        //{
        //    GetIPZDS(dataRow["guid"].ToString());
        //}
    }

    private void GetIPZDS(string guid)
    {
        string sql = "select IPDZ2 - IPDZ1 from t_logic_equ_ip where guid='" + guid + "'";
        int zdzs = DataFunction.GetIntResult(sql);
        sql = "select sum(IPDZ2 - IPDZ1) from t_logic_equ_ip where p_guid='" + guid + "'";
        int dzs = DataFunction.GetIntResult(sql);
        if (dzs == 0)
        {
            sql = string.Format(@"select sum(b.ipdz2-b.ipdz1) from t_logic_equ_ip  a, t_logic_equ_ip_pz  b,t_con_logic_equ_ip p,RMSS r
where a.ipdz1 <= b.ipdz1 and a.ipdz2 >= b.ipdz2  and b.pk_guid = p.guid and p.Subscriber_ID = r.SUBSCRIBER_ID
 and a.guid = '{0}'and (b.SFHS <> '1' or b.SFHS is null)", guid);
            dzs = DataFunction.GetIntResult(sql);
        }

        int kydzs = zdzs - dzs;
        string fpzt = "部分分配";
        if (kydzs == 0)
        {
            fpzt = "已分配";
        }
        else if (zdzs == kydzs)
        {
            fpzt = "未分配";
        }
        sql = string.Format("update t_logic_equ_ip set KYDZS={0},IPFPZT='{1}' where guid='{2}'", kydzs, fpzt, guid);
        DataFunction.ExecuteNonQuery(sql);
        
    }

    private void GetPGuid(string ip1, string ip2, string ip3,  ref string p_guid,string ywlx)
    {
        
        string ip = "";
        if (ip1 == "10" && ywlx == "HDTV")
        {
            ip = ip1 + "." + ip2 + "." + ip3 + ".0/23";
        }
        else if (ip1 == "10" && ywlx != "HDTV")
        {
            ip = "10.0.0.0/8";
        }
        else
        {
            ip = ip1 + "." + ip2 + "." + ip3 + ".0/24";
        }
        string strSql = "select * from t_logic_equ_ip t where t.ip like '" + ip + "'";
        p_guid = DataFunction.GetStringResult(strSql);
    }

   

    private void GetIpdz(string ip1,string ip2,string ip3,string ip4,string fd,ref string zwym,ref string ipdz1,ref string ipdz2)
    {

        Int64 ipfd = Convert.ToInt64(fd);
        Int64 ipdz11 = Convert.ToInt64(ip1), ipdz12 = 0, ipdz13 = 0, ipdz14 = 0;
        Int64 ipdz21 = ipdz11, ipdz22 = 0, ipdz23 = 0, ipdz24 = 0;
        Int64 zwip1 = 255, zwip2 = 0, zwip3 = 0, zwip4 = 0;
        if (ipfd > 16)
        {
            zwip2 = 255;
            ipdz12 = Convert.ToInt64(ip2);
            ipdz22 = ipdz12;
            if (ipfd > 24)
            {
                zwip3 = 255;
                ipdz13 = Convert.ToInt64(ip3);
                ipdz23 = ipdz13;
                getIp(ref ipdz14, ref ipdz24, ref zwip4, Convert.ToInt64(ip4), 32 - ipfd);
            }
            else
            {
                getIp(ref ipdz13, ref ipdz23, ref zwip3, Convert.ToInt64(ip3), 24 - ipfd);
            }
        }
        else if (ipfd > 8)
        {
            getIp(ref ipdz12, ref ipdz22, ref zwip2, Convert.ToInt64(ip2), 16 - ipfd);
        }
        zwym = zwip1 + "." + zwip2 + "." + zwip3 + "." + zwip4;
        ipdz1 = Convert.ToString(Convert.ToInt64(ipdz11) * 256 * 256 * 256 + ipdz12 * 256 * 256 + ipdz13 * 256 + ipdz14);
        ipdz2 = Convert.ToString(Convert.ToInt64(ipdz21) * 256 * 256 * 256 + ipdz22 * 256 * 256 + ipdz23 * 256 + ipdz24);
    }

    private void getIp(ref Int64 ips1, ref Int64 ips2, ref Int64 zwip, Int64 ipdz, Int64 ipfd)
    {
        Int64[] ips = new Int64[8];
        ips[0] = ipdz % 2;
        ips[1] = ipdz / 2 % 2;
        ips[2] = ipdz / 4 % 2;
        ips[3] = ipdz / 8 % 2;
        ips[4] = ipdz / 16 % 2;
        ips[5] = ipdz / 32 % 2;
        ips[6] = ipdz / 64 % 2;
        ips[7] = ipdz / 128 % 2;
        Int64 ip = 0;
        for (Int64 i = 7; i >= ipfd; i--)
        {
            ip += ips[i] * Convert.ToInt64(Math.Pow(2, i));
            zwip += Convert.ToInt64(Math.Pow(2, i));
        }
        ips1 = ip;
        ips2 = ip + Convert.ToInt64(Math.Pow(2, ipfd));
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string strSql = "select t.* from odbc_import1 t where t.vlan is not null";
        DataSet ds = DataFunction.FillDataSet(strSql);
        strSql = "select t.* from t_con_logic_equ_vlan t";
        DataSet dataSet = DataFunction.FillDataSet(strSql);
        strSql=@"select guid from t_logic_equ_vlan t where t.vlanbh='{0}' and t.ssjf_code='{1}'";
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            string [] VlanList = dr["vlan"].ToString().Split(',');
            foreach (string vlan in VlanList)
            {
                if(vlan=="")
                {
                    continue;
                }
                DataRow dataRow = dataSet.Tables[0].NewRow();
                //dataRow = dataSet.Tables[0].NewRow();
                dataRow["GUID"] = "1220" + Guid.NewGuid().ToString("N");
                dataRow["PK_GUID"] = dr["guid"];
                dataRow["sfhs"] = 0;
                dataRow["VLANBH"] = vlan;
                dataRow["VLANGUID"] = DataFunction.GetStringResult(string.Format(strSql, vlan, dr["机房"].ToString()));
                dataSet.Tables[0].Rows.Add(dataRow);
            }
        }
        DataFunction.SaveData(dataSet, "t_con_logic_equ_vlan");
    }


}

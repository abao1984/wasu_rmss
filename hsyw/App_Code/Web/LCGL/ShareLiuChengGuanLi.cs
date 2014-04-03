using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///ShareLiuChengGuanLi 的摘要说明
/// </summary>
public class ShareLiuChengGuanLi
{
	public ShareLiuChengGuanLi()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//

	}

    public void SaveFirstLczt(string lc_guid,string lcbm,string lczt)
    {
        string sql = string.Format("select t.* from t_lcgl_sys_lcjl t where t.lc_guid='{0}' and  lcjrzt='{1}'",
           lc_guid, lczt);
        DataSet ds = DataFunction.FillDataSet(sql);    
        if (ds.Tables[0].Rows.Count == 0)
        {       
            DataRow dr = ds.Tables[0].NewRow();
            dr["GUID"] = Guid.NewGuid().ToString();
            dr["LC_GUID"] = lc_guid;
            dr["LCJRSJ"] = DateTime.Now;
            dr["LCBM"] = lcbm;
            dr["LCJRZT"] = lczt;
            dr["SFQF"] = 0;
            ds.Tables[0].Rows.Add(dr);
        }       
        DataFunction.SaveData(ds, "T_LCGL_SYS_LCJL");
    }

    public string GetSqdbh(string tableName)
    {
        string ny = DateTime.Now.ToString("yyyyMM");
        string sql = string.Format(@"select '{1}'||replace(to_char(nvl(substr(max(t.sqdbh),7,4),0)+1,'0000'),' ','') as sqdbh
         from {0} t where t.sqdbh like '{1}%'", tableName, ny);
        return DataFunction.GetStringResult(sql);
    }

    public string GetPcode(string lcbm,string jdbm)
    {
        string sql = string.Format("select t.pcode from t_lcgl_sys_lckz_cb t where t.lcbm='{0}' and t.jdbm='{1}'",lcbm,jdbm);
        return DataFunction.GetStringResult(sql);
    }
    public string GetHeadTitle(string lcbm,string jdbm)
    {
        string sql = string.Format(@"select a.lcmc||'---'||b.jdmc as mc  from t_lcgl_sys_lckz_zb a,t_lcgl_sys_lckz_cb b where a.lcbm=b.lcbm and a.lcbm='{0}' and b.jdbm='{1}'",
          lcbm,jdbm  );
        return DataFunction.GetStringResult(sql);
    }
}

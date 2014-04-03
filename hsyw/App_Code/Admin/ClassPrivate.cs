using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///ClassPrivate 的摘要说明
/// </summary>
public class ClassPrivate
{
	public ClassPrivate()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public DataSet GetPrivateByMenuCode(string menucode)
    {
        string sql = string.Format("select * from T_SYS_PRIVATE where MENUCODE = '{0}'",menucode);
        return DataFunction.FillDataSet(sql);
    }
    public DataSet GetPrivateByPCODE(string pcode)
    {
        string sql = string.Format("select * from T_SYS_PRIVATE where PCODE = '{0}'", pcode);
        return DataFunction.FillDataSet(sql);
    }
     public string GetPCODE(string menucode)
    {
        string pcode = DataFunction.GetStringResult(string.Format("select pcode from T_SYS_PRIVATE where MENUCODE = '{0}' order by PCODE desc",menucode));
        int len = pcode.Length;
        if (len == 0)
        {
            pcode = menucode + "01";
        }
        else
        {
            int hz = Convert.ToInt32(pcode.Substring(len - 2, 2))+1;
            if (hz < 10)
            {
                pcode = menucode + "0" + hz;
            }
            else {
                pcode = menucode + hz;
            }
        }
        return pcode;
    }
     public string GetMenuName(string menucode)
     {
         return DataFunction.GetStringResult(string.Format("select MENUNAME from T_SYS_MENU where MENUCODE = '{0}'", menucode));
     }
}

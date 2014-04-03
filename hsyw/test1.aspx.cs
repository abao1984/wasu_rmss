using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class test1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //板卡
        string strSql = @"select t.* from a_temp_t_res_child_board1 t where dkxh is not null";
        DataSet ds = DataFunction.FillDataSet(strSql);
        strSql = "select t.* from t_res_child_port t where 1=2";
        DataSet dsPort = DataFunction.FillDataSet(strSql);
        foreach(DataRow dr in ds.Tables[0].Rows)
        {
            //DataRow drPort = dsPort.Tables[0].NewRow();
            string dkxh = dr["dkxh"].ToString();
            string dklx = dr["dklx"].ToString();
            if (dkxh.IndexOf('/') > -1)
            {
                string[] dkxhs = dkxh.Split('/');
                string[] dklxs = dklx.Split('-');
                for (int i = 0; i < dkxhs.Length; i++)
                {
                    string xh = dkxhs[i];
                    string lx = dklxs[i];
                    insertData1(dr, dsPort, xh, lx);
                }
            }
            else
            {
                insertData1(dr, dsPort, dkxh, dklx);
            }
        }
    }

    private void insertData1(DataRow dr, DataSet dsPort, string xh, string lx)
    {
        DataRow drPort;
        if (xh.IndexOf('-') > -1)
        {
            string[] xhs = xh.Split('-');
            int start=Convert.ToInt32(xhs[0]);
            int end=Convert.ToInt32(xhs[1]);
            for (int i = start; i <= end; i++)
            {
                drPort = dsPort.Tables[0].NewRow();
                string equ_name = dr["BOARD_NAME"].ToString();
                string dkbm = equ_name.Substring(equ_name.IndexOf("槽") - 1, 1);
                drPort["guid"] = Guid.NewGuid().ToString();
                drPort["CREATEDATETIME"] = Convert.ToDateTime("2012-02-09");
                drPort["EQU_NAME_GUID"] = dr["GUID"];
                drPort["EQU_NAME"] = equ_name;
                drPort["PORT_NUM"] = i;
                drPort["DKLX"] = lx;
                drPort["SFKFY"] = 0;
                drPort["DKZT"] = "启用";
                drPort["DKFL"] = "网络设备端口";
                drPort["PORT_NAME"] = dr["BOARD_NAME"] + "_" + i;
                if (lx == "1000Base")
                {
                    drPort["DKBM"] = "S0-S" + dkbm + "-G" + i;
                    drPort["DKLX_SHORT"] = "G";
                }
                else
                {
                    drPort["DKBM"] = "S0-S" + dkbm + "-P" + i;
                    drPort["DKLX_SHORT"] = "P";
                }
                dsPort.Tables[0].Rows.Add(drPort);
            }
        }
        else
        {
            drPort = dsPort.Tables[0].NewRow();
            string equ_name = dr["BOARD_NAME"].ToString();
            string dkbm = equ_name.Substring(equ_name.IndexOf('-'), 1);
            drPort["guid"] = Guid.NewGuid().ToString();
            drPort["CREATEDATETIME"] = Convert.ToDateTime("2012-02-09");
            drPort["EQU_NAME_GUID"] = dr["GUID"];
            drPort["EQU_NAME"] = equ_name;
            drPort["PORT_NUM"] = xh;
            drPort["DKLX"] = lx;
            drPort["SFKFY"] = 0;
            drPort["DKZT"] = "启用";
            drPort["DKFL"] = "网络设备端口";
            drPort["PORT_NAME"] = dr["BOARD_NAME"] + "_" + xh;
            if (lx == "1000Base")
            {
                drPort["DKBM"] = "S0-S" + dkbm + "-G" + xh;
                drPort["DKLX_SHORT"] = "G";
            }
            else
            {
                drPort["DKBM"] = "S0-S" + dkbm + "-P" + xh;
                drPort["DKLX_SHORT"] = "P";
            }
            dsPort.Tables[0].Rows.Add(drPort);
        }
        DataFunction.SaveData(dsPort, "t_res_child_port");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //设备
        string strSql = @"select * from a_temp_t_res_equ_net1 t where dkxh is not null";
        DataSet ds = DataFunction.FillDataSet(strSql);
        strSql = "select t.* from t_res_child_port t where 1=2";
        DataSet dsPort = DataFunction.FillDataSet(strSql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            //DataRow drPort = dsPort.Tables[0].NewRow();
            string dkxh = dr["dkxh"].ToString();
            string dklx = dr["dklx"].ToString();
            if (dkxh.IndexOf('/') > -1)
            {
                string[] dkxhs = dkxh.Split('/');
                string[] dklxs = dklx.Split('-');
                for (int i = 0; i < dkxhs.Length; i++)
                {
                    string xh = dkxhs[i];
                    string lx = dklxs[i];
                    insertData2(dr, dsPort, xh, lx);
                }
            }
            else
            {
                insertData2(dr, dsPort, dkxh, dklx);
            }
        }
    }

    private void insertData2(DataRow dr, DataSet dsPort, string xh, string lx)
    {
        DataRow drPort;
        if (xh.IndexOf('-') > -1)
        {
            string[] xhs = xh.Split('-');
            int start = Convert.ToInt32(xhs[0]);
            int end = Convert.ToInt32(xhs[1]);
            for (int i = start; i <= end; i++)
            {
                drPort = dsPort.Tables[0].NewRow();
                //string equ_name = dr["BOARD_NAME"].ToString();
                //string dkbm = equ_name.Substring(equ_name.IndexOf("槽") - 1, 1);
                drPort["guid"] = Guid.NewGuid().ToString();
                drPort["CREATEDATETIME"] = Convert.ToDateTime("2012-02-09");
                drPort["EQU_NAME_GUID"] = dr["GUID"];
                drPort["EQU_NAME"] = dr["EQU_NAME"];
                drPort["PORT_NUM"] = i;
                drPort["DKLX"] = lx;
                drPort["SFKFY"] = 0;
                drPort["DKZT"] = "启用";
                drPort["DKFL"] = "网络设备端口";
                drPort["PORT_NAME"] = dr["EQU_NAME"] + "_" + i;
                if (lx == "1000Base")
                {
                    drPort["DKBM"] = "G" + i;
                    drPort["DKLX_SHORT"] = "G";
                }
                else
                {
                    drPort["DKBM"] = "P" + i;
                    drPort["DKLX_SHORT"] = "P";
                }
                dsPort.Tables[0].Rows.Add(drPort);
            }
        }
        else
        {
            drPort = dsPort.Tables[0].NewRow();
            //string equ_name = dr["BOARD_NAME"].ToString();
            //string dkbm = equ_name.Substring(equ_name.IndexOf('-'), 1);
            drPort["guid"] = Guid.NewGuid().ToString();
            drPort["CREATEDATETIME"] = Convert.ToDateTime("2012-02-09");
            drPort["EQU_NAME_GUID"] = dr["GUID"];
            drPort["EQU_NAME"] = dr["EQU_NAME"];
            drPort["PORT_NUM"] = xh;
            drPort["DKLX"] = lx;
            drPort["SFKFY"] = 0;
            drPort["DKZT"] = "启用";
            drPort["DKFL"] = "网络设备端口";
            drPort["PORT_NAME"] = dr["BOARD_NAME"] + "_" + xh;
            if (lx == "1000Base")
            {
                drPort["DKBM"] = "G" + xh;
                drPort["DKLX_SHORT"] = "G";
            }
            else
            {
                drPort["DKBM"] = "P" + xh;
                drPort["DKLX_SHORT"] = "P";
            }
            dsPort.Tables[0].Rows.Add(drPort);
        }
        DataFunction.SaveData(dsPort, "t_res_child_port");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmssSync
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime time_now = DateTime.Now;
            string drop_table = @"if object_id('dbo.ClientTypeA','U') is not null
                drop table dbo.ClientTypeA";
            string create_table = @"if not exists (select * from sysobjects where name='ClientTypeA' and xtype='U')
create table ClientTypeA(
    [SUBSCRIBERNO] [varchar](256) COLLATE Chinese_PRC_CI_AS NULL,
	[DESCRIPTION] [varchar](256) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_NO] [varchar](63) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTTYPE] [varchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[LINKMAN] [varchar](63) COLLATE Chinese_PRC_CI_AS NULL,
	[EMAIL] [varchar](30) COLLATE Chinese_PRC_CI_AS NULL,
	[MOBILE_NO] [varchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[PHONE_NO] [varchar](129) COLLATE Chinese_PRC_CI_AS NULL,
	[FAX_NO] [varchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[ZIP_CODE] [varchar](6) COLLATE Chinese_PRC_CI_AS NULL,
	[ADDRESS] [varchar](704) COLLATE Chinese_PRC_CI_AS NULL,
	[TYPE] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[REMARK] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[CUSTOMER_LEVEL] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[SALE_NAME] [varchar](64) COLLATE Chinese_PRC_CI_AS NULL
) on [primary]";
            string import_table = @"insert into ClientTypeA
select * from openrowset('OraOLEDB.Oracle','REPORTDB';'rmsssync';'rmss3#2014',
'select SUBSCRIBERNO,DESCRIPTION,CUSTOMER_NO,CUSTTYPE,LINKMAN,EMAIL,MOBILE_NO,PHONE_NO,FAX_NO,ZIP_CODE,ADDRESS,TYPE,REMARK,TO_CHAR(CUSTOMER_LEVEL),SALE_NAME from rmss')";
            string get_result_count = @"select count(*) from ClientTypeA";
            DataClassDataContext db = new DataClassDataContext();
            db.CommandTimeout = 60 * 10;
            int dropOp = db.ExecuteCommand(drop_table);
            /*if (dropOp != 0)
            {
                Console.WriteLine("error on drop table");
                return;
            }*/
            int createOp = db.ExecuteCommand(create_table);
            /*if (createOp != 0)
            {
                Console.WriteLine("error on create table");
                return;
            }*/
            int importOp = db.ExecuteCommand(import_table);
            /*if (importOp != 0)
            {
                Console.WriteLine("error on import data");
                return;
            }*/
            int result = db.ExecuteQuery<Int32>(get_result_count).SingleOrDefault();
            Console.WriteLine("total record " + result.ToString());

            TimeSpan ts = DateTime.Now - time_now;

            Console.WriteLine(String.Format("total use time {0:c}", ts.ToString("c")));
            Console.ReadLine();

        }
    }
}

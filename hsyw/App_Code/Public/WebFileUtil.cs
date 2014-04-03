using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;

/// <summary>
///WebFileUtil 的摘要说明
/// </summary>
public class WebFileUtil
{
		public WebFileUtil()
	    {   
		}
		public static HttpServerUtility Server
		{
			get
			{
				return  System.Web.HttpContext.Current.Server;
			}
		}

		public static void ViewUrl(string url)
		{
			System.Web.HttpContext.Current.Response.Write("<script>window.open('" + url + "');</script>");
		}
		public static void DownloadUrl(string url)
		{
			FileDownload(Server.MapPath(url));
		}
		/// <summary>
		/// 直接出线保存下载对话框，不在浏览器打开
		/// </summary>
		/// <param name="strDownloadFile"></param>
		public static void FileDownload(string filepath)
		{
			string strFileName = Server.UrlEncode(FileUtil.GetFileName(filepath));            
			System.Web.HttpContext.Current.Response.Clear();
			System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
			System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
			//System.Web.HttpContext.Current.Response.ContentEncoding=System.Text.Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.Flush();
			System.Web.HttpContext.Current.Response.WriteFile(filepath, false); //-- 下载文件
			System.Web.HttpContext.Current.Response.End(); //--此句犹为重要，否则会把本页的HTML源码写在文件里
		} 
		public static void UrlDownload(string url)
		{
			FileDownload(ConvertUrl2Path(url));
		} 
		
		public static string ConvertUrl2Path(string url)
		{
			return Server.MapPath(url);
		}

		public static void MoveFile(string sourceUrl,string targetUrl)
		{
			HttpServerUtility util = Server;
			string sourcePath = util.MapPath(sourceUrl);
			string targetPath = util.MapPath(targetUrl);
			FileUtil.MoveFile(sourcePath,targetPath);
		}
		public static void CopyFile(string sourceUrl,string targetUrl)
		{
			HttpServerUtility util = Server;
			string sourcePath = util.MapPath(sourceUrl);
			string targetPath = util.MapPath(targetUrl);
			FileUtil.CopyFile(sourcePath,targetPath);
		} 
		public static void DeleteUrl(string fileUrl)
		{
			HttpServerUtility util =  System.Web.HttpContext.Current.Server;
			File.Delete(util.MapPath(fileUrl));		
		}
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="FileSort">文件分类</param>
        /// <param name="IsUseFull">是否启用：ALL为所有文档，1为有用文档，0为归档文档。</param>
        /// <returns></returns>
        public static DataSet GetFileList(string FileSort,string IsUseFull)
        {
            string sql = string.Format("select t.* from T_FAU_FILELIST t where t.FILESORT='{0}'", FileSort);
            if (IsUseFull != "ALL")
            {
                sql +=" and t.ISUSEFULL='" + IsUseFull + "'";
            }
            sql += " order by t.UPLOADTIME desc";
            return DataFunction.FillDataSet(sql);
        }
        
        private static double GetAllFileSize()
        {
            string sql = string.Format("select sum(FILESIZE) from T_FAU_FILELIST");
            string size = DataFunction.GetStringResult(sql);
            if (size == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(size);
            }
        }
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="InputFile">文件控件</param>
        /// <param name="FileSort">文件分类</param>
        /// <param name="UserName">上传人</param>
        /// <returns></returns>
        public static bool FileUpLoad(HtmlInputFile InputFile,string FileSort,string UserName)
        {
                classData cdata = new classData();
                double maxSize = Convert.ToDouble(cdata.GetZDZ("FJSC", "MAXSIZE")) * 1024 * 1024;

                if (GetAllFileSize() < maxSize)
                {
                    string str_path = cdata.GetZDZ("FJSC", "PATH") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/";
                    if (!Directory.Exists(str_path))
                    {
                        Directory.CreateDirectory(str_path);
                    }
                    string fileNmae = Path.GetFileName(InputFile.PostedFile.FileName).Replace(" ", "").Replace("#", "＃");
                    string str_url = cdata.GetZDZ("FJSC", "URL") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/" + fileNmae;
                    InputFile.PostedFile.SaveAs(str_path + fileNmae);
                    string sql = string.Format("insert into T_FAU_FILELIST (FILEGUID,FILENAME,FILEURL,FILESORT,UPLOADNAME,UPLOADTIME,FILESIZE,ISUSEFULL) values('{0}','{1}','{2}','{3}','{4}',to_date('"+DateTime.Now.ToString()+"','YYYY-MM-dd HH24:MI:SS'),'{5}','1')",
                        Guid.NewGuid().ToString(), fileNmae, str_url, FileSort, UserName, Convert.ToDecimal(InputFile.PostedFile.ContentLength/1024));
                    DataFunction.ExecuteNonQuery(sql);
                    return true;
                }
            return false;
        }
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="InputFile">文件控件</param>
        /// <param name="FileSort">文件分类</param>
        /// <param name="UserName">上传人</param>
        /// <returns></returns>
        public static bool FileUpLoad(HtmlInputFile InputFile, string FileSort, string UserName,string OrgName)
        {

            classData cdata = new classData();
            double maxSize = Convert.ToDouble(cdata.GetZDZ("FJSC", "MAXSIZE")) * 1024 * 1024;

                if (GetAllFileSize() < maxSize)
                {
                    string str_path = cdata.GetZDZ("FJSC", "PATH") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/";
                    if (!Directory.Exists(str_path))
                    {
                        Directory.CreateDirectory(str_path);
                    }
                    string fileNmae = Path.GetFileName(InputFile.PostedFile.FileName).Replace(" ", "").Replace("#", "＃");
                    string str_url = cdata.GetZDZ("FJSC", "URL") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/" + fileNmae;
                    InputFile.PostedFile.SaveAs(str_path + fileNmae);
                    string sql = string.Format("insert into T_FAU_FILELIST (FILEGUID,FILENAME,FILEURL,FILESORT,UPLOADNAME,FILESIZE,ISUSEFULL,UPLOADORG) values('{0}','{1}','{2}','{3}','{4}',to_date('" + DateTime.Now.ToString() + "','YYYY-MM-dd HH24:MI:SS'),'{5}','1','{6}')",
                        Guid.NewGuid().ToString(), fileNmae, str_url, FileSort, UserName, InputFile.PostedFile.ContentLength, OrgName);
                    DataFunction.ExecuteNonQuery(sql);
                    return true;
                }
            return false;
        }
        //public static bool FileUpLoad(jmail.Attachment InputFile, string FileSort, string UserName)
        //{
        //    DataSet ds = CodeMaintenanceDb.GetCode("8118cc15-cf60-4415-a1a7-5e970dc835f6");
        //    foreach (DataRow DR in ds.Tables[0].Rows)
        //    {
        //        string codeguid = DR["CODE_GUID"].ToString();
        //        double maxSize = Convert.ToDouble(CodeMaintenanceDb.GetCodeProper(codeguid, "MAXSIZE")) * 1024 * 1024;

        //        if (GetAllFileSize(codeguid) < maxSize)
        //        {
        //            string str_path = CodeMaintenanceDb.GetCodeProper(codeguid, "PATH") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/";
        //            if (!Directory.Exists(str_path))
        //            {
        //                Directory.CreateDirectory(str_path);
        //            }
        //            string fileNmae = Path.GetFileName(InputFile.Name).Replace(" ", "").Replace("#", "＃");
        //            string str_url = CodeMaintenanceDb.GetCodeProper(codeguid, "URL") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/" + fileNmae;
        //            InputFile.SaveToFile(str_path + fileNmae);
        //            string sql = string.Format("insert into FILE_LIST (FILEGUID,FILENAME,FILEURL,FILESORT,UPLOADNAME,UPLOADTIME,FILESIZE,CODEGUID,ISUSEFULL) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','1')",
        //                Guid.NewGuid().ToString(), fileNmae, str_url, FileSort, UserName, DateTime.Now.ToString(), InputFile.Size, codeguid);
        //            DataFunction.ExecuteNonQuery(sql);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <param name="InputFile">文件控件</param>
        /// <param name="FileSort">文件分类</param>
        /// <param name="UserName">上传人</param>
        /// <returns></returns>
        //public static bool FileUpLoad(HttpPostedFile InputFile, string FileSort, string UserName)
        //{
        //    DataSet ds = CodeMaintenanceDb.GetCode("8118cc15-cf60-4415-a1a7-5e970dc835f6");
        //    foreach (DataRow DR in ds.Tables[0].Rows)
        //    {
        //        string codeguid = DR["CODE_GUID"].ToString();
        //        double maxSize = Convert.ToDouble(CodeMaintenanceDb.GetCodeProper(codeguid, "MAXSIZE")) * 1024 * 1024;

        //        if (GetAllFileSize(codeguid) < maxSize)
        //        {
        //            string str_path = CodeMaintenanceDb.GetCodeProper(codeguid, "PATH") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/";
        //            if (!Directory.Exists(str_path))
        //            {
        //                Directory.CreateDirectory(str_path);
        //            }
        //            string fileNmae = Path.GetFileName(InputFile.FileName).Replace(" ", "").Replace("#", "＃");
        //            string str_url = CodeMaintenanceDb.GetCodeProper(codeguid, "URL") + "/" + FileSort + "/" + DateTime.Now.ToShortDateString() + "/" + fileNmae;
        //            InputFile.SaveAs(str_path + fileNmae);
        //            string sql = string.Format("insert into FILE_LIST (FILEGUID,FILENAME,FILEURL,FILESORT,UPLOADNAME,UPLOADTIME,FILESIZE,CODEGUID,ISUSEFULL) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','1')",
        //                Guid.NewGuid().ToString(), fileNmae, str_url, FileSort, UserName, DateTime.Now.ToString(), InputFile.ContentLength, codeguid);
        //            DataFunction.ExecuteNonQuery(sql);
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        public static void DelFileByID(string fileguid)
        {
            string sql = string.Format("delete from T_FAU_FILELIST where FILEGUID = '{0}'", fileguid);
            DataFunction.ExecuteNonQuery(sql);
        }
	
}

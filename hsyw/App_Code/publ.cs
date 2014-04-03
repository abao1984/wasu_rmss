using System;
using System.Data;
using System.Configuration;
////using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
////using System.Xml.Linq;

using System.Text;
using System.Collections.Specialized;



/// <summary>
/// Summary description for publ
/// </summary>
public class publ
{
    //获取客户端IP
    public static string GetClientIP()
    {
        string userIP;
        HttpRequest Request = HttpContext.Current.Request;
        // 如果使用代理，获取真实IP
        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
            userIP = Request.ServerVariables["REMOTE_ADDR"];
        else
            userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (userIP == null || userIP == "")
            userIP = Request.UserHostAddress;
        return userIP;
    }

    //对字符串进行MD5加密
    public static string MD5(string Str_Temp)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(Str_Temp, "MD5");
    }

    //获取汉字的拼音
    public static string GetPyFromHz(string Str_Temp)
    {
        return ConvertHzToPz_Gb2312.Convert(Str_Temp);
    }

    //获取汉字拼音的首字母
    public static string GetPySzmFromHz(string Str_Temp)
    {
        return StrToPinyin.GetChineseSpell(Str_Temp);
    }

    //去除不规范字符
    public static string GetCatchMsg(string Str_Temp)
    {
        return Str_Temp.Replace("\'", "").Replace("\n", "").Replace("\r", "");
    }

    //将小写金额转换成大写金额
    public static string GetMoneyCap(string Str_Temp)
    {
        Money money = new Money();
        return money.ConvertSumToCap(Str_Temp);
    }

    //字符串加密
    public static string GetStrToPass(string Str_Temp)
    {
        ClassTripleDES des = new ClassTripleDES();
        return des.DESEncrypt(Str_Temp);
    }

    //字符串解密
    public static string GetPassToStr(string Str_Temp)
    {
        ClassTripleDES des = new ClassTripleDES();
        return des.DESDecrypt(Str_Temp);
    }

    //地址栏参数发送转换
    public static string GetUrlToSend(string Str_Temp)
    {
        //无加密仅做转换
        return System.Web.HttpUtility.UrlEncode(Str_Temp);


        //加密发送参数
       // return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Str_Temp));
    }

    //地址栏参数接收转换
    public static string GetUrlToReceive(string Str_Temp)
    {
        //无加密仅做转换
        return System.Web.HttpUtility.UrlDecode(Str_Temp);


        //加密接受参数
        //return System.Text.Encoding.Default.GetString(Convert.FromBase64String(Str_Temp));
    }

    //得到当前执行文件的路径
    public static string GetHttpUrl()
    {
        string url = HttpContext.Current.Request.Url.ToString();
        url = url.Substring(0, url.LastIndexOf("/") + 1);
        return url;

    }






}

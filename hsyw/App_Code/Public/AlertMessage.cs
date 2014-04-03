using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// AlertMessage 的摘要描述
/// </summary>
public class AlertMessage
{
    public AlertMessage()
    {
        //
        // TODO: 在此加入构造函数的程序代码
        //
    }


    public static void showdialog(string AlertMessage)
    {
        HttpContext.Current.Response.Write("<script>window.showModalDialog('" + AlertMessage + "',  window, 'dialogHeight:600px;dialogWidth:540px;center:yes;status:no;help:no;scroll:yes')</script>)</script>");
    }

    //显示JavaScript的Alert信息
    public static void showMsg(Page thisPage, string AlertMessage)
    {
        Literal txtMsg = new Literal();
        txtMsg.Text = "<script>alert('" + AlertMessage + "')</script>" + "<BR/>";
        thisPage.Controls.Add(txtMsg);
    }
    /// <summary>
    /// page_Load中使用Alert
    /// </summary>
    /// <param name="AlertMessage"></param>
    public static void showMsg(string AlertMessage)
    {
        HttpContext.Current.Response.Write("<script>alert('" + AlertMessage + "')</script>");
    }
    /// <summary>
    /// 显示js的Confirm
    /// </summary>
    /// <param name="thisPage"></param>
    /// <param name="AlertMessage"></param>
    public static void showConfirm(Page thisPage, string AlertMessage)
    {
        Literal txtMsg = new Literal();
        txtMsg.Text = "<script>confirm('" + AlertMessage + "')</script>" + "<BR/>";
        thisPage.Controls.Add(txtMsg);
    }
    /// <summary>
    /// page_Load中使用Confirm
    /// </summary>
    /// <param name="AlertMessage"></param>
    public static void showConfirm(string AlertMessage)
    {
        HttpContext.Current.Response.Write("<script>confirm('" + AlertMessage + "')</script>");
    }
    /// <summary>
    /// js跳转页面
    /// </summary>
    /// <param name="thisPage"></param>
    /// <param name="toPage"></param>
    public static void turnToPage(Page thisPage, string toPage)
    {
        Literal txtMsg = new Literal();
        txtMsg.Text = "<script>top.location.replace('" + toPage + "')</script>" + "<BR/>";
        thisPage.Controls.Add(txtMsg);
    }
    /// <summary>
    /// page_Load中使用js跳转页面
    /// </summary>
    /// <param name="thisPage"></param>
    /// <param name="toPage"></param>
    public static void turnToPage(string toPage)
    {
        HttpContext.Current.Response.Write("<script>top.location.replace('" + toPage + "')</script>");
    }
}

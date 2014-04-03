using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

/// <summary>
///SendMail 的摘要说明
/// </summary>
public class SendMail
{
	public SendMail()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="subject">邮件标题 </param>
    /// <param name="body">邮件内容</param>
    /// <param name="toEmail">收件人邮箱地址</param>
    /// <param name="toCcEmail">抄送人邮箱地址</param>
    /// <param name="fromEmail">发件人邮箱地址</param>
    /// <param name="fromName">发件人姓名</param>
    /// <param name="userName">发件人邮箱用户名</param>
    /// <param name="passWord">发件人邮箱密码</param>
    /// <param name="smtp">SMTP</param>
    public void SendEmail(string subject, string body, string toEmail, string toCcEmail, string fromEmail, string fromName, string userName, string passWord, string smtp)
    {
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();


        string[] mailNames = toEmail.Split(';');
        foreach (string name in mailNames)
        {
            if (name != string.Empty)
            {
                msg.To.Add(name);
            }
        }

        string[] mailCcNames = toCcEmail.Split(';');
        foreach (string name in mailCcNames)
        {
            if (name != string.Empty)
            {
                msg.CC.Add(name);
            }
        }


        msg.From = new MailAddress(fromEmail, fromName, System.Text.Encoding.UTF8);
        /* 上面3个参数分别是发件人地址，发件人姓名，编码*/

        msg.Subject = subject;//邮件标题 
        msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
        msg.Body = body;//邮件内容 
        msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
        msg.IsBodyHtml = true;//是否是HTML邮件 
        msg.Priority = MailPriority.Normal;//邮件优先级 

        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential(userName, passWord);
        client.Host = smtp;
        client.EnableSsl = true;//经过ssl加密 
        object userState = msg;
        client.Send(msg);

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace ebs.Tools
{
    public class MailSender
    {
        private static readonly MailSender me = new MailSender();

        protected SmtpClient client;
        private MailSender()
        {
            //获得并设置相关邮件服务器信息
            string smtpServer = "smtp.163.com";
            int smtpPort = 25;
            string smtpUserName = "xuli_100@163.com";
            string smtpPassword = "007asddsa";
            //设置客户端
            client = new SmtpClient(smtpServer, smtpPort);
            client.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
        }

        /// <summary>
        /// 单件实例
        /// </summary>
        public static MailSender Instance
        {
            get { return me; }
        }

        public bool SendPsdServiceMail(string to,string url)
        {
            try
            {
                //设置发送地址和接收地址
                string mailAddress = "xuli_100@163.com";

                //获取邮件内容
                string mailFormat = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
                                                <html>
                                                <head>
                                                    <title></title>
                                                </head>
                                                <body>
                                                        Hi,<br />
                                                        <br />
                                                        This is a system mail from EBS system .
                                                        <br />
                                                        <br />
    
                                                        Please click the below URL to reset your password <a href='{0}'>
                                                            Reset password</a>
                                                </body>
                                                </html>";
                string mailContext = string.Format(mailFormat, "http://121.43.107.232/Register/pasReset.aspx?userID=" + Security.EnCode(url));

                using (MailMessage mail = new MailMessage(mailAddress, to))
                {
                    //设置邮件内容
                    mail.Body = mailContext;
                    //html格式
                    mail.IsBodyHtml = true;
                    //设置标题 
                    mail.Subject = "this is a mail for resetting your password";
                    //设置优先级
                    mail.Priority = MailPriority.High;
                    //发送
                    client.Send(mail);
                }
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
    }
}
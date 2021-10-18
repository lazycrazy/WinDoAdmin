using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
namespace WinDo.Utilities
{
    /// <summary>
    /// EmailHelper
    /// 发送邮件类
    /// 
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// Email帐户
        /// </summary>
        public static class Account //這個物件用來設定 SMTP 位置、帳號、密碼
        {
            /// <summary>
            /// SMTP位置
            /// </summary>
            public const string EmailSMTPServer = "smtp.gmail.com";
 
            /// <summary>
            /// SMTP Port
            /// </summary>
            public const int    EmailSMTPPort = 587;                 

            /// <summary>
            /// 發信者名稱
            /// </summary>
            public const string EmailSMTPName   = "---"; 

            /// <summary>
            /// SMTP 帳號 (ex:00xx@gmail.com)
            /// </summary>
            public const string EmailSMTPUser   = "---"; 

            /// <summary>
            /// SMTP 密碼
            /// </summary>
            public const string EmailSMTPPass   = "---";  
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        public class MailSender
        {
            /// <summary>
            /// 信件標題
            /// </summary>
            public string Subject { get; set; }  

            /// <summary>
            /// 信件內容
            /// </summary>
            public string Content { get; set; }     
     
            /// <summary>
            /// 信件回覆位置
            /// </summary>
            public MailAddress ReplayTo { get; set; }    
 
            /// <summary>
            /// 信件接收者
            /// </summary>
            public List<MailAddress> Receiver { get; set; } 

            /// <summary>
            /// 副本
            /// </summary>
            public List<MailAddress> CC { get; set; }     

            /// <summary>
            /// 副本
            /// </summary>
            public List<MailAddress> BCC { get; set; }  

            /// <summary>
            /// 是否啟用SSL
            /// </summary>
            public bool EnableSSL { get; set; }  

            /// <summary>
            /// 是否為HTML內容
            /// </summary>
            public bool EnableTHML { get; set; } 


            /// <summary>
            /// 构造函数
            /// </summary>
            public MailSender()
            {
                Receiver = new List<MailAddress>();
                CC = new List<MailAddress>();
                BCC = new List<MailAddress>();
                this.EnableSSL = true;
                this.EnableTHML = true;
            }

            /// <summary>
            /// 信件發送函數。
            /// </summary>
            /// <param name="subject">信件標題</param>
            /// <param name="content">信件內容</param>
            /// <param name="receiver">收件者</param>
            public void Send(string subject, string content, List<MailAddress> receiver)
            {
                this.Subject = subject;
                this.Content = content;
                this.Receiver = receiver;
                Send();
            }

            /// <summary>
            /// 信件發送函數
            /// </summary>
            public void Send()
            {
                var mailFrom = new MailAddress(Account.EmailSMTPUser, Account.EmailSMTPName, Encoding.UTF8);
                var mail = new MailMessage {From = mailFrom};
                foreach (var item in Receiver)
                {
                    mail.To.Add(item);                  //加入信件接收者
                }
                mail.IsBodyHtml = EnableTHML;           //信件本文是否為HTML
                mail.Body = Content;                    //設定本文內容
                mail.BodyEncoding = Encoding.UTF8;
                mail.Subject = Subject;                 //設定信件標題
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Priority = MailPriority.High;
                mail.ReplyTo = ReplayTo;                //設定信件回覆位置
                foreach (var item in CC)
                {
                    mail.CC.Add(item);                  //加入信件副本
                }
                foreach (var item in BCC)
                {
                    mail.Bcc.Add(item);                 //加入信件密件副本
                }

                var SC = new SmtpClient(Account.EmailSMTPServer, Account.EmailSMTPPort)
                {
                    Credentials = new System.Net.NetworkCredential(Account.EmailSMTPUser, Account.EmailSMTPPass),
                    EnableSsl = EnableSSL
                };
                SC.Send(mail);              //發送
            }
        }
    }
}

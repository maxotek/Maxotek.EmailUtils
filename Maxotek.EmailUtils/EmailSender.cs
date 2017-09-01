#region Copyright

// Maxotek CONFIDENTIAL INFORMATION
// © 2007-2017 Maxotek Inc.
// All Rights Reserved
//                                                                   
// This program contains confidential and proprietary information   
// of the Maxotek, Inc.  Any reproduction, disclosure, or use       
// in whole or in part is expressly prohibited, except as may be    
// specifically authorized by prior written agreement.

#endregion

#region Imports

using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

#endregion

namespace Maxotek.EmailUtils
{
    public class EmailSender
    {
        private readonly string _smtpHost;
        private readonly string _smtpPass;
        private readonly int _smtpPort;
        private readonly string _smtpUser;

        public EmailSender(string smtpHost, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public void SendMail(string subject, string to, string from, string template,
            Dictionary<string, string> parameters, MailAddress replyTo = null)
        {
            var body = ReplaceVars(template, parameters);

            using (var client = new SmtpClient(_smtpHost, _smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass)
            })
            {
                var message = new MailMessage(from, to)
                {
                    Body = body,
                    IsBodyHtml = true,
                    Subject = subject
                };

                if (replyTo != null)
                    message.ReplyToList.Add(replyTo);

                client.Send(message);
            }
        }

        public static string ReplaceVars(string body, Dictionary<string, string> parameters)
        {
            foreach (var parameter in parameters)
                body = ReplaceVar(body, parameter);

            return body;
        }

        public static string ReplaceVar(string template, KeyValuePair<string, string> parameter)
        {
            return template.Replace($"${{{parameter.Key}}}", parameter.Value);
        }
    }
}
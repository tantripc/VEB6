using Azure.Identity;
using HtmlAgilityPack;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using MiddlewareTool.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareTool.Utility
{
    public class MailUtility
    {
        #region Properties
        public static MailUtility Instance { get { return new MailUtility(); } }
        #endregion

        #region Constructors
        public MailUtility() { }
        #endregion

        #region Public Methods
        public bool Send(Mail mail, string authenticonMode = "")
        {
            bool result = false;
            try
            {
                if (authenticonMode == "O365")
                {
                    if (Validate365(mail.TenantID, mail.ClientID, mail.ClientSecret, mail.ObjectID))
                    {
                        var lstMailTo = (mail.LstMailTo != null && mail.LstMailTo.Count > 0)
                            ? ConvertToList(mail.LstMailTo)
                            : ConvertToList(mail.MailTo);
                        var lstMailCc = (mail.LstMailCc != null && mail.LstMailCc.Count > 0)
                            ? ConvertToList(mail.LstMailCc)
                            : ConvertToList(mail.MailCc);
                        MailMessage mailMessage = AddMailMessage(mail.ObjectID, mail.Subject, mail.Body, lstMailTo, lstMailCc);

                        result = SendSmtpClient365(mail, mailMessage);
                    }
                }
                else
                {
                    if (Validate(mail.MailFrom, mail.Host, mail.Port, mail.Timeout))
                    {
                        var lstMailTo = (mail.LstMailTo != null && mail.LstMailTo.Count > 0)
                            ? ConvertToList(mail.LstMailTo)
                            : ConvertToList(mail.MailTo);
                        var lstMailCc = (mail.LstMailCc != null && mail.LstMailCc.Count > 0)
                            ? ConvertToList(mail.LstMailCc)
                            : ConvertToList(mail.MailCc);
                        MailMessage mailMessage = AddMailMessage(mail.MailFrom, mail.Subject, mail.Body, lstMailTo, lstMailCc);
                        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors)
                            => true;
                        result = SendSmtpClient(mail, mailMessage);
                    }
                }
            }
            catch (Exception ex) { Logging.LogError("Send", ex); }
            return result;
        }
        public async Task<bool> SendAsync(Mail mail)
        {
            bool result = false;
            try
            {
                if (Validate(mail.MailFrom, mail.Host, mail.Port, mail.Timeout))
                {
                    var lstMailTo = (mail.LstMailTo != null && mail.LstMailTo.Count() > 0)
                        ? ConvertToList(mail.LstMailTo)
                        : ConvertToList(mail.MailTo);
                    var lstMailCc = (mail.LstMailCc != null && mail.LstMailCc.Count() > 0)
                        ? ConvertToList(mail.LstMailCc)
                        : ConvertToList(mail.MailCc);
                    MailMessage mailMessage = AddMailMessage(mail.MailFrom, mail.Subject, mail.Body, lstMailTo, lstMailCc);
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors)
                        => true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    result = await SendSmtpClientAsync(mail, mailMessage);
                }
            }
            catch (Exception ex) { Logging.LogError("SendAsync", ex); }
            return result;
        }

        #endregion

        #region Private Methods
        private bool SendSmtpClient(Mail mail, MailMessage mailMessage)
        {
            bool result = false;
            try
            {
                SmtpClient client = new SmtpClient(mail.Host, mail.Port);
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(mail.MailFrom, mail.Password);
                client.EnableSsl = mail.EnableSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mailMessage);
                result = true;
            }
            catch (SmtpException ex) { Logging.LogError("SendSmtpClient", ex); }
            return result;
        }
        private bool SendSmtpClient365(ConfigEmail365 ConfigEmail365, MailMessage mailMessage)
        {
            bool result = false;
            try
            {
                var scopes = new[] { "https://graph.microsoft.com/.default" };
                var options = new TokenCredentialOptions
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var credentials = new ClientSecretCredential(ConfigEmail365.TenantID, ConfigEmail365.ClientID, ConfigEmail365.ClientSecret, options);

                var _graphClient = new GraphServiceClient(credentials, scopes);
                var requestBody = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
                {
                    Message = new Message
                    {
                        Subject = mailMessage.Subject,
                        Body = new ItemBody
                        {
                            ContentType = mailMessage.IsBodyHtml ? BodyType.Html : BodyType.Text,
                            Content = mailMessage.Body,
                        }
                    },
                    SaveToSentItems = true,
                };

                if (mailMessage.To != null)
                {
                    requestBody.Message.ToRecipients = new List<Recipient>();
                    foreach (var item in mailMessage.To)
                    {
                        requestBody.Message.ToRecipients.Add(new Recipient() { EmailAddress = new EmailAddress { Address = item.Address, Name = item.DisplayName } });
                    }
                }
                if (mailMessage.CC != null)
                {
                    requestBody.Message.CcRecipients = new List<Recipient>();
                    foreach (var item in mailMessage.CC)
                    {
                        requestBody.Message.CcRecipients.Add(new Recipient() { EmailAddress = new EmailAddress { Address = item.Address, Name = item.DisplayName } });
                    }
                }
                if (mailMessage.Attachments?.Count > 0)
                {
                    requestBody.Message.HasAttachments = true;
                    requestBody.Message.Attachments = new List<Microsoft.Graph.Models.Attachment>();

                    foreach (var item in mailMessage.Attachments)
                    {
                        var memoryStream = new MemoryStream();
                        item.ContentStream.CopyTo(memoryStream);
                        byte[] arr = memoryStream.ToArray();

                        requestBody.Message.Attachments.Add(new Microsoft.Graph.Models.Attachment
                        {
                            Id = item.ContentId,
                            ContentType = item.ContentType.MediaType,
                            Name = item.Name,
                            Size = (int)item.ContentStream.Length,
                            OdataType = "#microsoft.graph.fileAttachment",
                            AdditionalData = new Dictionary<string, object> { { "contentBytes", Convert.ToBase64String(arr) } }
                        });
                    }
                }
                _graphClient.Users[ConfigEmail365.ObjectID].SendMail.PostAsync(requestBody).GetAwaiter();
                result = true;
            }
            catch (Exception ex) { Logging.LogError("SendSmtpClient365", ex); }
            return result;
        }
        private async Task<bool> SendSmtpClientAsync(Mail mail, MailMessage mailMessage)
        {
            bool result = false;
            try
            {
                SmtpClient client = new SmtpClient(mail.Host, mail.Port);
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(mail.MailFrom, mail.Password);
                client.EnableSsl = mail.EnableSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(mailMessage);
                result = true;
            }
            catch (SmtpException ex) { Logging.LogError("SendSmtpClientAsync", ex); }
            return result;
        }
        private MailMessage AddMailMessage(string mailFrom, string subject, string body, List<string> lstMailTo, List<string> lstMailCc)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = this.RemoveSpecifiedString(subject);
                mailMessage.Body = HtmlEntity.DeEntitize(body);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Priority = MailPriority.High;
                mailMessage.From = new MailAddress(mailFrom);
                if (lstMailTo.Count > 0) lstMailTo.ForEach(x =>
                {
                    if (IsValidEmail(x))
                        mailMessage.To.Add(x);
                });
                if (lstMailCc.Count > 0) lstMailCc.ForEach(x =>
                {
                    if (IsValidEmail(x))
                        mailMessage.CC.Add(x);
                });
            }
            catch (Exception ex)
            {
                this.LogError("AddMailMessage", ex);
            }
            return mailMessage;
        }
        private string RemoveSpecifiedString(string subject)
        {
            return subject.Replace('\r', ' ').Replace('\n', ' ');
        }
        private List<string> ConvertToList(string strMail)
        {
            List<string> lstMail = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(strMail))
                {
                    if (strMail.Contains(';'))
                    {
                        string[] lstRelativePeople = strMail.Split(';');
                        foreach (string person in lstRelativePeople)
                        {
                            if (!string.IsNullOrEmpty(person) && !lstMail.Any(x => x == person))
                            {
                                lstMail.Add(person);
                            }
                        }
                    }
                    else
                    {
                        lstMail.Add(strMail);
                    }
                }
            }
            catch (Exception ex) { this.LogError("ConvertToList", ex); }
            return lstMail;
        }
        private List<string> ConvertToList(List<string> lstMail)
        {
            List<string> lstResult = new List<string>();
            try
            {
                lstMail.ForEach(item =>
                {
                    lstResult.AddRange(ConvertToList(item));
                });
            }
            catch (Exception ex) { this.LogError("ConvertToList(List<string> lstMail)", ex); }
            return lstMail;
        }
        private bool Validate(string email, string host, int port, int timeOut)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(host) && port > 0 && timeOut > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex) { this.LogError("Validate", ex); }
            return result;
        }
        private bool Validate365(string tenantId, string clientId, string clientSecret, string objectId)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(tenantId) && !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret) && !string.IsNullOrEmpty(objectId))
                {
                    result = true;
                }
            }
            catch (Exception ex) { this.LogError("Validate", ex); }
            return result;
        }
        private void LogError(string method, Exception ex = null)
        {
            var services = this.GetType();
            string _service = services.FullName;
            Logging.LogError($"{_service}-{method}(): ", ex);
        }
        protected bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }

    public class Mail : ConfigEmail365
    {
        public string MailFrom { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string MailTo { get; set; }
        public List<string> LstMailTo { get; set; }
        public string MailCc { set; get; }
        public List<string> LstMailCc { get; set; }
        public bool EnableSsl { get; set; }
    }
    public class ConfigEmail365
    {
        public string TenantID { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string ObjectID { get; set; }
    }
}

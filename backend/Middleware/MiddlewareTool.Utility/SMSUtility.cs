using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// SMSUtility
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "")]
    public sealed class SMSUtility
    {
        readonly string phone_from = string.Empty;
        readonly string apiUrl = string.Empty;
        readonly string authenticateToken = string.Empty;

        public SMSUtility()
        {
        }

        /// <summary>
        /// ctror
        /// </summary>
        /// <param name="link"></param>
        /// <param name="AuthenticateToken"></param>
        /// <param name="SmsBrandName"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "")]
        public SMSUtility(string link, string AuthenticateToken, string SmsBrandName)
        {
            phone_from = SmsBrandName;
            // ex: "http://api-01.worldsms.vn/webapi";
            // ex: "http://api-01.worldsms.vn/webapi/sendMTSMS";
            apiUrl = link;
            // ex: "bXNjOkxjMDVGOGE1";
            // ex: "bGFjdmlldDAyOnFYMVdxMHdG"
            authenticateToken = AuthenticateToken;
        }
        /// <summary>
        /// Send SMS
        /// </summary>
        /// <param name="phone_to">phone number</param>
        /// <param name="sms_message"> message</param>
        /// <returns></returns>
        public ResultSms Send(string phone_to, string sms_message)
        {
            //only support message length max = 612 char.
            if (sms_message.Length > 612)
            {
                sms_message = sms_message.Substring(0, 612);
            }
            string result = string.Empty;
            string package =
                @"
                  ""from"": ""{0}"",
                  ""to"": ""{1}"",
                  ""text"": ""{2}"",
                  ""unicode"": ""0""
                 ";
            package = "{" + string.Format(package, phone_from, phone_to, sms_message) + "}";
            WebRequest request = WebRequest.Create(apiUrl);
            request.Headers.Add("Authorization", "Basic " + authenticateToken);
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";

            //post to server
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(package);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            ResultSms ret = JsonConvert.DeserializeObject<ResultSms>(result);

            return ret;
        }
    }
    /// <summary>
    /// Result Sms
    /// </summary>
    public class ResultSms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Vulnerability", "S1104:Fields should not have public accessibility", Justification = "")]
        public int status;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Vulnerability", "S1104:Fields should not have public accessibility", Justification = "")]
        public int errorcode;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Vulnerability", "S1104:Fields should not have public accessibility", Justification = "")]
        public string description;
    }
}

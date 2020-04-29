using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class SmsService : ISmsService
    {
        private ISmsServiceConfig _smsServiceConfig;

        public SmsService(IOptions<SmsServiceConfig> smsServiceConfig)
        {
            if (smsServiceConfig == null) throw new ApplicationException("SMS service configuration was not found!");
            _smsServiceConfig = smsServiceConfig?.Value;
            ValidateConfiguration();
        }

        public SmsService(SmsServiceConfig smsServiceConfig)
        {
            _smsServiceConfig = smsServiceConfig;
            ValidateConfiguration();
        }

        public SmsService()
        {

        }

        private void ValidateConfiguration()
        {
            if (_smsServiceConfig == null) throw new ArgumentNullException(nameof(_smsServiceConfig), "SMS Service configuration was not supplied");
        }


        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IEnumerable<string> SendBulkSms(IBulkSms bulkSms)
        {
            if (bulkSms == null) return null;
            List<string> successList = new List<string>();
            var smses = bulkSms.Recepients?.Select(x => new Sms() { Message = bulkSms.Message, Recepient = x})?.ToArray();
            foreach (var sms in smses)
            {
                try
                {
                    if (SendSms(sms)) successList.Add(sms.Recepient);
                }
                catch (Exception)
                {
                }
            }
            return successList;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IEnumerable<string>> SendBulkSmsAsync(IBulkSms bulkSms)
        {
            if (bulkSms == null) return null;
            List<string> successList = new List<string>();
            var smses = bulkSms.Recepients?.Select(x => new Sms() { Message = bulkSms.Message, Recepient = x })?.ToArray();
            foreach (var sms in smses)
            {
                try
                {
                    if (await SendSmsAsync(sms)) successList.Add(sms.Recepient);
                }
                catch (Exception)
                {
                }
            }
            return successList;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool SendSms(ISms sms)
        {
            try
            {
                if (!sms?.IsValid(_smsServiceConfig) ?? true) return false;
                var paramDict = new Dictionary<string, string>();
                paramDict.Add(_smsServiceConfig.MessageMask, sms.Message);
                paramDict.Add(_smsServiceConfig.RecepientMask, sms.Recepient);
                _smsServiceConfig?.RequestParameters?.ToList()?.ForEach(x => paramDict.TryAdd(x.Key, x.Value));
                HttpWebRequest request = null;
                string postData = null;
                byte[] postBytes = null;

                switch (_smsServiceConfig.RequestContentType)
                {
                    case RequestContentType.FormData:
                        NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(string.Empty);
                        paramDict?.ToList()?.ForEach(x => outgoingQueryString.Add(x.Key, x.Value));
                        postData = outgoingQueryString?.ToString();
                        postBytes = _smsServiceConfig.Encoding.GetBytes(postData);
                        request = (HttpWebRequest)WebRequest.Create(_smsServiceConfig.BaseUrl);
                        request.ContentType = "application/form-data";
                        break;
                    case RequestContentType.JSON:
                        postData = JsonConvert.SerializeObject(paramDict);
                        postBytes = _smsServiceConfig.Encoding.GetBytes(postData);
                        request = (HttpWebRequest)WebRequest.Create(_smsServiceConfig.BaseUrl);
                        request.ContentType = "application/json";
                        break;
                    default://Handles Enums.RequestContentType.URL too
                        string @params = String.Join("&&", paramDict?.Select(x => $"{x.Key}={x.Value}")?.ToArray());
                        string url = _smsServiceConfig.BaseUrl + '?' + @params;
                        request = (HttpWebRequest)WebRequest.Create(url);
                        break;
                }

                request.Method = _smsServiceConfig.RequestMethod.Method;
                if (postBytes != null)
                {
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(postBytes, 0, postBytes.Length);
                    }
                }

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<bool> SendSmsAsync(ISms sms)
        {
            try
            {
                if (!sms?.IsValid(_smsServiceConfig) ?? true) return false;
                var paramDict = new Dictionary<string, string>();
                paramDict.Add(_smsServiceConfig.MessageMask, sms.Message);
                paramDict.Add(_smsServiceConfig.RecepientMask, sms.Recepient);
                _smsServiceConfig?.RequestParameters?.ToList()?.ForEach(x => paramDict.TryAdd(x.Key, x.Value));
                HttpWebRequest request = null;
                string postData = null;
                byte[] postBytes = null;

                switch (_smsServiceConfig.RequestContentType)
                {
                    case RequestContentType.FormData:
                        NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(string.Empty);
                        paramDict?.ToList()?.ForEach(x => outgoingQueryString.Add(x.Key, x.Value));
                        postData = outgoingQueryString?.ToString();
                        postBytes = _smsServiceConfig.Encoding.GetBytes(postData);
                        request = (HttpWebRequest)WebRequest.Create(_smsServiceConfig.BaseUrl);
                        request.ContentType = "application/form-data";
                        break;
                    case RequestContentType.JSON:
                        postData = JsonConvert.SerializeObject(paramDict);
                        postBytes = _smsServiceConfig.Encoding.GetBytes(postData);
                        request = (HttpWebRequest)WebRequest.Create(_smsServiceConfig.BaseUrl);
                        request.ContentType = "application/json";
                        break;
                    default://Handles Enums.RequestContentType.URL too
                        string @params = String.Join("&", paramDict?.Select(x => $"{x.Key}={x.Value}")?.ToArray());
                        string url = _smsServiceConfig.BaseUrl + '?' + @params;
                        request = (HttpWebRequest)WebRequest.Create(url);
                        break;
                }
                
                request.Method = _smsServiceConfig.RequestMethod.Method;
                if(postBytes != null)
                {
                    using (var stream = await request.GetRequestStreamAsync())
                    {
                        await stream.WriteAsync(postBytes, 0, postBytes.Length);
                    }
                }
                
                HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void SetConfig(ISmsServiceConfig smsServiceConfig)
        {
            this._smsServiceConfig = smsServiceConfig;
            ValidateConfiguration();
        }
    }
}

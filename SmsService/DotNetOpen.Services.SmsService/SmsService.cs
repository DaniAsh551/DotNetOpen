using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Web;

namespace DotNetOpen.Services.SmsService
{
    /// <inheritdoc/>
    public class SmsService : ISmsService
    {
        private ISmsServiceConfig _smsServiceConfig;

        public SmsService(ISmsServiceConfig smsServiceConfig)
        {
            if (smsServiceConfig == null) throw new ApplicationException("SMS service configuration was not found!");
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


        /// <inheritdoc/>
        public IEnumerable<ISmsSendResult> SendBulkSms(IBulkSms bulkSms)
        {
            if (bulkSms == null) return null;
            var statusList = new List<ISmsSendResult>();
            var smses = bulkSms.Recepients?.Select(x => new Sms() { Message = bulkSms.Message, Recepient = x, From = bulkSms.From })?.ToArray();
            foreach (var sms in smses)
            {
                try
                {
                    var status = SendSms(sms);
                    statusList.Add(status);
                }
                catch (Exception e)
                {
                    statusList.Add(new SmsSendResult(sms.Recepient, e.Message));
                }
            }
            return statusList;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ISmsSendResult>> SendBulkSmsAsync(IBulkSms bulkSms)
        {
            if (bulkSms == null) return null;
            var statusList = new List<ISmsSendResult>();
            var smses = bulkSms.Recepients?.Select(x => new Sms() { Message = bulkSms.Message, Recepient = x, From = bulkSms.From })?.ToArray();
            foreach (var sms in smses)
            {
                try
                {
                    var status = await SendSmsAsync(sms);
                    statusList.Add(status);
                }
                catch (Exception e)
                {
                    statusList.Add(new SmsSendResult(sms.Recepient, e.Message));
                }
            }
            return statusList;
        }

        /// <inheritdoc/>
        public ISmsSendResult SendSms(ISms sms)
        {
            try
            {
                if (!sms?.IsValid(_smsServiceConfig) ?? true)
                    throw new Exception("Invalid SMS request.");

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
                return new SmsSendResult(sms.Recepient, response.StatusCode, response.GetResponseStream());
            }
            catch (Exception e)
            {
                return new SmsSendResult(sms.Recepient, e.Message);
            }
        }

        /// <inheritdoc/>
        public async Task<ISmsSendResult> SendSmsAsync(ISms sms)
        {
            try
            {
                if (!sms?.IsValid(_smsServiceConfig) ?? true)
                    throw new Exception("Invalid SMS request.");
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
                
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return new SmsSendResult(sms.Recepient, response.StatusCode, response.GetResponseStream());
            }
            catch (Exception e)
            {
                return new SmsSendResult(sms.Recepient, e.Message);
            }
        }
        /// <inheritdoc/>
        public void SetConfig(ISmsServiceConfig smsServiceConfig)
        {
            this._smsServiceConfig = smsServiceConfig;
            ValidateConfiguration();
        }
    }
}

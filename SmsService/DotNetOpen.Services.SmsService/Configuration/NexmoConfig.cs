using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace DotNetOpen.Services.SmsService.Configuration
{
    /// <summary>
    /// The Dhiraagu SMS service configuration
    /// </summary>
    public class NexmoConfig : SmsServiceConfig
    {
        public NexmoConfig(string api_key, string api_secret, string from = null) : base()
        {
            base.RequestParameters.Add("api_key", api_key);
            base.RequestParameters.Add("api_secret", api_secret);
            base.RequestParameters.Add("from", from);
            base.RequestMethod = HttpMethod.Post;
            base.RequestContentType = RequestContentType.JSON;
            base.BaseUrl = "https://rest.nexmo.com/sms/json";
        }

        public NexmoConfig()
        {
            base.RequestMethod = HttpMethod.Post;
            base.RequestContentType = RequestContentType.JSON;
            base.BaseUrl = "https://rest.nexmo.com/sms/json";
        }
        /// <summary>
        /// API Key from Nexmo
        /// </summary>
        public override string UserName
        {
            get
            {
                return RequestParameters.FirstOrDefault(x => x.Key == "api_key").Value;
            }
            set
            {
                if (RequestParameters.Any(x => x.Key == "api_key"))
                {
                    RequestParameters["api_key"] = value;
                }
                else
                {
                    RequestParameters.Add("api_key", value);
                }
            }
        }
        /// <summary>
        /// API Secret from Nexmo
        /// </summary>
        public override string Password
        {
            get
            {
                return RequestParameters.FirstOrDefault(x => x.Key == "api_secret").Value;
            }
            set
            {
                if (RequestParameters.Any(x => x.Key == "api_secret"))
                {
                    RequestParameters["api_secret"] = value;
                }
                else
                {
                    RequestParameters.Add("api_secret", value);
                }

            }
        }
        /// <summary>
        /// Indicates where the message came from
        /// </summary>
        public string From
        {
            get
            {
                return RequestParameters.FirstOrDefault(x => x.Key == "from").Value;
            }
            set
            {
                if (RequestParameters.Any(x => x.Key == "from"))
                {
                    RequestParameters["from"] = value;
                }
                else
                {
                    RequestParameters.Add("from", value);
                }

            }
        }
        public override string RecepientMask => "to";
        public override string MessageMask => "text";
    }
}

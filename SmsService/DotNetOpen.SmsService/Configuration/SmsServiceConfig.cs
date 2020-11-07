using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// The configuration to be used within the SMS Service
    /// </summary>
    public class SmsServiceConfig : ISmsServiceConfig
    {
        /// <inheritdoc/>
        public string BaseUrl { get; set; }
        /// <inheritdoc/>
        public HttpMethod RequestMethod { get; set; }
        /// <inheritdoc/>
        public RequestContentType RequestContentType { get; set; }
        /// <inheritdoc/>
        public IDictionary<string, string> RequestParameters { get; set; }
        /// <inheritdoc/>
        public int? CharacterLimit { get; set; }
        /// <inheritdoc/>
        public virtual string UserName { get; set; }
        /// <inheritdoc/>
        public virtual string Password { get; set; }
        /// <inheritdoc/>
        public virtual string RecepientMask { get; }
        /// <inheritdoc/>
        public virtual string MessageMask { get;}
        /// <inheritdoc/>
        public Encoding Encoding { get; set; }
        ///// <summary>
        ///// Instantiate a new SmsServiceConfig
        ///// </summary>
        ///// <param name="url">The url of the Sms Service Provider</param>
        ///// <param name="requestParameters">The parameters to send. Eg:userid and password</param>
        ///// <param name="characterLimit"></param>
        ///// <param name="requestMethod">The Http Method to use. Default:Get</param>
        //public SmsServiceConfig(string url, IDictionary<string, string> requestParameters, int? characterLimit = null, HttpMethod requestMethod = null)
        //{
        //    requestMethod = requestMethod ?? HttpMethod.Get;
        //    RequestMethod = requestMethod;
        //    CharacterLimit = characterLimit;
        //    BaseUrl = url;
        //    RequestParameters = requestParameters;
        //}

        ///// <summary>
        ///// Instantiate a new SmsServiceConfig
        ///// </summary>
        ///// <param name="url">The url of the Sms Service Provider</param>
        ///// <param name="requestParametersObject">The parameters to send. Eg:userid and password</param>
        ///// <param name="requestMethod">The Http Method to use. Default:Get</param>
        //public SmsServiceConfig(string url, object requestParametersObject = null, int? characterLimit = null, HttpMethod requestMethod = null)
        //{
        //    requestMethod = requestMethod ?? HttpMethod.Get;
        //    RequestMethod = requestMethod;
        //    CharacterLimit = characterLimit;
        //    BaseUrl = url;
        //    RequestParameters = requestParametersObject?.GetType()?.GetProperties()?.ToDictionary(x => x.Name, x => Convert.ToString(x.GetValue(requestParametersObject))) ?? new Dictionary<string, string>();
        //}

        /// <summary>
        /// Instantiate a new SmsServiceConfig
        /// </summary>
        public SmsServiceConfig()
        {
            RequestParameters = new Dictionary<string, string>();
            Encoding = Encoding.UTF8;
        }
    }
}

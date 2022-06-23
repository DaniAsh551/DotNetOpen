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

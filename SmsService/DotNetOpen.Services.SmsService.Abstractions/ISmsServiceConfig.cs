using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// The configuration to be used within the SMS Service
    /// </summary>
    public interface ISmsServiceConfig
    {
        /// <summary>
        /// The Url to call upon SMS send
        /// </summary>
        string BaseUrl { get; set; }
        /// <summary>
        /// Defines how the data should be sent
        /// </summary>
        RequestContentType RequestContentType { get; set; }
        /// <summary>
        /// The Http Method 
        /// </summary>
        HttpMethod RequestMethod { get; set; }
        /// <summary>
        /// The encoding for messages
        /// </summary>
        Encoding Encoding { get; set; }
        /// <summary>
        /// The parameters to include in the SMS request
        /// </summary>
        IDictionary<string, string> RequestParameters { get; set; }
        /// <summary>
        /// Character limit for each SMS. Keep null for unlimited.
        /// </summary>
        int? CharacterLimit { get; set; }
        /// <summary>
        /// The Username or Id required for the server authentication
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// The password required for the server authentication
        /// </summary>
        string Password { get;  set; }
        /// <summary>
        /// The keyword which the server uses for 'Recepient' or 'Target' or 'To'
        /// </summary>
        string RecepientMask { get; }
        /// <summary>
        /// The keyword which the server uses for SMS body
        /// </summary>
        string MessageMask { get; }

    }
}

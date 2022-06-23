using System.Linq;
using System.Net.Http;

namespace DotNetOpen.Services.SmsService.Configuration
{
    /// <summary>
    /// The Dhiraagu SMS service configuration
    /// </summary>
    public class DhiraaguConfig : SmsServiceConfig
    {
        /// <summary>
        /// Create a new instance of Dhiraagu config
        /// </summary>
        /// <param name="userid">userid form Dhiraagu</param>
        /// <param name="password">password from Dhiraagu</param>
        public DhiraaguConfig(string userid, string password) : base()
        {
            base.RequestParameters.Add("userid", userid);
            base.RequestParameters.Add("password", password);
            base.RequestMethod = HttpMethod.Get;
            base.RequestContentType = RequestContentType.URL;
            base.BaseUrl = "http://bulkmessage.com.mv/jsp/receiveSMS.jsp";
        }
        /// <summary>
        /// Create a new instance of Dhiraagu config
        /// </summary>
        public DhiraaguConfig()
        {
            base.RequestMethod = HttpMethod.Get;
            base.RequestContentType = RequestContentType.URL;
            base.BaseUrl = "http://bulkmessage.com.mv/jsp/receiveSMS.jsp";
        }
        /// <summary>
        /// Dhiraagu userid
        /// </summary>
        public override string UserName
        {
            get
            {
                return RequestParameters.FirstOrDefault(x => x.Key == "userid").Value;
            }
            set
            {
                if (RequestParameters.Any(x => x.Key == "userid"))
                {
                    RequestParameters["userid"] = value;
                }
                else
                {
                    RequestParameters.Add("userid", value);
                }
            }
        }
        /// <summary>
        /// Dhiraagu password
        /// </summary>
        public override string Password
        {
            get
            {
                return RequestParameters.FirstOrDefault(x => x.Key == "password").Value;
            }
            set
            {
                if (RequestParameters.Any(x => x.Key == "password"))
                {
                    RequestParameters["password"] = value;
                }
                else
                {
                    RequestParameters.Add("password", value);
                }

            }
        }
        /// <inheritdoc/>
        public override string RecepientMask => "to";
        /// <inheritdoc/>
        public override string MessageMask => "text";
    }
}

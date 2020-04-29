using DotNetOpen.Services.SmsService.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// Provides a set of pre-configured SMS Provider Configurations for in use within the SMS Service
    /// </summary>
    public static class SmsServiceConfigProvider
    {
        /// <summary>
        /// Get a configuration for Dhiraagu
        /// </summary>
        /// <param name="userid">userid from Dhiraagu</param>
        /// <param name="password">password from Dhiraagu</param>
        /// <returns>new DhiraaguConfig</returns>
        public static DhiraaguConfig GetDhiraaguConfig(string userid, string password) => new DhiraaguConfig(userid, password);
        /// <summary>
        /// Get a configuration for Nexmo
        /// </summary>
        /// <param name="api_key">api_key from Nexmo</param>
        /// <param name="api_secret">api-secret from Nexmo</param>
        /// <returns>new NexmoConfig</returns>
        public static NexmoConfig GetNexmoConfig(string api_key, string api_secret) => new NexmoConfig(api_key, api_secret);
        /// <summary>
        /// Get a configuration for Nexmo
        /// </summary>
        /// <param name="api_key">api_key from Nexmo</param>
        /// <param name="api_secret">api-secret from Nexmo</param>
        /// <param name="from">Define who sent the SMS</param>
        /// <returns>new NexmoConfig</returns>
        public static NexmoConfig GetNexmoConfig(string api_key, string api_secret, string from) => new NexmoConfig(api_key, api_secret, from);
    }
}

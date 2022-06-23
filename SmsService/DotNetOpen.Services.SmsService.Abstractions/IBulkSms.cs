using System.Collections.Generic;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// SMS to be sent to multiple Recepients
    /// </summary>
    public interface IBulkSms
    {
         /// <summary>
        /// The Name of the SMS's sender
        /// </summary>
        string From { get; set; }
        /// <summary>
        /// The recepients of the SMS
        /// </summary>
        IEnumerable<string> Recepients { get; set; }
        /// <summary>
        /// The Contents of the SMS
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// Gets whether the SMS is in acccordance with the smsServiceConfig
        /// </summary>
        /// <param name="smsServiceConfig">The configuration to be used within the SMS Service</param>
        /// <returns>Whether the SMS is valid</returns>
        bool IsValid(ISmsServiceConfig smsServiceConfig);
    }
}

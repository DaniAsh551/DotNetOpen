using System.Collections.Generic;
using System.Linq;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class BulkSms : IBulkSms
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IEnumerable<string> Recepients { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool IsValid(ISmsServiceConfig smsServiceConfig)
        {
            return (Recepients?.Any() ?? false) && (Recepients?.All(x => !string.IsNullOrWhiteSpace(x)) ?? false) && (smsServiceConfig?.CharacterLimit.HasValue ?? false ? Message.Length <= smsServiceConfig?.CharacterLimit : true);
        }
    }
}

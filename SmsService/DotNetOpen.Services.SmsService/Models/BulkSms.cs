using System.Collections.Generic;
using System.Linq;

namespace DotNetOpen.Services.SmsService
{
    /// <inheritdoc/>
    public class BulkSms : IBulkSms
    {
        /// <inheritdoc/>
        public string From { get; set; }
        /// <inheritdoc/>
        public IEnumerable<string> Recepients { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public bool IsValid(ISmsServiceConfig smsServiceConfig)
        {
            return (Recepients?.Any() ?? false) && (Recepients?.All(x => !string.IsNullOrWhiteSpace(x)) ?? false) && (smsServiceConfig?.CharacterLimit.HasValue ?? false ? Message.Length <= smsServiceConfig?.CharacterLimit : true);
        }
    }
}

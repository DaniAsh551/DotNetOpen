namespace DotNetOpen.Services.SmsService
{
    /// <inheritdoc/>
    public class Sms : ISms
    {
        /// <inheritdoc/>
        public string Recepient { get; set; }
        /// <inheritdoc/>
        public string Message { get; set; }
        /// <inheritdoc/>
        public bool IsValid(ISmsServiceConfig smsServiceConfig)
        {
            return !string.IsNullOrWhiteSpace(Recepient) && (!(smsServiceConfig?.CharacterLimit.HasValue ?? false) || Message.Length <= smsServiceConfig?.CharacterLimit);
        }
    }
}

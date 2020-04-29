namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class Sms : ISms
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string Recepient { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public bool IsValid(ISmsServiceConfig smsServiceConfig)
        {
            return !string.IsNullOrWhiteSpace(Recepient) && (!(smsServiceConfig?.CharacterLimit.HasValue ?? false) || Message.Length <= smsServiceConfig?.CharacterLimit);
        }
    }
}

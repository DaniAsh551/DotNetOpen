namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// SMS to be sent
    /// </summary>
    public interface ISms
    {
        /// <summary>
        /// The Name of the SMS's sender
        /// </summary>
        string From { get; set; }
        /// <summary>
        /// The recepient of the SMS
        /// </summary>
        string Recepient { get; set; }
        /// <summary>
        /// The Contents of the SMS
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets whether the SMS is in acccordance with the smsServiceConfig
        /// </summary>
        /// <returns>Whether the SMS is valid</returns>
        bool IsValid(ISmsServiceConfig smsServiceConfig);
    }
}

using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Net;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// Indicates the status of the SMS send request.
    /// </summary>
    public interface ISmsSendResult
    {
        /// <summary>
        /// The recepient of the SMS
        /// </summary>
        string Recepient { get; }
        /// <summary>
        /// Indicates whether the SMS send request succeeded.
        /// </summary>
        bool IsSuccess { get; }
        /// <summary>
        /// HTTP response code for the SMS send request.
        /// </summary>
        HttpStatusCode? ResponseCode { get; }
        /// <summary>
        /// The raw stream from SMS provider's HTTP response.
        /// </summary>
        MemoryStream ResponseStream { get; }
        /// <summary>
        /// The raw stream from SMS provider's HTTP response read as a string.
        /// <param name="encoding">Encoding to use to decode Provider's response. Defaults to ASCII</param>
        /// </summary>
        string GetResponseString(Encoding encoding = null);
        /// <summary>
        /// The raw stream from SMS provider's HTTP response read as a string asynchronously.
        /// <param name="encoding">Encoding to use to decode Provider's response. Defaults to ASCII</param>
        /// </summary>
        Task<string> GetResponseStringAsync(Encoding encoding = null);
    }
}

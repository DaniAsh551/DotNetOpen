using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace DotNetOpen.Services.SmsService
{
    /// <inheritdoc/>
    public class SmsSendResult : ISmsSendResult
    {
        private readonly string errorMessage;

        /// <summary>
        /// Create a SmsSendResult object asynchronously.
        /// </summary>
        /// <param name="recepient">Recepient of the SMS.</param>
        /// <param name="response">Response from SMS provider.</param>
        /// <returns cref="SmsSendResult">SmsSendResult</returns>
        public static async Task<SmsSendResult> ParseAsync(string recepient, HttpWebResponse response)
        {
            var responseStream = new MemoryStream();
            await response.GetResponseStream().CopyToAsync(responseStream);

            return new SmsSendResult(recepient, response.StatusCode, responseStream);
        }

        public SmsSendResult(){
        }

        public SmsSendResult(string recepient, string errorMessage){
            if (string.IsNullOrWhiteSpace(recepient))
            {
                throw new ArgumentException($"'{nameof(recepient)}' cannot be null or empty.", nameof(recepient));
            }

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException($"'{nameof(errorMessage)}' cannot be null or empty.", nameof(errorMessage));
            }

            Recepient = recepient;
            this.errorMessage = errorMessage;
        }

        public SmsSendResult(string recepient, HttpStatusCode responseCode, MemoryStream responseStream){
            if (string.IsNullOrWhiteSpace(recepient))
            {
                throw new ArgumentException($"'{nameof(recepient)}' cannot be null or empty.", nameof(recepient));
            }

            Recepient = recepient;
            ResponseCode = responseCode;
            ResponseStream = responseStream ?? throw new ArgumentNullException(nameof(responseStream));
        }

        /// <summary>
        /// Create a SmsSendResult object.
        /// </summary>
        /// <param name="recepient">Recepient of the SMS.</param>
        /// <param name="response">Response from SMS provider.</param>
        /// <returns cref="SmsSendResult">SmsSendResult</returns>
        public SmsSendResult(string recepient, HttpWebResponse response)
        {
            if (string.IsNullOrWhiteSpace(recepient))
            {
                throw new ArgumentException($"'{nameof(recepient)}' cannot be null or empty.", nameof(recepient));
            }

            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            Recepient = recepient;
            ResponseCode = response.StatusCode;
            ResponseStream = new MemoryStream();
            response.GetResponseStream().CopyTo(ResponseStream);
        }

        /// <summary>
        /// Create a SmsSendResult object asynchronously.
        /// </summary>
        /// <param name="recepient">Recepient of the SMS.</param>
        /// <param name="responseCode">HTTP response code from SMS provider.</param>
        /// <param name="responseStream">HTTP response content from SMS provider.</param>
        /// <returns cref="SmsSendResult">SmsSendResult</returns>
        public SmsSendResult(string recepient, HttpStatusCode responseCode, Stream responseStream)
        {
            if (string.IsNullOrWhiteSpace(recepient))
            {
                throw new ArgumentException($"'{nameof(recepient)}' cannot be null or empty.", nameof(recepient));
            }

            if (responseStream is null)
            {
                throw new ArgumentNullException(nameof(responseStream));
            }

            Recepient = recepient;
            ResponseCode = responseCode;
            ResponseStream = new MemoryStream();
            responseStream.CopyTo(ResponseStream);
        }

        /// <inheritdoc/>
        public string Recepient { get; }
        /// <inheritdoc/>
        public bool IsSuccess
            => ResponseCode.HasValue && ResponseCode.Value == HttpStatusCode.OK;
        /// <inheritdoc/>
        public HttpStatusCode? ResponseCode { get; }
        /// <inheritdoc/>
        public MemoryStream ResponseStream { get; }
        /// <inheritdoc/>
        public string GetResponseString(Encoding encoding = null)
            => ResponseStream != null ? new System.IO.StreamReader(ResponseStream, encoding ?? Encoding.ASCII).ReadToEnd() : errorMessage;
        /// <inheritdoc/>
        public Task<string> GetResponseStringAsync(Encoding encoding = null)
            => ResponseStream != null ? new System.IO.StreamReader(ResponseStream, encoding ?? Encoding.ASCII).ReadToEndAsync() : Task.FromResult<string>(errorMessage);
    }
}

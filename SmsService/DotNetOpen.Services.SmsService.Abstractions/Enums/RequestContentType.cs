using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetOpen.Services.SmsService
{
    public enum RequestContentType
    {
        /// <summary>
        /// All parameters are sent as a query in the URL
        /// </summary>
        URL = 0,
        /// <summary>
        /// Parameters are sent as form data in the request body (Incompatible with HttpGet)
        /// </summary>
        FormData = 1,
        /// <summary>
        /// Parameters are sent as JSON in the request body (Incompatible with HttpGet)
        /// </summary>
        JSON = 2
    }
}

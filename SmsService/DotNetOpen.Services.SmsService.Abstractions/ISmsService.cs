﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetOpen.Services.SmsService
{
    /// <summary>
    /// Service for sending SMS
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// Send an SMS
        /// </summary>
        /// <param name="sms">SMS to be sent</param>
        /// <returns>Whether SMS was sent successfully</returns>
        ISmsSendResult SendSms(ISms sms);
        /// <summary>
        /// Send an SMS
        /// </summary>
        /// <param name="sms">SMS to be sent</param>
        /// <returns>Whether SMS was sent successfully</returns>
        Task<ISmsSendResult> SendSmsAsync(ISms sms);
        /// <summary>
        /// Send SMS to multiple recepients
        /// </summary>
        /// <param name="bulkSms">Bulk SMS to be sent</param>
        /// <returns>Receipients whose SMS were sent successfully</returns>
        IEnumerable<ISmsSendResult> SendBulkSms(IBulkSms bulkSms);
        /// <summary>
        /// Send SMS to multiple recepients
        /// </summary>
        /// <param name="bulkSms">Bulk SMS to be sent</param>
        /// <returns>Receipients whose SMS were sent successfully</returns>
        Task<IEnumerable<ISmsSendResult>> SendBulkSmsAsync(IBulkSms bulkSms);
        /// <summary>
        /// Set the SmsService Config
        /// </summary>
        /// <param name="smsServiceConfig">The configuration relevant to the SMS provider</param>
        void SetConfig(ISmsServiceConfig smsServiceConfig);
    }
}

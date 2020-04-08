using System;
namespace nachos.APIController.lib.globelabs
{
    public class AuthTokenRequest
    {
        public String app_id { get; set; }

        public String app_secret { get; set; }

        public String code { get; set; }
    }

    public class AuthTokenResult
    {
        public String access_token { get; set; }
        public String subscriber_number { get; set; }
    }

    public class SMSSendRequest
    {
        public SMSSendRequestBody outboundSMSMessageRequest { get; set; }
    }

    public class SMSSendRequestBody
    {
        public String clientCorrelator { get; set; }
        public String senderAddress { get; set; }
        public SMSMessage outboundSMSTextMessage { get; set; }
        public String address { get; set; }
    }

    public class SMSMessage
    {
        public String message { get; set; }
    }
} 

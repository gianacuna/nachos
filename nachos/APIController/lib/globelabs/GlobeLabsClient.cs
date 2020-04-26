using System;
using System.Net.Http;
using nachos.APIController.core;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace nachos.APIController.lib.globelabs
{
    public class GlobeLabsClient : LibraryCore
    {
        private String AppId { get; set; }
        private String AppSecret { get; set; }

        private HttpClient client { get; set; }

        public GlobeLabsClient()
        {
            AppId = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["GlobeLabsAppId"];
            AppSecret = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["GlobeLabsAppSecret"];
            client = new HttpClient();
        }

        public async Task<SubscriberInfo> GetAccessToken(String code)
        {
            SubscriberInfo result = null;

            String address = String.Format("https://developer.globelabs.com.ph/oauth/access_token?app_id={0}app_secret={1}&code={2}", AppId, AppSecret, code);
            using HttpClient client = new HttpClient();
            result = await Get<SubscriberInfo>(client, address);

            return result;
        }

        public async Task<LocationResult> Locate(String accessToken, String subscriberNumber, float accuracy)
        {
            LocationResult result = null;

            String address = String.Format("https://devapi.globelabs.com.ph/location/v1/queries/location?access_token={0}&address={1}&requestedAccuracy={2}", accessToken, subscriberNumber, accuracy);
            using HttpClient client = new HttpClient();
            result = await Get<LocationResult>(client, address);

            return result;
        }
    }

    public class SubscriberInfo
    {
        public String access_token { get; set; }
        public String subscriber_number { get; set; }
        public String time_stamp { get; set; }
    }

    public class UnsubscribedInfo
    {
        public SubscriberInfo unsubscribed { get; set; }
    }

    public class LocationResult
    {
        public List<TerminalLocation> terminalLocationList { get; set; }
    }

    public class TerminalLocation
    {
        public String address { get; set; }
        public String locationRetrievalStatus { get; set; }
        public CurrentLocation currentLocation { get; set; }
    }

    public class CurrentLocation
    {
        public float accuracy { get; set; }
        public String latitude { get; set; }
        public String longitude { get; set; }
        public String map_url { get; set; }
        public String timestamp { get; set; }
    }
}

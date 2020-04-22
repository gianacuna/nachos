using System;
using System.Net.Http;
using nachos.APIController.core;

namespace nachos.APIController.lib.globelabs
{
    public class GlobeLabsClient : LibraryCore
    {
        private String AppId { get; set; }
        private String AppSecret { get; set; }

        private HttpClient client { get; set; }

        public GlobeLabsClient()
        {
            //TODO: get from config
            AppId = "";
            AppSecret = "";
            client = new HttpClient();
        }
    }
}

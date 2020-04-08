using System;
namespace nachos.APIController.lib.globelabs
{
    public class GlobeLabsClient
    {
        private String AppId { get; set; }
        private String AppSecret { get; set; }

        public GlobeLabsClient()
        {
            //TODO: get from config
            AppId = "";
            AppSecret = "";
        }
    }
}

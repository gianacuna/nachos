using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nachos.Controllers
{
    public class PublicController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public void Handle([FromQuery] String code, [FromQuery] String access_token, [FromQuery] String subscriber_number)
        {
            if (Request.QueryString.HasValue)
            {
                //TODO: Check request origination
                //1. Via web form: check if has CODE, then do POST to get access token
                //2. Via SMS: (requires hosting) check if has access token

                // access token needs to be stored along phone number

                NachoDBPOST(code);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public void Handle()
        {
            //TODO: handle unsubscription
        }


        private void NachoDBPOST(String code)
        {
            
        }

    }
}

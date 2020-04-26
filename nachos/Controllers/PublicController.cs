using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using nachos.io;
using nachos.io.manager;
using nachos.APIController.lib.globelabs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace nachos.Controllers
{
    public class PublicController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async void Handle([FromQuery] String code, [FromQuery] String access_token, [FromQuery] String subscriber_number)
        {
            if (Request.QueryString.HasValue)
            {
                //TODO: Check request origination
                //1. Via web form: check if has CODE, then do POST to get access token
                //2. Via SMS: (requires hosting) check if has access token

                // access token needs to be stored along phone number
                bool isRequestValid = false;
                bool isRegister = false;

                var queryValues = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(Request.QueryString.Value);
                if (queryValues.ContainsKey("code"))
                {
                    GlobeLabsClient globeLabs = new GlobeLabsClient();
                    var subscriber = await globeLabs.GetAccessToken(code);

                    if (subscriber != null)
                    {
                        access_token = subscriber.access_token;
                        subscriber_number = subscriber.subscriber_number;
                        isRequestValid = true;
                        isRegister = true;
                    }
                }
                if (queryValues.ContainsKey("access_token") && queryValues.ContainsKey("subscriber_number"))
                {
                    isRequestValid = true;
                    isRegister = true;
                }

                if (isRequestValid)
                {
                    if (isRegister)
                    {
                        using nachos.io.Client cli = new Client(base.ConnectionString);
                        var subscriber = ContactIO.Find(cli, mobileNumber: subscriber_number);
                        if (subscriber != null)
                        {
                            ContactIO.Upsert(cli, new io.model.Contact
                            {
                                Id = subscriber.Id,
                                AccessToken = access_token
                            });
                        }
                    }
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public void Handle([FromBody]UnsubscribedInfo info)
        {
            //todo: check origination
            if (info != null)
            {
                if (info.unsubscribed != null)
                {
                    if (!String.IsNullOrEmpty(info.unsubscribed.subscriber_number))
                    {
                        using nachos.io.Client cli = new Client(base.ConnectionString);
                        var subscriber = ContactIO.Find(cli, mobileNumber: info.unsubscribed.subscriber_number);
                        if (subscriber != null)
                        {
                            ContactIO.Unregister(cli, subscriber.Id);
                        }
                    }
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            /*
            using var cli = new nachos.io.Client(base.ConnectionString);
            var user = new  io.model.User
            {
                FirstName = "Gian",
                LastName = "Acuna",
                UserName = "gianacuna",
                PasswordSalt = nachos.io.utils.Encrypt.CreateSalt()
            };
            user.PasswordHash = nachos.io.utils.Encrypt.Create("1234", user.PasswordSalt);
            UserIO.Upsert(cli, user);
            */

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var claims = new List<Claim>();
                if (LoginUser(model.Username, model.Password, out claims))
                {
                    var userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return Redirect("/Home/Index");
                }
            }

            return View();
        }

        private Boolean LoginUser(string username, string password, out List<Claim> claims)
        {
            Boolean result = false;
            claims = null;

            using var cli = new nachos.io.Client(base.ConnectionString);
            var user = UserIO.Find(cli, userName: username);

            if (user != null)
            {
                if (nachos.io.utils.Encrypt.Validate(password, user.PasswordSalt, user.PasswordHash))
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.EmailAddress)
                    };

                    return true;
                }
            }

            return result;
        }
    }
}
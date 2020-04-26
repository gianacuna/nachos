using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nachos.Models;
using System.Data;
using nachos.io;
using nachos.io.manager;
using Microsoft.Extensions.Configuration;

namespace nachos.Controllers
{
    public class BaseController : Controller
    {
        public String ConnectionString
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ConnectionString"];
            }
        }

        public BaseController()
        {

        }
    }
}

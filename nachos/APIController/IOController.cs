using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nachos.APIController.core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860~

namespace nachos
{
    [Route("api/[controller]/[action]")]
    public class IOController : APICore
    {
        [HttpGet]
        public String Test()
        {
            return "test";
        }
    }
}

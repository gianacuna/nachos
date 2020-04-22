using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nachos.Models;
using System.Data;
using System.Data.SqlClient;

namespace nachos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection("Server=sql5050.site4now.net;Database=DB_A5D73D_nachos;User Id=DB_A5D73D_nachos_admin;Password=nachos2020;");
                connection.Open();

                var status = connection.State;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Locator()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

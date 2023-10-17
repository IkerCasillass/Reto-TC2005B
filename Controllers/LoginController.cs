using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebLogin.Models;

namespace WebReto.Controllers
{
    public class LoginController:Controller
    {
        public IActionResult BaseLogin()
        {
            return View();
        }
    }

    
}

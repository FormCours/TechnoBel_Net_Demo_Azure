using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Azure_Message.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormationController : ControllerBase
    {
        /* Ceci est réaliser dans un but pedagogique =) */
        private IConfiguration _Config;

        public FormationController(IConfiguration configuration)
        {
            _Config = configuration;
        }

        [HttpGet]
        public IActionResult GetConfigInfo()
        {
            string demoConfig = _Config["Demo"];
            string connectionString = "Nope"; //_Config.GetConnectionString("default");

            return Ok(new
            {
                Demo = demoConfig,
                ConnectionString = connectionString
            });
        }
    }
}

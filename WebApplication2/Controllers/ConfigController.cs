using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<AttachmentOptions> _attachmentOptions;// [ scobed  IOptionsSnapshot] /[ sigletone  IOptions /IOptionsMonitor]

        public ConfigController(IConfiguration configuration, IOptionsMonitor<AttachmentOptions> attachmentOptions)
        {
            this._configuration = configuration;
            this._attachmentOptions = attachmentOptions;
            //var value = attachmentOptions.Value;
            var value = _attachmentOptions.CurrentValue;
        }


        [HttpGet]
        [Route("")]
        public ActionResult GetConfig()
        {
            Thread.Sleep(10000);
            var Config = new
            {
                EnvName = _configuration["ASPNETCORE_ENVIRONMENT"],
                AllowedHosts = _configuration["AllowedHosts"],
                DefaultConnection = _configuration.GetConnectionString("DefaultConnection"),
                DefaultLoglevel = _configuration["Logging:Loglevel:Default"],
                Testkey = _configuration["TestKey"],
                AttchamentsOptions = _attachmentOptions.CurrentValue
                //AttchamentsOptions = _attachmentOptions.Value
            };
            return Ok(Config);
        }



    }
}

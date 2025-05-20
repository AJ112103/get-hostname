using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DeviceResolverApi.Controllers
{
    [ApiController]
    [Route("api/device")]
    public class DeviceController : ControllerBase
    {
        [HttpGet("hostname")]
        public IActionResult GetHostname()
        {
            var remote = HttpContext.Connection.RemoteIpAddress;
            if (remote == null)
                return BadRequest(new { error = "Cannot determine client IP" });

            string ipString;
            string hostname;

            if (IPAddress.IsLoopback(remote))
            {
                hostname = Dns.GetHostName();
                ipString  = "127.0.0.1";
            }
            else
            {
                ipString = remote.MapToIPv4().ToString();
                try
                {
                    hostname = Dns.GetHostEntry(ipString).HostName;
                }
                catch
                {
                    hostname = ipString;
                }
            }

            return Ok(new { hostname, ip = ipString });
        }
    }
}

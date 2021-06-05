using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace roulette_game_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        // método para leer los parámetros en el body
        public Dictionary<string, string> read_body()
        {
            Dictionary<string, string> reqObj = new Dictionary<string, string>();
            using (var reader = new StreamReader(Request.Body))
            {
                var body = reader.ReadToEnd();
                reqObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
            }
            return reqObj;
        }

        // método para leer los parámetros en el headers
        public Dictionary<string, string> read_headers()
        {
            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                requestHeaders.Add(header.Key, header.Value);
            }
            return requestHeaders;
        }
    }
}

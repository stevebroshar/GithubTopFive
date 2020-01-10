using System.IO;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace SebHomework.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopFive : ControllerBase
    {
        /// <summary>
        /// Queries top five starred github repos and writes repo info as JSON array.
        /// Format of result is defined by github API.
        /// If specified, selects projects by language.
        /// </summary>
        [HttpGet]
        public string Get(string language)
        {
            using var client = new WebClient();
            client.Headers.Add("user-agent", "SebHomework");
            string url = $"https://api.github.com/search/repositories?q=language:{language}&sort=stars&order=desc&per_page=5";
            using Stream stream = client.OpenRead(url);
            using var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            using var jsonDoc = JsonDocument.Parse(text);
            var root = jsonDoc.RootElement;
            var items = root.GetProperty("items");
            return items.ToString();
        }
    }
}

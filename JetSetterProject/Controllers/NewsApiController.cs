using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using JetSetterProject.ViewModels;

namespace JetSetterProject.Controllers
{
    [Route("api/[controller]/{country}")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
        private readonly string url = "https://newsapi.org/v2/top-headlines?";
        private readonly string apikey = "apiKey=772941f406474aa497dc367c0a27288f";

        [HttpGet]
        public NewsVM Get(string country)
        {
            WebClient webClient = new WebClient();
            var option = "country=" + country + "&";
            var pageSize = "pageSize=10&";
            var page = "page=1&";
            var finalizedUrl = url + option + pageSize + page + apikey;
            NewsVM result = JsonConvert.DeserializeObject<NewsVM>(webClient.DownloadString(finalizedUrl));
            return result;
        }

    }
}
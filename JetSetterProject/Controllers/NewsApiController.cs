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
    [Route("api/[controller]")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
        private readonly string url = "https://newsapi.org/v2/top-headlines?" +
          "country=us&" +
          "apiKey=772941f406474aa497dc367c0a27288f";

        [HttpGet]
        public dynamic GetAll()
        {
            WebClient webClient = new WebClient();
            NewsVM result = JsonConvert.DeserializeObject<NewsVM>(webClient.DownloadString(url));
            return result;
        }

    }
}
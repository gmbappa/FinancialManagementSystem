using System.Net.Http.Headers;
using System.Net.Http;
using System.Web.Mvc;
using AutoMapper;
using UI.Models;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IMapper _mapper;

        public BaseController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>(); 
            });
            _mapper = config.CreateMapper(); 
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:55312/api/v1/"); // Set your API base URL here
        }

        protected void SetAuthorizationHeader()
        {
            var token = Session["JWToken"] as string;        ;
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
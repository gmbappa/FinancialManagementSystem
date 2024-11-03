using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Text;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {           
            var token = Session["JWToken"]?.ToString();
          
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.IsAuthenticated = false; 
                return View();
            }

            try
            {                
                LoginResponse tokenResponse = JsonConvert.DeserializeObject<LoginResponse>(token);
                var json = JsonConvert.SerializeObject(new { token });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _httpClient.PostAsync("Authentication/ValidateToken", content);

                if (response.IsSuccessStatusCode)
                {                   
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var resultData = JsonConvert.DeserializeObject<Dictionary<string, bool>>(responseContent);
                  
                    bool isAuthenticated = resultData["result"];                   
                    Session["nav"] = isAuthenticated;

                }
                else
                {
                    ViewBag.IsAuthenticated = false; 
                }
            }
            catch (Exception ex)
            {               
                ViewBag.IsAuthenticated = false;               
            }

            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
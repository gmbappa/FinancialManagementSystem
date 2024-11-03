using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController :  BaseController
    {      

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginModel account)
        {
            var json = JsonConvert.SerializeObject(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("Authentication/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                // Save token in session
                Session["JWToken"] = token;         
                return Json(new { status = true, message = "Login successful" });               
            }
            else
            {
                return Json(new { status = false, message = "Invalid username or password" });
            }      
        }
        public ActionResult Logout()
        {           
            Session.Remove("JWToken");
            Session.Remove("nav");  

            return RedirectToAction("Login", "Account");
        }
    }
}
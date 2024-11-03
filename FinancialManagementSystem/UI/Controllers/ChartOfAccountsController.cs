using Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    
    public class ChartOfAccountsController : BaseController
    {

        // GET: ChartOfAccounts 
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            SetAuthorizationHeader();
            HttpResponseMessage response = await _httpClient.GetAsync("ChartOfAccounts/GetActiveAccounts");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var accounts = JsonConvert.DeserializeObject<List<ChartOfAccount>>(jsonData);
                return View(accounts);
            }
            return View(new List<ChartOfAccount>());
        }

        // GET: ChartOfAccounts/Create
        public ActionResult Create()
        {
            var model = new AccountDto
            {
                IsActive = true 
            };
            return View(model);
        }

        // POST: ChartOfAccounts/Create
        [HttpPost]
        public async Task<ActionResult> Create(AccountDto account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            var itemToAdd = _mapper.Map<AccountDto>(account);
            ChartOfAccountValidator validator = new ChartOfAccountValidator();
            var validationResult = validator.Validate(itemToAdd);
            if (!validationResult.IsValid)
            {                
                foreach (var failure in validationResult.Errors)
                {                    
                    ModelState.AddModelError("", $"Failed to add Chart Of Accounts: {failure.ErrorMessage}");
                }
                return View(account);
            }          

            var json = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("ChartOfAccounts/Add", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Failed to Chart Of Accounts: {error}");
                return View(account);
            }
        }

        // GET: ChartOfAccounts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"ChartOfAccounts/Details/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<ChartOfAccount>(jsonData);
                return View(account);
            }
            return HttpNotFound();
        }

        // POST: ChartOfAccounts/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ChartOfAccount account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }
            var itemToAdd = _mapper.Map<ChartOfAccount>(account);
            ChartOfAccountUpdateValidator validator = new ChartOfAccountUpdateValidator();
            var validationResult = validator.Validate(itemToAdd);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    ModelState.AddModelError("", $"Failed to update Chart Of Accounts: {failure.ErrorMessage}");
                }
                return View(account);
            }

            var json = JsonConvert.SerializeObject(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"ChartOfAccounts/Edit/{account.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: ChartOfAccounts/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"ChartOfAccounts/Details/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<ChartOfAccount>(jsonData);
                return View(account);
            }
            return HttpNotFound();
        }

        // POST: ChartOfAccounts/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"ChartOfAccounts/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { status = true });
            }

            return Json(new { status = false });
        }
        [HttpGet]
        public async Task<ActionResult> Reports()
        {           
            return View(new List<ChartOfAccount>());
        }       

        // GET: ChartOfAccounts
        [HttpGet]
        public async Task<ActionResult> GetReportData(DateTime? startDate, DateTime? endDate, string accountType = null)
        {
            
            var queryParameters = new List<string>();            
            if (startDate.HasValue)
                queryParameters.Add($"startDate={startDate.Value.ToString("yyyy-MM-dd")}");
            if (endDate.HasValue)
                queryParameters.Add($"endDate={endDate.Value.ToString("yyyy-MM-dd")}");
            if (!string.IsNullOrEmpty(accountType))
                queryParameters.Add($"accountType={Uri.EscapeDataString(accountType)}"); 
            
            var queryString = queryParameters.Any() ? "?" + string.Join("&", queryParameters) : string.Empty;
           
            var response = await _httpClient.GetAsync($"ChartOfAccounts/GetReportData{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var accounts = JsonConvert.DeserializeObject<List<AccountReportsDto>>(jsonData);
                // Check if the request is an AJAX request for JSON data
                if (Request.IsAjaxRequest())
                {
                    return Json(new { data = accounts }, JsonRequestBehavior.AllowGet);
                }      
                
                return View(accounts);
            }
            else if (!response.IsSuccessStatusCode)
            {
                return Json(new { data = new List<AccountReportsDto>() }, JsonRequestBehavior.AllowGet);
            }

            return new HttpStatusCodeResult(response.StatusCode, "Error retrieving data");
        }




    }
}

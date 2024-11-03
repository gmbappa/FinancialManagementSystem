using Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class JournalEntryController : BaseController
    {
        // GET: JournalEntry
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            SetAuthorizationHeader();
            HttpResponseMessage response = await _httpClient.GetAsync("JournalEntry/GetAllEntries");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var journalEntries = JsonConvert.DeserializeObject<List<JournalEntriesDto>>(jsonData);

                var entryList = journalEntries
                     .GroupBy(je => new { je.JournalEntryId, je.EntryDate, je.Description, je.Reference })
                     .Select(g => new JournalEntry
                     {
                         Id = g.Key.JournalEntryId,
                         EntryDate = g.Key.EntryDate,
                         Description = g.Key.Description,
                         Reference = g.Key.Reference,
                         Lines = g.Select(line => new JournalEntryLine
                         {
                             Id = line.LineId,
                             AccountName = line.AccountName,
                             Amount = line.DebitAmount + line.CreditAmount,
                             IsDebit = line.DebitAmount > 0
                         }).ToList()
                     }).ToList();

                return View(entryList.OrderBy(x => x.Id));
            }
            return View(new List<JournalEntry>());
        }

        // GET: JournalEntry/Create
        public ActionResult Create()
        {
            var model = new JournalEntryDto
            {
                EntryDate = DateTime.Now, 
                Lines = new List<JournalEntryLine>
                {
                    new JournalEntryLine(), 
                    new JournalEntryLine()  
                }
            };
            return View(model);
        }

        // POST: JournalEntry/Create
        [HttpPost]
        public async Task<ActionResult> Create(JournalEntryDto journalEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(journalEntry);
            }

            var itemToAdd = _mapper.Map<JournalEntryDto>(journalEntry);
            JournalEntryValidator validator = new JournalEntryValidator();
            var validationResult = validator.Validate(itemToAdd);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    ModelState.AddModelError("", $"Failed to Add Journal Entry: {failure.ErrorMessage}");
                }
                return View(journalEntry);
            }

            var json = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("JournalEntry/Add", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Failed to create journal entry: {error}");
                return View(journalEntry);
            }
        }

        // GET: JournalEntry/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"JournalEntry/Details/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var journalEntry = JsonConvert.DeserializeObject<JournalEntryDto>(jsonData);
                return View(journalEntry);
            }
            return HttpNotFound();
        }

        // POST: JournalEntry/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(JournalEntryDto journalEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(journalEntry);
            }

            var itemToAdd = _mapper.Map<JournalEntryDto>(journalEntry);
            JournalEntryUpdateValidator validator = new JournalEntryUpdateValidator();
            var validationResult = validator.Validate(itemToAdd);
            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    ModelState.AddModelError("", $"Failed to update Journal Entry: {failure.ErrorMessage}");
                }
                return View(journalEntry);
            }

            var json = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"JournalEntry/Edit/{journalEntry.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(journalEntry);
        }

        // GET: JournalEntry/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"JournalEntry/Details/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var journalEntry = JsonConvert.DeserializeObject<JournalEntryDto>(jsonData);
                return View(journalEntry);
            }
            return HttpNotFound();
        }

        // POST: JournalEntry/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"JournalEntry/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { status = true });
            }

            return Json(new { status = false });
        }

        // GET: JournalEntry/Reports
        [HttpGet]
        public async Task<ActionResult> Reports()
        {
            return View(new List<JournalEntry>());
        }

        [HttpGet]
        public async Task<ActionResult> GetReportData(DateTime? startDate, DateTime? endDate)
        {
            var queryParameters = new List<string>();

            if (startDate.HasValue)
                queryParameters.Add($"startDate={startDate.Value.ToString("yyyy-MM-dd")}");
            if (endDate.HasValue)
                queryParameters.Add($"endDate={endDate.Value.ToString("yyyy-MM-dd")}");

            var queryString = queryParameters.Any() ? "?" + string.Join("&", queryParameters) : string.Empty;

            var response = await _httpClient.GetAsync($"JournalEntry/GetReportData{queryString}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var reports = JsonConvert.DeserializeObject<List<JournalEntry>>(jsonData);
                Session["ReportData"] = reports;

                if (Request.IsAjaxRequest())
                {
                    return Json(new { data = reports }, JsonRequestBehavior.AllowGet);
                }

                return View(reports);
            }
            else if (!response.IsSuccessStatusCode)
            {
                return Json(new { data = new List<JournalEntry>() }, JsonRequestBehavior.AllowGet);
            }

            return new HttpStatusCodeResult(response.StatusCode, "Error retrieving data");
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactionsByEntryId(string entryId)
        {
            if (Session["ReportData"] is List<JournalEntry> reports)
            {
                int id = (int)Convert.ToInt64(entryId);
                var journalEntryLines = reports
                    .Where(x => x.Id == id)
                    .SelectMany(x => x.Lines)
                    .ToList();

                if (Request.IsAjaxRequest())
                {
                    return Json(new { data = journalEntryLines }, JsonRequestBehavior.AllowGet);
                }

                return View(journalEntryLines);

            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Report data not found in session");
        }
    }
}

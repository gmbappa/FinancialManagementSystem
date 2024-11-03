using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Application.Commands;
using Application.Handlers;
using Application.Queries;


namespace API.Controllers.v1
{
    [JwtAuthorize("Admin")]
    [RoutePrefix("api/v{apiVersion}/JournalEntry")]
    public class JournalEntriesController : ApiController
    {
        private readonly CreateJournalEntryHandler _createHandler;
        private readonly GetActiveJournalEntriesHandler _getActiveHandler;
        private readonly UpdateJournalEntryHandler _updateHandler; 
        private readonly GetJournalEntryByIdHandler _getByIdHandler; 
        private readonly DeleteJournalEntryHandler _deleteHandler; 
        private readonly GetJournalEntriesByDateHandler _getReportActiveHandler;

        public JournalEntriesController(
            CreateJournalEntryHandler createHandler,
            GetActiveJournalEntriesHandler getActiveHandler,
            UpdateJournalEntryHandler updateHandler,
            GetJournalEntryByIdHandler getByIdHandler,
            DeleteJournalEntryHandler deleteHandler,
            GetJournalEntriesByDateHandler getReportActiveHandler)
        {
            _createHandler = createHandler;
            _getActiveHandler = getActiveHandler;
            _updateHandler = updateHandler;
            _getByIdHandler = getByIdHandler;
            _deleteHandler = deleteHandler;
            _getReportActiveHandler = getReportActiveHandler;
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Create(CreateJournalEntryCommand command)
        {
            if (command == null) return BadRequest("Invalid data.");

            _createHandler.Handle(command);
            return Ok("Journal entry created successfully.");
        }

        [HttpGet]
        [Route("GetAllEntries")]
        public IHttpActionResult GetAllEntries()
        {
            var entries = _getActiveHandler.Handle(new GetActiveJournalEntriesQuery());
            if (entries == null || !entries.Any())
            {
                return NotFound();
            }
            return Ok(entries);
        }

        [HttpGet]
        [Route("Details/{id:int}")]
        public IHttpActionResult Details(int id)
        {
            try
            {
                var entry = _getByIdHandler.Handle(id);
                return Ok(entry);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("Edit/{id:int}")]
        public IHttpActionResult Edit(int id, [FromBody] UpdateJournalEntryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                command.Id = id; 
                _updateHandler.Handle(command);
                return Ok("Journal entry updated successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _deleteHandler.Handle(id);
                return StatusCode(HttpStatusCode.NoContent); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetReportData")]
        public IHttpActionResult GetReportData(DateTime? startDate = null, DateTime? endDate = null)
        {           
            var query = new GetJournalEntriesByDateQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };
          
            var entries = _getReportActiveHandler.Handle(query);

           
            if (entries == null || !entries.Any())
            {
                return NotFound();
            }
           
            return Ok(entries);
        }       
    }
}

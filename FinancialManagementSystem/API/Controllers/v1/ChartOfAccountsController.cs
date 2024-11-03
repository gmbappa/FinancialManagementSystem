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
    [RoutePrefix("api/v{apiVersion}/ChartOfAccounts")]
    public class ChartOfAccountsController : ApiController
    {
        private readonly CreateChartOfAccountHandler _createHandler;
        private readonly GetActiveChartOfAccountsHandler _getActiveHandler;
        private readonly UpdateChartOfAccountHandler _updateHandler; 
        private readonly GetChartOfAccountByIdHandler _getByIdHandler; 
        private readonly DeleteChartOfAccountHandler _deleteHandler; 
        private readonly GetActiveChartOfAccountsReportHandler _getReportActiveHandler;



        public ChartOfAccountsController(
            CreateChartOfAccountHandler createHandler,
            GetActiveChartOfAccountsHandler getActiveHandler,
             UpdateChartOfAccountHandler updateHandler,
        GetChartOfAccountByIdHandler getByIdHandler,
        DeleteChartOfAccountHandler deleteHandler,
         GetActiveChartOfAccountsReportHandler getReportActiveHandler
            
            )
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
        public IHttpActionResult Create(CreateChartOfAccountCommand command)
        {
            if (command == null) return BadRequest("Invalid data.");

            _createHandler.Handle(command);
            return Ok("Account created successfully.");
        }

      
        [HttpGet]
        [Route("GetActiveAccounts")]
        public IHttpActionResult GetActiveAccounts()
        {
            var accounts = _getActiveHandler.Handle(new GetActiveChartOfAccountsQuery());
            if (accounts == null || !accounts.Any())
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        [HttpGet]
        [Route("Details/{id:int}")]
        public IHttpActionResult Details(int id)
        {
            try
            {
                var account = _getByIdHandler.Handle(id);
                return Ok(account);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("Edit/{id:int}")]
        public IHttpActionResult Edit(int id, [FromBody] UpdateChartOfAccountCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _updateHandler.Handle(command);
                return Ok(); 
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
        public IHttpActionResult GetReportData(DateTime? startDate = null, DateTime? endDate = null, string accountType = null)
        {            
            var query = new GetActiveChartOfAccountsReportQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                AccountType = accountType
            };
                        
            var accounts = _getReportActiveHandler.Handle(query);
          
            if (accounts == null || !accounts.Any())
            {
                return NotFound();
            }           
            return Ok(accounts);
        }


        [HttpGet]
        [Route("Index")]
        public IHttpActionResult Index()
        {
            return Ok("Account created successfully.");
        }
    }

}
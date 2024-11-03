using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {     
        private readonly DataAccessHelper _dataAccessHelper;

        public ChartOfAccountRepository(DataAccessHelper dataAccessHelper)
        {       
           _dataAccessHelper = dataAccessHelper;
        }
        public IEnumerable<ChartOfAccount> GetActiveAccounts() =>
           _dataAccessHelper.QueryData<ChartOfAccount>("sp_GetAllChartOfAccounts", null).ToList();
        public ChartOfAccount GetById(int id)
        {
            var parameters = new
            {
                Id = id 
            };
           
            return _dataAccessHelper.QueryData<ChartOfAccount>("sp_GetChartOfAccountById", parameters).FirstOrDefault();
        }      

        public void Add(ChartOfAccount entity)
        {
            var parameters = new
            {
                AccountName = entity.AccountName,
                AccountType = entity.AccountType,
                IsActive = entity.IsActive
            };
            _dataAccessHelper.ExecuteData("sp_InsertChartOfAccounts", parameters);            
        }

        public void Update(ChartOfAccount entity)
        {
            var parameters = new
            {
                id = entity.Id,
                AccountName = entity.AccountName,
                AccountType = entity.AccountType,
                IsActive = entity.IsActive
            };
            _dataAccessHelper.ExecuteData("sp_UpdateChartOfAccounts", parameters);
        }

        public void Delete(int id)
        {
            var parameters = new
            {
                Id = id 
            };
            _dataAccessHelper.ExecuteData("sp_DeleteChartOfAccounts", parameters);
        }
        public IEnumerable<ChartOfAccount> GetAll() => _dataAccessHelper.QueryData<ChartOfAccount>("sp_GetAllChartOfAccounts", null).ToList();

        public IEnumerable<ChartOfAccount> GetReports(DateTime? startDate, DateTime? endDate, string accountType = null)
        {
            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate,
                AccountType = accountType
            };
          return  _dataAccessHelper.QueryData<ChartOfAccount>("sp_GetChartOfAccountsReports", parameters).ToList();
        }               
    }
}

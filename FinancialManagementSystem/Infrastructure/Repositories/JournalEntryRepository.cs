using Application.Queries;
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
    public class JournalEntryRepository : IJournalEntryRepository
    {       
        private readonly DataAccessHelper _dataAccessHelper;

        public JournalEntryRepository(DataAccessHelper dataAccessHelper)
        {           
            _dataAccessHelper = dataAccessHelper;
        }    
        public IEnumerable<JournalEntry> GetAll() =>
            _dataAccessHelper.QueryData<JournalEntry>("sp_GetAllJournalEntries", null).ToList();

        public JournalEntry GetById(int id)
        {
            var parameters = new
            {
                Id = id
            };
       
            var data = _dataAccessHelper.QueryData<JournalEntriesWithLines>("sp_GetJournalEntryById", parameters);
            
            var journalEntry = data
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
                }).FirstOrDefault(); 

            return journalEntry; 
        }

        public void Add(JournalEntry entity)
        {           
            _dataAccessHelper.ExecuteJournalEntryData(entity);
        }

        public void Update(JournalEntry entity)
        {           
            _dataAccessHelper.UpdateJournalEntryData(entity);
        }

        public void Delete(int id)
        {
            var parameters = new
            {
                Id = id
            };
            _dataAccessHelper.ExecuteData("sp_DeleteJournalEntry", parameters);
        }      
        public IEnumerable<JournalEntry> GetByDateRange(DateTime? startDate, DateTime? endDate)
        {
            var parameters = new
            {
                StartDate = startDate,
                EndDate = endDate,               
            };
            var data = _dataAccessHelper.QueryData<JournalEntriesWithLines>("sp_GetJournalEntriesReport", parameters).ToList();

            var dataList = data
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
                     }).ToList().OrderBy(x=>x.Id);

            return dataList;
        }

        public void GetActiveJournalAccounts(out IEnumerable<JournalEntriesWithLines> dataCollection)
        {
            var data = _dataAccessHelper.QueryData<JournalEntriesWithLines>("sp_GetJournalEntriesWithLines", null).ToList();
            dataCollection = data.Any() ? data : Enumerable.Empty<JournalEntriesWithLines>();
        }

    }
}

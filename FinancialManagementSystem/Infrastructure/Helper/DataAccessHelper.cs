using Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration; 
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Helper
{
    public class DataAccessHelper
    {
        private readonly string _connectionString; 
 
        public DataAccessHelper()
        {         
            _connectionString = ConfigurationManager.ConnectionStrings["FinancialDbContext"].ConnectionString;
        }
        
        public string GetConnectionString()
        {
            return _connectionString; 
        }
      
        public List<T> QueryData<T>(string storedProcedure, object parameters = null) where T : new()
        {
            List<T> results = new List<T>(); 

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    if (parameters != null)
                    {
                        var parameterProperties = parameters.GetType().GetProperties();
                        foreach (var property in parameterProperties)
                        {
                            command.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters, null) ?? DBNull.Value);
                        }
                    }

                    connection.Open(); 

                    using (SqlDataReader reader = command.ExecuteReader()) 
                    {
                        while (reader.Read())
                        {                         
                            T item = new T();
                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                                {
                                    prop.SetValue(item, reader.GetValue(reader.GetOrdinal(prop.Name)));
                                }
                            }
                            results.Add(item); 
                        }
                    }
                }
            }

            return results; 
        }

        public void ExecuteData(string storedProcedure, object parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                   
                    if (parameters != null)
                    {
                        var parameterProperties = parameters.GetType().GetProperties();
                        foreach (var property in parameterProperties)
                        {
                            command.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters, null) ?? DBNull.Value);
                        }
                    }

                    connection.Open(); 
                    command.ExecuteNonQuery(); 
                }
            }
        }

        public void ExecuteJournalEntryData(JournalEntry entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_InsertJournalEntry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EntryDate", entity.EntryDate);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@Reference", entity.Reference);
                 
                    var linesTable = new DataTable();
                    linesTable.Columns.Add("AccountName", typeof(string));
                    linesTable.Columns.Add("Amount", typeof(decimal));
                    linesTable.Columns.Add("IsDebit", typeof(bool));
                   
                    foreach (var line in entity.Lines)
                    {
                        linesTable.Rows.Add(line.AccountName, line.Amount, line.IsDebit);
                    }
                  
                    var linesParameter = command.Parameters.AddWithValue("@Lines", linesTable);
                    linesParameter.SqlDbType = SqlDbType.Structured;                          
                   
                    command.ExecuteNonQuery();

                }
            }
        }

        public void UpdateJournalEntryData(JournalEntry entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_UpdateJournalEntry", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                 
                    command.Parameters.AddWithValue("@JournalEntryId", entity.Id);
                    command.Parameters.AddWithValue("@EntryDate", entity.EntryDate);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@Reference", entity.Reference);
                  
                    var linesTable = new DataTable();
                    linesTable.Columns.Add("AccountName", typeof(string));
                    linesTable.Columns.Add("Amount", typeof(decimal));
                    linesTable.Columns.Add("IsDebit", typeof(bool));
                   
                    foreach (var line in entity.Lines)
                    {
                        linesTable.Rows.Add(line.AccountName, line.Amount, line.IsDebit);
                    }
                 
                    var linesParameter = command.Parameters.AddWithValue("@Lines", linesTable);
                    linesParameter.SqlDbType = SqlDbType.Structured; 
                 
                    command.ExecuteNonQuery();
                }
            }
        }

    }

}



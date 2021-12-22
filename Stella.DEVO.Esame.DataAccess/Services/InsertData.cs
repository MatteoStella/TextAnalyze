using Microsoft.Extensions.Configuration;
using Stella.DEVO.Esame.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Stella.DEVO.Esame.DataAccess.Services
{
    public class InsertData : IInsertData
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public InsertData(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Database");
        }

        public IEnumerable<SqlModel> GetData()
        {
            using var connection = new SqlConnection(_connectionString);

            const string query = @"
SELECT [Text]
      ,[Date]
      ,[Sentiment]
      ,[Positive]
      ,[Negative]
      ,[Neutral]
  FROM [dbo].[TextAnalize]
";
            return connection.Query<SqlModel>(query);
        }

        public void Insert(SqlModel model)
        {
            using var connection = new SqlConnection(_connectionString);

            const string query = @"
INSERT INTO [dbo].[TextAnalize]
           ([Text]
           ,[Date]
           ,[Sentiment]
           ,[Positive]
           ,[Negative]
           ,[Neutral])
     VALUES
           (@Text
           ,@Date
           ,@Sentiment
           ,@Positive
           ,@Negative
           ,@Neutral)
;";
            connection.Execute(query, model);
        }
    }
}

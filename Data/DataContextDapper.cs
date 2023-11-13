using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace DotnetAPI.Data
{
    class DataContextDapper
    {
        //private readonly IConfiguration _config;
        private string? _connectionString;

        public DataContextDapper(IConfiguration config)
        {
            //_config = config;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }

        public bool ExecuteSqlWithParameters(string sql,List<SqlParameter> parameters)
        {
            SqlCommand commandWithParams = new SqlCommand(sql);
            foreach(SqlParameter parameter in parameters)
            {
                commandWithParams.Parameters.Add(parameter);
            }

            SqlConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            commandWithParams.Connection = dbConnection;
            int rowsAffected = commandWithParams.ExecuteNonQuery();
            dbConnection.Close();
            return rowsAffected>0;
        }


    }
}
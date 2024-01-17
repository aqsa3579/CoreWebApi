using System.Data;
using Dapper;
using System.Data.SqlClient;
using CoreWebApiPractice.Models;

namespace CoreWebApiPractice.Services
{
    public class CustomerServicesStoredProcedure : ICustomerServicesStoredProcedure
    {
        ////// GET ALL///////////
        public async Task<List<Customers>> GetCustomers()
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var Customers = await connection.QueryAsync<Customers>("GetAllCustomers", null, commandType: CommandType.StoredProcedure);
                return Customers.ToList();
            }
        }
        ////// GET BY ID ///////////
        public async Task<Customers> GetCustomers(int Customer_Id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Customer_Id", Customer_Id);
                var Customers = await connection.QueryAsync<Customers>("GetCustomerByID", parameters, commandType: CommandType.StoredProcedure);
                return Customers.FirstOrDefault();
            }
        }

        ////////// POST /////////
        public async Task<int> CreateCustomers(Customers customers)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Customer_Name", customers.Customer_Name);
                parameters.Add("@Customer_Gender", customers.Customer_Gender);
                parameters.Add("Customer_phone", customers.Customer_phone);
                parameters.Add("@Customer_Father_Name", customers.Customer_Father_Name);
                parameters.Add("@Customer_Address", customers.Customer_Address);
                parameters.Add("@Customer_Age", customers.Customer_Age);
                parameters.Add("@Customer_Account_NO", customers.Customer_Account_NO);

                var result = await connection.ExecuteAsync("InsertCustomer", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

       ///////////// UPDATE BY ID ////////////
        public async Task<int> UpdateCustomers(Customers customers)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Customer_Name", customers.Customer_Name);
                parameters.Add("@Customer_Gender", customers.Customer_Gender);
                parameters.Add("Customer_phone", customers.Customer_phone);
                parameters.Add("@Customer_Father_Name", customers.Customer_Father_Name);
                parameters.Add("@Customer_Address", customers.Customer_Address);
                parameters.Add("@Customer_Age", customers.Customer_Age);
                parameters.Add("@Customer_Account_NO", customers.Customer_Account_NO);

                var result = await connection.ExecuteAsync("UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        ///////////// DELETE /////////////
        public async Task<int> DeleteCustomers(int Customer_id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Customer_Id", Customer_id);
                var result = await connection.ExecuteAsync("DeleteCustomer", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}

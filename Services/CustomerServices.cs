using System.Data;
using Dapper;
using System.Data.SqlClient;
using CoreWebApiPractice.Models;

namespace CoreWebApiPractice.Services
{
    public class CustomerServices : ICustomerServices
    {
        public async Task<List<Customers>> GetCustomers()
        {
            var query = "SELECT * FROM Customers;";

            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var Customers = await connection.QueryAsync<Customers>(query);
                return Customers.ToList();
            }
        }


        ////// GET BY ID///////
        public async Task<Customers> GetCustomers(int Customer_Id)
        {
            var query = $"SELECT * FROM Customers where Customer_Id ={Customer_Id}";

            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var Customers = await connection.QueryAsync<Customers>(query);
                return Customers.FirstOrDefault();
            }
        }


        /////// POST //////
        public async Task<int> CreateCustomers(Customers Customers)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var query = $"insert into Customers ( Customer_Name, Customer_Gender,Customer_phone,Customer_Father_Name,Customer_Address,Customer_Age,Customer_Account_NO ) values " +
                                  $"('{Customers.Customer_Name}','{Customers.Customer_Gender}','{Customers.Customer_phone}','{Customers.Customer_Father_Name}','{Customers.Customer_Address}', {Customers.Customer_Age},{Customers.Customer_Account_NO});";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

        ///////// UPDATE BY ID ////////////
        public async Task<int> UpdateCustomers(Customers Customers)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var query = $"update Customers set  Customer_Name= '{Customers.Customer_Name}', Customer_Gender= '{Customers.Customer_Gender}',Customer_phone= '{Customers.Customer_phone}', Customer_Father_Name ='{Customers.Customer_Father_Name} ', Customer_Address = '{Customers.Customer_Address}', Customer_Age= '{Customers.Customer_Age}',Customer_Account_NO= '{Customers.Customer_Account_NO}'" +
                                  $"where ProductId ={Customers.Customer_Id}";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

        ////////// DELETE ///////
        public async Task<int> DeleteCustomers(int id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var query = $"delete from Customers where Customer_Id ={id}";
                var result = await connection.ExecuteAsync(query);
                return result;
            }
        }

        public Task<List<Customers>> GetCustomers(Customers Customers)
        {
            throw new NotImplementedException();
        }
    }
}

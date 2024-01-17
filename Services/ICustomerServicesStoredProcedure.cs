using CoreWebApiPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiPractice.Services
{
    public interface ICustomerServicesStoredProcedure
    {
        Task<List<Customers>> GetCustomers();
        Task<Customers> GetCustomers(int Customer_Id);
        Task<int> CreateCustomers(Customers customers);
        Task<int> UpdateCustomers(Customers customers);
        Task<int> DeleteCustomers(int Customer_id);
    }
}
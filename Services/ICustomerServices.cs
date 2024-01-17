//using CoreWebapi.Models;
using CoreWebApiPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApiPractice.Services
{
    public interface ICustomerServices
    {
        Task<List<Customers>> GetCustomers();
        Task<Customers> GetCustomers(int id);
        Task<int> CreateCustomers(Customers Customers);
        Task<int> UpdateCustomers(Customers Customers);
        Task<int> DeleteCustomers(int Id);
        //Task GetCustomers();
    }
}

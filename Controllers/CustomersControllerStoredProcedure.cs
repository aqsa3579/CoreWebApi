using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using CoreWebApiPractice.Services;
using CoreWebApiPractice.Models;
using System.Data;
using System.Security.AccessControl;

namespace CoreWebApiPractice.Controllers
{
    [Route("SPAPI/")]
    public class CustomersControllerStoredProcedure : Controller
    {
        private readonly ICustomerServicesStoredProcedure _customerServicesStoredProcedure;
        public CustomersControllerStoredProcedure(ICustomerServicesStoredProcedure customerServicesStoredProcedure)
        {
            _customerServicesStoredProcedure = customerServicesStoredProcedure;
        }

        ////////// GET ALL ///////////
        [HttpGet("SPCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var Customers = await _customerServicesStoredProcedure.GetCustomers();
            if (Customers.Count == 0)
            {
                return NotFound("Customer do not exist");
            }

            return this.Ok(Customers);
        }

        //////////// GET BY ID//////////////
        [HttpGet("SPCustomer/{Customer_Id}")]
        public async Task<IActionResult> GetCustomers(int Customer_Id)
        {
            var Customers = await _customerServicesStoredProcedure.GetCustomers(Customer_Id);
            if (Customers == null)
            {
                return NotFound($"Customer{Customer_Id} not exist");
            }
            return this.Ok(Customers);
        }

        //////////// POST //////////////
        [HttpPost("SPCreate Customer")]
        public async Task<IActionResult> CreateProduct([FromBody] Customers customers)
        {
            try
            {
                int result = await _customerServicesStoredProcedure.CreateCustomers(customers);
                if (result > 0)
                    return this.Ok("Customer Sucessfully Added");
                else
                    return this.BadRequest("Error Adding New Customer");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }

        }

        ////////////////// UPDATE BY ID //////////////////
        [HttpPut("SP Update Customer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int Customer_id, [FromBody] Customers customer)
        {
            try
            {
                var dbcustomer = await _customerServicesStoredProcedure.GetCustomers(Customer_id);
                if (dbcustomer == null)
                {
                    return this.NotFound($"Customer Id {Customer_id} not found ...!");
                }
                customer.Customer_Id = Customer_id;
                int result = await _customerServicesStoredProcedure.UpdateCustomers(customer);
                if (result > 0)
                    return this.Ok("Customer Updated Sucessfully...!");
                else
                    return this.BadRequest("Error While Updating The Customer ...!");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

         ////////////// DELETE ////////////////
        [HttpDelete("SP Delete Customer/{id}")]
        public async Task<IActionResult> DeleteCustomers(int Customer_Id)
        {
            try
            {
                var dbcustomer = await _customerServicesStoredProcedure.GetCustomers(Customer_Id);
                if (dbcustomer == null)
                {
                    return this.NotFound($"Customer id {Customer_Id} not founds ...!");
                }
                int result = await _customerServicesStoredProcedure.DeleteCustomers(Customer_Id);
                if (result != null)
                {
                    return this.Ok("Customer Deleted Sucessfully ...!");
                }

                else
                {
                    return this.BadRequest("Error While Deleting The Customer ...! ");
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

    }


}

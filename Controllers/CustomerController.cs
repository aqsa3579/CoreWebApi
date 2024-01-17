using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
using CoreWebApiPractice.Services;
using CoreWebApiPractice.Models;

namespace CoreWebApiPractice.Controllers
{
    [Route("api/")]
    public class CustomerController : Controller
    {

        private readonly ICustomerServices _customerServices;
        public CustomerController(ICustomerServices CustomerServices)
        {
            _customerServices = CustomerServices;
        }

        ///// GET ALL/////
        [HttpGet("Customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var Customers = await _customerServices.GetCustomers();
            if (Customers.Count == 0)
            {
                return NotFound("Customers do not exist");
            }

            return this.Ok(Customers);
        }

        //////// GET BY ID//////
        [HttpGet("Customers/{Customer_Id}")]
        public async Task<IActionResult> GetCustomers(int Customer_Id)
        {
            var Customers = await _customerServices.GetCustomers(Customer_Id);
            if (Customers == null)
            {
                return NotFound($"Customer{Customer_Id} not exist");
            }


            return this.Ok(Customers);
        }


        ////////// POST ////////
        [HttpPost("Create Customers")]
        public async Task<IActionResult> CreateCustomers([FromBody] Customers Customers)
        {
            try
            {
                int result = await _customerServices.CreateCustomers(Customers);
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


        /////// UPDATE //////
        [HttpPut("Update Customers/{id}")]
        public async Task<IActionResult> UpdateCustomers(int id, [FromBody] Customers Customers)
        {
            try
            {
                var dbCustomers = await _customerServices.GetCustomers(id);
                if (dbCustomers == null)
                {
                    return this.NotFound($"Product Id {id} not found ...!");
                }
                Customers.Customer_Id = id;
                int result = await _customerServices.UpdateCustomers(Customers);
                if (result > 0)
                    return this.Ok("Customers Updated Sucessfully...!");
                else
                    return this.BadRequest("Error While Updating The customers data ...!");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /////// DELETE //////
        [HttpDelete("Delete Customers/{id}")]
        public async Task<IActionResult> DeleteCustomers(int id)
        {
            try
            {
                var dbCustomers = await _customerServices.GetCustomers(id);
                if (dbCustomers == null)
                {
                    return this.NotFound($"Customer_id {id} not founds ...!");
                }
                int result = await _customerServices.DeleteCustomers(id);
                if (result > 0)
                    return this.Ok("Customer Deleted Sucessfully ...!");
                else
                    return this.BadRequest("Error While deleting The Customer data ...! ");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }



    }
}

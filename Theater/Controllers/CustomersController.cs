﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater.Data;

namespace Theater.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly PlayContext _context;

        public CustomersController(PlayContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _context.Customers
                .Include(customer => customer.Performances)
                    .ThenInclude(performance => performance.Play)
                        .ThenInclude(play => play.Genre);

            return customers;
        }

        // GET: api/Customers/5
        [HttpGet("Total/{id}")]
        public IActionResult CalculateTotalCustomerById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _context.Customers
                .Include(cust => cust.Performances)
                    .ThenInclude(performance => performance.Play)
                     //   .ThenInclude(play => play.Genre)
                 .FirstOrDefault(cust => cust.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer.CalculateTotalPerformances());
        }

        // PUT: api/Customers/5
        [HttpGet("/Bill/")]
        public async Task<IActionResult> CalculateBill([FromBody] Customer customer, List<Performance> performances)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var customer = _context.Customers.FirstOrDefault(x => x.Name == name);
            foreach (var performance in performances)
            {
                performance.CustomerId = customer.Id;
            }
            return Ok(customer.CalculateTotalPerformances());
        }






        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
using AspNetCoreLibrary.FluentValidation.Dtos;
using AutoMapper;
using FluentValidation;
using FluentValidation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreLibrary.FluentValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Customer> _validator;
        private readonly IMapper _mapper;

        public CustomersApiController(AppDbContext context, IValidator<Customer> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        // GET: api/CustomersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            List<Customer> customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        // GET: api/CustomersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [Route("MappingOrnek")]
        [HttpGet("{id}")]
        public IActionResult MappingOrnek()
        {
            Customer customer = new()
            {
                Id = 1,
                Name = "Murat",
                Email = "mrt.agyzz.00@outlook.com",
                Age = 23,
                CreditCard = new CreditCard()
                {
                    Number = "111111111111",
                    ValidDate = DateTime.Now
                }
            };
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        // PUT: api/CustomersApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
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

        // POST: api/CustomersApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customer)
        {
            var customerMap = _mapper.Map<Customer>(customer);
            var result = _validator.Validate(customerMap);

            if (result.IsValid)
            {
                _context.Customers.Add(customerMap);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCustomer", new { id = customerMap.Id }, customerMap);
            }

            return BadRequest(result.Errors.Select(r => new { Property = r.PropertyName, Error = r.ErrorMessage }));
        }

        // DELETE: api/CustomersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

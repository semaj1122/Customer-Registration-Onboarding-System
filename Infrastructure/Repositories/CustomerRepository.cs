using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository
    : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(
            ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);

            await _db.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _db.Customers
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return _db.Customers.ToListAsync();
        }
    }
}

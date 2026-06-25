using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(
            ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(
            CreateCustomerRequest request)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Signature = request.Signature,
                DateCreated = DateTime.UtcNow
            };

            await _repository.AddAsync(customer);

            return customer.Id;
        }

        public async Task<CustomerDto?> GetByIdAsync(Guid id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                DateCreated = customer.DateCreated
            };
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(x => new CustomerDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                DateCreated = x.DateCreated
            }).ToList();
        }
    }
}

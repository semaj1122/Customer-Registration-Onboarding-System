using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);

        Task<Customer?> GetByIdAsync(Guid id);

        Task<List<Customer>> GetAllAsync();
    }
}

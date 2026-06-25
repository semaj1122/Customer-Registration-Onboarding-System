using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<Guid> CreateAsync(CreateCustomerRequest request);

        Task<CustomerDto?> GetByIdAsync(Guid id);

        Task<List<CustomerDto>> GetAllAsync();
    }
}

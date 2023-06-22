using Microsoft.EntityFrameworkCore;
using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Interfaces.Repositories;
using ParanaBanco.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApiDbContext context): base(context)
        {
            _dataSet = context.Set<Customer>();
        }

        public override async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                return await _dataSet.Include(x => x.Phones).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task<Customer> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.Include(x => x.Phones).SingleOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer> GetByEmailAddress(string emailAddress)
        {
            var queryCustomer = _dataSet
                .Include(x => x.Phones)
                .WhereEmailAddress(emailAddress);

            var customer = await queryCustomer.FirstOrDefaultAsync();

            return customer;
        }

        public async Task<Customer> GetByPhone(string phone)
        {
            var queryCustomer = _dataSet
                .Include(x => x.Phones)
                .WherePhone(phone);

            var customer = await queryCustomer.FirstOrDefaultAsync();

            return customer;
        }
    }
}

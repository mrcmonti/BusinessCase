using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Entities.Phones;
using ParanaBanco.Domain.Interfaces.Repositories;
using ParanaBanco.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Infrastructure.Repositories
{
    public class PhoneRepository : BaseRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(ApiDbContext context) : base(context)
        {
            _dataSet = context.Set<Phone>();
        }
    }
}

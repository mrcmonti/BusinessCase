using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Entities.SeedWork;
using ParanaBanco.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Domain.Entities.Phones
{
    public class Phone : EntityBase
    {
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public PhoneType Type { get; set; }
        public Guid CustomerId { get; set; }

    }
}

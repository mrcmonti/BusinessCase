using ParanaBanco.Domain.Entities.Phones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Application.DTO
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public virtual IEnumerable<PhoneDTO> Phones { get; set; }
    }
}

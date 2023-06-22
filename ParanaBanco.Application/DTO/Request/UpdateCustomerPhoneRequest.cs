using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Application.DTO.Request
{
    public class UpdateCustomerPhoneRequest
    {
        public Guid Id { get; set; }
        public PhoneDTO Phone { get; set; }
    }
}

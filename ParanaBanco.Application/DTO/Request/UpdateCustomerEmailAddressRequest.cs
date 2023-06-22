using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Application.DTO.Request
{
    public class UpdateCustomerEmailAddressRequest
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}

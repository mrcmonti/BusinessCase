using ParanaBanco.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Application.DTO
{
    public class PhoneDTO
    {
        public Guid Id { get; set; }
        public string AreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
    }
}

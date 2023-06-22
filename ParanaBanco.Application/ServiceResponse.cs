using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParanaBanco.Application
{
    public abstract class ServiceResponse
    {
        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        [JsonIgnore]
        public bool Success { get; set; } = true;
        [JsonIgnore]
        public string ErrorCode { get; set; }
    }
}

﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Application.DTO.Response
{
    public class AddCustomerResponse : ServiceResponse
    {
        public CustomerDTO Customer { get; set; }
    }
}

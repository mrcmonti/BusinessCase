using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ParanaBanco.Application.DTO.Request;
using ParanaBanco.Application.DTO.Response;
using ParanaBanco.Application.Interfaces.Services;
using ParanaBanco.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace ParanaBanco.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [SwaggerOperation(Summary = "Consultar todos os clientes")]
        [SwaggerResponse(200, "Ok", typeof(GetAllCustomersResponse))]
        [SwaggerResponse(500, "InternalServerError", typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetAll();
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, nameof(ErrorCode.SERVER_INTERNAL_ERROR));
            }
        }

        [SwaggerOperation(Summary = "Consultar um cliente através do DDD e número")]
        [SwaggerResponse(200, "Ok", typeof(GetCustomerResponse))]
        [SwaggerResponse(500, "InternalServerError", typeof(string))]
        [HttpGet("{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            try
            {
                var response = await _service.GetByPhone(phone);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, nameof(ErrorCode.SERVER_INTERNAL_ERROR));
            }
        }

        [SwaggerOperation(Summary = "Cadastrar um novo cliente")]
        [SwaggerResponse(200, "Ok", typeof(AddCustomerResponse))]
        [SwaggerResponse(400, "BadRequest", typeof(ValidationResult))]
        [SwaggerResponse(400, "BadRequest", typeof(string))]
        [SwaggerResponse(500, "InternalServerError", typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCustomerRequest request)
        {
            try
            {
                var response = await _service.Add(request);

                if(!response.Success)
                {
                    if (!response.ValidationResult.IsValid)
                        return BadRequest(response.ValidationResult);

                    return BadRequest(response.ErrorCode);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, nameof(ErrorCode.SERVER_INTERNAL_ERROR));
            }
        }

        [SwaggerOperation(Summary = "Atualizar e-mail do cliente", Description = "Permite atualizar um e-mail do cliente informando sua identificação e seu novo número")]
        [SwaggerResponse(200, "Ok", typeof(UpdateCustomerEmailAddressResponse))]
        [SwaggerResponse(400, "BadRequest", typeof(ValidationResult))]
        [SwaggerResponse(400, "BadRequest", typeof(string))]
        [SwaggerResponse(500, "InternalServerError", typeof(string))]
        [HttpPatch("update-email")]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateCustomerEmailAddressRequest request)
        {
            try
            {
                var response = await _service.UpdateEmailAddress(request);

                if (!response.Success)
                {
                    if (!response.ValidationResult.IsValid)
                        return BadRequest(response.ValidationResult);

                    return BadRequest(response.ErrorCode);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, nameof(ErrorCode.SERVER_INTERNAL_ERROR));
            }
        }

        [SwaggerOperation(Summary = "Atualizar o telefone do Cliente", Description = "Permite atualizar um telefone do cliente informando sua identificação e os dados de telefone")]
        [SwaggerResponse(200, "Ok", typeof(UpdateCustomerEmailAddressResponse))]
        [SwaggerResponse(400, "BadRequest", typeof(ValidationResult))]
        [SwaggerResponse(400, "BadRequest", typeof(string))]
        [SwaggerResponse(500, "InternalServerError", typeof(string))]
        [HttpPut("update-phone")]
        public async Task<IActionResult> UpdatePhone([FromBody] UpdateCustomerPhoneRequest request)
        {
            try
            {
                var response = await _service.UpdatePhone(request);

                if (!response.Success)
                {
                    if (!response.ValidationResult.IsValid)
                        return BadRequest(response.ValidationResult);

                    return BadRequest(response.ErrorCode);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, nameof(ErrorCode.SERVER_INTERNAL_ERROR));
            }
        }

        [SwaggerOperation(Summary = "Excluir Cliente", Description = "Permite a exclusão de um cliente informando seu endereço de e-mail")]
        [SwaggerResponse(200, "Ok", typeof(UpdateCustomerEmailAddressResponse))]
        [SwaggerResponse(400, "BadRequest", typeof(ValidationResult))]
        [SwaggerResponse(400, "BadRequest", typeof(string))]
        [SwaggerResponse(500, "InternalServerError", typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromQuery] string emailAddress)
        {
            try
            {
                var response = await _service.Delete(emailAddress);

                if (!response.Success)
                {
                    if (!response.ValidationResult.IsValid)
                        return BadRequest(response.ValidationResult);

                    return BadRequest(response.ErrorCode);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, nameof(ErrorCode.SERVER_INTERNAL_ERROR));
            }
        }
    }
}

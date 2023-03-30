using EmployeeSystem.Application.Common.Dto;
using EmployeeSystem.Application.Exceptions;
using EmployeeSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<IActionResult> Get( 
            [FromQuery] string? nameFilter, 
            [FromQuery] int pageNumber, 
            [FromQuery] int pageSize)
        {
            return Ok(await _employeeService.GetAllAsync(nameFilter, pageNumber, pageSize));
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _employeeService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return GenerateResponse(ex);
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeRequest employeeRequest)
        {
            try
            {
                var result = await _employeeService.CreateAsync(employeeRequest);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return GenerateResponse(ex);
            }
        }

        // PUT api/<EmployeeController>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeRequest employeeRequest)
        {
            try
            {
                var result = await _employeeService.UpdateAsync(employeeRequest);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return GenerateResponse(ex);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return GenerateResponse(ex);
            }
        }

        /// <summary>
        /// Method to handle exceptions
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Status code according to the type of the exception</returns>
        [NonAction]
        private IActionResult GenerateResponse(Exception ex)
        {
            var (statusCode, error) = ex switch
            {
                //Check first for validation errors
                IValidationException validationException => (
                    (int)validationException.StatusCode,
                    new ErrorResponse(validationException.ErrorMessage, validationException.Errors)),
                /// <summary>
                ///Check for other custom errors
                /// </summary>
                /// <returns></returns>
                IServiceException exception => (
                    (int)exception.StatusCode,
                    new ErrorResponse(exception.ErrorMessage)),
                //Handle any other exception
                _ => (StatusCodes.Status500InternalServerError, new ErrorResponse(ex.Message))
            };

            return StatusCode(statusCode, error);
        }
    }
}

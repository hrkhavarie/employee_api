using DemoWebAPI.Data;
using DemoWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees() 
        {
           var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id) 
        {
            var employee = await _employeeService.GetEmployee(id);
            if (employee == null) return NotFound();
            return Ok(employee);
           
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] TblEmployee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }

                _logger.LogInformation("Attempting to add employee: {@Employee}", employee);
                var newEmployee = await _employeeService.AddEmployee(employee);
                _logger.LogInformation("Successfully added employee with ID: {EmpId}", newEmployee.EmpId);

                return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmpId }, newEmployee);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error while adding employee");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding employee");
                return StatusCode(500, new { error = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id , [FromBody] TblEmployee employee)
        {
            if(id != employee.EmpId) return BadRequest("Employee ID mismatch.");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedEmployee = await _employeeService.UpdateEmployee(employee);
            return Ok(updatedEmployee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            if(!result) return NotFound();
     
            return NoContent();

        }
       
      
    }
}

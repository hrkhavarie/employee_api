using DemoWebAPI.Data;
using DemoWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Controllers
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newEmployee = await _employeeService.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmpId }, newEmployee);

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

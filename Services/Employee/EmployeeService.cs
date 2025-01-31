using DemoWebAPI.Data;
using DemoWebAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DemoWebAPI.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext _employeeService;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ApplicationDBContext context, ILogger<EmployeeService> logger)
        {
            _employeeService = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TblEmployee>> GetEmployees()
        {
            return await _employeeService.Employees.Include(equals => equals.Designation).ToListAsync();
        }
        public async Task<TblEmployee> GetEmployee(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _employeeService.Employees.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<TblEmployee> AddEmployee(TblEmployee employee)
        {
            try
            {
                // Log the incoming employee data
                _logger.LogInformation("Adding new employee: {@Employee}", employee);

                // Validate required fields
                if (string.IsNullOrEmpty(employee.FirstName))
                    throw new ArgumentException("FirstName is required");
                if (string.IsNullOrEmpty(employee.LastName))
                    throw new ArgumentException("LastName is required");
                if (string.IsNullOrEmpty(employee.Email))
                    throw new ArgumentException("Email is required");

                // Add the employee
                _employeeService.Employees.Add(employee);
                await _employeeService.SaveChangesAsync();

                _logger.LogInformation("Successfully added employee with ID: {EmpId}", employee.EmpId);
                return employee;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while adding employee");
                throw new Exception("Failed to add employee to database", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding employee");
                throw;
            }
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _employeeService.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _employeeService.Employees.Remove(employee);
            await _employeeService.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployee(TblEmployee employee)
        {
            var existingEmployee = await _employeeService.Employees.FindAsync(employee.EmpId);
            if (existingEmployee == null) return false;
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.EmpAge = employee.EmpAge;
            existingEmployee.IsActive = employee.IsActive;
            existingEmployee.DateOfJoining = employee.DateOfJoining;
            existingEmployee.Gender = employee.Gender;
            existingEmployee.IsMarried = employee.IsMarried;
            existingEmployee.DesId = employee.DesId;

            await _employeeService.SaveChangesAsync();
            return true;
            
        }




    }
}

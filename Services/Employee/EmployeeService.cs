using DemoWebAPI.Data;
using DemoWebAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDBContext _employeeService;

        public EmployeeService(ApplicationDBContext context)
        {
            _employeeService = context;
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

        public async Task<TblEmployee> AddEmployee( TblEmployee employee)
        {

            _employeeService.Employees.Add(employee);
            await _employeeService.SaveChangesAsync();
            return employee;
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

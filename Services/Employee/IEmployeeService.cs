using DemoWebAPI.Model;


    public interface IEmployeeService
    {
        Task<IEnumerable<TblEmployee>> GetEmployees();
        Task<TblEmployee> GetEmployee(int id);
        Task<TblEmployee> AddEmployee(TblEmployee employee);
        Task<bool> UpdateEmployee(TblEmployee employee);
        Task<bool> DeleteEmployee(int id);
    }


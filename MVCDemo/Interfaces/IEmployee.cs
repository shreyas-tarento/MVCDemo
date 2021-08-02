using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Interfaces
{
    public interface IEmployee
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int id);
        Task<bool> CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee exisitng, Employee employee);
        Task<Employee> DeleteEmployee(Employee employee);
    }
}

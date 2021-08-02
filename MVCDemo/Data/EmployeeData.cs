using Microsoft.EntityFrameworkCore;
using MVCDemo.Context;
using MVCDemo.Interfaces;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Data
{
    public class EmployeeData : IEmployee
    {
        private readonly SqlDbContext _sqlDbContext;
        public EmployeeData(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }
        public async Task<bool> CreateEmployee(Employee employee)
        {
            await _sqlDbContext.Employee.AddAsync(employee);
            await _sqlDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> DeleteEmployee(Employee employee)
        {
            _sqlDbContext.Employee.Remove(employee);
            await _sqlDbContext.SaveChangesAsync();
            return employee;
        }

        public Task<List<Employee>> GetAllEmployees()
        {
            return _sqlDbContext.Employee.ToListAsync();
        }

        public Task<Employee> GetEmployee(int id)
        {
            return _sqlDbContext.Employee.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> UpdateEmployee(Employee exisitng, Employee employee)
        {
            exisitng.FirstName = employee.FirstName;
            exisitng.LastName = employee.LastName;
            exisitng.Department = employee.Department;
            await _sqlDbContext.SaveChangesAsync();
            return employee;
        }
    }
}

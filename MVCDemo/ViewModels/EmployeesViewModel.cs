using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.ViewModels
{
    public class EmployeesViewModel
    {
        public List<Employee> Employees { get; set; }
        public String Title { get; set; }
    }
}

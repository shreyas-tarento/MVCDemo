using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Context;
using MVCDemo.Interfaces;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _context;

        public EmployeesController(IEmployee context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllEmployees());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _context.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET:Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var exisitingEmployee = await _context.GetEmployee(id);
                await _context.UpdateEmployee(exisitingEmployee, employee);

                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            await _context.DeleteEmployee(employee);
            return RedirectToAction("Index");
        }
    }
}

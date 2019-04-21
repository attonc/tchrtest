using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TcHrTest.Data;
using TcHrTest.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TcHrTest.Controllers
{
    public class HrController : Controller
    {
        private readonly HrContext _context;

        public HrController(HrContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Employees.Include(emp => emp.Department).Include(emp => emp.Position).ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Include(emp => emp.Department).Include(emp => emp.Position).FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            foreach( var emp in _context.Employees)
            {
                if(emp.ManagerId == id)
                {
                    emp.ManagerId = 1;
                }
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index"); ;
        }

        public IActionResult Edit(int? id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var copy = new List<Employee>(_context.Employees);
            copy.Remove(employee);
            ViewBag.ManagerId = new SelectList(copy, "Id", "Name", employee.ManagerId);
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.PositionId = new SelectList(_context.Positions, "Id", "Title", employee.PositionId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(employee).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(_context.Employees, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.PositionId = new SelectList(_context.Positions, "Id", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ManagerId = new SelectList(_context.Employees, "Id", "Name");
                ViewBag.DepartmentId = new SelectList(_context.Departments, "Id", "Name");
                ViewBag.PositionId = new SelectList(_context.Positions, "Id", "Title");
                return View(employee);
            }
        }
    }
}

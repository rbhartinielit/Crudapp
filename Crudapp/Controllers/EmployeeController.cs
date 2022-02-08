using Crudapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crudapp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _Db;

        public EmployeeController(EmployeeContext db)
        {
            _Db = db;
        }
        public IActionResult Emplist()
        {
            try
            {
                //var empl = _Db.employee.ToList();
                var empl = from a in _Db.employee
                           join b in _Db.dept
                           on a.deptid equals b.id
                           into Dep
                           from b in Dep.DefaultIfEmpty()

                           select new Employee
                 {
                    id = a.id,
                    ename = a.ename,
                    fname = a.fname,
                    email = a.email,
                    mobile = a.mobile,
                    descr = a.descr,
                    Department =b==null?"":b.Department


                };
                


                return View(empl);
            }
            catch(Exception ex)
            {
                return View();
            }
           
        }
        public IActionResult Create(Employee obj)
        {
            loadddl();
            return View(obj);
        }
        [HttpPost]

        public async Task<IActionResult> AddEmployee(Employee obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.id == 0)
                    {
                        _Db.employee.Add(obj);
                        await _Db.SaveChangesAsync();
                    }else
                    {
                        _Db.Entry(obj).State = EntityState.Modified;
                        await _Db.SaveChangesAsync();
                    }
                    return RedirectToAction("Emplist");

                }
               return View();

            }
            catch
            {
                return RedirectToAction("Emplist");
            }
        }
        public async Task<IActionResult> DeleteEmp(int id)
        {
            try
            {
                var emp = await _Db.employee.FindAsync(id);
                if (emp!=null)
                {
                    _Db.employee.Remove(emp);
                    await _Db.SaveChangesAsync();
                }
                return RedirectToAction("Emplist");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Emplist");
            }
        }
        private void loadddl()
        {
            try
            {
                List<dept> deplist = new List<dept>();
                deplist = _Db.dept.ToList();
                deplist.Insert(0, new dept { id = 0, Department = "pl select" });
                ViewBag.Deplist = deplist;


            }
            catch
            {

            }
        }

        private IActionResult RedirectToAction(Func<IActionResult> emplist)
        {
            throw new NotImplementedException();
        }
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCoreProject_Iqbal.Data;
using MvcCoreProject_Iqbal.Models;

namespace MvcCoreProject_Iqbal.Controllers
{
    public class TeacherController : Controller
    {
        private ITeacherRepository db1;

        private ICourseRepository db2;

        private readonly ApplicationDbContext _context;


        public TeacherController(ITeacherRepository db1, ICourseRepository db2, ApplicationDbContext _context)
        {
            this.db1 = db1;
            this.db2 = db2;

            this._context = _context;
        }
        public IActionResult Index()
        {


            var applicationDbContext = _context.Courses.ToList();

            return View(db1.GetAll());
        }



        // GET:Create
        public IActionResult Create()
        {
            ViewBag.CourseID = db2.GetAll();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.CourseID = db2.GetAll();
            db1.Add(teacher);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            db1.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var applicationDbContext = _context.Courses.ToList();
            ViewBag.CourseID = db2.GetAll();

            return View(db1.GetTeacher(id));
        }

        [HttpPost]
        public IActionResult Edit(Teacher teacher)
        {
            ViewBag.CourseID = db2.GetAll();
            db1.Update(teacher);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db1.GetTeacher(id));
        }
    }
}
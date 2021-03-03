using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCoreProject_Iqbal.Models;

namespace MvcCoreProject_Iqbal.Controllers
{
    public class CourseController : Controller
    {
        private ICourseRepository db;

        public CourseController(ICourseRepository db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.GetAll());
        }



        // GET:Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {

            db.Add(course);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(db.GetCourse(id));
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            db.Update(course);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db.GetCourse(id));
        }
    }
}
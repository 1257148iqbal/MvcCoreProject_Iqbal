using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCoreProject_Iqbal.Data;
using MvcCoreProject_Iqbal.Models;

namespace MvcCoreProject_Iqbal.Controllers
{
    public class SchoolClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readonly IHostingEnvironment _hostingEnvironment;

        public SchoolClassController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.CategoryID = new SelectList(_context.SchoolClasses, "ID", "Name");
            return View(await _context.SchoolClasses.ToListAsync());
        }
        public ActionResult GetSchoolClassStudents(long? id)
        {
            if (id == null)
            {
                NotFound();
            }

            ViewData["id"] = id;
            List<Student> students = _context.Students.Where(e => e.SchoolId == id).ToList();

            if (students == null)
            {
                NotFound();
            }

            return PartialView("SchoolClassStudents", students);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student students, SchoolClass schoolClass, IFormFile[] Image)
        {
            try
            {

                if (Image != null)
                {
                    if (schoolClass.Students.Count == Image.Count())
                    {
                        for (int i = 0; i < schoolClass.Students.Count; i++)
                        {

                            string picture = System.IO.Path.GetFileName(Image[i].FileName);
                            var file = picture;
                            var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images", picture);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                Image[i].CopyTo(ms);
                                schoolClass.Students[i].Image = ms.GetBuffer();
                            }
                        }
                    }
                    _context.SchoolClasses.Add(schoolClass);
                    _context.SaveChanges();
                    TempData["id"] = schoolClass.ID;
                    return RedirectToAction("Index");
                }

                return View(schoolClass);
            }
            catch (Exception)
            {
                return View(schoolClass);
            }
        }

        public IActionResult Edit(long id)
        {
            SchoolClass schoolClass = _context.SchoolClasses.Find(id);
            if (schoolClass != null)
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Student student, SchoolClass schoolClass, IFormFile[] Image)
        {
            try
            {

                if (Image != null)
                {
                    if (schoolClass.Students.Count == Image.Count())
                    {
                        for (int i = 0; i < schoolClass.Students.Count; i++)
                        {

                            string picture = System.IO.Path.GetFileName(Image[i].FileName);
                            var file = picture;
                            var uploadFile = Path.Combine(_hostingEnvironment.WebRootPath, "images", picture);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                Image[i].CopyTo(ms);
                                schoolClass.Students[i].Image = ms.GetBuffer();
                            }
                        }
                    }
                    _context.SchoolClasses.Add(schoolClass);
                    _context.SaveChanges();
                    TempData["id"] = schoolClass.ID;
                    return RedirectToAction("Index");
                }

                return View(schoolClass);
            }
            catch (Exception)
            {
                return View(schoolClass);
            }
        }

        public IActionResult Delete(long id)
        {
            SchoolClass schoolClass = _context.SchoolClasses.Find(id);
            if (schoolClass != null)
            {
                _context.SchoolClasses.Remove(schoolClass);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(long id)
        {
            Student student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCoreProject_Iqbal.Data;
using MvcCoreProject_Iqbal.Models;

namespace MvcCoreProject_Iqbal.Controllers
{
    public class TrainersController : Controller
    {
        
        private ITrainerRepository db;
        private readonly IHostingEnvironment db2;

        public TrainersController(ITrainerRepository db, IHostingEnvironment db2)
        {
            this.db = db;
            this.db2 = db2;

        }

        public IActionResult Index()
        {
            return View(db.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Trainer _trainer)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";

                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(db2.WebRootPath, "Uploads");
                        if (file.Length > 0)
                        {
                            
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                UrlImage = fileName;

                            }

                        }
                    }
                }

                var data = new Trainer()
                {
                    TrainerName = _trainer.TrainerName,
                    JoinDate = _trainer.JoinDate,
                    Salary = _trainer.Salary,
                    UrlImage = UrlImage,
                };

                db.Add(data);
                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Index");
        }



        public IActionResult Edit(int id)
        {
            return View(db.GetProduct(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<ActionResult> Edit(int id, Trainer _trainer)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(db2.WebRootPath, "Uploads");
                        if (file.Length > 0)
                        {
                           
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                UrlImage = fileName;
                            }

                        }
                    }
                }
                var data = db.GetProduct(id);
                data.TrainerName = _trainer.TrainerName;
                data.JoinDate = _trainer.JoinDate;
                data.Salary = _trainer.Salary;
                data.UrlImage = UrlImage;

                db.Update(data);
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            return View(db.GetProduct(id));

        }

        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcCoreProject_Iqbal.Data;
using MvcCoreProject_Iqbal.Models;
using MvcCoreProject_Iqbal.ViewModels;

namespace MvcCoreProject_Iqbal.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment webHostEnvironment;
        public SpeakersController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            db = context;
            webHostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index(
      string sortOrder,
      string currentFilter,
      string searchString,
      int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var instractors = from s in db.Speakers
                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                instractors = instractors.Where(s => s.SpeakerName.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    instractors = instractors.OrderByDescending(s => s.SpeakerName);
                    break;
                case "Date":
                    instractors = instractors.OrderBy(s => s.SpeakingDate);
                    break;
                case "date_desc":
                    instractors = instractors.OrderByDescending(s => s.SpeakerName);
                    break;
                default:
                    instractors = instractors.OrderBy(s => s.SpeakerName);
                    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Speaker>.CreateAsync(instractors.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await instractors.AsNoTracking().ToListAsync());
        }


        //public async Task<IActionResult> Index()
        //{
        //    return View(await db.Speakers.ToListAsync());
        //}

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await db.Speakers
                .FirstOrDefaultAsync(m => m.Id == id);

            var speakerViewModel = new SpeakerViewModel()
            {
                Id = speaker.Id,
                SpeakerName = speaker.SpeakerName,
                Qualification = speaker.Qualification,
                Experience = speaker.Experience,
                SpeakingDate = speaker.SpeakingDate,
                SpeakingTime = speaker.SpeakingTime,
                Venue = speaker.Venue,
                ExistingImage = speaker.ProfilePicture
            };

            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        [Authorize(Policy = "CreateRolePolicy")]


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpeakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Speaker speaker = new Speaker
                {
                    SpeakerName = model.SpeakerName,
                    Qualification = model.Qualification,
                    Experience = model.Experience,
                    SpeakingDate = model.SpeakingDate,
                    SpeakingTime = model.SpeakingTime,
                    Venue = model.Venue,
                    ProfilePicture = uniqueFileName
                };

                db.Add(speaker);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Policy = "EditRolePolicy")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await db.Speakers.FindAsync(id);
            var speakerViewModel = new SpeakerViewModel()
            {
                Id = speaker.Id,
                SpeakerName = speaker.SpeakerName,
                Qualification = speaker.Qualification,
                Experience = speaker.Experience,
                SpeakingDate = speaker.SpeakingDate,
                SpeakingTime = speaker.SpeakingTime,
                Venue = speaker.Venue,
                ExistingImage = speaker.ProfilePicture
            };

            if (speaker == null)
            {
                return NotFound();
            }
            return View(speakerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpeakerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var speaker = await db.Speakers.FindAsync(model.Id);
                speaker.SpeakerName = model.SpeakerName;
                speaker.Qualification = model.Qualification;
                speaker.Experience = model.Experience;
                speaker.SpeakingDate = model.SpeakingDate;
                speaker.SpeakingTime = model.SpeakingTime;
                speaker.Venue = model.Venue;

                if (model.SpeakerPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    speaker.ProfilePicture = ProcessUploadedFile(model);
                }
                db.Update(speaker);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Policy = "DeleteRolePolicy")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await db.Speakers
                .FirstOrDefaultAsync(m => m.Id == id);

            var speakerViewModel = new SpeakerViewModel()
            {
                Id = speaker.Id,
                SpeakerName = speaker.SpeakerName,
                Qualification = speaker.Qualification,
                Experience = speaker.Experience,
                SpeakingDate = speaker.SpeakingDate,
                SpeakingTime = speaker.SpeakingTime,
                Venue = speaker.Venue,
                ExistingImage = speaker.ProfilePicture
            };
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speakerViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speaker = await db.Speakers.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", speaker.ProfilePicture);
            db.Speakers.Remove(speaker);
            if (await db.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return db.Speakers.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(SpeakerViewModel model)
        {
            string uniqueFileName = null;

            if (model.SpeakerPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SpeakerPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SpeakerPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}

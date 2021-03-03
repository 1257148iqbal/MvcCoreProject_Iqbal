using MvcCoreProject_Iqbal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class CourseRepository: ICourseRepository
    {
        private readonly ApplicationDbContext db;
        public CourseRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Course Add(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();

            return course;
        }

        public Course Delete(int id)
        {
            Course course = db.Courses.Find(id);
            if (course != null)
            {
                db.Courses.Remove(course);
                db.SaveChanges();
            }
            return course;
        }

        public IEnumerable<Course> GetAll()
        {
            return db.Courses;
        }

        public Course GetCourse(int id)
        {
            return db.Courses.Where(x => x.CourseID == id).SingleOrDefault();
        }

        public Course Update(Course course)
        {
            db.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return course;
        }
    }
}

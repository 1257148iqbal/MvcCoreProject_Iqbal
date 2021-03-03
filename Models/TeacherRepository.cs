using MvcCoreProject_Iqbal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class TeacherRepository: ITeacherRepository
    {
        private readonly ApplicationDbContext db;
        public TeacherRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Teacher Add(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();

            return teacher;
        }

        public Teacher Delete(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher != null)
            {
                db.Teachers.Remove(teacher);
                db.SaveChanges();
            }
            return teacher;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return db.Teachers;
        }

        public Teacher GetTeacher(int id)
        {
            return db.Teachers.Where(x => x.TeacherID == id).SingleOrDefault();
        }

        public Teacher Update(Teacher teacher)
        {
            db.Entry(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return teacher;
        }
    }
}

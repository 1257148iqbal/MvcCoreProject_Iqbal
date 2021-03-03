using Microsoft.EntityFrameworkCore;
using MvcCoreProject_Iqbal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class TrainerRepository:ITrainerRepository
    {
        private readonly ApplicationDbContext db;


        public TrainerRepository(ApplicationDbContext db)
        {
            this.db = db;
        }


        public Trainer Add(Trainer trainer)
        {

            db.Trainers.Add(trainer);
            db.SaveChanges();
            return trainer;
        }


        public Trainer Delete(int id)
        {
            Trainer trainer = db.Trainers.Find(id);
            if (trainer != null)
            {
                db.Trainers.Remove(trainer);
                db.SaveChanges();
            }
            return trainer;

        }

        public IEnumerable<Trainer> GetAll()
        {
            return db.Trainers;
        }

        public Trainer GetProduct(int id)
        {
            return db.Trainers.Where(x => x.TrainerID == id).SingleOrDefault();
        }



        public Trainer Update(Trainer trainer)
        {
            db.Entry(trainer).State = EntityState.Modified;
            db.SaveChanges();
            return trainer;
        }

    }
}

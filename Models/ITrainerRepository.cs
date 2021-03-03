using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public interface ITrainerRepository
    {
        Trainer GetProduct(int id);

        IEnumerable<Trainer> GetAll();

        Trainer Add(Trainer trainer);

        Trainer Update(Trainer trainer);

        Trainer Delete(int id);
    }
}

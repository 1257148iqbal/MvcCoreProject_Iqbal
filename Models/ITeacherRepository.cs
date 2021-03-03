using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public interface ITeacherRepository
    {
        Teacher GetTeacher(int id);

        IEnumerable<Teacher> GetAll();

        Teacher Add(Teacher teacher);
        Teacher Update(Teacher teacher);
        Teacher Delete(int id);
    }
}

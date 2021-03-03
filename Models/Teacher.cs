using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }


        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string CellPhone { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}

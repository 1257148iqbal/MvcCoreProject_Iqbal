using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        public string CourseName { get; set; }


        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}

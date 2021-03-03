using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class SchoolClass
    {
        [Key]
        public long ID { get; set; }

        public string Name { get; set; }

        public virtual IList<Student> Students { get; set; }
    }
}

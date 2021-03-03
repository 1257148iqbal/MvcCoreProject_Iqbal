using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class Student
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public byte[] Image { get; set; }

        [DataType(DataType.Date)]

        public DateTime EntryDate { get; set; }

        [Required]
        public long Fee { get; set; }
        public long SchoolId { get; set; }


        public virtual SchoolClass SchoolClass { get; set; }
    }
}

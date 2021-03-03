using MvcCoreProject_Iqbal.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class Trainee
    {
        [Key]
        public int TraineeId { get; set; }
        [PersonalValidation]
        [Column(TypeName = "nvarchar(12)")]
        [DisplayName("Round")]
        [Required(ErrorMessage = "Must be filled Round")]
        [MaxLength(12, ErrorMessage = "Maximum 12 characters only")]
        public string Round { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Course Name")]
        [Required(ErrorMessage = "Please input Course name")]
        public string CourseName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("T.S.P Name")]
        [Required(ErrorMessage = "Please input T.S.P name")]
        public string TSPName { get; set; }


        [PersonalValidation2]
        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("Batch Number")]
        [Required(ErrorMessage = "please input your Batch Number.")]
        [MaxLength(11)]
        public string BatchNumber { get; set; }

        [DisplayName("Amount")]
        [Required(ErrorMessage = "must be filled amount.")]
        public int Amount { get; set; }

        [HireDate]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Date { get; set; }
    }
}
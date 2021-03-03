﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class Trainer
    {
        public int TrainerID { get; set; }
        [Required(ErrorMessage = "This Field is Required!")]

        public string TrainerName { get; set; }

        [Required(ErrorMessage = "This Field is Required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }

        public string UrlImage { get; set; }

        [NotMapped]
        public IFormFile UploadImage { get; set; }
    }
}

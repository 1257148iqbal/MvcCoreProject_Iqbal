using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace MvcCoreProject_Iqbal.CustomValidation
{
    public class HireDate : ValidationAttribute
    {

        public HireDate():base("{0} Should Less Than Current Date")
        {

        }
        public override bool IsValid(object value)
        {
            DateTime propValue = Convert.ToDateTime(value);
            if (propValue <= DateTime.Now)
                return true;
            else
                return false;
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourceManagementSystem.DateValidation
{
    public class greaterdateAttribute : ValidationAttribute
    {
        public greaterdateAttribute() : base("{0} Date should greater then Current date")
        {

        }

        public override bool IsValid(object value)
        {
            DateTime date =Convert.ToDateTime(value);
            if (date>DateTime.Now)
            {
                return true;
            }

            return false;
        }
    }
    
}
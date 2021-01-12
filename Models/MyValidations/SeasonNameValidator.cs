using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace T_ShirtStore.Models.MyValidations
{
    public class SeasonNameValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var season = (Season)validationContext.ObjectInstance;
            string seasonName = season.SeasonName;
            bool cond = true;
            Regex regex = new Regex(@"^([1-9]\d+)$");
            if (seasonName != null)
            {
                if (seasonName.Length != 9)
                    cond = false;
                else
                {
                    string firstYear = seasonName.Substring(0, 4);
                    string secondYear = seasonName.Substring(5, 4);
                    string slash = seasonName.Substring(4, 1);
                    if (slash != "/")
                    {
                        cond = false;
                    }
                    else
                    {
                        if (!regex.IsMatch(firstYear) || !regex.IsMatch(secondYear))
                        {
                            cond = false;
                        }
                    }
                }
            }
            else
            { 
                cond = false;
            }
            return cond ? ValidationResult.Success : new ValidationResult("The season must be year/year!");
        }
    }
}
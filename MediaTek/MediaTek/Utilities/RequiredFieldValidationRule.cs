using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Reflection;

namespace MediaTek.Utilities
{
    public class RequiredFieldValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool valid = true;
            if (value == null)
            {
                valid = false;
            }
            else if (value is string)
            {
                valid = !String.IsNullOrEmpty(value as string);
            }
            else if (valid.GetType().FullName == "System.Nullable`1")
            {
                Type t = valid.GetType();
                PropertyInfo prop = t.GetProperty("HasValue");
                valid = (bool)prop.GetValue(value, null);
            }

            return new ValidationResult(valid, "This is a required field");
        }
    }
}

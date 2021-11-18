//Authors: Gowridevi Akasapu, Anju Thomas, Apurva Jain, Aditya Gupta, Emmanuel Kevin, Ravali Chilucoti, Amritpal Singh
//Copy Rights:      Conestoga College
//Group Number:     4

using System;
using System.Globalization;
using System.Windows.Controls;

namespace BeeBreeding
{
    public class NumericValidationRule : ValidationRule
    {
        private const string REQUIRED_FIELD_ERROR = "Please enter a valid maggot cell number";
        private const string RANGE_ERROR = "Please enter valid maggot cell range";
        private const string INPUT_DATA_ERROR = "Please enter a number between 1 to 10000";
        private const string TYPE_ERROR = "Unsupported Validation Type!";
        public string ValidationType { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
                return new ValidationResult(false, INPUT_DATA_ERROR);

            if (value is string v && int.TryParse(v, out var cell))
            {
                if (cell > Max)
                    return new ValidationResult(false, RANGE_ERROR);
            }

            switch (ValidationType)
            {
                case "Positive_Int":
                    bool canConvert = int.TryParse(strValue, out int intVal);
                    if (canConvert)
                        canConvert = intVal > 0;
                    return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, REQUIRED_FIELD_ERROR);
                default:
                    throw new InvalidCastException(TYPE_ERROR);
            }
        }
    }
}

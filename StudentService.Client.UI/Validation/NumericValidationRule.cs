using System.Windows.Controls;

namespace StudentService.Client.UI.Validation
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is string str && int.TryParse(str, out _))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Введите число");
        }
    }
}

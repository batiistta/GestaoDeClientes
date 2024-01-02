using System.Globalization;
using System.Windows.Controls;

namespace GestaoDeClientes.UI.Views
{
    public class MinLengthValidationRule : ValidationRule
    {
        public int MinLength { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length < MinLength || value.ToString() is null)
            {
                return new ValidationResult(false, "O campo deve conter no mínimo " + MinLength + " caracteres.");
            }                
            else if (IsTextOnly(value.ToString()))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Somente texto é permitido no campo.");
            }
        }

        private bool IsTextOnly(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z]+$");
        }

    }
}

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
                return new ValidationResult(false, "O campo deve conter no mínimo " + MinLength + " caracteres.");
            else
                return ValidationResult.ValidResult;
        }
    }
}

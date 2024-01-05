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

    public class TelefoneValidationRule : ValidationRule
    {
        private int MinLength = 15;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string strValue)
            {
                if (strValue.Length < MinLength || strValue is null)
                    return new ValidationResult(false, "O telefone deve conter no mínimo 10 números.");
                else
                    return ValidationResult.ValidResult;
            }

            return ValidationResult.ValidResult;
        }
    }

    public class EnderecoValidationRule : ValidationRule
    {
        public int MinLength { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string strValue)
            {
                if (strValue.Length < MinLength || strValue is null)
                    return new ValidationResult(false, "O endereço deve conter no mínimo " + MinLength +" caracteres.");
                else
                    return ValidationResult.ValidResult;
            }

            return ValidationResult.ValidResult;
        }        
    }
}

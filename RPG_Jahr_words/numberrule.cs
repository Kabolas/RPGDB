using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RPG_Jahr_words
{
    public class NumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse((string)value, out int check)) return new ValidationResult(true, null);
            return new ValidationResult(false, "Votre entrée n'est pas un entier");
        }
    }

    public class DoubleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (double.TryParse((string)value, out double check)) return new ValidationResult(true, null);
            return new ValidationResult(false, "Votre entrée n'est pas un nombre");
        }
    }

    public class IntUnder : ValidationRule
    {
        private int _max;

        public string Max { get => ""+_max; set => _max = int.Parse(value); }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse((string)value, out int check))
                if (check < _max) return new ValidationResult(true, null);
                else return new ValidationResult(false, "Votre entier est trop grand.");
            else return new ValidationResult(false, "Votre entrée n'est pas un entier.");
        }
    }
}

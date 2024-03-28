using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ボタンのスタイルをいじってみる.Validations {
    public class IntValidationAttribute : ValidationAttribute {
        public override bool IsValid(object value)
            => int.TryParse(value.ToString(), out var _);
    }
}

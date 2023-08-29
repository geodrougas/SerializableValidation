using SerializableValidator.Models;
using SerializableValidator.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializableValidator.Validators.Checks
{
    public abstract class BaseCheck : ISerializedValidator
    {
        public abstract int Type { get; }

        public string Field { get; set; }
        public IComparable Value { get; set; }
        public string? Message { get; set; }

        public BaseCheck(string field, IComparable value, string? message)
        {
            Field = field;
            Value = value;
            Message = message;
        }

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs)
        {
            ValidationResult result = new ValidationResult();
            var exists = keyValuePairs.TryGetValue(Field, out var value);

            if (!exists)
            {
                result.AddError("Value targeted by Field ({0}) not found", Field);
                return result;
            }

            if (value is not IComparable comparable)
            {
                result.AddError("Cannot compare values from typed that don't implement the IComparable interface ({0})", value?.GetType().Name);
                return result;
            }

            IComparable tempValue = Value;
            Type comparableType = comparable.GetType();
            if (comparableType != tempValue.GetType())
            {
                try
                {
                    tempValue = (IComparable)Convert.ChangeType(Value, comparableType);
                }
                catch (Exception) { }
            }

            try
            {
                result += Comparison(comparable, tempValue);
            }
            catch (ArgumentException ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        public abstract ValidationResult Comparison(IComparable entity, IComparable validators);
    }
}

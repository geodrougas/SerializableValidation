using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using SerializableValidator.Serialization.Factories.Checks;

namespace SerializableValidator.Validators.Checks
{
    [TypedValidatorFactory<IsNullCheckFactory>(80)]
    public class IsNullCheck : ISerializedValidator
    {
        public string Field { get; set; }
        public string? Message { get; set; }

        public IsNullCheck(string field, string? message)
        {
            Field = field;
            Message = message;
        }

        public int Type => 80;

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs)
        {
            ValidationResult result = new ValidationResult();
            var exists = keyValuePairs.TryGetValue(Field, out var value);

            if (exists && value != null)
            {
                result.AddError(Message ?? "");
            }

            return result;
        }
    }
}

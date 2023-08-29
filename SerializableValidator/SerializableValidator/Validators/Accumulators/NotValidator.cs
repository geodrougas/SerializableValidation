

using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using SerializableValidator.Serialization.Factories.Accumulators;

namespace SerializableValidator.Validators.Accumulators
{
    [TypedValidatorFactory<NotValidatorFactory>(30)]
    public class NotValidator : ISerializedValidator
    {
        public int Type => 30;

        public string? Message { get; set; }
        public ISerializedValidator Validator { get; set; }

        public NotValidator(ISerializedValidator validator, string? message)
        {
            Validator = validator;
            Message = message;
        }

        ValidationResult ISerializedValidator.Validate(IDictionary<string, object> keyValuePairs)
        {
            var result = new ValidationResult();

            var newResult = Validator.Validate(keyValuePairs);

            if (newResult.IsValid)
            {
                result.AddError(Message ?? "");
            }

            return result;
        }
    }
}

using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using SerializableValidator.Serialization.Factories.Accumulators;

namespace SerializableValidator.Validators.Accumulators
{
    [TypedValidatorFactory<OrValidatorFactory>(32)]
    public partial class OrValidator : ISerializedValidator
    {
        public int Type => 32;

        public List<ISerializedValidator> Validators { get; set; }

        public OrValidator(List<ISerializedValidator> validators)
        {
            Validators = validators;
        }

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs)
        {
            var result = new ValidationResult();

            foreach (var validator in Validators)
            {
                var newResult = validator.Validate(keyValuePairs);

                if (newResult.IsValid)
                {
                    return newResult;
                }

                result += newResult;
            }

            return result;
        }
    }
}

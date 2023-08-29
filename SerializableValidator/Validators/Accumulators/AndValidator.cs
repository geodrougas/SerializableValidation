using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using SerializableValidator.Serialization.Factories.Accumulators;

namespace SerializableValidator.Validators.Accumulators
{
    [TypedValidatorFactory<AndValidatorFactory>(31)]
    public partial class AndValidator : ISerializedValidator
    {
        public int Type => 31;

        public List<ISerializedValidator> Validators { get; set; }

        public AndValidator(List<ISerializedValidator> validators)
        {
            Validators = validators;
        }

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs)
        {
            var result = new ValidationResult();

            foreach (var validator in Validators)
            {
                var newResult = validator.Validate(keyValuePairs);

                result += newResult;
            }

            return result;
        }
    }
}

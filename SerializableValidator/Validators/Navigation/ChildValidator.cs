using SerializableValidator.Analyzers;
using SerializableValidator.Models;
using SerializableValidator.Models.Interface;

namespace SerializableValidator.Validators.Navigation
{
    public class ChildValidator
    {
        public int Type => 26;

        public string Name { get; set; }

        public ISerializedValidator Validator { get; set; }

        public ChildValidator(string name, ISerializedValidator validator)
        {
            Name = name;
            Validator = validator;
        }

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs)
        {
            var validationResult = new ValidationResult();

            var exists = keyValuePairs.TryGetValue(Name, out var property);

            if (!exists || property is null)
            {
                validationResult.AddError("Property {0} is missing", Name);
                return validationResult;
            }

            try
            {
                var result = new ModelAnalyzer(property).GetKeyValues().ToDictionary(m => m.Key, m => m.Value!);

                var newValidation = Validator.Validate(result);

                validationResult += newValidation;
            }
            catch (Exception ex)
            {
                validationResult.AddError(ex.Message);
            }

            return validationResult;
        }
    }
}

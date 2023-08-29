using SerializableValidator.Analyzers;
using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using SerializableValidator.Serialization.Factories.Navigation;
using System.Collections;

namespace SerializableValidator.Validators.Navigation
{
    [TypedValidatorFactory<ForEachChildValidatorFactory>(25)]
    public class ForEachChildValidator : ISerializedValidator
    {
        public int Type => 25;

        public string Name { get; set; }

        public bool IsAll { get; set; }

        public ISerializedValidator Validator { get; set; }

        public ForEachChildValidator(string name, bool isAll, ISerializedValidator validator)
        {
            Name = name;
            IsAll = isAll;
            Validator = validator;
        }

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs)
        {
            var validationResult = new ValidationResult();

            var exists = keyValuePairs.TryGetValue(Name, out var property);

            if (!exists)
            {
                validationResult.AddError("Property {0} is missing", Name);
            }

            if (!typeof(IEnumerable).IsAssignableFrom(property!.GetType()))
            {
                validationResult.AddError("Property was of the wrong type.");
                return validationResult;
            }

            foreach (var item in (IEnumerable)property)
            {
                var result = new ModelAnalyzer(item).GetKeyValues().ToDictionary(m => m.Key, m => m.Value!);

                var newValidation = Validator.Validate(result);

                if (!IsAll && newValidation.IsValid)
                {
                    return newValidation;
                }

                validationResult += newValidation;
            }

            return validationResult;
        }
    }
}

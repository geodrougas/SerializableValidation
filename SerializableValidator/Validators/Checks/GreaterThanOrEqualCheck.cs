using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using SerializableValidator.Serialization.Factories.Checks;

namespace SerializableValidator.Validators.Checks
{
    [TypedValidatorFactory<BaseValidatorFactory>(73)]
    public class GreaterThanOrEqualCheck : BaseCheck, ISerializedValidator
    {
        public GreaterThanOrEqualCheck(string field, IComparable value, string? message) : base(field, value, message)
        {
        }

        public override int Type => 73;

        public override ValidationResult Comparison(IComparable entity, IComparable validators)
        {
            ValidationResult result = new ValidationResult();
            if (entity.CompareTo(validators) < 0)
            {
                result.AddError(Message ?? "");
            }
            return result;
        }
    }
}

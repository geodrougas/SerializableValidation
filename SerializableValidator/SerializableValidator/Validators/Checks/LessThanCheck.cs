using SerializableValidator.Models;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Serialization.Factories.Checks;

namespace SerializableValidator.Validators.Checks
{
    [TypedValidatorFactory<BaseValidatorFactory>(72)]
    public class LessThanCheck : BaseCheck
    {
        public LessThanCheck(string field, IComparable value, string? message) : base(field, value, message)
        {
        }

        public override int Type => 72;

        public override ValidationResult Comparison(IComparable entity, IComparable validators)
        {
            ValidationResult result = new ValidationResult();
            if (entity.CompareTo(validators) >= 0)
            {
                result.AddError(Message ?? "");
            }
            return result;
        }
    }
}

using SerializableValidator.Models.Interface;

namespace SerializableValidator.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TypedValidatorFactoryAttribute<T> : ValidatorFactoryAttribute
        where T : IValidatorFactory
    {
        public TypedValidatorFactoryAttribute(int Type) : base(Type, typeof(T))
        {
        }
    }
}

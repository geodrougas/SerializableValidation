using SerializableValidator.Models.Interface;

namespace SerializableValidator.Models.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidatorFactoryAttribute : Attribute
    {
        public ValidatorFactoryAttribute(int Type, Type type)
        {
            this.Type = Type;
            var emptyConstructor = type.GetConstructor(new Type[0]);
            if (emptyConstructor == null)
                throw new ArgumentException("Empty constructor not found");
            Generator = (IValidatorFactory)emptyConstructor.Invoke(new object[0]);
        }

        public int Type { get; }
        public IValidatorFactory Generator { get; }
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options)
        {
            return Generator.Generate(options);
        }
    }
}

namespace SerializableValidator.Models.Interface
{
    public interface IValidatorFactory
    {
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options);
    }
}

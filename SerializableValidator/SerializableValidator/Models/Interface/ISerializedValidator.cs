namespace SerializableValidator.Models.Interface
{
    public interface ISerializedValidator
    {
        public int Type { get; }

        public ValidationResult Validate(IDictionary<string, object> keyValuePairs);
    }
}

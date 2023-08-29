using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SerializableValidator.Models.Interface;

namespace SerializableValidator.Serialization
{
    public class ValidatorDeserializer
    {
        [JsonExtensionData]
        public JObject? Value;

        public ValidatorDeserializer()
        {
            Value = null;
        }

        public ValidatorDeserializer(JObject obj)
        {
            Value = obj;
        }

        public ISerializedValidator? GetValidator()
        {
            return ModelFactory.Create(Value);
        }
    }
}
using Newtonsoft.Json.Linq;
using SerializableValidator.Models.Interface;
using SerializableValidator.Validators.Accumulators;
using System.Collections.Generic;
using System.Reflection;

namespace SerializableValidator.Serialization.Factories.Accumulators
{
    public class AndValidatorFactory : IValidatorFactory
    {
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options)
        {
            List<ISerializedValidator>? validators = null;

            foreach (var keyValuePair in options)
            {
                switch (keyValuePair.Key)
                {
                    case "Validators":
                        validators = ((JArray)keyValuePair.Value).Select(m => ModelFactory.Create(m.ToObject<JObject>())).Where(m => m is not null).ToList()!;
                        break;
                }
            }

            if (validators == null || validators.Count == 0)
                throw new ArgumentException(nameof(validators));

            return new AndValidator(validators);
        }
    }
}

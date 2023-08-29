using Newtonsoft.Json.Linq;
using SerializableValidator.Models.Attributes;
using SerializableValidator.Models.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SerializableValidator
{
    public class ModelFactory
    {
        private static readonly IDictionary<int, Func<IEnumerable<KeyValuePair<string, object>>, ISerializedValidator>> ValidatorFactories = GenerateValidatorFactories();

        internal static ISerializedValidator? Create(JObject? value)
        {
            if (value == null)
                return null;

            var dictionary = value.ToObject<Dictionary<string, object>>();

            if (dictionary == null)
                return null;

            var typePair = dictionary["Type"];

            if (Equals(typePair, default))
                throw new ArgumentException("Object type field is missing");

            var type = (int)Convert.ChangeType(typePair, typeof(int));

            return ValidatorFactories[type].Invoke(dictionary);
        }

        private static IDictionary<int, Func<IEnumerable<KeyValuePair<string, object>>, ISerializedValidator>> GenerateValidatorFactories()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Select(m => m.GetCustomAttribute<ValidatorFactoryAttribute>())
                .Where(m => m is not null)
                .ToDictionary(m => m!.Type, m => { return new Func<IEnumerable<KeyValuePair<string, object>>, ISerializedValidator>((x) => m!.Generate(x)); });
        }
    }
}

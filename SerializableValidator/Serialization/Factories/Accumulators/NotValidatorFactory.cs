using Newtonsoft.Json.Linq;
using SerializableValidator.Models.Interface;
using SerializableValidator.Validators.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializableValidator.Serialization.Factories.Accumulators
{
    public class NotValidatorFactory : IValidatorFactory
    {
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options)
        {
            string? message = null;
            ISerializedValidator? notValidator = null;

            foreach (var kvp in options)
            {
                switch (kvp.Key)
                {
                    case "Message":
                        message = (string)kvp.Value;
                        break;
                    case "Validator":
                        notValidator = ModelFactory.Create((JObject?)kvp.Value);
                        break;
                }
            }

            if (notValidator == null)
                throw new ArgumentException(nameof(notValidator));

            return new NotValidator(notValidator, message);
        }
    }
}

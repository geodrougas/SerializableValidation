using SerializableValidator.Models.Interface;
using SerializableValidator.Validators.Checks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializableValidator.Serialization.Factories.Checks
{
    public class IsNullCheckFactory : IValidatorFactory
    {
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options)
        {
            string? field = null;
            string? message = null;

            foreach (var pair in options)
            {
                switch (pair.Key)
                {
                    case "Field":
                        field = (string)pair.Value;
                        break;
                    case "Message":
                        message = (string)pair.Value;
                        break;
                }
            }

            if (field == null)
                throw new ArgumentException();

            return new IsNullCheck(field, message);
        }
    }
}

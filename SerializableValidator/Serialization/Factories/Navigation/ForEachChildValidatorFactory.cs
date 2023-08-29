using Newtonsoft.Json.Linq;
using SerializableValidator.Models.Interface;
using SerializableValidator.Validators.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializableValidator.Serialization.Factories.Navigation
{
    public class ForEachChildValidatorFactory : IValidatorFactory
    {
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options)
        {
            string? name = null;
            bool? isAll = null;
            ISerializedValidator? validator = null;

            foreach (var pair in options)
            {
                switch (pair.Key)
                {
                    case "Name":
                        name = (string)pair.Value;
                        break;
                    case "IsAll":
                        isAll = (bool)Convert.ChangeType(pair.Value, typeof(bool));
                        break;
                    case "Validator":
                        validator = ModelFactory.Create((JObject?)pair.Value);
                        break;
                }
            }

            if (name == null || isAll == null || validator == null)
                throw new ArgumentException();

            return new ForEachChildValidator(name, isAll.Value, validator);
        }
    }
}

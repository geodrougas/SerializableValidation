using SerializableValidator.Models.Interface;
using SerializableValidator.Validators.Checks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializableValidator.Serialization.Factories.Checks
{
    public class BaseValidatorFactory : IValidatorFactory
    {
        public ISerializedValidator Generate(IEnumerable<KeyValuePair<string, object>> options)
        {
            int type = 0;
            string? field = null;
            string? message = null;
            IComparable? value = null;

            foreach (var pair in options)
            {
                switch (pair.Key)
                {
                    case "Type":
                        type = (int)Convert.ChangeType(pair.Value, typeof(int));
                        break;
                    case "Field":
                        field = (string)pair.Value;
                        break;
                    case "Message":
                        message = (string)pair.Value;
                        break;
                    case "Value":
                        var nValue = pair.Value;
                        if (nValue is IComparable)
                            value = (IComparable)nValue;
                        break;
                }
            }

            AssertIsNotNull(field);
            AssertIsNotNull(value);

            switch (type)
            {
                case 70:
                    return new EqualityCheck(field, value, message);
                case 71:
                    return new GreaterThanCheck(field, value, message);
                case 72:
                    return new LessThanCheck(field, value, message);
                case 73:
                    return new GreaterThanOrEqualCheck(field, value, message);
                case 74:
                    return new LessThanOrEqualCheck(field, value, message);
                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        void AssertIsNotNull([NotNull] object? nullableReference)
        {
            if (nullableReference == null)
            {
                throw new ArgumentNullException();
            }
        }

        void AssertIsTrue(bool isComparable)
        {
            if (isComparable)
            {
                throw new ArgumentException();
            }
        }
    }
}

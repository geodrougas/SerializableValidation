using System.Reflection;

namespace SerializableValidator.Analyzers
{
    public class ModelAnalyzer
    {
        private IDictionary<string, object> propertyValues = new Dictionary<string, object>();
        public ModelAnalyzer(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in properties)
            {
                var value = item.GetValue(obj);
                if (value != default)
                    propertyValues.Add(item.Name, value);
            }

            foreach (var item in fields)
            {
                var value = item.GetValue(obj);
                if (value != default)
                    propertyValues.Add(item.Name, value);
            }
        }

        public object? GetValue(string field)
        {
            var exists = propertyValues.TryGetValue(field, out var value);

            if (!exists)
                throw new NullReferenceException("The field does not exist");

            return value;
        }

        public IEnumerable<object?> GetValues()
        {
            return propertyValues.Values;
        }

        public IEnumerable<KeyValuePair<string, object>> GetKeyValues()
        {
            return propertyValues;
        }

        public IDictionary<string, object> GetDictionary()
        {
            return propertyValues;
        }
    }
}

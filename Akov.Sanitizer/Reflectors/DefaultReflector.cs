using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Akov.Sanitizer.Attributes;

namespace Akov.Sanitizer.Reflectors
{
    internal class DefaultReflector
    {
        private readonly Lazy<Dictionary<string, ReplaceForAttribute>> _sanitizedPropertiesDictionary;
        public DefaultReflector(Assembly[] assemblies)
        {
            _sanitizedPropertiesDictionary = 
                new Lazy<Dictionary<string, ReplaceForAttribute>>(() => Collect(assemblies));
        }

        public Dictionary<string, ReplaceForAttribute> SanitizedPropertiesDictionary 
            => _sanitizedPropertiesDictionary.Value;

        private static Dictionary<string, ReplaceForAttribute> Collect(Assembly[] assemblies)
        {
            var dictionary = new Dictionary<string, ReplaceForAttribute>();

            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    var attributes = type.GetCustomAttributes(typeof(SanitizedAttribute), false);
                    if (attributes.Length > 0)
                    {
                        PropertyInfo[] properties = type.GetProperties()
                            .Where(prop => prop.IsDefined(typeof(ReplaceForAttribute), false))
                            .ToArray();

                        foreach (var property in properties)
                        {
                            var attribute = (ReplaceForAttribute)
                                property.GetCustomAttributes(typeof(ReplaceForAttribute), false)
                                    .SingleOrDefault();

                            if (attribute != null)
                            {
                                string key = attribute.PropertyName ?? property.Name;

                                if (dictionary.ContainsKey(key))
                                    throw new NotSupportedException($"Property {key} already exists.");

                                dictionary.Add(key, attribute);
                            }
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}

using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Akov.Sanitizer.Patterns;
using Akov.Sanitizer.Reflectors;
using Akov.Sanitizer.Sanitizers;

namespace Akov.Sanitizer.Services
{
    public class SanitizerService
    {
        private readonly DefaultReflector _reflector;
        private readonly ISanitizerFactory _sanitizerFactory;
        private readonly IPatternHelper _patternHelper;

        public SanitizerService(
            Assembly[] assemblies,
            ISanitizerFactory? sanitizerFactory = null,
            IPatternHelper? patternHelper = null)
        {
            if(assemblies is null || !assemblies.Any())
                throw new ArgumentException("Assembly list should not be empty");

            _reflector = new DefaultReflector(assemblies);
            _sanitizerFactory = sanitizerFactory ?? new SanitizerFactory();
            _patternHelper = patternHelper ?? new JsonPatternHelper();
        }

        public string ReplaceSensitiveData(string value)
        {
            foreach (var propertyAttribute in _reflector.SanitizedPropertiesDictionary)
            {
                string searchTemplate = string.Format(_patternHelper.SearchTemplate, propertyAttribute.Key);
                Regex regex = new Regex(searchTemplate, RegexOptions.IgnoreCase);

                SanitizerBase sanitizer = _sanitizerFactory.GetBy(propertyAttribute.Value.SanitizerType);

                foreach (Match match in regex.Matches(value))
                {
                    if (match == null) continue;
                    
                    string oldValue = _patternHelper.GetOldValue(match.Value);
                    string replacementValue = sanitizer.GetSanitizedValue(oldValue, propertyAttribute.Value);
                    string replacement = string.Format(_patternHelper.ReplacementTemplate, propertyAttribute.Key, replacementValue);
                    value = Regex.Replace(value, searchTemplate, replacement, RegexOptions.IgnoreCase);
                }
            }

            return value;
        }
    }
}

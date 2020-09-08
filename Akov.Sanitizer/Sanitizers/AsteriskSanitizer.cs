using System;
using System.Linq;
using Akov.Sanitizer.Attributes;

namespace Akov.Sanitizer.Sanitizers
{
    public class AsteriskSanitizer : SanitizerBase
    {
        /// <summary>
        /// Attribute Pattern format: X,Y
        /// where X is number, e.g. 5 or 17 or 1999, represents the length of replacement string
        /// If Pattern is null or X is 0 then the length will be taken from the value length
        /// where Y is one character for replacement e.g. @ or # or A
        /// The defaults: 0,*
        /// </summary>
        public override string GetSanitizedValue(object value, ReplaceForAttribute attribute)
        {
            if(value is null) return String.Empty;

            string valueAsString = value.ToString()!;

            string[] parts = (attribute.Pattern?.ToString() ?? "0,*")
                .Split(",")
                .ToArray();

            int length = parts.Length > 0 && parts[0][0] != '0' ? int.Parse(parts[0]) : valueAsString.Length;
            char replace = parts.Length > 1 ? parts[1][0] : '*';

            return new String(replace, length);
        }
    }
}
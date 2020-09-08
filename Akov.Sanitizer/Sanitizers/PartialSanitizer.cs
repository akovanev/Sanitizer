using System;
using System.Linq;
using Akov.Sanitizer.Attributes;

namespace Akov.Sanitizer.Sanitizers
{
    /// <summary>
    /// Attribute Pattern format: X,Y,Z
    /// where X is number, e.g. 5 or 17 or 1999, represents
    /// how many real characters will be shown from the left
    /// where Y is number, e.g. 5 or 17 or 1999, represents
    /// how many real characters will be shown from the right
    /// where Z is one character for replacement e.g. @ or # or A
    /// The defaults: 0,4,*
    /// </summary>
    public class PartialSanitizer : SanitizerBase
    {
        public override string GetSanitizedValue(object value, ReplaceForAttribute attribute)
        {
            if (value is null) return String.Empty;

            string[] parts = (attribute.Pattern?.ToString() ?? "0,4,*")
                .Split(",")
                .ToArray();

            int leftTrim = parts.Length > 0 ? int.Parse(parts[0]) : 0;
            int rightTrim = parts.Length > 1 ? int.Parse(parts[1]) : 4;
            char replace = parts.Length > 2 ? parts[2][0] : '*';

            int trimsCount = leftTrim + rightTrim;
            string valueAsString = value.ToString()!;

            if (valueAsString.Length < trimsCount)
                return valueAsString;

            return $"{valueAsString.Substring(0, leftTrim)}" +
                   $"{new String(replace, valueAsString.Length - trimsCount)}" +
                   $"{valueAsString.Substring(valueAsString.Length - rightTrim)}";
        }
    }
}
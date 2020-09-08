using System;

namespace Akov.Sanitizer.Attributes
{
    public class ReplaceForAttribute : Attribute
    {
        public ReplaceForAttribute(
            Type sanitizerType, 
            object? pattern = null, 
            string? propertyName = null)
        {
            PropertyName = propertyName;
            SanitizerType = sanitizerType;
            Pattern = pattern;
        }

        public string? PropertyName { get; }
        public Type SanitizerType { get; }
        public object? Pattern { get; }
    }
}
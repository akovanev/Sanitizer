using Akov.Sanitizer.Attributes;

namespace Akov.Sanitizer.Sanitizers
{
    public abstract class SanitizerBase
    {
        public abstract string GetSanitizedValue(object value, ReplaceForAttribute attribute);
    }
}

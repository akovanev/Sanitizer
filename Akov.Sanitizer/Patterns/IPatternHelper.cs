namespace Akov.Sanitizer.Patterns
{
    public interface IPatternHelper
    {
        string SearchTemplate { get; }
        string ReplacementTemplate { get; }
        string GetOldValue(string value);
    }
}

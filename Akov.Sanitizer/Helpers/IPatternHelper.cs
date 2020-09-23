namespace Akov.Sanitizer.Helpers
{
    public interface IPatternHelper
    {
        string SearchTemplate { get; }
        string ReplacementTemplate { get; }
        string GetOldValue(string value);
    }
}

namespace Akov.Sanitizer.Helpers
{
    public class JsonPatternHelper : IPatternHelper
    {
        public string SearchTemplate => @"""{0}""\s*:\s*([""'])(?:(?=(\\?))\2.)*?\1";
        public string ReplacementTemplate => @"""{0}"": ""{1}""";

        public string GetOldValue(string value)
        {
            int index = value.LastIndexOfAny(new[] { '\'', '"' }, value.Length - 2);
            return value.Substring(index + 1, value.Length - index - 2);
        }
    }
}
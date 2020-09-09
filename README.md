# Sanitizer

Sanitizes sensitive data, replacing them by fake placeholders.

[![](https://img.shields.io/nuget/v/Akov.Sanitizer)](https://www.nuget.org/packages/Akov.Sanitizer/)

## Author's blog

[How to Sanitize Sensitive Data While Handling Http Requests](https://akovanev.com/blogs/2020/09/09/how-to-sanitize-sensitive-data)

## Example

```
//Models
[Sanitized]
public class Card
{
    [ReplaceFor(typeof(PartialSanitizer), pattern: "4,4,*")]
    public string? Number { get; set; }

    [ReplaceFor(typeof(AsteriskSanitizer), propertyName: "name", pattern: "6,@")]
    public string? CardholderName { get; set; }

    public int? Year { get; set; }
    public int? Month { get; set; }

    [ReplaceFor(typeof(AsteriskSanitizer))]
    public string? Cvc { get; set; }

    public Address? Address { get; set; }
}

//Test
//string json = ...
var _sanitizerService = new SanitizerService(new []{typeof(Card).Assembly});
string sanitizedData = _sanitizerService.ReplaceSensitiveData(json);
```

Default sanitizers: 

[AsteriskSanitizer](https://github.com/akovanev/Sanitizer/blob/master/Akov.Sanitizer/Sanitizers/AsteriskSanitizer.cs)

[PartialSanitizer](https://github.com/akovanev/Sanitizer/blob/master/Akov.Sanitizer/Sanitizers/PartialSanitizer.cs)

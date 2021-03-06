﻿using Akov.Sanitizer.Attributes;
using Akov.Sanitizer.Sanitizers;
using Newtonsoft.Json;

namespace Akov.Sanitizer.Demo.Models
{
    [Sanitized]
    public class Card
    {
        [JsonProperty("cardNumber")]
        [ReplaceFor(typeof(PartialSanitizer), propertyName: "cardNumber", pattern: "4,4,*")]
        public string? Number { get; set; }

        [ReplaceFor(typeof(AsteriskSanitizer), pattern: "6,@")]
        public string? CardholderName { get; set; }

        public int? Year { get; set; }
        public int? Month { get; set; }

        [ReplaceFor(typeof(AsteriskSanitizer))]
        public string? Cvc { get; set; }

        public Address? Address { get; set; }
    }

    [Sanitized]
    public class Address
    {
        [ReplaceFor(typeof(PartialSanitizer), pattern: "4,0,*")]
        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
    }
}

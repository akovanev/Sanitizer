using System;
using Akov.Sanitizer.Demo.Models;
using Akov.Sanitizer.Services;
using Newtonsoft.Json;
using Xunit;

namespace Akov.Sanitizer.Demo
{
    public class Demo
    {
        private readonly SanitizerService _sanitizerService;

        public Demo()
        {
            _sanitizerService= new SanitizerService(new []{typeof(Demo).Assembly});
        }
            

        [Fact]
        public void Test_Card()
        {
            var card = new
            {
                name = "Alex Kovanev",
                number = "1234567890123456",
                month = 1,
                year = 2050,
                cvc = "555",
                address = new
                {
                    line1 = "street 1",
                    line2 = "",
                    city = "Prague",
                    country = "Chechia"
                }
            };

            string json = JsonConvert.SerializeObject(card);

            string sanitizedData = _sanitizerService.ReplaceSensitiveData(json);

            var deserializedCard = JsonConvert.DeserializeObject<Card>(sanitizedData);

            Assert.Equal("@@@@@@", deserializedCard.CardholderName);
            Assert.Equal("1234********3456", deserializedCard.Number);
            Assert.Equal("***", deserializedCard.Cvc);
            Assert.Equal("stre****", deserializedCard.Address?.Line1);
            Assert.Equal(2050, deserializedCard.Year);
        }
    }
}

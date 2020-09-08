using System;
using System.Collections.Generic;
using System.Linq;

namespace Akov.Sanitizer.Sanitizers
{
    public class SanitizerFactory : ISanitizerFactory
    {
        private readonly IEnumerable<SanitizerBase> _patterns = new List<SanitizerBase>
        {
            new AsteriskSanitizer(),
            new PartialSanitizer()
        };

        public virtual IEnumerable<SanitizerBase> Get() => _patterns;

        public SanitizerBase GetBy(Type t)
            => Get().SingleOrDefault(p => p.GetType() == t);
    }
}
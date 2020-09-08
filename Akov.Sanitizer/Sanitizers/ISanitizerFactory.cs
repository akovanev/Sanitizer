using System;
using System.Collections.Generic;

namespace Akov.Sanitizer.Sanitizers
{
    public interface ISanitizerFactory
    {
        IEnumerable<SanitizerBase> Get();
        SanitizerBase GetBy(Type t);
    }
}
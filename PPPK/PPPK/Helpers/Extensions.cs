using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPK.Helpers
{
    public static class Extensions
    {
        public static bool IsCollection(this Type type)
        => type != typeof(string) && typeof(IEnumerable<>).IsAssignableFrom(type);
    }
}
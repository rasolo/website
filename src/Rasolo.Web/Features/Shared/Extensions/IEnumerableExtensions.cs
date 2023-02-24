using System.Collections.Generic;
using System.Linq;

namespace Rasolo.Web.Features.Shared.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
           return list == null || !list.Any();
        }
    }
}

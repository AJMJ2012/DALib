using System.Collections.Generic;
using System.Linq;

namespace DALib.Extensions;

public static class ListExtensions {
	public static bool ContainsCI<T>(this List<T> haystack, T needle) {
		if (haystack == null || needle == null) {
			return false;
		}
		if (needle is string) {
			return haystack.Any(s => (s as string).ToLower() == (needle as string).ToLower());
		}
		else {
			return haystack.Contains(needle);
		}
	}
}

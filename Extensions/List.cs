using System.Collections.Generic;
using System.Linq;

namespace DALib.Extensions;

public static class ListExtensions
{
	public static bool ContainsCI<T>(this List<T> haystack, T needle)
		=> ContainsCI(haystack.ToArray(), needle);

	public static bool ContainsCI<T>(this T[] haystack, T needle)
	{
		if (haystack == null || needle == null)
		{
			return false;
		}
		if (needle is string)
		{
			return haystack.Any(s => string.Equals(s as string, needle as string, System.StringComparison.OrdinalIgnoreCase));
		}
		else
		{
			return haystack.Contains(needle);
		}
	}

	public static bool ConainsWC(this List<string> haystack, string needle)
		=> ConainsWC(haystack.ToArray(), needle);

	public static bool ConainsWC(this string[] haystack, string needle)
	{
		if (haystack == null || needle == null)
		{
			return false;
		}
		return haystack.Any(s => needle.Matches(s));
	}
}

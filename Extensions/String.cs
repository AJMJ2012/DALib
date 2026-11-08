using System;
using System.Text.RegularExpressions;

namespace DALib.Extensions;

public static class StringExtensions
{
	public static string FirstCharToUpper(this string input)
		=> input switch
		{
			null => throw new ArgumentNullException(nameof(input)),
			"" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
			_ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
		};

	public static string WithPrefix(this string input, string prefix)
		=> "[" + prefix + "] " + input;

	public static bool Matches(this string input, string pattern)
	{
		pattern = $"^{pattern.Replace("*", ".*?")}$";
		return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
	}
}


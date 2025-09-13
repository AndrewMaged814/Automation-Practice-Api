using System;
using System.Text.RegularExpressions;

namespace Common.Extensions;
public static class StringExtensions
{
	public static string WithMaxLength(this string text, int length)
	{
		if (string.IsNullOrEmpty(text) || text.Length <= length)
			return text;

		return text.Substring(0, length - 1);
	}
	public static string RemoveSpecialCharacters(this string str)
	{
		if (string.IsNullOrEmpty(str)) return str;

		var regex = new Regex(@"[^a-zA-Z0-9\s]");
		return regex.Replace(str, string.Empty);
	}
	public static bool IsNull(this string? value)
	{
		if (value is null) return true;
		if (value.Trim() == "") return false;
		return string.Equals(value, "null", StringComparison.OrdinalIgnoreCase);
	}
}
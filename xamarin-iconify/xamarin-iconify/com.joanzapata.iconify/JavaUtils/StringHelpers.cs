using System.Text.RegularExpressions;

namespace JoanZapata.XamarinIconify.JavaUtils
{
	internal static class StringHelpers
	{
		public static bool matches (this string str, string regex)
		{
			return new Regex (regex).IsMatch (str);
		}
	}
}
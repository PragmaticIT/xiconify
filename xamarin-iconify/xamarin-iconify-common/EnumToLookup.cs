using System;
using System.Linq;

namespace JoanZapata.XamarinIconify
{
	public static class EnumToLookup
	{
		public static ILookup<string, Icon> ToLookup<TEnum>() {
			var enumType = typeof(TEnum);
//			var names = Enum.GetNames (enumType);
//			foreach (var name in names) {
//				System.Diagnostics.Debug.WriteLine ("{0}: {1}, {2}", name, (int)Enum.Parse (enumType, name), (char)(int)Enum.Parse (enumType, name));
//			}
			return Enum.GetNames (enumType)
				.Select(name => new Icon (name, (char)(int)Enum.Parse (enumType, name)))
				.ToLookup(x=>x.Key);
		}
	}
}


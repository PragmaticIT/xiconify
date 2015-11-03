using System.Linq;

namespace JoanZapata.XamarinIconify.Fonts
{
	public class TypiconsModule : IIconFontDescriptor
	{
		public string FontFileName {
			get {
				return "android-iconify-typicons.ttf";
			}
		}

		private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<TypiconsIcons> ();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
using System.Linq;

namespace JoanZapata.XamarinIconify.Fonts
{
	public class MeteoconsModule : IIconFontDescriptor
	{
		public string FontFileName {
			get {
				return "android-iconify-meteocons.ttf";
			}
		}

		private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<MeteoconsIcons> ();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
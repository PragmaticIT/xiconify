using System.Linq;

namespace JoanZapata.XamarinIconify.Fonts
{
	public class SimpleLineIconsModule : IIconFontDescriptor
	{
		public string FontFileName {
			get {
				return "android-iconify-simplelineicons.ttf";
			}
		}

		private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<SimpleLineIcons> ();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
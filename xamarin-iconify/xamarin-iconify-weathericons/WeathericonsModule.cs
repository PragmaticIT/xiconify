using System.Linq;

namespace JoanZapata.XamarinIconify.Fonts
{
	public class WeathericonsModule : IIconFontDescriptor
	{
		public string FontFileName {
			get {
				return "android-iconify-weathericons.ttf";
			}
		}

		private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<WeathericonsIcons> ();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
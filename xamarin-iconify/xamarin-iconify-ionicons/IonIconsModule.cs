using System.Linq;

namespace JoanZapata.XamarinIconify.Fonts
{
	public class IonIconsModule : IIconFontDescriptor
	{

		public string FontFileName {
			get {
				return "android-iconify-ionicons.ttf";
			}
		}

		private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<IonIcons> ();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
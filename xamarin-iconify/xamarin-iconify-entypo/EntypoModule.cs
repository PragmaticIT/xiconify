using System.Linq;
using JoanZapata.XamarinIconify;

namespace JoanZapata.XamarinIconify.Fonts
{
	public class EntypoModule : IIconFontDescriptor
	{

		public string FontFileName {
			get {
				return "android-iconify-entypo.ttf";
			}
		}

		private readonly ILookup<string, Icon> _characters=EnumToLookup.ToLookup<EntypoIcons>();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
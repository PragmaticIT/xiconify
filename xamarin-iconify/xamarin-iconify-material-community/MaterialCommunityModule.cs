using System.Linq;

namespace JoanZapata.XamarinIconify.Fonts
{
public class MaterialCommunityModule : IIconFontDescriptor {


		public string FontFileName {
			get {
				return "android-iconify-material-community.ttf";
			}
		}

		private static readonly ILookup<string, Icon> _characters = EnumToLookup.ToLookup<MaterialCommunityIcons> ();

		public ILookup<string, Icon> Characters {
			get{ return _characters; }
		}
	}
}
    


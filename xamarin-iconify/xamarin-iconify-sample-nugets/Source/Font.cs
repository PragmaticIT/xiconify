using JoanZapata.XamarinIconify;
using JoanZapata.XamarinIconify.Fonts;
using System.Collections.Generic;


namespace JoanZapata.XamarinIconify.Sample
{

	public static class FontManager{
		private static readonly Dictionary<string, IIconFontDescriptor> _fonts=new Dictionary<string,IIconFontDescriptor>{
			{"FontAwesome",new FontAwesomeModule()},
			{"Entypo",new EntypoModule()},
			{"Typicons",new TypiconsModule()},
			{"IonIcons",new IonIconsModule()},
			{"Material",new MaterialModule()},
			{"Material Community",new MaterialCommunityModule()},
			{"Meteocons",new MeteoconsModule()},
			{"WeatherIcons",new WeathericonsModule()},
			{"SimpleLineIcons",new SimpleLineIconsModule()}
		};
			

		public static Dictionary<string, IIconFontDescriptor>Fonts {get{return _fonts;}}

	}
}
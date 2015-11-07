namespace com.joanzapata.iconify.sample
{

	using com.joanzapata.iconify.fonts;

//JAVA TO C# CONVERTER TODO TASK: Enums cannot implement interfaces in .NET:
//ORIGINAL LINE: public enum Font implements FontIconsViewPagerAdapter.FontWithTitle
	public enum Font
	{
		FONTAWESOME("FontAwesome", new FontAwesomeModule()),
		ENTYPO("Entypo", new EntypoModule()),
		TYPICONS("Typicons", new TypiconsModule()),
		IONICONS("Ionicons", new IoniconsModule()),
		MATERIAL("Material", new MaterialModule()),
		MATERIALCOMMUNITY("Material Community", new MaterialCommunityModule()),
		METEOCONS("Meteocons", new MeteoconsModule()),
		WEATHERICONS("WeatherIcons", new WeathericonsModule()),
		SIMPLELINEICONS("SimpleLineIcons", new SimpleLineIconsModule());

		private final String title;
		private final com.joanzapata.iconify.IconFontDescriptor descriptor;

		public String getTitle()
		{
			return title;
		}

		public com.joanzapata.iconify.IconFontDescriptor getFont()
		{
			return descriptor;
		}

		Font(String title, com.joanzapata.iconify.IconFontDescriptor descriptor)
		{
			this.title = title
			this.descriptor = descriptor
		}
	}
}
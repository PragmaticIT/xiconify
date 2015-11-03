namespace com.joanzapata.iconify.fonts
{


	public class SimpleLineIconsModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-simplelineicons.ttf";
		}

		public override Icon[] characters()
		{
			return SimpleLineIconsIcons.values();
		}
	}

}
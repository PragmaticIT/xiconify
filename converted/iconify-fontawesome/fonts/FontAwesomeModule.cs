namespace com.joanzapata.iconify.fonts
{


	public class FontAwesomeModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-fontawesome.ttf";
		}

		public override Icon[] characters()
		{
			return FontAwesomeIcons.values();
		}
	}

}
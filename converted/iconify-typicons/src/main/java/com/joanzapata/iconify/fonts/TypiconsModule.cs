namespace com.joanzapata.iconify.fonts
{


	public class TypiconsModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-typicons.ttf";
		}

		public override Icon[] characters()
		{
			return TypiconsIcons.values();
		}
	}

}
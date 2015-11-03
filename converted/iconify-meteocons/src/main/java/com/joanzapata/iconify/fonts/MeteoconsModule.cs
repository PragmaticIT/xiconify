namespace com.joanzapata.iconify.fonts
{


	public class MeteoconsModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-meteocons.ttf";
		}

		public override Icon[] characters()
		{
			return MeteoconsIcons.values();
		}
	}

}
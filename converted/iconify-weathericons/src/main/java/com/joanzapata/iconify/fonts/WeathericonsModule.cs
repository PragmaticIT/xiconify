namespace com.joanzapata.iconify.fonts
{


	public class WeathericonsModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-weathericons.ttf";
		}

		public override Icon[] characters()
		{
			return WeathericonsIcons.values();
		}
	}

}
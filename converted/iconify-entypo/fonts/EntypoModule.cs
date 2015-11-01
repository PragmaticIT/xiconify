namespace com.joanzapata.iconify.fonts
{


	public class EntypoModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-entypo.ttf";
		}

		public override Icon[] characters()
		{
			return EntypoIcons.values();
		}
	}

}
namespace com.joanzapata.iconify.fonts
{


	public class IoniconsModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-ionicons.ttf";
		}

		public override Icon[] characters()
		{
			return IoniconsIcons.values();
		}
	}

}
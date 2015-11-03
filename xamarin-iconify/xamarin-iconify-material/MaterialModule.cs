namespace com.joanzapata.iconify.fonts
{


	public class MaterialModule : IconFontDescriptor
	{

		public override string ttfFileName()
		{
			return "iconify/android-iconify-material.ttf";
		}

		public override Icon[] characters()
		{
			return MaterialIcons.values();
		}
	}

}
namespace com.joanzapata.iconify.sample
{

	using Application = android.app.Application;

	public class SampleApplication : Application
	{

		public override void onCreate()
		{
			base.onCreate();
			foreach (Font font in Font.values())
			{
				Iconify.with(font.Font);
			}
		}
	}

}
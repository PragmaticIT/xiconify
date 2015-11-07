using System;

namespace JoanZapata.XamarinIconify.Sample
{
	[Android.App.Application(
		Name="joanzapata.xamariniconify.sample.sampleapplication",
		AllowBackup=true, 
		//Icon=""
		Theme="@style/AppTheme",
		Label="@string/app_name"
	)]
	public class SampleApplication :Android.App.Application
	{
		public SampleApplication () : base ()
		{
		}

		public SampleApplication (IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}
		public override void OnCreate()
		{
			base.OnCreate();
			foreach (var font in FontManager.Fonts)
			{
				Iconify.with(font.Value);
			}
		}
	}

}

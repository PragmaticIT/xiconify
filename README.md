# xiconify
Xamarin bindings to android-iconify


1. Initialization:
Yaou have to initialize XamarinIconify before using. If you plan to use icons in widgets (like IconTextView or IconButton) and declare them in XML, you have to init Iconify before inflation of the schema. There are two possible ways to do it:
Easier - you can place initialization just before SetContentView in your Activity code like that:
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			JoanZapata.XamarinIconify.Iconify.with (new JoanZapata.XamarinIconify.Fonts.FontAwesomeModule ());
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			/*... rest of the activity creation code ...*/
		}
Better - create and register overriden application class:
namespace testapp
{
	[Android.App.Application]
	public class Application:Android.App.Application
	{
		public Application () : base ()
		{
		}

		public Application (IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base (javaReference, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();
			JoanZapata.XamarinIconify.Iconify.with (new JoanZapata.XamarinIconify.Fonts.FontAwesomeModule ());
		}
	}
}


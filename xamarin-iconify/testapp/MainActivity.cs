using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;

namespace testapp
{
	[Activity (Label = "testapp", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			//JoanZapata.XamarinIconify.Iconify.With (new JoanZapata.XamarinIconify.Fonts.FontAwesomeModule ());
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);

			};
			button.SetBackgroundDrawable (new JoanZapata.XamarinIconify.IconDrawable (this, JoanZapata.XamarinIconify.Fonts.FontAwesomeIcons.fa_500px.ToString ()).WithColor (Color.Red));
	//		button.Background = new JoanZapata.XamarinIconify.IconDrawable (this, JoanZapata.XamarinIconify.Fonts.FontAwesomeIcons.fa_500px.ToString()).WithColor(Color.Red);
		}


	}
}



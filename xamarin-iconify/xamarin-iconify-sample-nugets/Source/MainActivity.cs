using Android.Support.V7.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;

namespace JoanZapata.XamarinIconify.Sample
{
	[Android.App.Activity(
		Name="joanzapata.xamariniconify.sample.mainactivity",
		Label="@string/app_name",
		MainLauncher=true
	)]
	public class MainActivity : AppCompatActivity
	{
		public MainActivity ():base(){}

		public MainActivity (System.IntPtr javaRef, Android.Runtime.JniHandleOwnership transfer):base(javaRef,transfer){}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.tabs) android.support.design.widget.TabLayout tabLayout;
		internal TabLayout tabLayout;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.toolbar) android.support.v7.widget.Toolbar toolbar;
		internal Toolbar toolbar;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.viewPager) android.support.v4.view.ViewPager viewPager;
		internal ViewPager viewPager;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView (Resource.Layout.activity_main);
			tabLayout = FindViewById<TabLayout> (Resource.Id.tabs);
			toolbar = FindViewById<Toolbar> (Resource.Id.toolbar);
			viewPager = FindViewById<ViewPager> (Resource.Id.viewPager);

			// Setup toolbar
			this.SetSupportActionBar(toolbar);

			// Fill view pager
			viewPager.Adapter = new FontIconsViewPagerAdapter(FontManager.Fonts);
			tabLayout.SetupWithViewPager(viewPager);
		}
	}

}
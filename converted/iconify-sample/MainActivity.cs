namespace com.joanzapata.iconify.sample
{

	using Bundle = android.os.Bundle;
	using TabLayout = android.support.design.widget.TabLayout;
	using ViewPager = android.support.v4.view.ViewPager;
	using AppCompatActivity = android.support.v7.app.AppCompatActivity;
	using Toolbar = android.support.v7.widget.Toolbar;
	using Bind = butterknife.Bind;
	using ButterKnife = butterknife.ButterKnife;

	public class MainActivity : AppCompatActivity
	{

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.tabs) android.support.design.widget.TabLayout tabLayout;
		internal TabLayout tabLayout;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.toolbar) android.support.v7.widget.Toolbar toolbar;
		internal Toolbar toolbar;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.viewPager) android.support.v4.view.ViewPager viewPager;
		internal ViewPager viewPager;

		protected internal override void onCreate(Bundle savedInstanceState)
		{
			base.onCreate(savedInstanceState);
			ContentView = R.layout.activity_main;
			ButterKnife.bind(this);

			// Setup toolbar
			SupportActionBar = toolbar;

			// Fill view pager
			viewPager.Adapter = new FontIconsViewPagerAdapter(Font.values());
			tabLayout.setupWithViewPager(viewPager);
		}
	}

}
namespace com.joanzapata.iconify.sample
{

	using Activity = android.app.Activity;
	using Context = android.content.Context;
	using PagerAdapter = android.support.v4.view.PagerAdapter;
	using GridLayoutManager = android.support.v7.widget.GridLayoutManager;
	using RecyclerView = android.support.v7.widget.RecyclerView;
	using LayoutInflater = android.view.LayoutInflater;
	using View = android.view.View;
	using ViewGroup = android.view.ViewGroup;
	using AndroidUtils = com.joanzapata.iconify.sample.utils.AndroidUtils;

	public class FontIconsViewPagerAdapter : PagerAdapter
	{

		public interface FontWithTitle
		{
			IconFontDescriptor Font {get;}

			string Title {get;}
		}

		private readonly FontWithTitle[] fonts;

		public FontIconsViewPagerAdapter(FontWithTitle[] fonts)
		{
			this.fonts = fonts;
		}

		public override int Count
		{
			get
			{
				return fonts.Length;
			}
		}

		public override object instantiateItem(ViewGroup container, int position)
		{
			Context context = container.Context;
			LayoutInflater inflater = LayoutInflater.from(context);
			View view = inflater.inflate(R.layout.item_font, container, false);
			RecyclerView recyclerView = (RecyclerView) view.findViewById(R.id.recyclerView);
			int nbColumns = AndroidUtils.getScreenSize((Activity) context).width / context.Resources.getDimensionPixelSize(R.dimen.item_width);
			recyclerView.LayoutManager = new GridLayoutManager(context, nbColumns);
			recyclerView.Adapter = new IconAdapter(fonts[position].Font.characters());
			container.addView(view);
			return view;
		}

		public override CharSequence getPageTitle(int position)
		{
			return fonts[position].Title;
		}

		public override bool isViewFromObject(View view, object @object)
		{
			return view == @object;
		}

		public override void destroyItem(ViewGroup container, int position, object @object)
		{
			container.removeView((View) @object);
		}
	}

}

using Activity = Android.App.Activity;
using Context = Android.Content.Context;
using PagerAdapter = Android.Support.V4.View.PagerAdapter;
using GridLayoutManager = Android.Support.V7.Widget.GridLayoutManager;
using RecyclerView = Android.Support.V7.Widget.RecyclerView;
using LayoutInflater = Android.Views.LayoutInflater;
using View = Android.Views.View;
using ViewGroup = Android.Views.ViewGroup;
using JoanZapata.XamarinIconify.Sample.Utils;
using System.Linq;

namespace JoanZapata.XamarinIconify.Sample
{
	

	public class FontIconsViewPagerAdapter : PagerAdapter
	{
		System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, IIconFontDescriptor>> fonts;

		

		public FontIconsViewPagerAdapter (System.Collections.Generic.Dictionary<string, IIconFontDescriptor> fonts)
		{
			this.fonts = fonts.ToList();
		}

		public override int Count
		{
			get
			{
				return fonts.Count;
			}
		}


		public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
		{
			Context context = container.Context;
			LayoutInflater inflater = LayoutInflater.From(context);
			View view = inflater.Inflate(Resource.Layout.item_font, container, false);
			RecyclerView recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
			int nbColumns = AndroidUtils.getScreenSize((Activity) context).width / context.Resources.GetDimensionPixelSize(Resource.Dimension.item_width);
			recyclerView.SetLayoutManager (new GridLayoutManager (context, nbColumns));
			recyclerView.SetAdapter (new IconAdapter (fonts[position].Value.Characters));
			container.AddView(view);
			return view;
		}

		public override Java.Lang.ICharSequence GetPageTitleFormatted (int position)
		{
			return new Java.Lang.String (fonts [position].Key);
		}

//		public override bool IsViewFromObject(View view, object @object)
//		{
//			return view == @object;
//		}
//
//		public override void DestroyItem(ViewGroup container, int position, object @object)
//		{
//			container.RemoveView((View) @object);
//		}

		public override bool IsViewFromObject (View view, Java.Lang.Object objectValue)
		{
			return view == objectValue;
		}

		public override void DestroyItem (ViewGroup container, int position, Java.Lang.Object objectValue)
		{
			container.RemoveView ((View)objectValue);
		}
	}

}
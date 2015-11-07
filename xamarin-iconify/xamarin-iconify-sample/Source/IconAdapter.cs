using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace JoanZapata.XamarinIconify.Sample
{



	public class IconAdapter : RecyclerView.Adapter
	{
		readonly ILookup<string, Icon> icons;

		public IconAdapter (ILookup<string, Icon> icons)
		{
			this.icons = icons;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			View view = LayoutInflater.From (parent.Context).Inflate (Resource.Layout.item_icon, parent, false);
			return new IconViewHolder (view);
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			Icon icon = icons [icons.ElementAt (position).Key].First ();
			IconViewHolder iconViewHolder = holder as IconViewHolder;
			if (iconViewHolder == null)
				return;
			iconViewHolder.Icon.Text = "{" + icon.Key + "}";
			iconViewHolder.Name.Text = icon.Key;
		}

		public override int ItemCount {
			get {
				return icons.Count;
			}
		}

		public class IconViewHolder : RecyclerView.ViewHolder
		{
			public TextView Icon{ get; private set; }

			public TextView Name{ get; private set; }

			public IconViewHolder (View itemView) : base (itemView)
			{
				Icon = itemView.FindViewById<TextView> (Resource.Id.icon);
				Name = itemView.FindViewById<TextView> (Resource.Id.name);
			}
		}
	}

}
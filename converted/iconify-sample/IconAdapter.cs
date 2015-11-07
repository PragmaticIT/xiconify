namespace com.joanzapata.iconify.sample
{

	using RecyclerView = android.support.v7.widget.RecyclerView;
	using LayoutInflater = android.view.LayoutInflater;
	using View = android.view.View;
	using ViewGroup = android.view.ViewGroup;
	using TextView = android.widget.TextView;
	using Bind = butterknife.Bind;
	using ButterKnife = butterknife.ButterKnife;

	public class IconAdapter : RecyclerView.Adapter<IconAdapter.ViewHolder>
	{

		private readonly Icon[] icons;

		public IconAdapter(Icon[] icons)
		{
			this.icons = icons;
		}

		public override ViewHolder onCreateViewHolder(ViewGroup parent, int viewType)
		{
			View view = LayoutInflater.from(parent.Context).inflate(R.layout.item_icon, parent, false);
			return new ViewHolder(view);
		}

		public override void onBindViewHolder(ViewHolder viewHolder, int position)
		{
			Icon icon = icons[position];
			viewHolder.icon.Text = "{" + icon.key() + "}";
			viewHolder.name.Text = icon.key();
		}

		public override int ItemCount
		{
			get
			{
				return icons.Length;
			}
		}

		public class ViewHolder : RecyclerView.ViewHolder
		{

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.icon) android.widget.TextView icon;
			internal TextView icon;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Bind(R.id.name) android.widget.TextView name;
			internal TextView name;

			public ViewHolder(View itemView) : base(itemView)
			{
				ButterKnife.bind(this, itemView);
			}
		}
	}

}
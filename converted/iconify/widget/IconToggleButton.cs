namespace com.joanzapata.iconify.widget
{

	using Context = android.content.Context;
	using AttributeSet = android.util.AttributeSet;
	using ToggleButton = android.widget.ToggleButton;
	using HasOnViewAttachListener = com.joanzapata.iconify.@internal.HasOnViewAttachListener;

	public class IconToggleButton : ToggleButton, HasOnViewAttachListener
	{

		private com.joanzapata.iconify.@internal.HasOnViewAttachListener_HasOnViewAttachListenerDelegate @delegate;

		public IconToggleButton(Context context, AttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			init();
		}

		public IconToggleButton(Context context, AttributeSet attrs) : base(context, attrs)
		{
			init();
		}

		public IconToggleButton(Context context) : base(context)
		{
			init();
		}

		private void init()
		{
			TransformationMethod = null;
		}

		public override void setText(CharSequence text, BufferType type)
		{
			base.setText(Iconify.compute(Context, text, this), BufferType.NORMAL);
		}

		public virtual com.joanzapata.iconify.@internal.HasOnViewAttachListener_OnViewAttachListener OnViewAttachListener
		{
			set
			{
				if (@delegate == null)
				{
					@delegate = new com.joanzapata.iconify.@internal.HasOnViewAttachListener_HasOnViewAttachListenerDelegate(this);
				}
				@delegate.OnViewAttachListener = value;
			}
		}

		protected internal override void onAttachedToWindow()
		{
			base.onAttachedToWindow();
			@delegate.onAttachedToWindow();
		}

		protected internal override void onDetachedFromWindow()
		{
			base.onDetachedFromWindow();
			@delegate.onDetachedFromWindow();
		}

	}

}
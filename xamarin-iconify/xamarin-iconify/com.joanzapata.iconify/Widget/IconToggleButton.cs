using Context = Android.Content.Context;
using AttributeSet = Android.Util.IAttributeSet;
using ToggleButton = Android.Widget.ToggleButton;
using JoanZapata.XamarinIconify.Internal;
using Java.Lang;

namespace JoanZapata.XamarinIconify.Widget
{

	public class IconToggleButton : ToggleButton, IHasOnViewAttachListener
	{

		private HasOnViewAttachListener_HasOnViewAttachListenerDelegate @delegate;

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

		public override void SetText(ICharSequence text, BufferType type)
		{
			base.SetText(Iconify.compute(Context, text, this), BufferType.Normal);
		}

		public virtual IHasOnViewAttachListener_OnViewAttachListener OnViewAttachListener
		{
			set
			{
				if (@delegate == null)
				{
					@delegate = new HasOnViewAttachListener_HasOnViewAttachListenerDelegate(this);
				}
				@delegate.OnViewAttachListener = value;
			}
		}

		protected override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			@delegate.OnAttachedToWindow();
		}

		protected override void OnDetachedFromWindow()
		{
			base.OnDetachedFromWindow();
			@delegate.OnDetachedFromWindow();
		}

	}

}
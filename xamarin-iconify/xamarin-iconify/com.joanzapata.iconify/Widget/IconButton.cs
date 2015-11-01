using Context = Android.Content.Context;
using AttributeSet = Android.Util.IAttributeSet;
using Button = Android.Widget.Button;
using JoanZapata.XamarinIconify.Internal;

namespace JoanZapata.XamarinIconify.Widget
{
	public class IconButton : Button, HasOnViewAttachListener
	{

		private HasOnViewAttachListener_HasOnViewAttachListenerDelegate @delegate;

		public IconButton(Context context) : base(context)
		{
			init();
		}

		public IconButton(Context context, AttributeSet attrs) : base(context, attrs)
		{
			init();
		}

		public IconButton(Context context, AttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			init();
		}

		private void init()
		{
			TransformationMethod = null;
		}

		public override void SetText (Java.Lang.ICharSequence text, BufferType type)
		{
			base.SetText (Iconify.compute (Context, text, this), type);
		}


		public virtual HasOnViewAttachListener_OnViewAttachListener OnViewAttachListener
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
			@delegate.onAttachedToWindow();
		}

		protected override void OnDetachedFromWindow()
		{
			base.OnDetachedFromWindow();
			@delegate.onDetachedFromWindow();
		}
	}

}
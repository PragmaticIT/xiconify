using System;
using Android.Runtime;
using Context = Android.Content.Context;
using AttributeSet = Android.Util.IAttributeSet;
using TextView = Android.Widget.TextView;
using JoanZapata.XamarinIconify.Internal;

namespace JoanZapata.XamarinIconify.Widget
{
	[Android.Runtime.Register("JoanZapata.XamarinIconify.Widget.IconTextView")]
	public class IconTextView : TextView, IHasOnViewAttachListener
	{

		private HasOnViewAttachListener_HasOnViewAttachListenerDelegate @delegate;

		public IconTextView(Context context) : base(context)
		{
			Init();
		}

		public IconTextView(Context context, AttributeSet attrs) : base(context, attrs)
		{
			Init();
		}

		public IconTextView(Context context, AttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			Init();
		}

	    public IconTextView(IntPtr javaRef, JniHandleOwnership transfer):base(javaRef, transfer)
	    {
	        Init();
	    }

		private void Init()
		{
			TransformationMethod = null;
		}
		public override void SetText (Java.Lang.ICharSequence text, BufferType type)
		{
			base.SetText (Iconify.compute(Context, text, this), type);
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

		protected override void OnAttachedToWindow ()
		{
			base.OnAttachedToWindow ();
			@delegate.OnAttachedToWindow ();
		}
		protected override void OnDetachedFromWindow ()
		{
			base.OnDetachedFromWindow ();
			@delegate.OnDetachedFromWindow ();
		}
	}

}
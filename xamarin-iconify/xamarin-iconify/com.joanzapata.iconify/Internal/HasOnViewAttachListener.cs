//using ViewCompat = Android.Support.V4.View.ViewCompat;
using TextView = Android.Widget.TextView;

namespace JoanZapata.XamarinIconify.Internal
{



	/// <summary>
	/// Any TextView subclass that wishes to call <seealso cref="com.joanzapata.iconify.Iconify#addIcons(TextView...)"/> on it
	/// needs to implement this interface if it ever want to use spinning icons.
	/// <br>
	/// IconTextView, IconButton and IconToggleButton already implement it, but if you need to implement it
	/// yourself, please use <seealso cref="com.joanzapata.iconify.internal.HasOnViewAttachListener.HasOnViewAttachListenerDelegate"/>
	/// to help you.
	/// </summary>
	public interface HasOnViewAttachListener
	{
		HasOnViewAttachListener_OnViewAttachListener OnViewAttachListener {set;}

		/// <summary>
		/// Helper class to implement <seealso cref="HasOnViewAttachListener"/>.
		/// Usual implementation should look like this:
		/// <pre>
		/// {@code
		/// class MyClass extends TextView implements HasOnViewAttachListener {
		/// 
		///       private HasOnViewAttachListenerDelegate delegate
		/// 
		///       @Override
		///       public void setOnViewAttachListener(OnViewAttachListener listener) {
		///          if (delegate == null) delegate = new HasOnViewAttachListenerDelegate(this);
		///          delegate.setOnViewAttachListener(listener);
		///       }
		/// 
		///       @Override
		///       protected void onAttachedToWindow() {
		///          super.onAttachedToWindow();
		///          delegate.onAttachedToWindow();
		///       }
		/// 
		///       @Override
		///       protected void onDetachedFromWindow() {
		///          super.onDetachedFromWindow();
		///          delegate.onDetachedFromWindow();
		///      }
		/// 
		///  }
		/// }
		/// </pre>
		/// </summary>
	}

	public interface HasOnViewAttachListener_OnViewAttachListener
	{
		void onAttach();

		void onDetach();
	}

	public class HasOnViewAttachListener_HasOnViewAttachListenerDelegate
	{

		internal readonly TextView view;
		internal HasOnViewAttachListener_OnViewAttachListener listener;

		public HasOnViewAttachListener_HasOnViewAttachListenerDelegate(TextView view)
		{
			this.view = view;
		}

		public virtual HasOnViewAttachListener_OnViewAttachListener OnViewAttachListener
		{
			set
			{
				if (this.listener != null)
				{
					this.listener.onDetach();
				}
				this.listener = value;
//				if (ViewCompat.isAttachedToWindow(view) && value != null)
//				{
//					value.onAttach();
//				}
			}
		}

		public virtual void onAttachedToWindow()
		{
			if (listener != null)
			{
				listener.onAttach();
			}
		}

		public virtual void onDetachedFromWindow()
		{
			if (listener != null)
			{
				listener.onDetach();
			}
		}

	}
}
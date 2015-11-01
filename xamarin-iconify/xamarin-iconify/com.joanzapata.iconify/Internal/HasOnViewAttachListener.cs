//using ViewCompat = Android.Support.V4.View.ViewCompat;
using TextView = Android.Widget.TextView;

namespace JoanZapata.XamarinIconify.Internal
{



	/// <summary>
	/// Any TextView subclass that wishes to call <seealso cref="com.joanzapata.iconify.Iconify#addIcons(TextView...)"/> on it
	/// needs to implement this interface if it ever want to use spinning icons.
	/// <br>
	/// IconTextView, IconButton and IconToggleButton already implement it, but if you need to implement it
	/// yourself, please use <seealso cref="IHasOnViewAttachListener.HasOnViewAttachListenerDelegate"/>
	/// to help you.
	/// </summary>
	public interface IHasOnViewAttachListener
	{
		IHasOnViewAttachListener_OnViewAttachListener OnViewAttachListener {set;}

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

	public interface IHasOnViewAttachListener_OnViewAttachListener
	{
		void OnAttach();

		void OnDetach();
	}

	public class HasOnViewAttachListener_HasOnViewAttachListenerDelegate
	{

		private readonly TextView _view;
		private IHasOnViewAttachListener_OnViewAttachListener _listener;

		public HasOnViewAttachListener_HasOnViewAttachListenerDelegate(TextView view)
		{
			_view = view;
		}

		public virtual IHasOnViewAttachListener_OnViewAttachListener OnViewAttachListener
		{
			set
			{
				if (_listener != null)
				{
					_listener.OnDetach();
				}
				_listener = value;
//				if (ViewCompat.isAttachedToWindow(view) && value != null)
//				{
//					value.onAttach();
//				}
			}
		}

		public virtual void OnAttachedToWindow()
		{
			if (_listener != null)
			{
				_listener.OnAttach();
			}
		}

		public virtual void OnDetachedFromWindow()
		{
			if (_listener != null)
			{
				_listener.OnDetach();
			}
		}

	}
}
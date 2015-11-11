#if V4COMPAT
using ViewCompat = Android.Support.V4.View.ViewCompat;
#endif

using Android.Widget;

namespace JoanZapata.XamarinIconify.Internal
{
    /// <summary>
    ///     Any TextView subclass that wishes to call <seealso cref="JoanZapata.Iconify.Iconify#AddIcons(TextView...)" />
    ///     on it
    ///     needs to implement this interface if it ever want to use spinning icons.
    ///     <br>
    ///         IconTextView, IconButton and IconToggleButton already implement it, but if you need to implement it
    ///         yourself, please use <seealso cref="HasOnViewAttachListenerDelegate" />
    ///         to help you.
    /// </summary>
    public interface IHasOnViewAttachListener
    {
        IOnViewAttachListener OnViewAttachListener { set; }
    }

    public interface IOnViewAttachListener
    {
        void OnAttach();
        void OnDetach();
    }

    public class HasOnViewAttachListenerDelegate
    {
        private readonly TextView _view;
        private IOnViewAttachListener _listener;

        public HasOnViewAttachListenerDelegate(TextView view)
        {
            _view = view;
        }

        public virtual IOnViewAttachListener OnViewAttachListener
        {
            set
            {
                if (_listener != null)
                {
                    _listener.OnDetach();
                }
                _listener = value;
#if V4COMPAT
                if (ViewCompat.isAttachedToWindow(_view) && value != null)
#else
                if (_view.WindowToken != null && value != null)
#endif
                {
                    _listener.OnAttach();
                }
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
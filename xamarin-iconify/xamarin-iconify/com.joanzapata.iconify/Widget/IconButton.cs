using System;
using Android.Content;
using Android.Runtime;
using Android.Widget;
using Java.Lang;
using JoanZapata.XamarinIconify.Internal;
using AttributeSet = Android.Util.IAttributeSet;

namespace JoanZapata.XamarinIconify.Widget
{
    public class IconButton : Button, IHasOnViewAttachListener
    {
        private HasOnViewAttachListenerDelegate _delegate;

        public IconButton(Context context) : base(context)
        {
            Init();
        }

        public IconButton(Context context, AttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public IconButton(Context context, AttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            Init();
        }

        public IconButton(IntPtr javaRef, JniHandleOwnership transfer) : base(javaRef, transfer)
        {
            Init();
        }

        public virtual IOnViewAttachListener OnViewAttachListener
        {
            set
            {
                if (_delegate == null)
                {
                    _delegate = new HasOnViewAttachListenerDelegate(this);
                }
                _delegate.OnViewAttachListener = value;
            }
        }

        private void Init()
        {
            TransformationMethod = null;
        }

        public override void SetText(ICharSequence text, BufferType type)
        {
            base.SetText(Iconify.Compute(Context, text, this), type);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            _delegate.OnAttachedToWindow();
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            _delegate.OnDetachedFromWindow();
        }
    }
}
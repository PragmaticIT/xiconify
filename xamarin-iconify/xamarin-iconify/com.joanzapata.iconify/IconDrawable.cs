using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Util;
using Java.Lang;

namespace JoanZapata.XamarinIconify
{

    /// <summary>
    ///     Embed an icon into a Drawable that can be used as TextView icons, or ActionBar icons.
    ///     <pre>
    ///         new IconDrawable(context, IconValue.icon_star)
    ///         .WithColorRes(R.WithColor.white)
    ///         .WithActionBarSize();
    ///     </pre>
    ///     If you don't set the _size of the drawable, it will use the _size
    ///     that is given to him. Note that in an ActionBar, if you don't
    ///     set the _size explicitly it uses 0, so please use WithActionBarSize().
    /// </summary>
    public class IconDrawable : Drawable
    {
        public const int AndroidActionbarIconSizeDp = 24;
        private int _alpha = 255;
        private Context _context;
        private Icon _icon;
        private TextPaint _paint;
        private int _size = -1;

        /// <summary>
        ///     Create an IconDrawable.
        /// </summary>
        /// <param name="context"> Your activity or application context. </param>
        /// <param name="iconKey"> The icon key you want this drawable to display. Calls iconKey.ToString() method internally to get icon name</param>
        /// <exception cref="IllegalArgumentException"> if the key doesn't match any icon. </exception>
        public IconDrawable(Context context, object iconKey):this(context, (iconKey??"").ToString())
        {
            if(iconKey==null)
                throw new ArgumentNullException("iconKey");
        }

        /// <summary>
        ///     Create an IconDrawable.
        /// </summary>
        /// <param name="context"> Your activity or application context. </param>
        /// <param name="iconKey"> The icon key you want this drawable to display. </param>
        /// <exception cref="IllegalArgumentException"> if the key doesn't match any icon. </exception>
        public IconDrawable(Context context, string iconKey)
        {
            if (string.IsNullOrWhiteSpace(iconKey)) 
                throw new ArgumentNullException("iconKey");

            var icon = Iconify.FindIconForKey(iconKey);
            if (!icon.HasValue)
            {
                throw new ArgumentException("No icon With that key \"" + iconKey + "\".");
            }
            Init(context, icon.Value);
        }

        /// <summary>
        ///     Create an IconDrawable.
        /// </summary>
        /// <param name="context"> Your activity or application context. </param>
        /// <param name="icon">    The icon you want this drawable to display. </param>
        public IconDrawable(Context context, Icon icon)
        {
            Init(context, icon);
        }

        public override int IntrinsicHeight
        {
            get { return _size; }
        }

        public override int IntrinsicWidth
        {
            get { return _size; }
        }

        public override bool IsStateful
        {
            get { return true; }
        }

        public override int Opacity
        {
            get { return _alpha; }
        }

        /// <summary>
        ///     Gets or sets paint style.
        /// </summary>
        public virtual Paint.Style Style
        {
            get { return _paint.GetStyle(); }
            set { _paint.SetStyle(value); }
        }

        private void Init(Context context, Icon icon)
        {
            _context = context;
            _icon = icon;
            _paint = new TextPaint();
            _paint.SetTypeface(Iconify.FindTypefaceOf(icon).GetTypeface(context));
            _paint.SetStyle(Paint.Style.Fill);
            _paint.TextAlign = Paint.Align.Center;
            _paint.UnderlineText = false;
            _paint.Color = Color.Black;
            _paint.AntiAlias = true;
        }

        /// <summary>
        ///     Set the size of this icon to the standard Android ActionBar.
        /// </summary>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WithActionBarSize()
        {
            return WIthSizeDp(AndroidActionbarIconSizeDp);
        }

        /// <summary>
        ///     Set the size of the drawable.
        /// </summary>
        /// <param name="dimenRes"> The dimension resource. </param>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WithSizeRes(int dimenRes)
        {
            return WithSizePx(_context.Resources.GetDimensionPixelSize(dimenRes));
        }

        /// <summary>
        ///     Set the size of the drawable.
        /// </summary>
        /// <param name="size"> The _size in density-independent pixels (dp). </param>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WIthSizeDp(int size)
        {
            return WithSizePx(ConvertDpToPx(_context, size));
        }

        /// <summary>
        ///     Set the size of the drawable.
        /// </summary>
        /// <param name="size"> The _size in pixels (px). </param>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WithSizePx(int size)
        {
            _size = size;
            SetBounds(0, 0, size, size);
            InvalidateSelf();
            return this;
        }

        /// <summary>
        ///     Set the Color of the drawable.
        /// </summary>
        /// <param name="color"> The WithColor, usually from android.graphics.Color or 0xFF012345. </param>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WithColor(Color color)
        {
            _paint.Color = color;
            InvalidateSelf();
            return this;
        }

        /// <summary>
        ///     Set the Color of the drawable.
        /// </summary>
        /// <param name="colorRes"> The WithColor resource id, from your Resource class. </param>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WithColorRes(int colorRes)
        {
            _paint.Color = _context.Resources.GetColor(colorRes);
            InvalidateSelf();
            return this;
        }

        /// <summary>
        ///     Set the WithAlpha of this drawable.
        /// </summary>
        /// <param name="alpha"> The WithAlpha, between 0 (transparent) and 255 (opaque). </param>
        /// <returns> The current IconDrawable for chaining. </returns>
        public virtual IconDrawable WithAlpha(int alpha)
        {
            SetAlpha(alpha);
            InvalidateSelf();
            return this;
        }

        public override void Draw(Canvas canvas)
        {
            var bounds = Bounds;
            var height = bounds.Height();
            _paint.TextSize = height;
            var textBounds = new Rect();
            var textValue = _icon.Character.ToString();
            _paint.GetTextBounds(textValue, 0, 1, textBounds);
            var textHeight = textBounds.Height();
            var textBottom = bounds.Top + (height - textHeight)/2f + textHeight - textBounds.Bottom;
            canvas.DrawText(textValue, bounds.ExactCenterX(), textBottom, _paint);
        }

        public override bool SetState(int[] stateSet)
        {
            var oldValue = _paint.Alpha;
            var newValue = isEnabled(stateSet) ? _alpha : _alpha/2;
            _paint.Alpha = newValue;
            return oldValue != newValue;
        }

        public override void SetAlpha(int alpha)
        {
            _alpha = alpha;
            _paint.Alpha = alpha;
        }

        public override void SetColorFilter(ColorFilter cf)
        {
            _paint.SetColorFilter(cf);
        }

        public override void ClearColorFilter()
        {
            _paint.SetColorFilter(null);
        }

        // Util
        private bool isEnabled(int[] stateSet)
        {
            foreach (var state in stateSet)
            {
                if (state == Android.Resource.Attribute.StateEnabled)
                {
                    return true;
                }
            }
            return false;
        }

        // Util
        private int ConvertDpToPx(Context context, float dp)
        {
            return (int) TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, context.Resources.DisplayMetrics);
        }
    }
}
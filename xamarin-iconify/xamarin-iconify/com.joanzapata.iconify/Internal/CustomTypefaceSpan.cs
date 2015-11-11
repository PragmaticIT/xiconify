using Android.Graphics;
using Android.Text.Style;
using Java.Lang;
using JoanZapata.XamarinIconify.JavaUtils;

namespace JoanZapata.XamarinIconify.Internal
{
    public class CustomTypefaceSpan : ReplacementSpan
    {
        private const int RotationDuration = 2000;
        private const float BaselineRatio = 1/7f;
        private static readonly Rect TextBounds = new Rect();
        private static readonly Paint LocalPaint = new Paint();

        private readonly string _icon;
        private readonly Color _iconColor;
        private readonly float _iconSizePx;
        private readonly float _iconSizeRatio;
        private readonly bool _rotate;
        private readonly long _rotationStartTime;
        private readonly Typeface _typeface;

        public CustomTypefaceSpan(Icon icon, Typeface type, float iconSizePx, float iconSizeRatio, int iconColor,
            bool rotate)
        {
            _rotate = rotate;
            _icon = icon.Character.ToString();
            _typeface = type;
            _iconSizePx = iconSizePx;
            _iconSizeRatio = iconSizeRatio;
            _iconColor = new Color(iconColor);
            _rotationStartTime = DateTimeHelpers.CurrentUnixTimeMillis();
        }

        public virtual bool Animated
        {
            get { return _rotate; }
        }

        public override int GetSize(Paint paint, ICharSequence text, int start, int end, Paint.FontMetricsInt fm)
        {
            LocalPaint.Set(paint);
            ApplyCustomTypeFace(LocalPaint, _typeface);
            LocalPaint.GetTextBounds(_icon, 0, 1, TextBounds);
            if (fm != null)
            {
                fm.Descent = (int) (TextBounds.Height()*BaselineRatio);
                fm.Ascent = -(TextBounds.Height() - fm.Descent);
                fm.Top = fm.Ascent;
                fm.Bottom = fm.Descent;
            }
            return TextBounds.Width();
        }

        public override void Draw(Canvas canvas, ICharSequence text, int start, int end, float x, int top, int y,
            int bottom, Paint paint)
        {
            ApplyCustomTypeFace(paint, _typeface);
            paint.GetTextBounds(_icon, 0, 1, TextBounds);
            canvas.Save();
            if (_rotate)
            {
                var rotation = (DateTimeHelpers.CurrentUnixTimeMillis() - _rotationStartTime)/(float) RotationDuration*
                               360f;
                var centerX = x + TextBounds.Width()/2f;
                var centerY = y - TextBounds.Height()/2f + TextBounds.Height()*BaselineRatio;
                canvas.Rotate(rotation, centerX, centerY);
            }

            canvas.DrawText(_icon, x - TextBounds.Left, y - TextBounds.Bottom + TextBounds.Height()*BaselineRatio,
                paint);
            canvas.Restore();
        }

        private void ApplyCustomTypeFace(Paint paint, Typeface typeface)
        {
            paint.FakeBoldText = false;
            paint.TextSkewX = 0f;
            paint.SetTypeface(typeface);
            if (_rotate)
            {
                paint.ClearShadowLayer();
            }
            if (_iconSizeRatio > 0)
            {
                paint.TextSize = paint.TextSize*_iconSizeRatio;
            }
            else if (_iconSizePx > 0)
            {
                paint.TextSize = _iconSizePx;
            }
            if (_iconColor < int.MaxValue)
            {
                paint.Color = _iconColor;
            }
        }
    }
}
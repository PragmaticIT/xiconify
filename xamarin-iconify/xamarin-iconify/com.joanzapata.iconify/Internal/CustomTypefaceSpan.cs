
using Canvas = Android.Graphics.Canvas;
using Paint = Android.Graphics.Paint;
using Rect = Android.Graphics.Rect;
using Typeface = Android.Graphics.Typeface;
using ReplacementSpan = Android.Text.Style.ReplacementSpan;
using Java.Lang;
using Android.Graphics;
using JoanZapata.XamarinIconify.JavaUtils;

namespace JoanZapata.XamarinIconify.Internal
{
	

	public class CustomTypefaceSpan : ReplacementSpan
	{
		private const int ROTATION_DURATION = 2000;
		private static readonly Rect TEXT_BOUNDS = new Rect();
		private static readonly Paint LOCAL_PAINT = new Paint();
		private static readonly float BASELINE_RATIO = 1 / 7f;

		private readonly string _icon;
		private readonly Typeface _typeface;
		private readonly float _iconSizePx;
		private readonly float _iconSizeRatio;
		private readonly Color _iconColor;
		private readonly bool _rotate;
		private readonly long _rotationStartTime;

		public CustomTypefaceSpan(Icon icon, Typeface type, float iconSizePx, float iconSizeRatio, int iconColor, bool rotate)
		{
			_rotate = rotate;
			_icon = icon.Character.ToString();
			_typeface = type;
			_iconSizePx = iconSizePx;
			_iconSizeRatio = iconSizeRatio;
			_iconColor = new Color (iconColor);
			_rotationStartTime = DateTimeHelpers.CurrentUnixTimeMillis ();
		}

		public override int GetSize(Paint paint, ICharSequence text, int start, int end, Paint.FontMetricsInt fm)
		{
			LOCAL_PAINT.Set(paint);
			ApplyCustomTypeFace(LOCAL_PAINT, _typeface);
			LOCAL_PAINT.GetTextBounds(_icon, 0, 1, TEXT_BOUNDS);
			if (fm != null)
			{
				fm.Descent = (int)(TEXT_BOUNDS.Height() * BASELINE_RATIO);
				fm.Ascent = -(TEXT_BOUNDS.Height() - fm.Descent);
				fm.Top = fm.Ascent;
				fm.Bottom = fm.Descent;
			}
			return TEXT_BOUNDS.Width();
		}

		public override void Draw(Canvas canvas, ICharSequence text, int start, int end, float x, int top, int y, int bottom, Paint paint)
		{
			ApplyCustomTypeFace(paint, _typeface);
			paint.GetTextBounds(_icon, 0, 1, TEXT_BOUNDS);
			canvas.Save();
			if (_rotate)
			{
				float rotation = (DateTimeHelpers.CurrentUnixTimeMillis() - _rotationStartTime) / (float) ROTATION_DURATION * 360f;
				float centerX = x + TEXT_BOUNDS.Width() / 2f;
				float centerY = y - TEXT_BOUNDS.Height() / 2f + TEXT_BOUNDS.Height() * BASELINE_RATIO;
				canvas.Rotate(rotation, centerX, centerY);
			}

			canvas.DrawText(_icon, x - TEXT_BOUNDS.Left, y - TEXT_BOUNDS.Bottom + TEXT_BOUNDS.Height() * BASELINE_RATIO, paint);
			canvas.Restore();
		}

		public virtual bool Animated
		{
			get
			{
				return _rotate;
			}
		}

		private void ApplyCustomTypeFace(Paint paint, Typeface typeface)
		{
			paint.FakeBoldText = false;
			paint.TextSkewX = 0f;
			paint.SetTypeface( typeface);
			if (_rotate)
			{
				paint.ClearShadowLayer();
			}
			if (_iconSizeRatio > 0)
			{
				paint.TextSize = paint.TextSize * _iconSizeRatio;
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
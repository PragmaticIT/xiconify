namespace com.joanzapata.iconify.@internal
{

	using Canvas = android.graphics.Canvas;
	using Paint = android.graphics.Paint;
	using Rect = android.graphics.Rect;
	using Typeface = android.graphics.Typeface;
	using ReplacementSpan = android.text.style.ReplacementSpan;

	public class CustomTypefaceSpan : ReplacementSpan
	{
		private const int ROTATION_DURATION = 2000;
		private static readonly Rect TEXT_BOUNDS = new Rect();
		private static readonly Paint LOCAL_PAINT = new Paint();
		private static readonly float BASELINE_RATIO = 1 / 7f;

		private readonly string icon;
		private readonly Typeface type;
		private readonly float iconSizePx;
		private readonly float iconSizeRatio;
		private readonly int iconColor;
		private readonly bool rotate;
		private readonly long rotationStartTime;

		public CustomTypefaceSpan(Icon icon, Typeface type, float iconSizePx, float iconSizeRatio, int iconColor, bool rotate)
		{
			this.rotate = rotate;
			this.icon = icon.character().ToString();
			this.type = type;
			this.iconSizePx = iconSizePx;
			this.iconSizeRatio = iconSizeRatio;
			this.iconColor = iconColor;
			this.rotationStartTime = DateTimeHelperClass.CurrentUnixTimeMillis();
		}

		public override int getSize(Paint paint, CharSequence text, int start, int end, Paint.FontMetricsInt fm)
		{
			LOCAL_PAINT.set(paint);
			applyCustomTypeFace(LOCAL_PAINT, type);
			LOCAL_PAINT.getTextBounds(icon, 0, 1, TEXT_BOUNDS);
			if (fm != null)
			{
				fm.descent = (int)(TEXT_BOUNDS.height() * BASELINE_RATIO);
				fm.ascent = -(TEXT_BOUNDS.height() - fm.descent);
				fm.top = fm.ascent;
				fm.bottom = fm.descent;
			}
			return TEXT_BOUNDS.width();
		}

		public override void draw(Canvas canvas, CharSequence text, int start, int end, float x, int top, int y, int bottom, Paint paint)
		{
			applyCustomTypeFace(paint, type);
			paint.getTextBounds(icon, 0, 1, TEXT_BOUNDS);
			canvas.save();
			if (rotate)
			{
				float rotation = (DateTimeHelperClass.CurrentUnixTimeMillis() - rotationStartTime) / (float) ROTATION_DURATION * 360f;
				float centerX = x + TEXT_BOUNDS.width() / 2f;
				float centerY = y - TEXT_BOUNDS.height() / 2f + TEXT_BOUNDS.height() * BASELINE_RATIO;
				canvas.rotate(rotation, centerX, centerY);
			}

			canvas.drawText(icon, x - TEXT_BOUNDS.left, y - TEXT_BOUNDS.bottom + TEXT_BOUNDS.height() * BASELINE_RATIO, paint);
			canvas.restore();
		}

		public virtual bool Animated
		{
			get
			{
				return rotate;
			}
		}

		private void applyCustomTypeFace(Paint paint, Typeface tf)
		{
			paint.FakeBoldText = false;
			paint.TextSkewX = 0f;
			paint.Typeface = tf;
			if (rotate)
			{
				paint.clearShadowLayer();
			}
			if (iconSizeRatio > 0)
			{
				paint.TextSize = paint.TextSize * iconSizeRatio;
			}
			else if (iconSizePx > 0)
			{
				paint.TextSize = iconSizePx;
			}
			if (iconColor < int.MaxValue)
			{
				paint.Color = iconColor;
			}
		}
	}
}
namespace com.joanzapata.iconify
{

	using Context = android.content.Context;
	using android.graphics;
	using Drawable = android.graphics.drawable.Drawable;
	using TextPaint = android.text.TextPaint;
	using TypedValue = android.util.TypedValue;

//JAVA TO C# CONVERTER TODO TASK: This Java 'import static' statement cannot be converted to C#:
//	import static android.util.TypedValue.COMPLEX_UNIT_DIP;

	/// <summary>
	/// Embed an icon into a Drawable that can be used as TextView icons, or ActionBar icons.
	/// <pre>
	///     new IconDrawable(context, IconValue.icon_star)
	///           .colorRes(R.color.white)
	///           .actionBarSize();
	/// </pre>
	/// If you don't set the size of the drawable, it will use the size
	/// that is given to him. Note that in an ActionBar, if you don't
	/// set the size explicitly it uses 0, so please use actionBarSize().
	/// </summary>
	public class IconDrawable : Drawable
	{

		public const int ANDROID_ACTIONBAR_ICON_SIZE_DP = 24;

		private Context context;

		private Icon icon;

		private TextPaint paint;

		private int size = -1;

		private int alpha_Renamed = 255;

		/// <summary>
		/// Create an IconDrawable. </summary>
		/// <param name="context"> Your activity or application context. </param>
		/// <param name="iconKey"> The icon key you want this drawable to display. </param>
		/// <exception cref="IllegalArgumentException"> if the key doesn't match any icon. </exception>
		public IconDrawable(Context context, string iconKey)
		{
			Icon icon = Iconify.findIconForKey(iconKey);
			if (icon == null)
			{
				throw new System.ArgumentException("No icon with that key \"" + iconKey + "\".");
			}
			init(context, icon);
		}

		/// <summary>
		/// Create an IconDrawable. </summary>
		/// <param name="context"> Your activity or application context. </param>
		/// <param name="icon">    The icon you want this drawable to display. </param>
		public IconDrawable(Context context, Icon icon)
		{
			init(context, icon);
		}

		private void init(Context context, Icon icon)
		{
			this.context = context;
			this.icon = icon;
			paint = new TextPaint();
			paint.Typeface = Iconify.findTypefaceOf(icon).getTypeface(context);
			paint.Style = Paint.Style.FILL;
			paint.TextAlign = Paint.Align.CENTER;
			paint.UnderlineText = false;
			paint.Color = Color.BLACK;
			paint.AntiAlias = true;
		}

		/// <summary>
		/// Set the size of this icon to the standard Android ActionBar. </summary>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable actionBarSize()
		{
			return sizeDp(ANDROID_ACTIONBAR_ICON_SIZE_DP);
		}

		/// <summary>
		/// Set the size of the drawable. </summary>
		/// <param name="dimenRes"> The dimension resource. </param>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable sizeRes(int dimenRes)
		{
			return sizePx(context.Resources.getDimensionPixelSize(dimenRes));
		}

		/// <summary>
		/// Set the size of the drawable. </summary>
		/// <param name="size"> The size in density-independent pixels (dp). </param>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable sizeDp(int size)
		{
			return sizePx(convertDpToPx(context, size));
		}

		/// <summary>
		/// Set the size of the drawable. </summary>
		/// <param name="size"> The size in pixels (px). </param>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable sizePx(int size)
		{
			this.size = size;
			setBounds(0, 0, size, size);
			invalidateSelf();
			return this;
		}

		/// <summary>
		/// Set the color of the drawable. </summary>
		/// <param name="color"> The color, usually from android.graphics.Color or 0xFF012345. </param>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable color(int color)
		{
			paint.Color = color;
			invalidateSelf();
			return this;
		}

		/// <summary>
		/// Set the color of the drawable. </summary>
		/// <param name="colorRes"> The color resource, from your R file. </param>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable colorRes(int colorRes)
		{
			paint.Color = context.Resources.getColor(colorRes);
			invalidateSelf();
			return this;
		}

		/// <summary>
		/// Set the alpha of this drawable. </summary>
		/// <param name="alpha"> The alpha, between 0 (transparent) and 255 (opaque). </param>
		/// <returns> The current IconDrawable for chaining. </returns>
		public virtual IconDrawable alpha(int alpha)
		{
			Alpha = alpha;
			invalidateSelf();
			return this;
		}

		public override int IntrinsicHeight
		{
			get
			{
				return size;
			}
		}

		public override int IntrinsicWidth
		{
			get
			{
				return size;
			}
		}

		public override void draw(Canvas canvas)
		{
			Rect bounds = Bounds;
			int height = bounds.height();
			paint.TextSize = height;
			Rect textBounds = new Rect();
			string textValue = icon.character().ToString();
			paint.getTextBounds(textValue, 0, 1, textBounds);
			int textHeight = textBounds.height();
			float textBottom = bounds.top + (height - textHeight) / 2f + textHeight - textBounds.bottom;
			canvas.drawText(textValue, bounds.exactCenterX(), textBottom, paint);
		}

		public override bool Stateful
		{
			get
			{
				return true;
			}
		}

		public override bool setState(int[] stateSet)
		{
			int oldValue = paint.Alpha;
			int newValue = isEnabled(stateSet) ? alpha_Renamed : alpha_Renamed / 2;
			paint.Alpha = newValue;
			return oldValue != newValue;
		}

		public override int Alpha
		{
			set
			{
				this.alpha_Renamed = value;
				paint.Alpha = value;
			}
		}

		public override ColorFilter ColorFilter
		{
			set
			{
				paint.ColorFilter = value;
			}
		}

		public override void clearColorFilter()
		{
			paint.ColorFilter = null;
		}

		public override int Opacity
		{
			get
			{
				return this.alpha_Renamed;
			}
		}

		/// <summary>
		/// Sets paint style. </summary>
		/// <param name="style"> to be applied </param>
		public virtual Paint.Style Style
		{
			set
			{
				paint.Style = value;
			}
		}

		// Util
		private bool isEnabled(int[] stateSet)
		{
			foreach (int state in stateSet)
			{
				if (state == android.R.attr.state_enabled)
				{
					return true;
				}
			}
			return false;
		}

		// Util
		private int convertDpToPx(Context context, float dp)
		{
			return (int) TypedValue.applyDimension(COMPLEX_UNIT_DIP, dp, context.Resources.DisplayMetrics);
		}
	}
}
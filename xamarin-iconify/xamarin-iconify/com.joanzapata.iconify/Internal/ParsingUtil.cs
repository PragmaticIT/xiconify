using System;
using System.Collections.Generic;	
using Context = Android.Content.Context;
using Resources = Android.Content.Res.Resources;
using Color = Android.Graphics.Color;
//using ViewCompat = Android.Support.V4.View.ViewCompat;
using SpannableStringBuilder = Android.Text.SpannableStringBuilder;
using Spanned = Android.Text.SpannedString;
using TypedValue = Android.Util.TypedValue;
using TextView = Android.Widget.TextView;
using Java.Lang;
using System.Linq;
using JoanZapata.XamarinIconify.JavaUtils;

namespace JoanZapata.XamarinIconify.Internal
{



	public sealed class ParsingUtil
	{

		// Prevents instantiation
		private ParsingUtil()
		{
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public static CharSequence parse(android.content.Context context, java.util.List<IconFontDescriptorWrapper> iconFontDescriptors, CharSequence text, final android.widget.TextView target)
		public static ICharSequence parse(Context context, IList<IconFontDescriptorWrapper> iconFontDescriptors, ICharSequence text, TextView target)
		{
			context = context.ApplicationContext;

			// Analyse the text and replace {} blocks with the appropriate character
			// Retain all transformations in the accumulator
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final android.text.SpannableStringBuilder spannableBuilder = new android.text.SpannableStringBuilder(text);
			SpannableStringBuilder spannableBuilder = new SpannableStringBuilder(text);
			recursivePrepareSpannableIndexes(context, text.ToString(), spannableBuilder, iconFontDescriptors, 0);
			bool isAnimated = hasAnimatedSpans(spannableBuilder);

			// If animated, periodically invalidate the TextView so that the
			// CustomTypefaceSpan can redraw itself
			if (isAnimated)
			{
				if (target == null)
				{
					throw new System.ArgumentException("You can't use \"spin\" without providing the target TextView.");
				}
				if (!(target is HasOnViewAttachListener))
				{
					throw new System.ArgumentException(target.GetType().Name + " does not implement " + "HasOnViewAttachListener. Please use IconTextView, IconButton or IconToggleButton.");
				}

				((HasOnViewAttachListener) target).OnViewAttachListener = new HasOnViewAttachListener_OnViewAttachListenerAnonymousInnerClassHelper(target);

			}
			else if (target is HasOnViewAttachListener)
			{
				((HasOnViewAttachListener) target).OnViewAttachListener = null;
			}

			return spannableBuilder;
		}

		private class HasOnViewAttachListener_OnViewAttachListenerAnonymousInnerClassHelper : HasOnViewAttachListener_OnViewAttachListener
		{
			private TextView target;

			public HasOnViewAttachListener_OnViewAttachListenerAnonymousInnerClassHelper(TextView target)
			{
				this.target = target;
			}

			internal bool isAttached = false;

			public virtual void onAttach()
			{
				isAttached = true;
//				ViewCompat.postOnAnimation(target, () =>
//				{
//					if (isAttached)
//					{
//						target.invalidate();
//						ViewCompat.postOnAnimation(target, this);
//					}
//				});
			}

			public virtual void onDetach()
			{
				isAttached = false;
			}
		}

		private static bool hasAnimatedSpans(SpannableStringBuilder spannableBuilder)
		{
			var p= spannableBuilder.GetSpans(0, spannableBuilder.Length(), Java.Lang.Class.FromType( typeof(CustomTypefaceSpan)));
			CustomTypefaceSpan[] spans = null;// spannableBuilder.GetSpans(0, spannableBuilder.Length(), Java.Lang.Class.FromType( typeof(CustomTypefaceSpan)));
			foreach (CustomTypefaceSpan span in spans)
			{
				if (span.Animated)
				{
					return true;
				}
			}
			return false;
		}

		private static void recursivePrepareSpannableIndexes(Context context, string fullText, SpannableStringBuilder text, IList<IconFontDescriptorWrapper> iconFontDescriptors, int start)
		{

			// Try to find a {...} in the string and extract expression from it
			string stringText = text.ToString();
			int startIndex = stringText.IndexOf("{", start, StringComparison.Ordinal);
			if (startIndex == -1)
			{
				return;
			}
			int endIndex = stringText.IndexOf("}", startIndex, StringComparison.Ordinal) + 1;
			string expression = stringText.Substring(startIndex + 1, endIndex - 1 - (startIndex + 1));

			// Split the expression and retrieve the icon key
			string[] strokes = expression.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
			string key = strokes[0];

			// Loop through the descriptors to find a key match
			IconFontDescriptorWrapper iconFontDescriptor = null;
			Icon? icon = null;
			for (int i = 0; i < iconFontDescriptors.Count; i++)
			{
				iconFontDescriptor = iconFontDescriptors[i];
				icon = iconFontDescriptor.GetIcon(key);
				if (!icon.HasValue)
				{
					break;
				}
			}

			// If no match, ignore and continue
			if (!icon.HasValue)
			{
				recursivePrepareSpannableIndexes(context, fullText, text, iconFontDescriptors, endIndex);
				return;
			}

			// See if any more stroke within {} should be applied
			float iconSizePx = -1;
			int iconColor = int.MaxValue;
			float iconSizeRatio = -1;
			bool spin = false;
			for (int i = 1; i < strokes.Length; i++)
			{
				string stroke = strokes[i];

				// Look for "spin"
				if (stroke.Equals("spin", StringComparison.CurrentCultureIgnoreCase))
				{
					spin = true;
				}

				// Look for an icon size
				else if (stroke.matches("([0-9]*(\\.[0-9]*)?)dp"))
				{
					iconSizePx = dpToPx(context, Convert.ToSingle(stroke.Substring(0, stroke.Length - 2)));
				}
				else if (stroke.matches("([0-9]*(\\.[0-9]*)?)sp"))
				{
					iconSizePx = spToPx(context, Convert.ToSingle(stroke.Substring(0, stroke.Length - 2)));
				}
				else if (stroke.matches("([0-9]*)px"))
				{
					iconSizePx = Convert.ToInt32(stroke.Substring(0, stroke.Length - 2));
				}
				else if (stroke.matches("@dimen/(.*)"))
				{
					iconSizePx = getPxFromDimen(context, stroke.Substring(7));
					if (iconSizePx < 0)
					{
						throw new System.ArgumentException("Unknown resource " + stroke + " in \"" + fullText + "\"");
					}
				}
				else if (stroke.matches("([0-9]*(\\.[0-9]*)?)%"))
				{
					iconSizeRatio = Convert.ToSingle(stroke.Substring(0, stroke.Length - 1)) / 100f;
				}

				// Look for an icon color
				else if (stroke.matches("#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})"))
				{
					iconColor = Color.ParseColor(stroke);
				}
				else if (stroke.matches("@color/(.*)"))
				{
					iconColor = getColorFromResource(context, stroke.Substring(7));
					if (iconColor == int.MaxValue)
					{
						throw new System.ArgumentException("Unknown resource " + stroke + " in \"" + fullText + "\"");
					}
				}
				else
				{
					throw new System.ArgumentException("Unknown expression " + stroke + " in \"" + fullText + "\"");
				}


			}

			// Replace the character and apply the typeface
			text.Replace(startIndex, endIndex, "" + icon.Value.Character);
			text.SetSpan(new CustomTypefaceSpan(icon.Value, iconFontDescriptor.GetTypeface(context), iconSizePx, iconSizeRatio, iconColor, spin), startIndex, startIndex + 1, Android.Text.SpanTypes.InclusiveExclusive);
			recursivePrepareSpannableIndexes(context, fullText, text, iconFontDescriptors, startIndex);
		}

		public static float getPxFromDimen(Context context, string resName)
		{
			Resources resources = context.Resources;
			int resId = resources.GetIdentifier(resName, "dimen", context.PackageName);
			if (resId <= 0)
			{
				return -1;
			}
			return resources.GetDimension(resId);
		}

		public static int getColorFromResource(Context context, string resName)
		{
			Resources resources = context.Resources;
			int resId = resources.GetIdentifier(resName, "color", context.PackageName);
			if (resId <= 0)
			{
				return int.MaxValue;
			}
			return resources.GetColor(resId);
		}

		public static float dpToPx(Context context, float dp)
		{
			return TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Dip, dp, context.Resources.DisplayMetrics);
		}

		public static float spToPx(Context context, float sp)
		{
			return TypedValue.ApplyDimension(Android.Util.ComplexUnitType.Sp, sp, context.Resources.DisplayMetrics);
		}

	}

}
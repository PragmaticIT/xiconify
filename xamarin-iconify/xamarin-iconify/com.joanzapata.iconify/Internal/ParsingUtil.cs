using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Util;
using Android.Widget;
using Java.Lang;
using JoanZapata.XamarinIconify.JavaUtils;
using Spanned = Android.Text.SpannedString;

#if V4COMPAT
using ViewCompat = Android.Support.V4.View.ViewCompat;
#endif

namespace JoanZapata.XamarinIconify.Internal
{
    public static class ParsingUtil
    {
        public static ICharSequence Parse(Context context, IList<IconFontDescriptorWrapper> iconFontDescriptors,
            ICharSequence text, TextView target)
        {
            context = context.ApplicationContext;

            // Analyse the text and replace {} blocks With the appropriate character
            // Retain all transformations in the accumulator
            var spannableBuilder = new SpannableStringBuilder(text);
            RecursivePrepareSpannableIndexes(context, text.ToString(), spannableBuilder, iconFontDescriptors, 0);
            var isAnimated = HasAnimatedSpans(spannableBuilder);

            // If animated, periodically invalidate the TextView so that the
            // CustomTypefaceSpan can redraw itself
            if (isAnimated)
            {
                if (target == null)
                {
                    throw new ArgumentException("You can't use \"spin\" without providing the target TextView.");
                }
                if (!(target is IHasOnViewAttachListener))
                {
                    throw new ArgumentException(target.GetType().Name + " does not implement " +
                                                "HasOnViewAttachListener. Please use IconTextView, IconButton or IconToggleButton.");
                }

                ((IHasOnViewAttachListener) target).OnViewAttachListener =
                    new OnViewAttachListenerOnViewAttachListenerAnonymousInnerClassHelper(target);
            }
            else if (target is IHasOnViewAttachListener)
            {
                ((IHasOnViewAttachListener) target).OnViewAttachListener = null;
            }

            return spannableBuilder;
        }

        private static bool HasAnimatedSpans(SpannableStringBuilder spannableBuilder)
        {
            var spans = spannableBuilder.GetSpans(0, spannableBuilder.Length(),
                Class.FromType(typeof (CustomTypefaceSpan)));
            return spans.OfType<CustomTypefaceSpan>().Any(customTypefaceSpan => customTypefaceSpan.Animated);
        }

        private static void RecursivePrepareSpannableIndexes(Context context, string fullText,
            SpannableStringBuilder text, IList<IconFontDescriptorWrapper> iconFontDescriptors, int start)
        {
            // Try to find a {...} in the string and extract expression from it
            var stringText = text.ToString();
            var startIndex = stringText.IndexOf("{", start, StringComparison.Ordinal);
            if (startIndex == -1)
            {
                return;
            }
            var endIndex = stringText.IndexOf("}", startIndex, StringComparison.Ordinal) + 1;
            var expression = stringText.Substring(startIndex + 1, endIndex - 1 - (startIndex + 1));

            // Split the expression and retrieve the icon key
            var strokes = expression.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var key = strokes[0];

            // Loop through the descriptors to find a key match
            IconFontDescriptorWrapper iconFontDescriptor = null;
            Icon? icon = null;
            for (var i = 0; i < iconFontDescriptors.Count; i++)
            {
                iconFontDescriptor = iconFontDescriptors[i];
                icon = iconFontDescriptor.GetIcon(key);
                if (icon.HasValue)
                {
                    break;
                }
            }

            // If no match, ignore and continue
            if (!icon.HasValue)
            {
                RecursivePrepareSpannableIndexes(context, fullText, text, iconFontDescriptors, endIndex);
                return;
            }

            // See if any more stroke within {} should be applied
            float iconSizePx = -1;
            var iconColor = int.MaxValue;
            float iconSizeRatio = -1;
            var spin = false;
            for (var i = 1; i < strokes.Length; i++)
            {
                var stroke = strokes[i];

                // Look for "spin"
                if (stroke.Equals("spin", StringComparison.CurrentCultureIgnoreCase))
                {
                    spin = true;
                }

                // Look for an icon size
                else if (stroke.Matches("([0-9]*(\\.[0-9]*)?)dp"))
                {
                    iconSizePx = DpToPx(context, Convert.ToSingle(stroke.Substring(0, stroke.Length - 2)));
                }
                else if (stroke.Matches("([0-9]*(\\.[0-9]*)?)sp"))
                {
                    iconSizePx = SpToPx(context, Convert.ToSingle(stroke.Substring(0, stroke.Length - 2)));
                }
                else if (stroke.Matches("([0-9]*)px"))
                {
                    iconSizePx = Convert.ToInt32(stroke.Substring(0, stroke.Length - 2));
                }
                else if (stroke.Matches("@dimen/(.*)"))
                {
                    iconSizePx = GetPxFromDimen(context, stroke.Substring(7));
                    if (iconSizePx < 0)
                    {
                        throw new ArgumentException("Unknown resource " + stroke + " in \"" + fullText + "\"");
                    }
                }
                else if (stroke.Matches("([0-9]*(\\.[0-9]*)?)%"))
                {
                    iconSizeRatio = Convert.ToSingle(stroke.Substring(0, stroke.Length - 1))/100f;
                }

                // Look for an icon WithColor
                else if (stroke.Matches("#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})"))
                {
                    iconColor = Color.ParseColor(stroke);
                }
                else if (stroke.Matches("@WithColor/(.*)"))
                {
                    iconColor = GetColorFromResource(context, stroke.Substring(7));
                    if (iconColor == int.MaxValue)
                    {
                        throw new ArgumentException("Unknown resource " + stroke + " in \"" + fullText + "\"");
                    }
                }
                else
                {
                    throw new ArgumentException("Unknown expression " + stroke + " in \"" + fullText + "\"");
                }
            }

            // Replace the character and apply the typeface
            text.Replace(startIndex, endIndex, "" + icon.Value.Character);
            text.SetSpan(
                new CustomTypefaceSpan(icon.Value, iconFontDescriptor.GetTypeface(context), iconSizePx, iconSizeRatio,
                    iconColor, spin), startIndex, startIndex + 1, SpanTypes.InclusiveExclusive);
            RecursivePrepareSpannableIndexes(context, fullText, text, iconFontDescriptors, startIndex);
        }

        public static float GetPxFromDimen(Context context, string resName)
        {
            var resources = context.Resources;
            var resId = resources.GetIdentifier(resName, "dimen", context.PackageName);
            if (resId <= 0)
            {
                return -1;
            }
            return resources.GetDimension(resId);
        }

        public static int GetColorFromResource(Context context, string resName)
        {
            var resources = context.Resources;
            var resId = resources.GetIdentifier(resName, "WithColor", context.PackageName);
            if (resId <= 0)
            {
                return int.MaxValue;
            }
            return resources.GetColor(resId);
        }

        public static float DpToPx(Context context, float dp)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, context.Resources.DisplayMetrics);
        }

        public static float SpToPx(Context context, float sp)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Sp, sp, context.Resources.DisplayMetrics);
        }

        private class OnViewAttachListenerOnViewAttachListenerAnonymousInnerClassHelper :
            IOnViewAttachListener
        {
            private readonly TextView _target;
            private bool _isAttached;

            public OnViewAttachListenerOnViewAttachListenerAnonymousInnerClassHelper(TextView target)
            {
                _target = target;
            }

            public void OnAttach()
            {
                _isAttached = true;
#if V4COMPAT
				ViewCompat.postOnAnimation(target, () =>
				{
					if (isAttached)
					{
						target.invalidate();
						ViewCompat.postOnAnimation(target, this);
					}
				});
				#else

                AnimationHandler();
#endif
            }

            public void OnDetach()
            {
                _isAttached = false;
            }

            private void AnimationHandler()
            {
                _target.PostDelayed(() =>
                {
                    if (_isAttached)
                    {
                        _target.Invalidate();
                        AnimationHandler();
                    }
                }, 10);
            }
        }
    }
}
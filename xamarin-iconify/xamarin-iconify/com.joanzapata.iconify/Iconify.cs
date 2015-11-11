using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Widget;
using Java.Lang;
using JoanZapata.XamarinIconify.Internal;

namespace JoanZapata.XamarinIconify
{
    public class Iconify
    {
        /// <summary>
        ///     List of icon font descriptors
        /// </summary>
        private static readonly IList<IconFontDescriptorWrapper> iconFontDescriptors =
            new List<IconFontDescriptorWrapper>();

        /// <summary>
        ///     Add support for a new icon font.
        /// </summary>
        /// <param name="iconFontDescriptor"> The IconDescriptor holding the ttf file reference and its mappings. </param>
        /// <returns> An initializer instance for chain calls. </returns>
        public static IconifyInitializer With(IIconFontDescriptor iconFontDescriptor)
        {
            return new IconifyInitializer(iconFontDescriptor);
        }

        /// <summary>
        ///     Replace "{}" tags in the given text views With actual icons, requesting the IconFontDescriptors
        ///     one after the others.
        ///     <para>
        ///         <strong>This is a one time call.</strong> If you call <seealso cref="TextView#setText(CharSequence)" /> after
        ///         this,
        ///         you'll need to call it again.
        ///     </para>
        /// </summary>
        /// <param name="textViews"> The TextView(s) to enhance. </param>
        public static void AddIcons(params TextView[] textViews)
        {
            foreach (var textView in textViews)
            {
                if (textView == null)
                {
                    continue;
                }
                textView.TextFormatted = Compute(textView.Context, textView.TextFormatted, textView);
            }
        }

        private static void AddIconFontDescriptor(IIconFontDescriptor iconFontDescriptor)
        {
            // Prevent duplicates
            if (iconFontDescriptors.Any(wrapper => wrapper.IconFontDescriptor.FontFileName.Equals(iconFontDescriptor.FontFileName)))
            {
                return;
            }

            // Add to the list
            iconFontDescriptors.Add(new IconFontDescriptorWrapper(iconFontDescriptor));
        }

        public static ICharSequence Compute(Context context, ICharSequence text)
        {
            return Compute(context, text, null);
        }

        public static ICharSequence Compute(Context context, ICharSequence text, TextView target)
        {
            return ParsingUtil.Parse(context, iconFontDescriptors, text, target);
        }

        /// <summary>
        ///     Finds the Typeface to apply for a given icon.
        /// </summary>
        /// <param name="icon"> The icon for which you need the typeface. </param>
        /// <returns>
        ///     The font descriptor which contains info about the typeface to apply, or null
        ///     if the icon cannot be found. In that case, check that you properly added the modules
        ///     using <seealso cref="#With(IconFontDescriptor)" />} prior to calling this method.
        /// </returns>
        public static IconFontDescriptorWrapper FindTypefaceOf(Icon icon)
        {
            return iconFontDescriptors.FirstOrDefault(iconFontDescriptor => iconFontDescriptor.HasIcon(icon));
        }

        /// <summary>
        ///     Retrieve an icon from a key,
        /// </summary>
        /// <returns> The icon, or null if no icon Matches the key. </returns>
        internal static Icon? FindIconForKey(string iconKey)
        {
            for (int i = 0, iconFontDescriptorsSize = iconFontDescriptors.Count; i < iconFontDescriptorsSize; i++)
            {
                var iconFontDescriptor = iconFontDescriptors[i];
                var icon = iconFontDescriptor.GetIcon(iconKey);
                if (icon.HasValue)
                {
                    return icon.Value;
                }
            }
            return null;
        }

        /// <summary>
        ///     Allows chain calls on <seealso cref="Iconify#With(IconFontDescriptor)" />.
        /// </summary>
        public class IconifyInitializer
        {
            public IconifyInitializer(IIconFontDescriptor iconFontDescriptor)
            {
                AddIconFontDescriptor(iconFontDescriptor);
            }

            /// <summary>
            ///     Add support for a new icon font.
            /// </summary>
            /// <param name="iconFontDescriptor"> The IconDescriptor holding the ttf file reference and its mappings. </param>
            /// <returns> An initializer instance for chain calls. </returns>
            public virtual IconifyInitializer With(IIconFontDescriptor iconFontDescriptor)
            {
                AddIconFontDescriptor(iconFontDescriptor);
                return this;
            }
        }
    }
}
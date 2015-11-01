using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using JoanZapata.XamarinIconify.Internal;
using Java.Lang;
using System.Linq;

namespace JoanZapata.XamarinIconify
{

	public class Iconify
	{

		/// <summary>
		/// List of icon font descriptors </summary>
		private static readonly IList<IconFontDescriptorWrapper> iconFontDescriptors = new List<IconFontDescriptorWrapper>();

		/// <summary>
		/// Add support for a new icon font. </summary>
		/// <param name="iconFontDescriptor"> The IconDescriptor holding the ttf file reference and its mappings. </param>
		/// <returns> An initializer instance for chain calls. </returns>
		public static IconifyInitializer with(IIconFontDescriptor iconFontDescriptor)
		{
			return new IconifyInitializer(iconFontDescriptor);
		}

		/// <summary>
		/// Replace "{}" tags in the given text views with actual icons, requesting the IconFontDescriptors
		/// one after the others.<para>
		/// <strong>This is a one time call.</strong> If you call <seealso cref="TextView#setText(CharSequence)"/> after this,
		/// you'll need to call it again.
		/// </para>
		/// </summary>
		/// <param name="textViews"> The TextView(s) to enhance. </param>
		public static void addIcons(params TextView[] textViews)
		{
			foreach (TextView textView in textViews)
			{
				if (textView == null)
				{
					continue;
				}
				textView.TextFormatted= compute(textView.Context, textView.TextFormatted, textView);
			}
		}

		private static void addIconFontDescriptor(IIconFontDescriptor iconFontDescriptor)
		{

			// Prevent duplicates
			foreach (IconFontDescriptorWrapper wrapper in iconFontDescriptors)
			{
				if (wrapper.IconFontDescriptor.FontFileName.Equals(iconFontDescriptor.FontFileName))
				{
					return;
				}
			}

			// Add to the list
			iconFontDescriptors.Add(new IconFontDescriptorWrapper(iconFontDescriptor));

		}

		public static ICharSequence compute(Context context, ICharSequence text)
		{
			return compute(context, text, null);
		}

		public static ICharSequence compute(Context context, ICharSequence text, TextView target)
		{
			return ParsingUtil.Parse(context, iconFontDescriptors, text, target);
		}

		/// <summary>
		/// Allows chain calls on <seealso cref="Iconify#with(IconFontDescriptor)"/>.
		/// </summary>
		public class IconifyInitializer
		{

			public IconifyInitializer(IIconFontDescriptor iconFontDescriptor)
			{
				Iconify.addIconFontDescriptor(iconFontDescriptor);
			}

			/// <summary>
			/// Add support for a new icon font. </summary>
			/// <param name="iconFontDescriptor"> The IconDescriptor holding the ttf file reference and its mappings. </param>
			/// <returns> An initializer instance for chain calls. </returns>
			public virtual IconifyInitializer with(IIconFontDescriptor iconFontDescriptor)
			{
				Iconify.addIconFontDescriptor(iconFontDescriptor);
				return this;
			}
		}

		/// <summary>
		/// Finds the Typeface to apply for a given icon. </summary>
		/// <param name="icon"> The icon for which you need the typeface. </param>
		/// <returns> The font descriptor which contains info about the typeface to apply, or null
		/// if the icon cannot be found. In that case, check that you properly added the modules
		/// using <seealso cref="#with(IconFontDescriptor)"/>} prior to calling this method. </returns>
		public static IconFontDescriptorWrapper findTypefaceOf(Icon icon)
		{
			foreach (IconFontDescriptorWrapper iconFontDescriptor in iconFontDescriptors)
			{
				if (iconFontDescriptor.HasIcon(icon))
				{
					return iconFontDescriptor;
				}
			}
			return null;
		}


		/// <summary>
		/// Retrieve an icon from a key, </summary>
		/// <returns> The icon, or null if no icon matches the key. </returns>
		internal static Icon? findIconForKey(string iconKey)
		{
			for (int i = 0, iconFontDescriptorsSize = iconFontDescriptors.Count; i < iconFontDescriptorsSize; i++)
			{
				IconFontDescriptorWrapper iconFontDescriptor = iconFontDescriptors[i];
				var icon = iconFontDescriptor.GetIcon(iconKey);
				if (icon.HasValue)
				{
					return icon.Value;
				}
			}
			return null;
		}
	}

}
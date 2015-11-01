using System.Collections.Generic;

namespace com.joanzapata.iconify.@internal
{

	using Context = android.content.Context;
	using Typeface = android.graphics.Typeface;


	public class IconFontDescriptorWrapper
	{

		private readonly IconFontDescriptor iconFontDescriptor;

		private readonly IDictionary<string, Icon> iconsByKey;

		private Typeface cachedTypeface;

		public IconFontDescriptorWrapper(IconFontDescriptor iconFontDescriptor)
		{
			this.iconFontDescriptor = iconFontDescriptor;
			iconsByKey = new Dictionary<string, Icon>();
			Icon[] characters = iconFontDescriptor.characters();
			for (int i = 0, charactersLength = characters.Length; i < charactersLength; i++)
			{
				Icon icon = characters[i];
				iconsByKey[icon.key()] = icon;
			}
		}

		public virtual Icon getIcon(string key)
		{
			return iconsByKey[key];
		}

		public virtual IconFontDescriptor IconFontDescriptor
		{
			get
			{
				return iconFontDescriptor;
			}
		}

		public virtual Typeface getTypeface(Context context)
		{
			if (cachedTypeface != null)
			{
				return cachedTypeface;
			}
			lock (this)
			{
				if (cachedTypeface != null)
				{
					return cachedTypeface;
				}
				cachedTypeface = Typeface.createFromAsset(context.Assets, iconFontDescriptor.ttfFileName());
				return cachedTypeface;
			}
		}

		public virtual bool hasIcon(Icon icon)
		{
			return iconsByKey.Values.Contains(icon);
		}
	}

}
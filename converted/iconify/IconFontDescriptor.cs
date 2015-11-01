namespace com.joanzapata.iconify
{

	/// <summary>
	/// An IconFontDescriptor defines a TTF font file
	/// and is able to map keys with characters in this file.
	/// </summary>
	public interface IconFontDescriptor
	{

		/// <summary>
		/// The TTF file name. </summary>
		/// <returns> a name with no slash, present in the assets. </returns>
		string ttfFileName();

		Icon[] characters();

	}

}
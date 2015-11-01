namespace com.joanzapata.iconify
{

	/// <summary>
	/// Icon represents one icon in an icon font.
	/// </summary>
	public interface Icon
	{

		/// <summary>
		/// The key of icon, for example 'fa-ok' </summary>
		string key();

		/// <summary>
		/// The character matching the key in the font, for example '\u4354' </summary>
		char character();

	}

}
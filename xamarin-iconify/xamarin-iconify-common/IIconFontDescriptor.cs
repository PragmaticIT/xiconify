using System.Collections.Generic;
using System.Linq;

namespace JoanZapata.XamarinIconify
{

	/// <summary>
	/// An IconFontDescriptor defines a TTF font file
	/// and is able to map keys with characters in this file.
	/// </summary>
	public interface IIconFontDescriptor
	{

		/// <summary>
		/// The TTF file name. </summary>
		/// <returns> a name with no slash, present in the assets. </returns>
		string FontFileName{get;}
		ILookup<string,Icon> Characters{ get; }
	}

}
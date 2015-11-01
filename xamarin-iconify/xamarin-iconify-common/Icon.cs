namespace JoanZapata.XamarinIconify
{

	/// <summary>
	/// Icon represents one icon in an icon font.
	/// key: The key of icon, for example 'fa-ok'
	/// value: The character matching the key in the font, for example '\u4354'
	/// </summary>
	public struct Icon
	{
		private readonly string _key;
		private readonly char _character;

		public Icon (string key, char character)
		{
			_key = key;
			_character = character;
		}
		/// <summary>
		/// The key of icon, for example 'fa-ok' </summary>
		public string Key{get{return _key;}}

		/// <summary>
		/// The character matching the key in the font, for example '\u4354' </summary>
		public char Character{get{return _character;}}

	}
//	public class Icon:IIcon{
//		public Icon (string key, char character)
//		{
//			Key = key;
//			Character = character;
//		}
//		#region IIcon implementation
//		public string Key { get; private set; }
//		public char Character { get; private set; }
//		#endregion
//	}
}
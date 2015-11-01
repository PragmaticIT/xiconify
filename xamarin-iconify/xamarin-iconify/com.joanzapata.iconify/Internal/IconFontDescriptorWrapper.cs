using System;
using System.IO;
using System.Linq;
using Android.Content;
using Android.Graphics;
using Path=System.IO.Path;

namespace JoanZapata.XamarinIconify.Internal
{
	public class IconFontDescriptorWrapper
	{
		private readonly object _lck = new object ();
		private readonly IIconFontDescriptor _iconFontDescriptor;
		private readonly ILookup<string, Icon> _characters;
		private Typeface _cachedTypeface;

		public IconFontDescriptorWrapper(IIconFontDescriptor iconFontDescriptor)
		{
			_iconFontDescriptor = iconFontDescriptor;
			_characters = iconFontDescriptor.Characters;
		}

		public virtual Icon? GetIcon(string key)
		{
//			var p1 = _characters [key.Replace ('-', '_')];
//			var p2 = p1.First ();
//			var p3 = _characters [key];
//			var p4 = p3.First ();
//			var p5 = _characters.First (x => x.Key == key || x.Key == key.Replace ('-', '_'));
			var found=_characters [key.Replace ('-', '_')].Union (_characters [key]);
			return (found.Any ()) ? found.First () : (Icon?)null;
		}

		public virtual IIconFontDescriptor IconFontDescriptor
		{
			get
			{
				return _iconFontDescriptor;
			}
		}

		public virtual Typeface GetTypeface(Context context)
		{
			if (_cachedTypeface != null)
			{
				return _cachedTypeface;
			}
			lock (_lck)
			{
                //if (_cachedTypeface != null)
                //{
                //    return _cachedTypeface;
                //}
				_cachedTypeface = CreateTypeface (context);
				return _cachedTypeface;
			}
		}

		public virtual bool HasIcon(Icon icon)
		{
			return _characters.Contains(icon.Key);
		}

		private Typeface CreateTypeface (Context context){
			if(context.Assets.List(_iconFontDescriptor.FontFileName).Any())
				return Typeface.CreateFromAsset(context.Assets, _iconFontDescriptor.FontFileName);
			var path = Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal), "fonts", _iconFontDescriptor.FontFileName);

			if (!FileExists (path)) {
				SaveFontFile (path);
			}

			if(FileExists(path)){
				return Typeface.CreateFromFile(path);
			}
            throw new ArgumentOutOfRangeException("FontFileName", "Font does not exist or can't write to disk");
		}


		private bool FileExists(string path){
			return File.Exists (path);
		}

		private void SaveFontFile(string path){
			var resourceName = _iconFontDescriptor.GetType ().Assembly.GetManifestResourceNames ().FirstOrDefault (x => x.EndsWith (_iconFontDescriptor.FontFileName));
			if (resourceName == null)
				throw new ArgumentOutOfRangeException ("No such font in resources: " + _iconFontDescriptor.FontFileName);
			Directory.CreateDirectory (Path.GetDirectoryName (path));
			using (var file=File.Create (path)) {
				using (var resource = _iconFontDescriptor.GetType ().Assembly.GetManifestResourceStream (resourceName)) {
					resource.CopyTo (file);
					resource.Close ();
				}
				file.Flush ();
				file.Close ();
			}
		}

	}

}
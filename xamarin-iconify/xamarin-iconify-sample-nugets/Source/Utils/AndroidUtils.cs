using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;

namespace JoanZapata.XamarinIconify.Sample.Utils
{
	public static class AndroidUtils
	{

		/// <summary>
		/// Returns the available screensize, including status bar and navigation bar </summary>
		public static Size getScreenSize(Activity context)
		{
			Display display = context.WindowManager.DefaultDisplay;
			int realWidth;
			int realHeight;

			if ((int)Build.VERSION.SdkInt >= 17)
			{
				DisplayMetrics realMetrics = new DisplayMetrics();
				display.GetRealMetrics(realMetrics);
				realWidth = realMetrics.WidthPixels;
				realHeight = realMetrics.HeightPixels;

			}
			else if ((int)Build.VERSION.SdkInt >= 14)
			{
//				try
//				{
//					Method mGetRawH = typeof(Display).GetMethod("getRawHeight");
//					Method mGetRawW = typeof(Display).GetMethod("getRawWidth");
//					realWidth = (int?) mGetRawW.invoke(display);
//					realHeight = (int?) mGetRawH.invoke(display);
//				}
//				catch (Exception e)
//				{
					//this may not be 100% accurate, but it's all we've got
					realWidth = display.Width;
					realHeight = display.Height;
//					Log.e("Display Info", "Couldn't use reflection to get the real display metrics.");
//				}

			}
			else
			{
				//This should be close, as lower API devices should not have window navigation bars
				realWidth = display.Width;
				realHeight = display.Height;
			}

			return new Size(realWidth, realHeight);
		}

		public class Size
		{
			public readonly int width;
			public readonly int height;

			public Size(int width, int height)
			{
				this.width = width;
				this.height = height;
			}
		}
	}

}
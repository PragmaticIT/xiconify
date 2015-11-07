using System;

namespace com.joanzapata.iconify.sample.utils
{

	using Activity = android.app.Activity;
	using Build = android.os.Build;
	using DisplayMetrics = android.util.DisplayMetrics;
	using Log = android.util.Log;
	using Display = android.view.Display;

	public sealed class AndroidUtils
	{

		// Prevent instantiation
		private AndroidUtils()
		{
		}

		/// <summary>
		/// Returns the available screensize, including status bar and navigation bar </summary>
		public static Size getScreenSize(Activity context)
		{
			Display display = context.WindowManager.DefaultDisplay;
			int realWidth;
			int realHeight;

			if (Build.VERSION.SDK_INT >= 17)
			{
				DisplayMetrics realMetrics = new DisplayMetrics();
				display.getRealMetrics(realMetrics);
				realWidth = realMetrics.widthPixels;
				realHeight = realMetrics.heightPixels;

			}
			else if (Build.VERSION.SDK_INT >= 14)
			{
				try
				{
					Method mGetRawH = typeof(Display).GetMethod("getRawHeight");
					Method mGetRawW = typeof(Display).GetMethod("getRawWidth");
					realWidth = (int?) mGetRawW.invoke(display);
					realHeight = (int?) mGetRawH.invoke(display);
				}
				catch (Exception e)
				{
					//this may not be 100% accurate, but it's all we've got
					realWidth = display.Width;
					realHeight = display.Height;
					Log.e("Display Info", "Couldn't use reflection to get the real display metrics.");
				}

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
/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 08.04.2011
 * Time: 21:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.ontslab.misc
{
	/// <summary>
	/// Description of IntervalUtils.
	/// </summary>
	public static class IntervalUtils {
		public static string yearKey(DateTime time) {
			return time.Year.ToString();
		}
		
		public static string monthKey(DateTime time) {
			return String.Format("{0}_{1}", time.Year, time.Month);
		}
		
		public static string dayKey(DateTime time) {
			return String.Format("{0}_{1}_{2}", time.Year, time.Month, time.Day);
		}
	}
}

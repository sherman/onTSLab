/***************************************************************************
*   Copyright (C) 2011 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

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

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

namespace org.ontslab.util
{
	/// <summary>
	/// Description of DateUtils.
	/// </summary>
	public static class DateUtils {
		public static DateTime alignDateToSeconds(DateTime date, int seconds) {
			double alignPeriod = ((double)seconds) * 10000000; // win specific
			long alignedTime = (long)(Math.Floor(date.Ticks / alignPeriod) * alignPeriod);
			return new DateTime(alignedTime);
		}
	}
}

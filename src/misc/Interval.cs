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
using System.Xml.Schema;

namespace org.ontslab.misc
{
	/// <summary>
	/// Description of Period.
	/// </summary>
	/// 
	public interface Interval {
		string keyFromTime(DateTime time);
		string getName();
		TimeSpan getTimeSpan();
	};
	
	public sealed class Year : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.yearKey(time);
		}
		
		public string getName() { return "year"; }
		
		public TimeSpan getTimeSpan() {
			throw new NotImplementedException();
		}
	}
	
	public sealed class Month : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.monthKey(time);
		}
		
		public string getName() { return "month"; }
		
		public TimeSpan getTimeSpan() {
			throw new NotImplementedException();
		}
	}
	
	public sealed class Day : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.dayKey(time);
		}
		
		public string getName() { return "day"; }
		
		public TimeSpan getTimeSpan() {
			return new TimeSpan(1, 0, 0, 0);
		}
	}
	
	public sealed class Hour : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.hourKey(time);
		}
		
		public string getName() { return "hour"; }
		
		public TimeSpan getTimeSpan() {
			return new TimeSpan(0, 1, 0, 0);
		}
	}

	public sealed class FifteenMinutes : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.fifteenMinutesKey(time);
		}
		
		public string getName() { return "15_minutes"; }
		
		public TimeSpan getTimeSpan() {
			return new TimeSpan(0, 0, 15, 0);
		}
	}
}

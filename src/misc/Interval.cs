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
	};
	
	public sealed class Year : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.yearKey(time);
		}
		
		public string getName() { return "year"; }
	}
	
	public sealed class Month : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.monthKey(time);
		}
		
		public string getName() { return "month"; }
	}
	
	public sealed class Day : Interval {
		public string keyFromTime(DateTime time) {
			return IntervalUtils.dayKey(time);
		}
		
		public string getName() { return "day"; }
	}
}

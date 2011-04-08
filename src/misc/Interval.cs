/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 08.04.2011
 * Time: 21:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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

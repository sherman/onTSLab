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
using System.Collections.Generic;
using System.Linq;
using TSLab.Script;
using TSLab.DataSource;

namespace org.ontslab.data {
	/// <summary>
	/// Description of DailySource.
	/// </summary>
	public class DailySource {
		private IDictionary<string, Bar> dailySource;
		private DateTime first;
		private DateTime last;
		
		public DailySource(ISecurity original) {
			createDailySourceFrom(original);
		}
		
		public DailySource(IList<Bar> original) {
			createDailySourceFrom(original);
		}
		
		public Bar getBar(DateTime date) {
			return dailySource[date.ToShortDateString()];
		}
		
		public Bar getPreviousBar(DateTime date) {
			string previousBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Subtract(new TimeSpan(1, 0, 0, 0, 0));
				previousBarKey = startBarDate.ToShortDateString();
				
				if (dailySource.ContainsKey(previousBarKey)) {
					break;
				}
			} while (startBarDate >= first);
			
			if (dailySource.ContainsKey(previousBarKey))
				return dailySource[previousBarKey];
			else
				return null;
		}
		
		public Bar getNextBar(DateTime date) {
			string nextBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Add(new TimeSpan(1, 0, 0, 0, 0));
				nextBarKey = startBarDate.ToShortDateString();
				
				if (dailySource.ContainsKey(nextBarKey)) {
					break;
				}
			} while (startBarDate <= last);
			
			if (dailySource.ContainsKey(nextBarKey))
				return dailySource[nextBarKey];
			else
				return null;
		}
		
		private void createDailySourceFrom(IList<Bar> source) {
			dailySource = new Dictionary<string, Bar>(source.Count);
			
			source.ToList().ForEach(
				bar => dailySource.Add(bar.Date.ToShortDateString(), bar)
			);
			
			first = source.First().Date;
			last = source.Last().Date;
		}
		
		private void createDailySourceFrom(ISecurity original) {
			ISecurity daySource = original.CompressTo(
				new Interval(1, DataIntervals.DAYS)
			);
			
			IDictionary<string, Bar> newSourceBars =
				new Dictionary<string, Bar>(daySource.Bars.Count);
			
			// convert to source bars
			int day = original.Bars[0].Date.Day;
			
			int dayBarsIndex = 0;
			
			for (int i = 0; i < original.Bars.Count; i++) {
				if (day != original.Bars[i].Date.Day) {
					++dayBarsIndex;
					day = original.Bars[i].Date.Day;
				}
				
				string key = daySource.Bars[dayBarsIndex].Date.ToShortDateString();
				
				if (!newSourceBars.ContainsKey(key)) {
					newSourceBars.Add(key, daySource.Bars[dayBarsIndex]);
				}
			}
			
			this.dailySource = newSourceBars;
			this.first = original.Bars[0].Date;
			this.last = original.Bars[original.Bars.Count - 1].Date;
		}
	}
}

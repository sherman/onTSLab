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
using org.ontslab.misc;

namespace org.ontslab.data {
	/// <summary>
	/// Description of HourlySource.
	/// </summary>
	public class HourlySource : BaseCompressedSource<Hour> {
		private DateTime first;
		private DateTime last;
		
		public HourlySource(ISecurity original) {
			createHourlySourceFrom(original);
		}
		
		public HourlySource(IList<Bar> original) {
			createHourlySourceFrom(original);
		}
		
		public override Bar getPreviousBar(DateTime date) {
			string previousBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Subtract(period.getTimeSpan());
				previousBarKey = period.keyFromTime(startBarDate);
				
				if (compressedSource.ContainsKey(previousBarKey)) {
					break;
				}
			} while (startBarDate >= first);
			
			if (compressedSource.ContainsKey(previousBarKey))
				return compressedSource[previousBarKey];
			else
				return null;
		}
		
		public override Bar getNextBar(DateTime date) {
			string nextBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Add(period.getTimeSpan());
				nextBarKey = period.keyFromTime(startBarDate);
				
				if (compressedSource.ContainsKey(nextBarKey)) {
					break;
				}
			} while (startBarDate <= last);
			
			if (compressedSource.ContainsKey(nextBarKey))
				return compressedSource[nextBarKey];
			else
				return null;
		}
		
		private void createHourlySourceFrom(IList<Bar> source) {
			compressedSource = new Dictionary<string, Bar>(source.Count);
			
			source.ToList().ForEach(
				bar => compressedSource.Add(period.keyFromTime(bar.Date), bar)
			);
			
			first = source.First().Date;
			last = source.Last().Date;
		}
		
		private void createHourlySourceFrom(ISecurity original) {
			ISecurity hourSource = original.CompressTo(
				new TSLab.DataSource.Interval(60, DataIntervals.MINUTE)
			);
			
			IDictionary<string, Bar> newSourceBars =
				new Dictionary<string, Bar>(hourSource.Bars.Count);
			
			// convert to source bars
			int hour = original.Bars[0].Date.Hour;
			
			int hourBarsIndex = 0;
			
			for (int i = 0; i < original.Bars.Count; i++) {
				if (hour != original.Bars[i].Date.Hour) {
					++hourBarsIndex;
					hour = original.Bars[i].Date.Hour;
				}
				
				string key = period.keyFromTime(hourSource.Bars[hourBarsIndex].Date);
				
				if (!newSourceBars.ContainsKey(key)) {
					newSourceBars.Add(key, hourSource.Bars[hourBarsIndex]);
				}
			}
			
			this.compressedSource = newSourceBars;
			this.first = original.Bars[0].Date;
			this.last = original.Bars[original.Bars.Count - 1].Date;
		}
	}
}

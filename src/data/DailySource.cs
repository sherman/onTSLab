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
	/// Description of DailySource.
	/// </summary>
	public class DailySource : BaseCompressedSource<Day> {
		public DailySource(ISecurity original) {
			createDailySourceFrom(original);
		}
		
		public DailySource(IList<Bar> original) {
			createCompressedSourceFrom(original);
		}
		
		private void createDailySourceFrom(ISecurity original) {
			ISecurity daySource = original.CompressTo(
				new TSLab.DataSource.Interval(1, DataIntervals.DAYS)
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
				
				string key = period.keyFromTime(daySource.Bars[dayBarsIndex].Date);
				
				if (!newSourceBars.ContainsKey(key)) {
					newSourceBars.Add(key, daySource.Bars[dayBarsIndex]);
				}
			}
			
			this.compressedSource = newSourceBars;
			this.first = original.Bars[0].Date;
			this.last = original.Bars[original.Bars.Count - 1].Date;
		}
	}
}

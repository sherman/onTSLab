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
		public HourlySource(ISecurity original) {
			createHourlySourceFrom(original);
		}
		
		public HourlySource(IList<IDataBar> original) {
			createCompressedSourceFrom(original);
		}
		
		private void createHourlySourceFrom(ISecurity original) {
			ISecurity hourSource = original.CompressTo(
				new TSLab.DataSource.Interval(60, DataIntervals.MINUTE)
			);
			
			IDictionary<string, IDataBar> newSourceBars =
				new Dictionary<string, IDataBar>(hourSource.Bars.Count);
			
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
			this.first = original.Bars[0];
			this.last = original.Bars[original.Bars.Count - 1];
		}
	}
}

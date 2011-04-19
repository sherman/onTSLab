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
	/// Description of HourlySource.
	/// </summary>
	public class HourlySource {
		private IDictionary<string, Bar> hourlySource;
		private DateTime first;
		private DateTime last;
		
		public HourlySource(IList<Bar> original) {
			createHourlySourceFrom(original);
		}
		
		public Bar getBar(DateTime date) {
			return hourlySource[date.ToShortDateString() + "_" + date.Hour.ToString()];
		}
		
		public Bar getPreviousBar(DateTime date) {
			string previousBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Subtract(new TimeSpan(0, 1, 0, 0, 0));
				previousBarKey = startBarDate.ToShortDateString() + "_" + startBarDate.Hour.ToString();
				
				if (hourlySource.ContainsKey(previousBarKey)) {
					break;
				}
			} while (startBarDate >= first);
			
			if (hourlySource.ContainsKey(previousBarKey))
				return hourlySource[previousBarKey];
			else
				return null;
		}
		
		public Bar getNextBar(DateTime date) {
			string nextBarKey = null;
			
			DateTime startBarDate = date;
			
			do {
				startBarDate = startBarDate.Add(new TimeSpan(0, 1, 0, 0, 0));
				nextBarKey = startBarDate.ToShortDateString() + "_" + startBarDate.Hour.ToString();
				
				if (hourlySource.ContainsKey(nextBarKey)) {
					break;
				}
			} while (startBarDate <= last);
			
			if (hourlySource.ContainsKey(nextBarKey))
				return hourlySource[nextBarKey];
			else
				return null;
		}
		
		private void createHourlySourceFrom(IList<Bar> source) {
			hourlySource = new Dictionary<string, Bar>(source.Count);
			
			source.ToList().ForEach(
				bar => hourlySource.Add(bar.Date.ToShortDateString() + "_" + bar.Date.Hour.ToString(), bar)
			);
			
			first = source.First().Date;
			last = source.Last().Date;
		}
	}
}

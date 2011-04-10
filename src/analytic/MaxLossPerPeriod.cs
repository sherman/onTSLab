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
using org.ontslab.misc;

namespace org.ontslab.analytic {
	/// <summary>
	/// Description of MaxLossPerPeriod.
	/// </summary>
	public class MaxLossPerPeriod<T> where T : Interval, new() {
		private ISecurity source;
		
		private IDictionary<string, double> profitPerPeriod =
			new Dictionary<string, double>();
		
		private IDictionary<string, double> maxLossPerPeriod =
			new Dictionary<string, double>();
		
		private T period = new T();
		
		private bool fetched = false;
		
		private Bar lastExit;
		
		public MaxLossPerPeriod(ISecurity source) { this.source = source; }
		
		public Bar getLastExitBar() {
			if (!fetched) {
				fetch();
			}
			
			return lastExit;
		}
		
		public List<double> getMaxLossPerPeriodList() {
			if (!fetched) {
				fetch();
			}
			
			return new List<double>(maxLossPerPeriod.Values);
		}
		
		public override string ToString() {
			List<string> periods = new List<string>(maxLossPerPeriod.Keys);
			string result = "";
			periods.ForEach(
				periodKey => {
					result += String.Format(
						"Max loss was {0:0.00} in {1} {2}\r\n",
						profitPerPeriod[periodKey],
						period.getName(),
						periodKey
					);
				}
			);
			
			return result;
		}
		
		private void fetch() {
			source.Positions.Where(
				position => !position.IsActive
			).ToList().ForEach(
				position => {
					string periodKey = period.keyFromTime(position.ExitBar.Date);
					
					if (!profitPerPeriod.ContainsKey(periodKey)) {
						profitPerPeriod[periodKey] = 0.0;
					}
					
					if (!maxLossPerPeriod.ContainsKey(periodKey)) {
						maxLossPerPeriod[periodKey] = 0.0;
					}
					
					profitPerPeriod[periodKey] += position.Profit();
					
					if (position.Profit() < 0 && profitPerPeriod[periodKey] < 0) {
						if (
							maxLossPerPeriod[periodKey] == 0
							|| profitPerPeriod[periodKey] < maxLossPerPeriod[periodKey]
						) {
							maxLossPerPeriod[periodKey] = profitPerPeriod[periodKey];
						}
					}
					
					lastExit = position.ExitBar;
				}
			);
			
			fetched = true;
		}
	}
}

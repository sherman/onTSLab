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
using Interval = org.ontslab.misc.Interval;

namespace org.ontslab.analytic
{
	/// <summary>
	/// Description of ProfitPerPeriod.
	/// </summary>
	/// 
	public class ProfitPerPeriod<T> where T : Interval, new() {
		private ISecurity source;
		
		private IDictionary<string, double> profitPerPeriod =
			new Dictionary<string, double>();
		
		private T period = new T();
		
		private bool fetched = false;
		
		private IDataBar lastExit;
		
		public ProfitPerPeriod(ISecurity source) { this.source = source; }
		
		public IDataBar getLastExitBar() {
			if (!fetched) {
				fetch();
			}
			
			return lastExit;
		}
		
		public List<double> getProfitPerPeriodList() {
			if (!fetched) {
				fetch();
			}
			
			return new List<double>(profitPerPeriod.Values);
		}
		
		public double getProfitPerDate(DateTime time) {
			if (!fetched) {
				fetch();
			}
			
			string periodKey = period.keyFromTime(time);
			
			if (profitPerPeriod.ContainsKey(periodKey))
				return profitPerPeriod[periodKey];
			else
				return 0.0;
		}
		
		public override string ToString() {
			List<string> periods = new List<string>(profitPerPeriod.Keys);
			string result = "";
			
			periods.ForEach(
				periodKey => {
					result += String.Format(
						"Profit was {0:0.00} in {1} {2}\r\n",
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
					
					profitPerPeriod[periodKey] += position.Profit();
					lastExit = position.ExitBar;
				}
			);
			
			fetched = true;
		}
	}
}

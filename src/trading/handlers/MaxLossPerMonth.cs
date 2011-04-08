/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 18:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using TSLab.Script;

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of MaxLossPerMonth.
	/// </summary>
	public class MaxLossPerMonth : BarHandler {
		private ISecurity source;
		
		private IDictionary<string, double> profitPerMonth =
			new Dictionary<string, double>();
		
		private IDictionary<string, double> maxLossPerMonth =
			new Dictionary<string, double>();
		
		private Bar lastExit;
		
		public MaxLossPerMonth(ISecurity source) { this.source = source; }
		
		public void handleBar(Bar bar) {
			IPosition previousPosition = source.Positions.LastPositionClosed;
			
			if (previousPosition != null && (lastExit == null || !previousPosition.ExitBar.Equals(lastExit))) {
				string monthKey = ProfitPerMonthUtils.key(previousPosition.ExitBar);
				
				if (!profitPerMonth.ContainsKey(monthKey)) {
					profitPerMonth[monthKey] = 0.0;
				}
				
				if (!maxLossPerMonth.ContainsKey(monthKey)) {
					maxLossPerMonth[monthKey] = 0.0;
				}
				
				if (previousPosition.Profit() < 0 && profitPerMonth[monthKey] < 0) {
					if (
						maxLossPerMonth[monthKey] == 0
						|| profitPerMonth[monthKey] < maxLossPerMonth[monthKey]
					) {
						maxLossPerMonth[monthKey] = profitPerMonth[monthKey];
					}
				}
				
				lastExit = previousPosition.ExitBar;
			}
		}
		
		public Bar getLastExitBar() {
			return lastExit;
		}
		
		public List<double> getMaxLossPerMonthList() {
			return new List<double>(maxLossPerMonth.Values);
		}
	}
}

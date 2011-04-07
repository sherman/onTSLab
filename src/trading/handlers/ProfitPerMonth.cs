/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 14:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using TSLab.Script;

namespace org.ontslab.trading.handlers
{
	/// <summary>
	/// Description of ProfitPerMonth.
	/// </summary>
	public class ProfitPerMonth : BarHandler {
		private ISecurity source;
		
		private IDictionary<string, double> profitPerMonth =
			new Dictionary<string, double>();
		
		private Bar lastExit;
		
		public ProfitPerMonth(ISecurity source) { this.source = source; }
		
		public void handleBar(Bar bar) {
			IPosition previousPosition = source.Positions.LastPositionClosed;
			
			if (previousPosition != null && (lastExit == null || !previousPosition.ExitBar.Equals(lastExit))) {
				string monthKey = ProfitPerMonthUtils.key(previousPosition.ExitBar);
				
				if (!profitPerMonth.ContainsKey(monthKey)) {
					profitPerMonth[monthKey] = 0.0;
				}
				
				profitPerMonth[monthKey] += previousPosition.Profit();
				lastExit = previousPosition.ExitBar;
			}
		}
		
		public Bar getLastExitBar() {
			return lastExit;
		}
		
		public List<double> getProfitPerMonthList() {
			List<double> profitList = new List<double>();
			IEnumerator<double> iter = profitPerMonth.Values.GetEnumerator();
			
			while (iter.MoveNext()) {
				profitList.Add(iter.Current);
			};
			
			return profitList;
		}
	}
}

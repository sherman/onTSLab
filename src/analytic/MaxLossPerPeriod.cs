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

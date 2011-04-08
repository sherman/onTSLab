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
				delegate(string periodKey){
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
			IEnumerator<IPosition> positionEnum = source.Positions.GetEnumerator();
			
			while (positionEnum.MoveNext()) {
				if (!positionEnum.Current.IsActive) {
					string key = period.keyFromTime(positionEnum.Current.ExitBar.Date);
					
					if (!profitPerPeriod.ContainsKey(key)) {
						profitPerPeriod[key] = 0.0;
					}
					
					if (!maxLossPerPeriod.ContainsKey(key)) {
						maxLossPerPeriod[key] = 0.0;
					}
				
					profitPerPeriod[key] += positionEnum.Current.Profit();
					
					if (positionEnum.Current.Profit() < 0 && profitPerPeriod[key] < 0) {
						if (
							maxLossPerPeriod[key] == 0
							|| profitPerPeriod[key] < maxLossPerPeriod[key]
						) {
							maxLossPerPeriod[key] = profitPerPeriod[key];
						}
					}
					
					lastExit = positionEnum.Current.ExitBar;
				}
			}
			
			fetched = true;
		}
	}
}

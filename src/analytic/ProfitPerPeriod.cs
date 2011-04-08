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
using org.ontslab.misc;

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
		
		private Bar lastExit;
		
		public ProfitPerPeriod(ISecurity source) { this.source = source; }
		
		public void handleBar(Bar bar) {
			// nope
		}
		
		public Bar getLastExitBar() {
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
		
		public override string ToString() {
			List<string> periods = new List<string>(profitPerPeriod.Keys);
			string result = "";
			periods.ForEach(
				delegate(string periodKey){
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
			IEnumerator<IPosition> positionEnum = source.Positions.GetEnumerator();
			
			while (positionEnum.MoveNext()) {
				if (!positionEnum.Current.IsActive) {
					string monthKey = period.keyFromTime(positionEnum.Current.ExitBar.Date);
					
					if (!profitPerPeriod.ContainsKey(monthKey)) {
						profitPerPeriod[monthKey] = 0.0;
					}
				
					profitPerPeriod[monthKey] += positionEnum.Current.Profit();
					
					lastExit = positionEnum.Current.ExitBar;
				}
			}
			
			fetched = true;
		}
	}
}

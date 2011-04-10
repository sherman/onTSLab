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
using System.Linq;
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

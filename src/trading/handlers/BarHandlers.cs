/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 14:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

namespace org.ontslab.trading.handlers
{
	/// <summary>
	/// Description of BarHandlers.
	/// </summary>
	public static class BarHandlers {
		public static BarHandler profitPerMonth(ISecurity source) {
			return new ProfitPerMonth(source);
		}
	}
	
	static class ProfitPerMonthUtils {
		public static string key(Bar bar) {
			return bar.Date.Year + "-" + bar.Date.Month;
		}
	}
}

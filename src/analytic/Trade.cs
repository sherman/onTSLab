/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 09.04.2011
 * Time: 21:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

namespace org.ontslab.analytic
{
	/// <summary>
	/// Trivia representation of a closed(inactive) trade.
	/// </summary>
	public sealed class Trade {
		private Bar entry;
		private Bar exit;
		private double profit;
		
		public Trade(Bar entry, Bar exit, double profit) {
			this.entry = entry;
			this.exit = exit;
			this.profit = profit;
		}
		
		public Bar getEntry() {
			return entry;
		}
		
		public Bar getExit() {
			return exit;
		}
		
		public double getProfit() {
			return profit;
		}
	}
}

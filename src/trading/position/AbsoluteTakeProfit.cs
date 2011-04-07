/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 17:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;
using org.ontslab.signal;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of AbsoluteTakeProfit.
	/// </summary>
	public class AbsoluteTakeProfit : TakeProfit {
		private double maxProfit;
		
		public AbsoluteTakeProfit(double maxProfit) {
			this.maxProfit = maxProfit;
		}
		
		public void createOn(IPosition position, int barIndex) {
			double profitPrice = 0.0;
			
			if (position.IsShort) {
				profitPrice = position.EntryPrice - maxProfit;
			} else {
				profitPrice = position.EntryPrice + maxProfit;
			}
			
			position.CloseAtProfit(
				barIndex,
				profitPrice,
				Signals.TAKE_PROFIT_CLOSE
			);
		}
	}
}

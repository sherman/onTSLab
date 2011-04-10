/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 10.04.2011
 * Time: 19:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of PercentOfBalanceBasedPositionCalculator.
	/// </summary>
	public class PercentOfBalanceBasedPositionCalculator : PositionCalculator {
		private double percent;
		private double currentBalance;
		private double maxLoss;
		
		public PercentOfBalanceBasedPositionCalculator(
			double percent,
			double currentBalance,
			double maxLoss
		) {
			this.percent = percent;
			this.currentBalance = currentBalance;
			this.maxLoss = maxLoss;
		}
		
		public double getPositionSize() {
			return Math.Floor(currentBalance * percent / maxLoss);
		}
	}
}

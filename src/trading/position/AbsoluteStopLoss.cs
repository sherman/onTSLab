/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 17:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;
using org.ontslab.signal;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of AbsoluteStopLoss.
	/// </summary>
	public class AbsoluteStopLoss : StopLoss {
		private double maxLoss;
		
		public AbsoluteStopLoss(double maxLoss) {
			this.maxLoss = maxLoss;
		}
		
		public void createOn(IPosition position, int barIndex) {
			double stopLossPrice = maxLoss;
						
			if (position.IsShort) {
				stopLossPrice = position.EntryPrice + maxLoss;
			} else {
				stopLossPrice = position.EntryPrice - maxLoss;
			}
			
			position.CloseAtStop(
				barIndex,
				stopLossPrice,
				Signals.STOP_LOSS_CLOSE
			);
		}
	}
}

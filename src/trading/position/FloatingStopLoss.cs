/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 17:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using TSLab.Script;
using org.ontslab.signal;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of FloatingStopLoss.
	/// </summary>
	public class FloatingStopLoss : StopLoss {
		private IList<double> based;
		private double koeff;
		
		public FloatingStopLoss(IList<double> based, double koeff) {
			this.based = based;
			this.koeff = koeff;
		}
		
		public void createOn(IPosition position, int barIndex) {
			double stopLossPrice = based[barIndex - 1] * koeff;
						
			if (position.IsShort) {
				stopLossPrice = position.EntryPrice + stopLossPrice;
			} else {
				stopLossPrice = position.EntryPrice - stopLossPrice;
			}
			
			position.CloseAtStop(
				barIndex,
				stopLossPrice,
				Signals.STOP_LOSS_CLOSE
			);
		}
	}
}

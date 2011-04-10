/***************************************************************************
*   Copyright (C) 2011 by Denis M. Gabaydulin                             *
*                                                                         *
*   This program is free software; you can redistribute it and/or modify  *
*   it under the terms of the GNU Lesser General Public License as        *
*   published by the Free Software Foundation; either version 3 of the    *
*   License, or (at your option) any later version.                       *
*                                                                         *
***************************************************************************/

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
			double stopLossPrice = 0.0;
						
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

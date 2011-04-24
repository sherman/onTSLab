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
	/// Description of MovingStopLoss.
	/// </summary>
	public class MovingStopLoss : StopLoss {
		private double stopLossPrice;
		
		public MovingStopLoss(double stopLossPrice) {
			this.stopLossPrice = stopLossPrice;
		}
		
		public void createOn(IPosition position, int barIndex) {
			position.CloseAtStop(
				barIndex,
				stopLossPrice,
				Signals.STOP_LOSS_CLOSE
			);
		}
	}
}

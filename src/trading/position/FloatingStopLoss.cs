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

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
	/// Description of FloatingTakeProfit.
	/// </summary>
	public class FloatingTakeProfit : TakeProfit {
		private IList<double> based;
		private double koeff;
		
		public FloatingTakeProfit(IList<double> based, double koeff) {
			this.based = based;
			this.koeff = koeff;
		}
		
		public void createOn(IPosition position, int barIndex) {
			double profitPrice = based[barIndex - 1] * koeff;
			
			if (position.IsShort) {
				profitPrice = position.EntryPrice - profitPrice;
			} else {
				profitPrice = position.EntryPrice + profitPrice;
			}
			
			position.CloseAtProfit(
				barIndex,
				profitPrice,
				Signals.TAKE_PROFIT_CLOSE
			);
		}
	}
}

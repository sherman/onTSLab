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

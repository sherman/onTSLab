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

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of Positions.
	/// </summary>
	public static class Positions {
		public static FixedSizePositionCalculator fixedSize(double size) {
			return new FixedSizePositionCalculator(size);
		}
		
		public static PercentOfBalanceBasedPositionCalculator percentOfBalance(
			double percent,
			double currentBalance,
			double maxLoss
		) {
			return new PercentOfBalanceBasedPositionCalculator(percent, currentBalance, maxLoss);
		}
		
		public static FloatingStopLoss floatingStopLoss(IList<double> based, double koeff) {
			return new FloatingStopLoss(based, koeff);
		}
		
		public static AbsoluteTakeProfit absoluteTakeProfit(double size) {
			return new AbsoluteTakeProfit(size);
		}
	}
}

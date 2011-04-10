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

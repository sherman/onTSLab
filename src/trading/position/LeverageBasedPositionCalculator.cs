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

namespace org.ontslab.trading.position
{
	/// <summary>
	/// Description of LeverageBasedPositionCalculator.
	/// </summary>
	public class LeverageBasedPositionCalculator : PositionCalculator {
		private readonly double maxLeverage;
		private readonly double currentBalance;
		private readonly double price;
		
		public LeverageBasedPositionCalculator(
			double maxLeverage,
			double currentBalance,
			double price
		) {
			this.maxLeverage = maxLeverage;
			this.currentBalance = currentBalance;
			this.price = price;
		}
		
		public int getPositionSize() {
			var res = Math.Floor(currentBalance * maxLeverage / price);
			if (double.NaN.Equals(res) || double.IsInfinity(res)) {
				return 1;
			}

			return Convert.ToInt32(res);
		}
	}
}

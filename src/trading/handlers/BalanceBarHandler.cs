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

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of BalanceBarHandler.
	/// </summary>
	/// 
	
	public delegate double ProfitConvertion(IPosition position);
	
	public class BalanceBarHandler : BarHandler {
		private double balance = 0.0;
		private Bar lastBar;
		private ISecurity source;
		private ProfitConvertion method;
		
		public BalanceBarHandler(ISecurity source) {
			this.source = source;
		}
		
		public void handleBar(Bar bar) {
			IPosition previousPosition = source.Positions.LastPositionClosed;
			
			if (previousPosition != null && (lastBar == null || !previousPosition.ExitBar.Equals(lastBar))) {
				if (null != method) {
					balance += method(previousPosition);
				} else {
					balance += previousPosition.Profit();
				}
				lastBar = previousPosition.ExitBar;
			}
		}
		
		public double getBalance() {
			return balance;
		}
		
		public BalanceBarHandler setProfitConvertionMethod(ProfitConvertion method) {
			this.method = method;
			return this;
		}
	}
}

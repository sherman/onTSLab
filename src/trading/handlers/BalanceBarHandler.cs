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
using TSLab.DataSource;
using TSLab.Script;
using TSLab.Utils;

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of BalanceBarHandler.
	/// </summary>
	/// 
	
	public delegate double ProfitConvertion(IPosition position);
	
	public class BalanceBarHandler : BarHandler {
		private double balance = 0.0;
		private ISecurity source;
		private ProfitConvertion method;
		private ISet<IPosition> _positions = new HashSet<IPosition>();
		
		public BalanceBarHandler(ISecurity source) {
			this.source = source;
		}
		
		public void handleBar(IDataBar bar, int index) {
			IPosition previousPosition = source.Positions.GetLastPositionClosed(index);
			
			if (previousPosition != null) {
				if (_positions.Contains(previousPosition)) {
					return;
				}
				
				if (null != method) {
					balance += method(previousPosition);
				} else {
					balance += previousPosition.Profit();
				}

				_positions.Add(previousPosition);
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

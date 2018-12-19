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
using org.ontslab.trading.handlers;
using org.ontslab.misc;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace org.ontslab.testing {
	/// <summary>
	/// Description of PnLReport.
	/// </summary>
	sealed public class PnLReport {
		private List<double> profitSource;
		private List<double> maxLossSource;
		
		// FIXME: find a way to fix the weakness of c# 3.5(the generic types aren't covariant)
		/*
		private ProfitPerPeriod<Interval> profitSource;
		private MaxLossPerMonth maxLossSource;
		*/
		
		public PnLReport() {}
		
		public PnLReport setProfitSource(List<double> profitSource) {
			this.profitSource = profitSource;
			return this;
		}
		
		public PnLReport setMaxLossSource(List<double> maxLossSource) {
			this.maxLossSource = maxLossSource;
			return this;
		}
		
		public PnLReport draw(IContext context) {
			var profitPane = context.CreateGraphPane("Profit", "Profit");
			profitPane.SizePct = 20.0;
			profitPane.HideLegend = true;
			
			if (null != profitSource) {
				profitPane.AddList(
					"ProfitPerMonth",
					profitSource,
					ListStyles.HISTOHRAM,
					0x336699,
					LineStyles.SOLID,
					PaneSides.LEFT
				);
			}
			
			if (null != maxLossSource) {
				profitPane.AddList(
					"MaxLossPerMonth",
					maxLossSource,
					ListStyles.HISTOHRAM,
					0xff00000,
					LineStyles.SOLID,
					PaneSides.LEFT
				);
			}
			
			if (null != profitSource) {
				IList<double> profitSma = context.GetData(
					"ProfitSma",
					new[] {"3"}, delegate {
						return Series.SMA(profitSource, 3);
					}
				);
				
				profitPane.AddList(
					"ProfitPerMonthSma",
					profitSma,
					ListStyles.LINE,
					0xff0000,
					LineStyles.SOLID,
					PaneSides.LEFT
				);
			}
			
			return this;
		}
	}
}

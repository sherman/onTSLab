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
using org.ontslab.analytic;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace org.ontslab.testing
{
	/// <summary>
	/// Description of TradeReport.
	/// </summary>
	public class TradeReport {
		private TradeSource source;
		
		public TradeReport(TradeSource source) {
			this.source = source;
		}
		
		public void draw(IContext context) {
			ISecurity baseSource = source.getBaseSource();
			IDictionary<int, Trade> trades = source.getBarIndexedTrades();
			
			IList<double> losses = new List<double>(baseSource.Bars.Count);
			IList<double> wins = new List<double>(baseSource.Bars.Count);
			
			for (int i = 0; i < baseSource.Bars.Count; i++) {
				if (trades.ContainsKey(i)) {
					if (trades[i].getProfit() > 0) {
						wins.Add(1);
						losses.Add(0);
					} else {
						wins.Add(0);
						losses.Add(1);
					}
				} else {
					wins.Add(0);
					losses.Add(0);
				}
			}
			
			IPane tradesPanel = context.CreatePane("Trades", 10.0, true);
			
			tradesPanel.AddList(
				"Win trades",
				wins,
				ListStyles.HISTOHRAM,
				0x00ff00,
				LineStyles.SOLID,
				PaneSides.VSIDE_LAST
			);
			
			tradesPanel.AddList(
				"Loss trades",
				losses,
				ListStyles.HISTOHRAM,
				0xff0000,
				LineStyles.SOLID,
				PaneSides.VSIDE_LAST
			);
		}
	}
}

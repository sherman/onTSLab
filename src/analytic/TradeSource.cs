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

namespace org.ontslab.analytic
{
	/// <summary>
	/// Description of TradeSource.
	/// </summary>
	public class TradeSource {
		private ISecurity source;
		private int fromBar;
		private int toBar;
		private List<Trade> trades = new List<Trade>();
		private bool fetched = false;
		
		public TradeSource(ISecurity source) : this(source, 0, source.Bars.Count) {
		}
		
		public TradeSource(ISecurity source, int fromBar, int toBar) {
			this.source = source;
			this.fromBar = fromBar;
			this.toBar = toBar;
		}
		
		public ISecurity getBaseSource() {
			return source;
		}
		
		public List<Trade> getTrades() {
			if (!fetched) {
				fetch();
			}
			
			return trades;
		}
		
		public IDictionary<int, Trade> getBarIndexedTrades() {
			if (!fetched) {
				fetch();
			}
			
			IDictionary<int, Trade> barIndexedTrades = new Dictionary<int, Trade>();
			
			trades.ForEach(trade => barIndexedTrades.Add(
					source.Bars.IndexOf(trade.getEntry()),
					trade
				)
			);
			
			return barIndexedTrades;
		}
		
		private void fetch() {
			IEnumerator<IPosition> positionEnum = source.Positions.GetEnumerator();
			
			while (positionEnum.MoveNext()) {
				if (
					positionEnum.Current.EntryBarNum > fromBar
					&& positionEnum.Current.EntryBarNum <= toBar
					&& !positionEnum.Current.IsActive
				) {
					Trade trade = new Trade(
						positionEnum.Current.EntryBar,
						positionEnum.Current.ExitBar,
						positionEnum.Current.IsLong
							? (positionEnum.Current.ExitPrice - positionEnum.Current.EntryPrice)
							: (positionEnum.Current.EntryPrice - positionEnum.Current.ExitPrice)
					);
					
					trades.Add(trade);
				}
			}
			
			fetched = true;	
		}
	}
}

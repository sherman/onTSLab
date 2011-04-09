/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 09.04.2011
 * Time: 21:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
		private List<Trade> trades = new List<Trade>();
		private bool fetched = false;
		
		public TradeSource(ISecurity source) { this.source = source; }
		
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
				if (!positionEnum.Current.IsActive) {
					Trade trade = new Trade(
						positionEnum.Current.EntryBar,
						positionEnum.Current.ExitBar,
						positionEnum.Current.Profit()
					);
					
					trades.Add(trade);
				}
			}
			
			fetched = true;	
		}
	}
}

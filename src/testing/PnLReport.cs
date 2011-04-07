/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 18:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using org.ontslab.trading.handlers;
using TSLab.Script;
using TSLab.Script.Handlers;
using TSLab.Script.Helpers;

namespace org.ontslab.testing {
	/// <summary>
	/// Description of PnLReport.
	/// </summary>
	sealed public class PnLReport {
		private ProfitPerMonth profitSource;
		private MaxLossPerMonth maxLossSource;
		
		public PnLReport(ISecurity source) {}
		
		public PnLReport setProfitSource(ProfitPerMonth profitSource) {
			this.profitSource = profitSource;
			return this;
		}
		
		public PnLReport setMaxLossSource(MaxLossPerMonth maxLossSource) {
			this.maxLossSource = maxLossSource;
			return this;
		}
		
		public void draw(IContext context) {
			IPane profitPane = context.CreatePane("Profit", 20.0, true);
			
			if (null != profitSource) {
				profitPane.AddList(
					"ProfitPerMonth",
					profitSource.getProfitPerMonthList(),
					ListStyles.HISTOHRAM,
					0x336699,
					LineStyles.SOLID,
					PaneSides.VSIDE_LAST
				);
			}
			
			if (null != maxLossSource) {
				profitPane.AddList(
					"MaxLossPerMonth",
					maxLossSource.getMaxLossPerMonthList(),
					ListStyles.HISTOHRAM,
					0xff00000,
					LineStyles.SOLID,
					PaneSides.VSIDE_LAST
				);
			}
			
			if (null != profitSource) {
				IList<double> profitSma = context.GetData(
					"ProfitSma",
					new[] {"3"}, delegate {
						return Series.SMA(profitSource.getProfitPerMonthList(), 3);
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
		}
	}
}

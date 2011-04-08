/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 08.04.2011
 * Time: 17:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of Handlers.
	/// </summary>
	public static class Handlers {
		public static ProfitPerMonth profitPerMonth(ISecurity source) {
			return new ProfitPerMonth(source);
		}
		
		public static MaxLossPerMonth maxLossPerMonth(ISecurity source) {
			return new MaxLossPerMonth(source);
		}
		
		public static CompositeBarHandler composite() {
			return new CompositeBarHandler();
		}
	}
}

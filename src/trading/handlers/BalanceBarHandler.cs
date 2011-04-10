/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 10.04.2011
 * Time: 19:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of BalanceBarHandler.
	/// </summary>
	/// 
	
	public delegate double BalanceConvertion(IPosition position);
	
	public class BalanceBarHandler : BarHandler {
		private double balance = 0.0;
		private Bar lastBar;
		private ISecurity source;
		private BalanceConvertion method;
		
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
		
		public BalanceBarHandler setConvertionMethod(BalanceConvertion method) {
			this.method = method;
			return this;
		}
	}
}

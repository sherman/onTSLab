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
using org.ontslab.misc;

namespace org.ontslab.trading.handlers {
	/// <summary>
	/// Description of Handlers.
	/// </summary>
	public static class Handlers {
		public static CompositeBarHandler composite() {
			return new CompositeBarHandler();
		}
		
		public static BalanceBarHandler balance(ISecurity source) {
			return new BalanceBarHandler(source);
		}
	}
}

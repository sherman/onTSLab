/*
 * Created by SharpDevelop.
 * User: Sherminator
 * Date: 07.04.2011
 * Time: 17:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TSLab.Script;

namespace org.ontslab.trading.position {
	/// <summary>
	/// Description of TakeProfit.
	/// </summary>
	public interface TakeProfit {
		void createOn(IPosition position, int barIndex);
	}
}
